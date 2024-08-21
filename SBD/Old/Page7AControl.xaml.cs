using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataLibrary;
using SBD.enumPool;
using SBD.InterfacePool;

namespace SBD.Old
{
    /// <summary>
    /// Page4BControl.xaml 的互動邏輯
    /// </summary>
    public partial class Page7AControl : UserControl,InterfacePool.PageInterface,InterfacePool.IDataLinkInterface
    {
        public Page7AControl()
        {
            InitializeComponent();
        }

        public BaggageScanStatus BaggageScan { get; set; }
        WorkStepInterface _WorkStepInterface;
        public WorkStepInterface MainWorkStep { get => _WorkStepInterface; set => _WorkStepInterface = value; }

        public void CloseWork()
        {
            throw new NotImplementedException();
        }

        public void InputDataUser(BoardingPassData UserBoardingPassData)
        {
            this.boardingPassData = UserBoardingPassData;
        }
    
        public void NextWork()
        {
            throw new NotImplementedException();
        }

        public void NGWork()
        {
            throw new NotImplementedException();
        }

        public BoardingPassData OutputDataUser()
        {
            throw new NotImplementedException();
        }

        public void showWork()
        {

          
        }

        public void UPWork()
        {
            throw new NotImplementedException();
        }



        public BoardingPassData boardingPassData
        {
            get;
            set;
          
        }

   
        public Luggage luggage


        { get; set; }
        public List<string> Print_data_list = new List<string>();//列印資料的list

      private   string GAteTIKE(string airlineCode)
        {

            // 隨機生成四位數，範圍從0000到9999
            Random random = new Random();
            string randomNumber = random.Next(0, 9999).ToString("D8");

            // 組合成完整的行李流水號
            string baggageTag = airlineCode + randomNumber;

            // 返回生成的行李流水號
            return baggageTag;



        }

        private void OKUSERDATA_Click(object sender, RoutedEventArgs e)
        {
            this.MainWorkStep.Step6();
            //列印收據

            Print_data_list.Add("-------------松山機場------------" );
            Print_data_list.Add("");
            Print_data_list.Add("使用期限:" + DateTime.Now.ToString("yyy/MM/dd"));
            Print_data_list.Add("");
            Print_data_list.Add("很好吃牛肉麵店");
            Print_data_list.Add("");
            Print_data_list.Add("優惠:" + "137元");
            Print_data_list.Add("");
           Print_data_list.Add("");
            //Print_data_list.Add("票號:" + GenerateBaggageTag("0308") + " X " + "45kg" + "  1件");
            ////測試跳過此行
            this.IPrintInterface.Printreceipt(Print_data_list);
            Print_data_list.Clear();

            ////ClearTouchPoints();
        }

        private void CancelBT_Click(object sender, RoutedEventArgs e)
        {
            this.MainWorkStep.Step2();
        }

        public PrintInterface IPrintInterface
        {
            get => default(PrintInterface);
            set
            {
            }
        }
    }
}
