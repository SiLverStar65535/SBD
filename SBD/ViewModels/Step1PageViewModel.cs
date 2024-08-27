using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase
    {
        private readonly IDataProvider _dataProvider;
        #region Constructors
        public Step1PageViewModel( )
        {
            if (App.IsDesignTime)
            {
            
            }
        }
        public Step1PageViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        #endregion

        #region Properties
        private readonly StringBuilder inputBuffer = new();
        public string ScandedString { get; set; }
        #endregion

        #region DelegateCommands
        private DelegateCommand<TextCompositionEventArgs> _previewTextInputCommand;
        public DelegateCommand<TextCompositionEventArgs> PreviewTextInputCommand => _previewTextInputCommand ??= new DelegateCommand<TextCompositionEventArgs>(ExcutePreviewTextInputCommand);
        private void ExcutePreviewTextInputCommand(TextCompositionEventArgs args) => inputBuffer.Append(args.Text);

        private DelegateCommand<KeyEventArgs> _keyDownCommand;
        public DelegateCommand<KeyEventArgs> KeyDownCommand => _keyDownCommand ??= new DelegateCommand<KeyEventArgs>(ExcuteKeyDownCommand);
        private void ExcuteKeyDownCommand(KeyEventArgs args)
        {
            if (args.Key != Key.Enter)
                return;

            BoardingPass BoardingPass;
            if (inputBuffer.ToString() == string.Empty)
            {
                BoardingPass = new BoardingPass();
                BoardingPass.DepartureAirportENG = "TSA";
                BoardingPass.ArrivalAirportENG = "MZG";
                BoardingPass.FlightNumber = "AE0381";
                BoardingPass.SeatNumber = "26A";
                BoardingPass.TicketNumber = "016";
                BoardingPass.PassengerName = "假資料FromDesignTime";
                BoardingPass.DepartureAirport = DataList.AirportNameDictionary[BoardingPass.DepartureAirportENG];
                BoardingPass.ArrivalAirport = DataList.AirportNameDictionary[BoardingPass.ArrivalAirportENG];
            }
            else
            {
                ScandedString = inputBuffer.ToString();
                var ScandedStringList = ScandedString.Split(',');
                var boardingPassInfo = ScandedStringList[0];
                var boardingPassPassengerName = ScandedStringList[1];
                BoardingPass = new BoardingPass();
                BoardingPass.PassengerName = boardingPassPassengerName;
                BoardingPass.DepartureAirportENG = boardingPassInfo.Substring(31, 3);
                BoardingPass.DepartureAirport = DataList.AirportNameDictionary[BoardingPass.DepartureAirportENG];
                BoardingPass.ArrivalAirportENG = boardingPassInfo.Substring(33, 3);
                BoardingPass.ArrivalAirport = DataList.AirportNameDictionary[BoardingPass.ArrivalAirportENG];
                BoardingPass.FlightNumber = boardingPassInfo.Substring(36, 7);
                BoardingPass.SeatNumber = boardingPassInfo.Substring(49, 3);
                BoardingPass.TicketNumber = boardingPassInfo.Substring(53, 3);
            }
            var Flight = _dataProvider.GetFlightDetail(BoardingPass.FlightNumber);
            inputBuffer.Clear();

            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step2PageView,
                NavigationParameters = new NavigationParameters
                { 
                    { nameof(BoardingPass), BoardingPass },
                    { nameof(Flight), Flight }
                }
            };
            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }
        #endregion
    }
}