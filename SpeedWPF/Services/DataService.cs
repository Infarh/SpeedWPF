using System;
using System.Threading;
using System.Threading.Tasks;
using SpeedWPF.Services.Interfaces;

namespace SpeedWPF.Services
{
    public class DataService : IDataService
    {
        public class DataUpdatedEventArgs : EventArgs
        {
            public byte[] Data { get; }

            public DataUpdatedEventArgs(byte[] Data) => this.Data = Data;
        }

        private const int __DataCount = 512;
        private const int __DataLength = 128;
        private readonly byte[][] _Data;

        private int _CurrentDataIndex = 0;

        public event EventHandler<DataUpdatedEventArgs> DataUpdated;

        public DataService()
        {
            _Data = new byte[__DataCount][];
            InitializeData();
        }

        private void InitializeData()
        {
            for (var i = 0; i < _Data.Length; i++)
            {
                Span<byte> data = _Data[i] = new byte[__DataLength * 8];

                for (var j = 0; j < __DataLength; j++)
                {
                    var data_location = data.Slice(j * 8, 8);

                    var value = Math.Sin(2 * Math.PI * ((double)j / __DataLength + (double)i / __DataCount));
                    BitConverter.TryWriteBytes(data_location, value);
                }
            }
        }

        private CancellationTokenSource _DataUpdateCancellation;
        public void StartDataUpdate(TimeSpan Timeout)
        {
            var cancellation = new CancellationTokenSource();
            Interlocked.Exchange(ref _DataUpdateCancellation, cancellation)?.Cancel();
            UpdateDataAsync(Timeout, cancellation.Token);
        }

        public void StopDataUpdate() => _DataUpdateCancellation?.Cancel();

        // ReSharper disable once FunctionNeverReturns
        private async void UpdateDataAsync(TimeSpan Timeout, CancellationToken Cancel)
        {
            while (true)
            {
                await Task.Delay(Timeout, Cancel).ConfigureAwait(false);
                Cancel.ThrowIfCancellationRequested();
                _CurrentDataIndex = (_CurrentDataIndex + 1) % __DataLength;
                var data = _Data[_CurrentDataIndex];
                DataUpdated?.Invoke(this, new DataUpdatedEventArgs(data));
            }
        }
    }
}
