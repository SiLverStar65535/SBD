using SBD.Domain;
using SBD.Domain.Interface;
using System.Windows;
using SBD.Infrastructure.Interface;
using SBD.Infrastructure.Service;
using SBD.Domain.Models;

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
        private readonly ISBDService _sbdervice;

        #region Constructor
        public MainWindow(IFileService fileService,IWMIService wmiService,
            IQRScanerService qrScanerService,  IDimensionCameraService dimensionCameraService,
            IPrinterService printerService,  IStickerService stickerService, 
            ISBDService sbdervice)
        {
            _fileService = fileService;
            _wmiService = wmiService;
            _qrScanerService = qrScanerService;
            _dimensionCameraService = dimensionCameraService;
            _printerService = printerService;
            _stickerService = stickerService;
            _sbdervice = sbdervice;

            InitializeComponent();
        }
        #endregion

        #region Properties
        private BoardingPass FakeBoardingPass { get; set; }
        private Luggage FakeLuggage { get; set; }
        #endregion


        #region Private Methods
        private void GenerateFakeData()
        {
            FakeBoardingPass = new BoardingPass
            {
                DepartureAirport = null,
                DepartureAirportENG = null,
                ArrivalAirport = null,
                ArrivalAirportENG = null,
                FlightNumber = null,
                SeatNumber = null,
                TicketNumber = null,
                PassengerName = null
            };
            FakeLuggage = new Luggage
            {
                Weight = 0,
                Length = 0,
                Width = 0,
                Height = 0,
                LuggageType = null,
                TagNumber = null,
                FlightTagNumber = null
            };

        


        }
        #endregion

        #region Event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateFakeData();
            var temp1 = _wmiService.QueryWMI("SELECT * FROM Win32_PnPEntity");
            var temp3 = _wmiService.QueryDevices(typeof(WMIQuery.KeyboardQuery));
            var temp2 = _wmiService.QueryDevice(typeof(WMIQuery.KeyboardQuery), "USB\\VID_2EFD&PID_7812\\6&3365FBAF&0&13");

            var QRScaner = _qrScanerService;
            var QRScanerDeviceID = QRScaner.ID;
            var QRScanerDeviceInformation = QRScaner.GetDeviceInformation();
            var QRScanerIsConnected = QRScaner.IsConnected();

            var DimensionCamera = _dimensionCameraService;
            var DimensionCameraDeviceID = DimensionCamera.ID;
            var DimensionCameraDeviceInformation = DimensionCamera.GetDeviceInformation();
            var DimensionCameraIsConnected = DimensionCamera.IsConnected();

            var Printer = _printerService;
            var PrinterDeviceID = Printer.ID;
            var PrinterDeviceInformation = Printer.GetDeviceInformation();
            var PrinterIsConnected = Printer.IsConnected();

            var Sticker = _stickerService;
            var StickerDeviceID = Sticker.ID;
            var StickerDeviceInformation = Sticker.GetDeviceInformation();
            var StickerIsConnected = Sticker.IsConnected();


        }

        //列印條碼貼紙
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _sbdervice.PrintLuggageSticker(FakeBoardingPass, FakeLuggage);
        }
        #endregion
    }
}
