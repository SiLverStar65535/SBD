using System.Collections.Generic;
using SBD.Domain;
using SBD.Domain.Interface;
using System.Windows;
using SBD.Infrastructure.Interface;
using SBD.Infrastructure.Service;
using SBD.Domain.Models;
using SBD.Infrastructure;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;

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
        public static IEnumerable<eDevice> DeviceList { get; } = Enum.GetValues(typeof(eDevice)).Cast<eDevice>();
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
        private string ClassPropertiesToString(object obj)
        {
            var type = obj.GetType();
            var props = type.GetProperties();
            var result = string.Empty;
            foreach (var prop in props)
            {

                result += $"{prop.Name}:{prop.GetValue(obj)}\n";
            }
            return result;
        }
        #endregion

        #region Event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateFakeData();
            var temp1 = _wmiService.QueryWMI("SELECT * FROM Win32_PnPEntity");
            var temp3 = _wmiService.QueryDevices(typeof(WMIQuery.KeyboardQuery));
            var temp2 = _wmiService.QueryDevice(typeof(WMIQuery.KeyboardQuery), "USB\\VID_2EFD&PID_7812\\6&3365FBAF&0&13");
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = string.Empty;
        }
        #endregion

        #region QRScanner
        //取得ID
        private void QRScanner_Button_Click1(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _qrScanerService.ID;
        }
        //取得設備資訊
        private void QRScanner_Button_Click2(object sender, RoutedEventArgs e)
        {
            var temp = _qrScanerService.GetDeviceInformation();
            OutputTextBox.Text = temp != null ? temp.ToString() : "取得失敗";
        }
        //是否連接
        private void QRScanner_Button_Click3(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _qrScanerService.IsConnected().ToString();
        }
        #endregion

        #region DimensionCamera
        //取得ID
        private void DimensionCamera_Button_Click_1(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _dimensionCameraService.ID;
        }  
        //取得設備資訊
        private void DimensionCamera_Button_Click_2(object sender, RoutedEventArgs e)
        {
            var temp = _dimensionCameraService.GetDeviceInformation();
            OutputTextBox.Text = temp != null ? temp.ToString() : "取得失敗";
        }
        //是否連接
        private void DimensionCamera_Button_Click_3(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _dimensionCameraService.IsConnected().ToString();
        }
        //取得尺寸
        private async void DimensionCamera_Button_Click_4(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = string.Empty;
            var temp = await _dimensionCameraService.GetSize();
            OutputTextBox.Text = temp.ToString();
        }
        #endregion

        #region Printer
        //取得ID
        private void Printer_Button_Click_1(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _printerService.ID;
        }
        //取得設備資訊
        private void Printer_Button_Click_2(object sender, RoutedEventArgs e)
        {
            var temp = _printerService.GetDeviceInformation();
            OutputTextBox.Text = temp != null ? temp.ToString() : "取得失敗";
        }
        //是否連接
        private void Printer_Button_Click_3(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = _printerService.IsConnected().ToString();
        }
        //列印文字
        private async void Printer_Button_Click_4(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = string.Empty;
            var temp = await _printerService.PrintListString(new List<string>());
            OutputTextBox.Text = temp.ToString();
        }
        #endregion

        #region Sticker
        //取得ID
        private void Sticker_Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        //取得設備資訊
        private void Sticker_Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
        //是否連接
        private void Sticker_Button_Click_3(object sender, RoutedEventArgs e)
        {
            
        }
        //列印條碼貼紙
        private void Sticker_Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region SBDService
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DeviceComboBox.SelectedValue is eDevice selectedDevice)
            {
                OutputTextBox.Text = _sbdervice.IsDeviceConnected(selectedDevice).ToString();
            }
        }
        //取得登機證資訊
        private void SBD_Button_Click_1(object sender, RoutedEventArgs e)
        {            
            //var scaneString = string.Empty;
            var scaneString = "M1CE/SHI               6JAWL7 TSAMZGAE 0381 275Y026A0016 34C>5180  3275BAE              2A             0 AE                        N,測試,";
            var temp = _sbdervice.CreateBoardingPassData(scaneString);
            OutputTextBox.Text = temp != null
                ? ClassPropertiesToString(temp)
                : "取得失敗";
        }
        //取得航班資訊
        private void SBD_Button_Click_2(object sender, RoutedEventArgs e)
        {
            var temp = _sbdervice.GetFlightDetail("AE0381");

            OutputTextBox.Text = temp != null 
                ? ClassPropertiesToString(temp) 
                : "取得失敗";
        }
        //取得航空公司規定的行李尺寸
        private async void SBD_Button_Click_3(object sender, RoutedEventArgs e)
        {
            await _sbdervice.GetAirlineLuggageSize("華信");
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