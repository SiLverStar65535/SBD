using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SBD.Domain.Models;
using SBD.InterfacePool;

namespace SBD.Old
{
    /// <summary>
    /// Page4Control.xaml 的互動邏輯
    /// </summary>
    public partial class Page5AControl : UserControl, InterfacePool.PageInterface, IDataLinkInterface
    {
        Storyboard RunBagStoryboardStoryboard;
        enumPool.BaggageScanStatus _BaggageScanStatus = new enumPool.BaggageScanStatus();
        public Page5AControl()
        {
            InitializeComponent();
          
        }

        public WorkStepInterface MainWorkStep { get; set ; }

        public void CloseWork()
        {
            throw new NotImplementedException();
        }

        public void InputDataUser(BoardingPassData UserBoardingPassData)
        {
           // this.boardingPassData = UserBoardingPassData;
        }

        public void NextWork()
        {
            this.MainWorkStep.Step5(luggage);
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
            RunBagStoryboardStoryboard = this.FindResource("RunBagStoryboard") as Storyboard;

            _BaggageScanStatus = enumPool.BaggageScanStatus.RUNBAGWeight;

            RunBagStoryboardStoryboard.Begin();

            //貼BARCODE動畫
            //Barprint
        }

        public void UPWork()
        {
            this.MainWorkStep.Step2();
        }

       

        private void OKUSERDATA_Click(object sender, RoutedEventArgs e)
        {
            NextWork();
        }
        public Luggage luggage

        { get; set; }
        private void CancelBT_Click(object sender, RoutedEventArgs e)
        {
            UPWork();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            this.MainWorkStep.Step7();
        }

        private void Grid_GotTouchCapture(object sender, TouchEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MainWorkStep.Step7();
        }
    }
}
