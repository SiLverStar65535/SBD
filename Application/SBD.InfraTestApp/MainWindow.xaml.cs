using SBD.Domain;
using SBD.Domain.Interface;
using System.Windows;
using SBD.Infrastructure.Interface;
using SBD.Infrastructure.Service;
using SBD.Domain.Models;
using SBD.Infrastructure;

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

        #region WindowEvent
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateFakeData();
            var temp1 = _wmiService.QueryWMI("SELECT * FROM Win32_PnPEntity");
            var temp3 = _wmiService.QueryDevices(typeof(WMIQuery.KeyboardQuery));
            var temp2 = _wmiService.QueryDevice(typeof(WMIQuery.KeyboardQuery), "USB\\VID_2EFD&PID_7812\\6&3365FBAF&0&13");

         
        }
        #endregion

        #region QRScanner
        private void QRScanner_Button_Click1(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _qrScanerService.ID;
        }
        private void QRScanner_Button_Click2(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _qrScanerService.GetDeviceInformation().ToString();
        }
        private void QRScanner_Button_Click3(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _qrScanerService.IsConnected().ToString();
        }
        #endregion

        #region DimensionCamera
        private void DimensionCamera_Button_Click_1(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _dimensionCameraService.ID;
        }
        private void DimensionCamera_Button_Click_2(object sender, RoutedEventArgs e)
        {
            var temp = _dimensionCameraService.GetDeviceInformation();
            OutputTextBox.Text = temp != null ? temp.ToString() : "null";
        }
        private void DimensionCamera_Button_Click_3(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _dimensionCameraService.IsConnected().ToString();
        }
        private async void DimensionCamera_Button_Click_4(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = string.Empty;
            var temp = await _dimensionCameraService.GetSize();
            OutputTextBox.Text = temp.ToString();
        }
        #endregion

        #region Printer
        private void Printer_Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        private void Printer_Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
        private void Printer_Button_Click_3(object sender, RoutedEventArgs e)
        {
            
        }
        private void Printer_Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region Sticker
        private void Sticker_Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        private void Sticker_Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
        private void Sticker_Button_Click_3(object sender, RoutedEventArgs e)
        {
            
        }
        private void Sticker_Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region SBDService
        //取得登機證資訊
        private void SBD_Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp = _sbdervice.GetBoardingPassData("");
         
        }
        //取得航班資訊
        private void SBD_Button_Click_2(object sender, RoutedEventArgs e)
        {
            //_sbdervice.GetBoardingPassData();
        }
        //取得航空公司規定的行李尺寸
        private void SBD_Button_Click_3(object sender, RoutedEventArgs e)
        {
            _sbdervice.GetAirlineLuggageSize("");
        }
        //取得航空公司規定的行李重量
        private void SBD_Button_Click_4(object sender, RoutedEventArgs e)
        {
            _sbdervice.GetAirlineLuggageWeight("");
        }
        //取得乘客託運行李尺寸
        private void SBD_Button_Click_5(object sender, RoutedEventArgs e)
        {
            _sbdervice.GetPassengerLuggageSize();
        }
        //取得乘客託運行李重量
        private void SBD_Button_Click_6(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _sbdervice.GetPassengerLuggageWieght().ToString();
        }
        //列印行李條貼紙
        private void SBD_Button_Click_7(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _sbdervice.GetPassengerLuggageWieght().ToString();
        }
        //列印收據
        private void SBD_Button_Click_8(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _sbdervice.GetPassengerLuggageWieght().ToString();
        }
        //列印優惠券
        private void SBD_Button_Click_9(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _sbdervice.GetPassengerLuggageWieght().ToString();
        }
        #endregion



    }
}
//_sbdervice.PrintLuggageSticker(FakeBoardingPass, FakeLuggage);