using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using SBD.Provider;
using SBD.Views;
using System;
using System.Windows;
using SBD.Domain.Interface;
using SBD.Infrastructure;
using SBD.Infrastructure.Interface;
using SBD.Infrastructure.Service;

namespace SBD
{
    class MainWindowBootstrapper : PrismBootstrapper
    {
        private readonly Type StartWindow = typeof(MainWindow);

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //SBD.Infrastructure
            containerRegistry.RegisterSingleton<IFileService, FileService>(); 
            containerRegistry.RegisterSingleton<IWMIService, WMIService>();
            containerRegistry.RegisterSingleton<IQRScanerService, QRScanerService>();
            containerRegistry.RegisterSingleton<IDimensionCameraService,  DimensionCameraService>();
            containerRegistry.RegisterSingleton<IStickerService, StickerService>();
            containerRegistry.RegisterSingleton<IPrinterService, PrinterService>();

            //SBD.Domain
            containerRegistry.RegisterSingleton<ISBDService, SBDService>();

            //SBD.Application
            containerRegistry.RegisterForNavigation<Step1PageView>();
            containerRegistry.RegisterForNavigation<Step2PageView>();
            containerRegistry.RegisterForNavigation<Step3PageView>();
            containerRegistry.RegisterForNavigation<Step4PageView>();
            containerRegistry.RegisterForNavigation<Step5PageView>();
            containerRegistry.RegisterForNavigation<Step6PageView>();
        }
        protected override DependencyObject CreateShell()
        {
            return (Window)Container.Resolve(StartWindow);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Step1PageView));
        }
    }
}
