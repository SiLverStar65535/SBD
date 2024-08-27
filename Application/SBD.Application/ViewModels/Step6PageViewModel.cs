using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step6PageViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;
        public Step6PageViewModel()
        {
        }

    
        private DelegateCommand _nextLuggageCommand;
        public DelegateCommand NextLuggageCommand => _nextLuggageCommand ??= new DelegateCommand(ExcuteNextLuggageCommand);
        private void ExcuteNextLuggageCommand()
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step3PageView,
                NavigationParameters = null
            };

            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }

        private DelegateCommand _finishedCommand;
        public DelegateCommand FinishedCommand => _finishedCommand ??= new DelegateCommand(ExcuteFinishedCommand);
        private void ExcuteFinishedCommand()
        {
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step1PageView,
                NavigationParameters = null
            };

            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }
    }
}
