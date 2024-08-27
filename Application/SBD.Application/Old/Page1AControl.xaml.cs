using System;
using System.Globalization;
using System.Windows.Controls;
using SBD.Domain.Models;
using SBD.InterfacePool;

namespace SBD.Old
{
    /// <summary>
    /// Page2AControl.xaml 的互動邏輯
    /// </summary>
    public partial class Page1AControl : UserControl, PageInterface, InterfacePool.IDataLinkInterface
    {
        public Page1AControl()
        {
            InitializeComponent();
        }
        public WorkStepInterface MainWorkStep { get => _WorkStepInterface; set => _WorkStepInterface = value; }
        WorkStepInterface _WorkStepInterface;
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
