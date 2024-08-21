using System;
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
    public partial class Page7BControl : UserControl,InterfacePool.PageInterface,InterfacePool.IDataLinkInterface
    {
        public Page7BControl()
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
        

        private void OKUSERDATA_Click(object sender, RoutedEventArgs e)
        {
            this.MainWorkStep.Step0();
        }

        private void CancelBT_Click(object sender, RoutedEventArgs e)
        {
            this.MainWorkStep.Step2();
        }
    }
}
