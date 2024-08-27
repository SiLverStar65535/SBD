using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step4PageViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;
        public Step4PageViewModel()
        {
             
        }

 

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteCancelCommand);
        private void ExcuteCancelCommand()
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step5PageView,
                NavigationParameters = null
            };

            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }

    }
}
