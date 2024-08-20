using DataLibrary;
using SBD.enumPool;
using SBD.InterfacePool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SBD
{
    /// <summary>
    /// Page4AControl.xaml 的互動邏輯
    /// </summary>
    public partial class Page2AControl : UserControl , InterfacePool.PageInterface,InterfacePool.IDataLinkInterface
    {
        public BaggageScanStatus BaggageScan { get; set; }
        WorkStepInterface _WorkStepInterface;
        public WorkStepInterface MainWorkStep { get => _WorkStepInterface; set => _WorkStepInterface = value; }
        Storyboard scanBageStoryboard;
        Storyboard hlepScanBgeButtonStoryboard;

        public Page2AControl()
        {
            InitializeComponent();
            BaggageScan = BaggageScanStatus.ScanSizeAndWeightcomplete;
        }

        private void Button_TouchDown(object sender, TouchEventArgs e)
        {

           
            scanBageStoryboard = this.FindResource("ScanBageStoryboard") as Storyboard;

            BaggageScan = BaggageScanStatus.ScanSizeAndWeight;

            scanBageStoryboard.Begin();

        }

        private void Button_TouchDown_1(object sender, TouchEventArgs e)
        {

            



          
        }

            private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           // var storyboard1 = ScanButton.Template.FindName("ScanBageStoryboard", this.ScanButton);

        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
           if( BaggageScan == BaggageScanStatus.ScanSizeAndWeight)
            {
                scanBageStoryboard.Begin();
            }
           else if (BaggageScan == BaggageScanStatus.ScanSizeAndWeightcomplete)
           {

                //測量值
                Luggage _Luggage = new Luggage();
                _Luggage.boardingPassData = _boardingPassData;
                _Luggage.Height = 12;
                _Luggage.Length = 70;
                _Luggage.LuggageType = "Locad";
                _Luggage.Width = 60;
                _Luggage.Weight= 30;
                this._WorkStepInterface.Step3(_Luggage);
           }

        }
    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaggageScan = BaggageScanStatus.ScanSizeAndWeightcomplete;
            
        }

        public void showWork()
        {
            hlepScanBgeButtonStoryboard = this.FindResource("HlepScanBgeButtonStoryboard") as Storyboard;

            BaggageScan = BaggageScanStatus.TellScanSizeAndWeight;

            hlepScanBgeButtonStoryboard.Begin();
        }

        public void CloseWork()
        {
            throw new NotImplementedException();
        }

        public void NGWork()
        {
            throw new NotImplementedException();
        }

        public void NextWork()
        {
            throw new NotImplementedException();
        }

        public void UPWork()
        {
            throw new NotImplementedException();
        }

        public void InputDataUser(BoardingPassData UserBoardingPassData)
        {
            this.boardingPassData = UserBoardingPassData;
        }

        public BoardingPassData OutputDataUser()
        {
            throw new NotImplementedException();
        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
          
        
        }

        private void Storyboard_Completed_2(object sender, EventArgs e)
        {
            if (BaggageScan == BaggageScanStatus.TellScanSizeAndWeight)
            {
                hlepScanBgeButtonStoryboard.Begin();
            }
        }

        BoardingPassData _boardingPassData;
        public BoardingPassData boardingPassData
        {
            get => _boardingPassData;
            set
            {
                value = _boardingPassData;
            }
        }
    }
}
