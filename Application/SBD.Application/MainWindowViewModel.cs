using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Provider;
using System;
using SBD.Domain;

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
        public DeviceInfo QRScaner {  get; set; }
        public DeviceInfo Printer { get; set; }
        public DeviceInfo Sticker { get; set; }
        public DeviceInfo DemensionCamera { get; set; }


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
            QRScaner = _sbdService.GetDeviceInfo(eDevice.QRScaner);
            DemensionCamera = _sbdService.GetDeviceInfo(eDevice.DemensionCamera);
            Printer = _sbdService.GetDeviceInfo(eDevice.Printer);
            Sticker = _sbdService.GetDeviceInfo(eDevice.Sticker);

            QRScaner = new DeviceInfo
            {
                Device = eDevice.QRScaner,
                DeviceID = "11",
                DeviceName = null,
                PosVID = "11",
                PosPID = "11"
            };

            RaisePropertyChanged(nameof(QRScaner));
            RaisePropertyChanged(nameof(DeviceString));
        }

        private DelegateCommand _unloadedCommand;
        public DelegateCommand UnloadedCommand => _unloadedCommand ??= new DelegateCommand(ExcuteUnloadedCommand);
        private void ExcuteUnloadedCommand()
        {
        }
    }
}
