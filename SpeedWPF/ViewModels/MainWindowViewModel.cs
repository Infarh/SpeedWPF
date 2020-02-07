using System;
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

        public ValueModel[] Values { get; }

        public MainWindowViewModel(IDataService DataService)
        {
            _DataService = DataService;
            var data = _DataService.Data;
            var values_count = data.Length / 8;
            var values = new ValueModel[values_count];
            for(var i = 0; i < values_count; i++)
                values[i] = new ValueModel(data, i);
            Values = values;
            _DataService.DataChanged += OnDataChanged;
        }

        private void OnDataChanged(object Sender, EventArgs E)
        {
            var values = Values;
            var values_count = values.Length;
            var new_data = _DataService.Data;
            for(var i = 0; i < values_count; i++) 
                values[i].Update(new_data);
        }
    }
}
