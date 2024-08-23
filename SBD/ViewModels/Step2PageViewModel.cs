using DataLibrary;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step2PageViewModel : BindableBase, INavigationAware
    {
        #region Constructors
        public Step2PageViewModel()
        {
            if (App.IsDesignTime)
            {
                BoardingPass = DesignTimeData.BoardingPass;
            }
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            BoardingPass = (BoardingPass)navigationContext.Parameters[nameof(BoardingPass)];

            RaisePropertyChanged(nameof(BoardingPass));
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        #region Properties
        public BoardingPass BoardingPass { get; set; }
        #endregion

        #region DelegateCommands
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(ExcuteCancelCommand);
        private void ExcuteCancelCommand()
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step1PageView,
                NavigationParameters = null
            };
            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }  
        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteConfirmCommand);
        private void ExcuteConfirmCommand() 
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step3PageView,
                NavigationParameters = null
            };
            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }
        #endregion
    }
}
