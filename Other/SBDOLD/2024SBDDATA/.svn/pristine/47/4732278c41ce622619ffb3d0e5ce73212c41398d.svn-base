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
using DataLibrary;
using SBD.enumPool;
using SBD.InterfacePool;

namespace SBD
{
    /// <summary>
    /// Page4BControl.xaml 的互動邏輯
    /// </summary>
    public partial class Page5BControl : UserControl,InterfacePool.PageInterface,InterfacePool.IDataLinkInterface
    {
        public Page5BControl()
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

            this.BHeight.Text =this.luggage.Height.ToString() + " cm";
            this.BWeight.Text = this.luggage.Weight.ToString() + " kg";
            this.BWidth.Text = this.luggage.Width.ToString() + " cm";
            this.BLength.Text = this.luggage.Length.ToString()+" cm";
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
            this.MainWorkStep.Step6();
        }

        private void CancelBT_Click(object sender, RoutedEventArgs e)
        {
            this.MainWorkStep.Step2();
        }
    }
}
