using System;
using SpeedWPF.Services.Interfaces;

namespace SpeedWPF.Services
{
    public class DataService : IDataService
    {
        private const int __DataCount = 512;
        private const int __DataLength = 128;
        private readonly byte[][] _Data;

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
    }
}
