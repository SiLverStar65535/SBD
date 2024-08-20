using System;
using System.Collections.Generic;
using System.Management; // 引用System.Management命名空間
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DataLibrary;
using SBD.InterfacePool;
using Thermal_Printer;

namespace SBD
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window, WorkStepInterface, PrintInterface
    {
        Storyboard ScanBarCodeMV;
        Storyboard BarprintMV;
        private List<string> comPorts = new List<string>(); // 儲存COM端口的列表
        public int PosComPort_scanRes = 0;//程式初始掃到的pos機comport位置。
        public string PosComPort = "";//pos機驗證過的compor文字，用來呼叫打印方法。
        public List<string> Print_data_list = new List<string>();//列印資料的list
        private StringBuilder inputBuffer = new StringBuilder();
        private StringBuilder barcodeBuilder = new StringBuilder();

        // 创建 Storyboard 并将动画添加到 Storyboard
        Storyboard LEDLIGHT = new Storyboard();
        private BoardingPassData _BoardingPassData = new BoardingPassData();
        public BoardingPassData boardingPassData
        {
            get => _BoardingPassData;
            set
            {
                value = _BoardingPassData;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //Step0();
            //OKUSERDATA.StylusUp += MyButton_StylusUp;
            //this.Page1B1.MainWorkStep = this;
            //this.Page2A.MainWorkStep = this;
            //this.Page3B1.MainWorkStep = this;
            //this.Page4.MainWorkStep = this;
            //this.Page5B.MainWorkStep = this;
            //this.Page5A.MainWorkStep = this;
            //this.Page7A.MainWorkStep = this;
            //this.Page7B.MainWorkStep = this;
            //this.Page7A.IPrintInterface = this;
        }

        private void MyButton_StylusUp(object sender, StylusEventArgs e)
        {
            // 创建一个缩放动画
            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            //OKUSERDATA.RenderTransform = scale;
           

           DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.2,  // 放大到 120%
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true  // 放大后自动缩小
            };

         
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
          
        }
    

        // 找到資源，將其轉換為 Storyboard，然後呼叫 Begin() 方法：
        private void Button_TouchUp(object sender, TouchEventArgs e)
        {



           
            
          //this.Page2A.Visibility = Visibility.Collapsed;
          //this.Page2B.Visibility = Visibility.Collapsed;
          //BarprintMV = (this.FindResource("Barprint") as Storyboard);
          //BarprintMV.Begin();
          //this.Page3A.Visibility = Visibility.Visible;
          //this.Page3B.Visibility = Visibility.Visible;
          //this.CV1.Visibility = Visibility.Collapsed;
         
            //列印收據

            //e.Handled = false;
            //LEDMOVIE(label3);
            //Print_data_list.Add("日期:" + DateTime.Now.ToString("yyy/MM/dd"));
            //Print_data_list.Add("");
            //Print_data_list.Add(_BoardingPassData.PassengerName);
            //Print_data_list.Add("");
            //Print_data_list.Add("搭乘:" + _BoardingPassData.FlightNumber + "班機");
            //Print_data_list.Add("");
            //Print_data_list.Add(_BoardingPassData.DepartureAirport + "---->" + _BoardingPassData.ArrivalAirport);
            //Print_data_list.Add("");
            //Print_data_list.Add("行李號碼:" + GenerateBaggageTag("0308") + " X " + "45kg" + "  1件");
            ////測試跳過此行
            ////printreceipt(Print_data_list);
            //Print_data_list.Clear();

            ////ClearTouchPoints();


        }
        private void ClearTouchPoints()
        {
          //  MainCanvas.Children.Clear();
        }
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            ScanBarCodeMV.Begin();
        }
        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
        
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //强制使用英文输入法
            InputMethod.Current.ImeState = InputMethodState.Off;
            //InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en-EU");
            System.Windows.Input.InputMethod.SetIsInputMethodEnabled(this, false);

            // 檢查comport與VID、PID，並檢查是否與預定的VID和PID匹配。
            void ScanForUsbDevices()
            {
                // 清除之前掃描的COM端口列表和顯示結果
                comPorts.Clear();
                //ComText.Text = "";

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
                                    //ComText.Text += displayText + "\n";
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
            ScanForUsbDevices();
            //LEDMOVIE(label1);
        }
        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 将输入的字符追加到 StringBuilder 中
            inputBuffer.Append(e.Text);
            // 显示输入的字符
           // KeyInfoTextBlock.Text = inputBuffer.ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {    
            if (e.Key == Key.Enter)
            {
                // 获取输入的完整数据
                string input = inputBuffer.ToString();

                try
                {

                   // _BoardingPassData.FlightNumber = input.Substring(36, 7); 
                   // _BoardingPassData.SeatNumber = input.Substring(49, 3);
                   // _BoardingPassData.DepartureAirport = input.Substring(31, 3); 
                   // _BoardingPassData.ArrivalAirport = input.Substring(33, 3); 
                   // string[] PassengerNamewords = input.Split(',');
                   // _BoardingPassData.PassengerName = PassengerNamewords[1]; //"先生/小姐 您好 :"; 
                   //_BoardingPassData.TicketNumber = input.Substring(53, 3);

                   // this.Page1A1.InputDataUser(_BoardingPassData);

                   // this.Step1();
                    //this.ToolBar1.Visibility = Visibility.Visible;

                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // 捕获索引超出范围的异常
                 
                }


                inputBuffer.Clear();
            }
        }
        private void LEDMOVIE(System.Windows.Controls.Label USlabe )
        {
            LEDLIGHT.Children.Clear();
           // 创建颜色动画帧
            ColorAnimationUsingKeyFrames colorAnimation = new ColorAnimationUsingKeyFrames();
            colorAnimation.KeyFrames.Add(new EasingColorKeyFrame(Colors.White, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            colorAnimation.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(255, 13, 11), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5)), new BounceEase { EasingMode = EasingMode.EaseOut }));
            colorAnimation.KeyFrames.Add(new EasingColorKeyFrame(Colors.White, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));

          
            LEDLIGHT.Children.Add(colorAnimation);
            Storyboard.SetTarget(colorAnimation, USlabe);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Foreground.Color"));

            // 动画完成事件
            LEDLIGHT.Completed += Storyboard_Completed_2;

            // 开始动画
            LEDLIGHT.Begin();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }
        /// <summary>
        /// 生成包含航空公司代碼、當前年月日和四位隨機數的行李流水號
        /// </summary>
        /// <param name="airlineCode">航空公司代碼</param>
        /// <returns>生成的行李流水號</returns>
        static string GenerateBaggageTag(string airlineCode)
        {
            // 隨機生成四位數，範圍從0000到9999
            Random random = new Random();
            string randomNumber = random.Next(0, 9999).ToString("D8");

            // 組合成完整的行李流水號
            string baggageTag = airlineCode + randomNumber;

            // 返回生成的行李流水號
            return baggageTag;
        }
        private void Storyboard_Completed_2(object sender, EventArgs e)
        {
            LEDLIGHT.Begin();
        }    
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //LEDMOVIE(label1);  
        }
        /// <summary>
        /// 工作流程+
        /// </summary>
        private void WORKSTEP(string StepString)
        {
            if ("Step1".Equals(StepString) == true)
            {

            }
            else if ("Step2".Equals(StepString) == true)
            {

            }
            else if ("Step3".Equals(StepString) == true)
            {

            }

        }
        private void PrintBT_TouchDown(object sender, TouchEventArgs e)
        {
      
        }
        private void PrintBT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void Storyboard_Completed_3(object sender, EventArgs e)
        {
            BarprintMV.Begin();
        }
        public void Step0()
        {
            //ScanBarCodeMV = (this.FindResource("ScanBarCodeMV") as Storyboard);
            //ComText.Text += ""; //掃描電腦usb設備，顯示在ComText   

            //ScanBarCodeMV.Begin();
            //this.Page0A.Visibility = Visibility.Visible;
            //this.Page0B.Visibility = Visibility.Visible;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Collapsed;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Collapsed;
            //ToolBar1.Visibility= Visibility.Visible;
        }
        public void Step1()
        {
            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Visible;
            //this.Page1A.Visibility = Visibility.Visible;
            //this.Page2A.Visibility = Visibility.Collapsed;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Collapsed;
            //LEDMOVIE(label2);
        }
        public void Step2()
        {

            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Visible;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Collapsed;
            //this.Page2A.showWork();
          
            //this.Page2A.InputDataUser(this._BoardingPassData);
            //LEDMOVIE(label3);
     
        }
        public void Step3(Luggage luggage)
        {


            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Visible;
            //this.Page3B1.Visibility = Visibility.Visible;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Collapsed;
            //this.Page3B1.luggage =luggage;
            //this.Page3B1.showWork();
            //LEDMOVIE(label3);
           
        }
        public void Step4(Luggage luggage)
        {
            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Collapsed;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Visible;
            //this.Page4.luggage = luggage;
            //this.Page4.showWork();
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Collapsed;
            //LEDMOVIE(label4);
        }
        public void Step5(Luggage luggage)
        {
            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Collapsed;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Visible;
            //this.Page5B.Visibility = Visibility.Visible;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Collapsed;
            //this.Page5B.luggage = luggage;
            //this.Page5B.showWork();
            //this.Page5A.luggage = luggage;
            //LEDMOVIE(label5);
        }
        public void Step6()
        {
            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Collapsed;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Visible;
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Collapsed;
            //this.Page7B.Visibility = Visibility.Visible;
            //this.Page5A.showWork();
            //LEDMOVIE(label6);
        }
        public void Step7()
        {
            //this.Page0A.Visibility = Visibility.Collapsed;
            //this.Page0B.Visibility = Visibility.Collapsed;
            //this.Page1A.Visibility = Visibility.Collapsed;
            //this.Page1B.Visibility = Visibility.Collapsed;
            //this.Page2A.Visibility = Visibility.Collapsed;
            //this.Page3B1.Visibility = Visibility.Collapsed;
            //this.Page4.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page5B.Visibility = Visibility.Collapsed;
            //this.Page5A.Visibility = Visibility.Collapsed;
            //this.Page7A.Visibility = Visibility.Visible;
            //this.Page7B.Visibility = Visibility.Visible;
         
            //LEDMOVIE(label7);
        }
        public void StepNG()
        {
            throw new NotImplementedException();
        }
        private void Page4A_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void MainCanvas_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
        public void Printreceipt(List<string> InputTex)
        {
            PosComPort = Post.Comport_check(PosComPort_scanRes);//檢查pos機的port是否存在
            //status.Text = PosComPort;//顯示port號碼
            if (PosComPort != "") //如果port不為""，代表檢查成功，pos機存在
            {
                //Print_data_list.Clear();//清除
                if (Print_data_list.Count == 0)//如果輸入為空，自動帶入測試字串"AB123456"
                {
                    Print_data_list.Add("AB123456");
                    Post.CallByCommandCode(PosComPort, Print_data_list);//發送列印指令
                }
                else
                {
                    // Print_data_list.Add(InputText);//將輸入的textbox組入list中。
                    Post.CallByCommandCode(PosComPort, Print_data_list);//發送列印指令
                }
            }
            else
            {
                MessageBox.Show("請檢察pos機，comport異常");
            }
        }
        public void BagePrintreceipt(List<string> InputTex)
        {
            throw new NotImplementedException();
        }
    }
}
