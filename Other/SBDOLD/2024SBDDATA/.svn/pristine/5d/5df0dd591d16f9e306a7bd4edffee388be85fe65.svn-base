using System;
using System.Collections.Generic;
using System.Globalization;
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
using SBD.InterfacePool;

namespace SBD
{
    /// <summary>
    /// Page2AControl.xaml 的互動邏輯
    /// </summary>
    public partial class Page1AControl : UserControl , PageInterface, InterfacePool.IDataLinkInterface


    {
        public Page1AControl()
        {
            InitializeComponent();
        }

         WorkStepInterface _WorkStepInterface;
        public WorkStepInterface MainWorkStep { get => _WorkStepInterface; set => _WorkStepInterface=value ; }

        public void CloseWork()
        {
            throw new NotImplementedException();
        }

        public void InputDataUser(BoardingPassData UserBoardingPassData)
        {
            DateTime now = DateTime.Now;
            // 创建英文（美国）文化信息对象
            CultureInfo enUS = new CultureInfo("en-US");

            this.DepartureAirport_en.Text = UserBoardingPassData.DepartureAirport;
             this.ArrivalAirport_en.Text = UserBoardingPassData.ArrivalAirport;
             this.FlightNumber.Text = UserBoardingPassData.FlightNumber;
             this.FlightGet.Text = UserBoardingPassData.BoardingGate;
             this.DepartureAirport.Text = UserBoardingPassData.DepartureAirport;
             this.FData.Text = UserBoardingPassData.DepartureTime;
             this.FData_MMMDD.Text = now.ToString("ddMMM", enUS).ToUpper();
            this.PassengerName.Text = UserBoardingPassData.PassengerName;
             this.TicketNumber.Text = UserBoardingPassData.TicketNumber;
    

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
            throw new NotImplementedException();
        }

        public void UPWork()
        {
            throw new NotImplementedException();
        }

    }
}
