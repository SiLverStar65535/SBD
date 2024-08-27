using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step5PageViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;
        public Step5PageViewModel()
        {
            
        }

   
        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step3PageView,
                NavigationParameters = null
            };
            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteCancelCommand);
        private void ExcuteCancelCommand()
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step6PageView,
                NavigationParameters = null
            };

            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }
    }
}
