using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain;
using SBD.Domain.Interface;
using SBD.Provider;
using System;

namespace SBD
{
    public class MainWindowViewModel : BindableBase
    {

        private readonly IRegionManager _regionManager;
        private readonly ISBDService _sbdService;

        public MainWindowViewModel()
        {

        }
        public MainWindowViewModel(IRegionManager regionManager,ISBDService sbdService)
        {
            _regionManager = regionManager;
            _sbdService = sbdService;
            ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }

        public string DeviceString { get; set; }
        public IDevice QRScaner {  get; set; }
        public IDevice Printer { get; set; }
        public IDevice Sticker { get; set; }
        public IDevice DemensionCamera { get; set; }


        private DelegateCommand<NaviInfo> _navigateCommand;
        public DelegateCommand<NaviInfo> NavigateCommand => _navigateCommand ??= new DelegateCommand<NaviInfo>(ExecuteNavigateCommand);
        private void ExecuteNavigateCommand(NaviInfo navigationPath)
        {
            if (navigationPath == null)
                throw new ArgumentNullException();
            
            _regionManager.RequestNavigate(
                  navigationPath.RegionName
                , navigationPath.NaviViewName
                , navigationPath.NavigationParameters);


            switch(navigationPath.NaviViewName)
            {
                case NavigatePath.Step1PageView:
                    StaticData.CurrentStep = "1" ;
                    break;
                case NavigatePath.Step2PageView:
                    StaticData.CurrentStep = "2";
                    break;
                case NavigatePath.Step3PageView:
                    StaticData.CurrentStep = "3";
                    break;
                case NavigatePath.Step4PageView:
                    StaticData.CurrentStep = "4";
                    break;
                case NavigatePath.Step5PageView:
                    StaticData.CurrentStep = "5";
                    break;
                case NavigatePath.Step6PageView:
                    StaticData.CurrentStep = "6";
                    break;

            }
           
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand => _loadedCommand ??= new DelegateCommand(ExecuteLoadedCommand);
        private void ExecuteLoadedCommand()
        {
            QRScaner = _sbdService.GetDevice(eDevice.QRScaner);
            var IsQRScanerConnected = QRScaner.IsConnected();

            DemensionCamera = _sbdService.GetDevice(eDevice.DemensionCamera);
            var IsDemensionCameraConnected = QRScaner.IsConnected();

            Printer = _sbdService.GetDevice(eDevice.Printer);
            var IsPrinterConnected = QRScaner.IsConnected();

            Sticker = _sbdService.GetDevice(eDevice.Sticker);
            var IsStickerConnected = QRScaner.IsConnected();

            RaisePropertyChanged(nameof(QRScaner));
            RaisePropertyChanged(nameof(DemensionCamera));
            RaisePropertyChanged(nameof(Printer));
            RaisePropertyChanged(nameof(Sticker));

            RaisePropertyChanged(nameof(DeviceString));
        }

        private DelegateCommand _unloadedCommand;
        public DelegateCommand UnloadedCommand => _unloadedCommand ??= new DelegateCommand(ExcuteUnloadedCommand);
        private void ExcuteUnloadedCommand()
        {
        }
    }
}
