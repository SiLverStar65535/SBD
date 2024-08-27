using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step3PageViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;

        public Step3PageViewModel()
        {
            IsGettedData = false;
        }


        public bool IsGettedData { get; set; }

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private void ExcuteScanCommand()
        {
            IsGettedData = true;
            RaisePropertyChanged(nameof(IsGettedData));
        }

        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            IsGettedData = false;
            RaisePropertyChanged(nameof(IsGettedData));
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteConfirmCommand);
        private void ExcuteConfirmCommand()
        {

            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step4PageView,
                NavigationParameters = null
            };

            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }

      
    }
}
