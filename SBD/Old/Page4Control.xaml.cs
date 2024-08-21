using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DataLibrary;
using SBD.InterfacePool;

namespace SBD.Old
{
    /// <summary>
    /// Page4Control.xaml 的互動邏輯
    /// </summary>
    public partial class Page4Control : UserControl, InterfacePool.PageInterface, IDataLinkInterface
    {
        Storyboard BaggagePrintStoryboard;
        enumPool.PrintBarCodeStatus _PrintBarCodeStatus =new enumPool.PrintBarCodeStatus();
        public Page4Control()
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
     
        }

        public BoardingPassData OutputDataUser()
        {
            throw new NotImplementedException();
        }

        public void showWork()
        {
            BaggagePrintStoryboard = this.FindResource("Barprint") as Storyboard;

            _PrintBarCodeStatus = enumPool.PrintBarCodeStatus.HelipBarPrint;

            BaggagePrintStoryboard.Begin();

            //貼BARCODE動畫
            //Barprint
        }

        public void UPWork()
        {
            this.MainWorkStep.Step2();
        }

        private void Storyboard_Completed_3(object sender, EventArgs e)
        {
          if (  _PrintBarCodeStatus == enumPool.PrintBarCodeStatus.HelipBarPrint)
            {
                BaggagePrintStoryboard.Begin();
            }
        
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
    }
}
