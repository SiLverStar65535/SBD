using System;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using SBD.Domain.Interface;
using SBD.Infrastructure;
using SBD.Provider;
using SBD.Views;

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
            containerRegistry.RegisterForNavigation<Step1PageView>();
            containerRegistry.RegisterForNavigation<Step2PageView>();
            containerRegistry.RegisterForNavigation<Step3PageView>();
            containerRegistry.RegisterForNavigation<Step4PageView>();
            containerRegistry.RegisterForNavigation<Step5PageView>();
            containerRegistry.RegisterForNavigation<Step6PageView>();

            containerRegistry.RegisterSingleton<IDataProvider, DataProvider>();
            containerRegistry.RegisterSingleton<IPrintService, PrintService>();
            containerRegistry.RegisterSingleton<IScaneService, ScaneService>();

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
