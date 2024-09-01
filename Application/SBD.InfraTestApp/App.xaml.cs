using Microsoft.Extensions.DependencyInjection;
using SBD.Domain.Interface;
using SBD.Infrastructure.Service;
using System;
using System.Windows;
using SBD.Infrastructure;
using SBD.Infrastructure.Interface;

namespace SBD.InfraTestApp
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 創建 ServiceCollection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // 建立 ServiceProvider
            ServiceProvider = services.BuildServiceProvider();

            // 使用 ServiceProvider 來解析 MainWindow 並啟動應用程式
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // SBD.Infrastructure
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IWMIService, WMIService>();
            services.AddSingleton<IQRScanerService, QRScanerService>();
            services.AddSingleton<IDimensionCameraService, DimensionCameraService>();
            services.AddSingleton<IStickerService, StickerService>();
            services.AddSingleton<IPrinterService, PrinterService>();

            // SBD.Domain
            services.AddSingleton<ISBDService, SBDService>();

            // 註冊 MainWindow
            services.AddSingleton<MainWindow>();
        }
    }
}
