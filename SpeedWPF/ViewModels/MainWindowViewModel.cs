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

        public MainWindowViewModel(IDataService DataService) => _DataService = DataService;
    }
}
