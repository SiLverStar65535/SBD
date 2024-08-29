using System.Collections.Generic;
using System.Threading.Tasks;
 
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Infrastructure.Interface;
using SBD.Provider;

namespace SBD.ViewModels
{
    public enum eScanState
    {
        ReadyScan,
        Scanning,
        FinishedScan
    }
    public class Step3PageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;
        private readonly ISBDService _sbdService;

        #region Constructors
        public Step3PageViewModel()
        {
            if (App.IsDesignTime)
            {
                Flight = DesignTimeData.Flight;
                AirlineLuggageSize = 158;
                AirlineLuggageWeight = 20;

                CustomLuggageSize = DesignTimeData.LuggageSize;
                CustomLuggageTotleSize = CustomLuggageSize.Width + CustomLuggageSize.Height + CustomLuggageSize.Length;
                CustomLuggageWeight = 18;
            }
        }


       
        public Step3PageViewModel(ISBDService sbdService )
        {
            _sbdService = sbdService;
            ScanState =  eScanState.ReadyScan;
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Flight = (Flight)navigationContext.Parameters[nameof(Flight)];
            RaisePropertyChanged(nameof(Flight));
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
        public eScanState ScanState { get; set; }

        public Flight Flight { get; set; }
        public int? AirlineLuggageSize { get; set; }
        public int? AirlineLuggageWeight { get; set; }

        public LuggageSize CustomLuggageSize { get; set; }
        public int? CustomLuggageWeight { get; set; }

        public int CustomLuggageTotleSize { get; set; }
        #endregion

        #region DelegateCommands
        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private async void ExcuteScanCommand()
        {
            ScanState = eScanState.Scanning;
            RaisePropertyChanged(nameof(ScanState));

            var AirlineLuggageSizeTask = _sbdService.GetAirlineLuggageSize(Flight.Airline);
            var AirlineLuggageWeightTask = _sbdService.GetAirlineLuggageWeight(Flight.Airline);

            var CustomLuggageSizeTask = _sbdService.GetLuggageSize();
            var CustomLuggageWeightTask = _sbdService.GetLuggageWieght();

            if (   await AirlineLuggageSizeTask   == null
                || await AirlineLuggageWeightTask == null
                || await CustomLuggageSizeTask    == null
                || await CustomLuggageWeightTask  == null)
                return;


            AirlineLuggageSize = CustomLuggageWeightTask.Result;
            AirlineLuggageWeight = AirlineLuggageWeightTask.Result;
            CustomLuggageSize =CustomLuggageSizeTask.Result;
            CustomLuggageWeight =CustomLuggageWeightTask.Result;

            ScanState = eScanState.FinishedScan;
            CustomLuggageTotleSize = CustomLuggageSize.Width + CustomLuggageSize.Length + CustomLuggageSize.Height;

            RaisePropertyChanged(nameof(AirlineLuggageSize));
            RaisePropertyChanged(nameof(AirlineLuggageWeight));
            RaisePropertyChanged(nameof(CustomLuggageSize));
            RaisePropertyChanged(nameof(CustomLuggageTotleSize));
            RaisePropertyChanged(nameof(CustomLuggageWeight));
            RaisePropertyChanged(nameof(ScanState));
        }
        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            ScanState = eScanState.ReadyScan; RaisePropertyChanged(nameof(ScanState));
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
        #endregion
    }
}
