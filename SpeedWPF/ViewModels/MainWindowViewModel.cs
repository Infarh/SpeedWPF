using SpeedWPF.ViewModels.Base;

namespace SpeedWPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Title : string - Заголовок кона

        /// <summary>Заголовок кона</summary>
        private string _Title;

        /// <summary>Заголовок кона</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion
    }
}
