using SBD.Domain;
using SBD.Domain.Interface;
using System.Windows;
using SBD.Infrastructure.Internel.Interface;

namespace SBD.InfraTestApp
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFileService _fileService;
        private readonly IWMIService _wmiService;
        private readonly IQRScanerService _qrScanerService;
        private readonly IDimensionCameraService _dimensionCameraService;
        private readonly IPrinterService _printerService;
        private readonly IStickerService _stickerService;

        public MainWindow(IFileService fileService,IWMIService wmiService,
            IQRScanerService qrScanerService,
            IDimensionCameraService dimensionCameraService,
            IPrinterService printerService,
            IStickerService stickerService )
        {
            _fileService = fileService;
            _wmiService = wmiService;
            _qrScanerService = qrScanerService;
            _dimensionCameraService = dimensionCameraService;
            _printerService = printerService;
            _stickerService = stickerService;
 
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           var QRScaner = _qrScanerService;
           var QRScanerDeviceID = QRScaner.DeviceID;
           var QRScanerIsConnected = QRScaner.IsConnected();
           var QRScanerDeviceInformation = QRScaner.GetDeviceInformation();

        
           var DimensionCamera = _dimensionCameraService;
           var DimensionCameraDeviceID = DimensionCamera.DeviceID;
           var DimensionCameraIsConnected = DimensionCamera.IsConnected();
           var DimensionCameraDeviceInformation = DimensionCamera.GetDeviceInformation();
 

           var Printer = _printerService;
           var PrinterDeviceID = Printer.DeviceID;
           var PrinterIsConnected = Printer.IsConnected();
           var PrinterDeviceInformation = Printer.GetDeviceInformation();

     
           var Sticker = _stickerService;
           var StickerDeviceID = Sticker.DeviceID;
           var StickerIsConnected = Sticker.IsConnected();
           var StickerDeviceInformation = Sticker.GetDeviceInformation();
 
        }
    }
}
