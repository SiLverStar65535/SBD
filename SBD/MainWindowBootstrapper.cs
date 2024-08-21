using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using System.Windows.Input;
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
