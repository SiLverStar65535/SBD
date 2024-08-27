using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Infrastructure;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step3PageViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IDataProvider _dataProvider;
        private readonly IScaneService _scaneService;
        public bool KeepAlive { get; } = false;

        public Step3PageViewModel(IDataProvider dataProvider, IScaneService scaneService)
        {
            _dataProvider = dataProvider;
            _scaneService = scaneService;
            IsGettedData = false;
        }


        public bool IsGettedData { get; set; }

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private void ExcuteScanCommand()
        {

            var rule = _dataProvider.GetFlightDetail("");
            var size = _scaneService.GetLuggageSize();
            var wieght = _scaneService.GetLuggageWieght();

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
