using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management; // 引用System.Management命名空間
using System.Text.RegularExpressions;
using Thermal_Printer;
using Therm_Sticker;

namespace NO6_FunctionTest
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    { 

        private List<string> comPorts = new List<string>(); // 儲存COM端口的列表
        public int PosComPort_scanRes = 0;//程式初始掃到的pos機comport位置。
        public string PosComPort = "";//pos機驗證過的compor文字，用來呼叫打印方法。
        public List<string> Print_data_list = new List<string>();//列印資料的list
        public string InputText = "";//用來存放textbox輸入的文字

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;//load的時候執行一些任務
            ComText.Text += ""; //掃描電腦usb設備，顯示在ComText
            


        }
        /// <summary>
        /// 檢查comport，強制鎖定游標到textbox內
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BarcodeInputTextBox.Focus();
            ScanForUsbDevices();
        }

        // 檢查comport與VID、PID，並檢查是否與預定的VID和PID匹配。
        private void ScanForUsbDevices()
        {
            // 清除之前掃描的COM端口列表和顯示結果
            comPorts.Clear();
            ComText.Text = "";

            // 使用WMI來搜索系統中的所有設備，篩選出含有COM端口描述的設備
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'");
            // 正則表達式，用於從設備描述中提取COM端口編號
            var comRegex = new Regex(@"\(COM(\d+)\)");

            try
            {
                // 遍歷搜索結果
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // 獲取設備描述信息
                    string caption = queryObj["Caption"].ToString();
                    // 使用正則表達式匹配COM端口
                    var matchCom = comRegex.Match(caption);
                    if (matchCom.Success)
                    {
                        // 成功匹配後提取COM端口號碼
                        string comPortNumberStr = matchCom.Groups[1].Value;
                        // 嘗試將提取的端口號碼字符串轉換為整數
                        if (int.TryParse(comPortNumberStr, out int comPortNumber))
                        {
                            // 提取設備ID，用於進一步匹配VID和PID
                            string deviceId = queryObj["DeviceID"].ToString();
                            // 正則表達式，用於從設備ID中提取VID和PID
                            var vidPidRegex = new Regex(@"VID_([0-9A-F]+)&PID_([0-9A-F]+)");
                            var matchVidPid = vidPidRegex.Match(deviceId);
                            // 檢查VID和PID是否與配置文件中設定的相符
                            if (matchVidPid.Success && matchVidPid.Groups[1].Value == Config.PosVID && matchVidPid.Groups[2].Value == Config.PosPID)
                            {
                                // 如果匹配成功，設定全域變數為提取的COM端口號碼
                                PosComPort_scanRes = comPortNumber;
                                // 組合顯示文字並更新界面上的顯示
                                string displayText = $"COM{comPortNumber} (VID={Config.PosVID}, PID={Config.PosPID}) detected and saved.";
                                ComText.Text += displayText + "\n";
                            }
                        }
                    }
                }
            }
            catch (ManagementException ex)
            {
                // 處理可能的異常，並在異常發生時彈出提示
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }
        }



        private void Printer1_Click(object sender, RoutedEventArgs e)
        {
            InputText = BarcodeInputTextBox.Text;//擷取輸入文字
            PosComPort = Post.Comport_check(PosComPort_scanRes);//檢查pos機的port是否存在
            status.Text = PosComPort;//顯示port號碼
            if (PosComPort != "") //如果port不為""，代表檢查成功，pos機存在
            {
                Print_data_list.Clear();//清除
                if (InputText == "")//如果輸入為空，自動帶入測試字串"AB123456"
                {
                    Print_data_list.Add("AB123456");
                    Post.CallByCommandCode(PosComPort, Print_data_list);//發送列印指令
                }
                else
                {
                    Print_data_list.Add(InputText);//將輸入的textbox組入list中。
                    Post.CallByCommandCode(PosComPort, Print_data_list);//發送列印指令
                }
            }
            else
            {
                MessageBox.Show("請檢察pos機，comport異常");
            }
        }

        private void Printer2_Click(object sender, RoutedEventArgs e)
        {
            InputText = BarcodeInputTextBox.Text;//擷取輸入文字
            if (InputText == "")//如果輸入為空，自動帶入測試字串"AB123456"
            {
                Therm.PrintData(Config.ThermPinterName, "AB123456");
            }
            else
            {
                Therm.PrintData(Config.ThermPinterName, InputText);
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
