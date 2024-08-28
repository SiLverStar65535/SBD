using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Infrastructure;
using SBD.Provider;

namespace SBD.ViewModels
{
    public enum eScanState
    {
        ReadyScan,
        Scaning,
        FinishedScan
    }

    public class Step3PageViewModel : BindableBase, IRegionMemberLifetime
    {
      


        private readonly IDataProvider _dataProvider;
        private readonly IScaneService _scaneService;
        public bool KeepAlive { get; } = false;

        public Step3PageViewModel()
        {
            
        }
        public Step3PageViewModel(IDataProvider dataProvider, IScaneService scaneService)
        {
            _dataProvider = dataProvider;
            _scaneService = scaneService;
            ScanState =  eScanState.ReadyScan;
        }


        public eScanState ScanState { get; set; }

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private async void ExcuteScanCommand()
        {
            ScanState =  eScanState.Scaning;
            RaisePropertyChanged(nameof(ScanState));

            var airlineLuggageSize = _dataProvider.GetAirlineLuggageSize("");
            var airlineLuggageWeight = _dataProvider.GetAirlineLuggageWeight("");

            var customLuggageSize = _scaneService.GetLuggageSize();
            var customLuggageWeight = _scaneService.GetLuggageWieght();

            await Task.Delay(3000);

            ScanState = eScanState.FinishedScan;
            RaisePropertyChanged(nameof(ScanState));
        }

        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            
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
