using System;
using System.Windows.Input;
using SpeedWPF.Commands;
using SpeedWPF.Services.Interfaces;
using SpeedWPF.ViewModels.Base;

namespace SpeedWPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IDataService _DataService;

        #region Title : string - Заголовок кона

        /// <summary>Заголовок кона</summary>
        private string _Title = "Тестирование производительности";

        /// <summary>Заголовок кона</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Timeout : int - Период обновления данных в мс

        /// <summary>Период обновления данных в мс</summary>
        private int _Timeout = 10;

        /// <summary>Период обновления данных в мс</summary>
        public int Timeout
        {
            get => _Timeout;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value,
                        "Значение должно быть больше 0");
                if (Set(ref _Timeout, value) && _DataService.Enabled)
                    _DataService.StartDataUpdate(TimeSpan.FromMilliseconds(value));
            }
        }

        #endregion

        #region TimeDelta : TimeSpan - Реальный период обновления

        /// <summary>Реальный период обновления</summary>
        private TimeSpan _TimeDelta;

        /// <summary>Реальный период обновления</summary>
        public TimeSpan TimeDelta
        {
            get => _TimeDelta;
            private set
            {
                if(Set(ref _TimeDelta, value))
                    OnPropertyChanged(nameof(TimeoutError));
            }
        }

        #endregion

        public TimeSpan TimeoutError => _TimeDelta - TimeSpan.FromMilliseconds(_Timeout);

        public ValueModel[] Values { get; }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public MainWindowViewModel(IDataService DataService)
        {
            StartCommand = new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);
            StopCommand = new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

            _DataService = DataService;
            var data = _DataService.Data;
            var values_count = data.Length / 8;
            var values = new ValueModel[values_count];
            for (var i = 0; i < values_count; i++)
                values[i] = new ValueModel(data, i);
            Values = values;
            _DataService.DataChanged += OnDataChanged;
        }

        private bool CanStartCommandExecute() => !_DataService.Enabled;

        private void OnStartCommandExecuted() => _DataService.StartDataUpdate(TimeSpan.FromMilliseconds(Timeout));

        private bool CanStopCommandExecute() => _DataService.Enabled;

        private void OnStopCommandExecuted() => _DataService.StopDataUpdate();

        private void OnDataChanged(object Sender, EventArgs E)
        {
            var values = Values;
            var values_count = values.Length;
            var new_data = _DataService.Data;
            for (var i = 0; i < values_count; i++)
                values[i].Update(new_data);
            TimeDelta = TimeSpan.FromMilliseconds(_DataService.TimeDelta);
        }
    }
}
