using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Provider;
using System.Text;
using System.Windows.Input;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;
        private readonly ISBDService _sbdService;


        #region Constructors
        public Step1PageViewModel( )
        {
            if (App.IsDesignTime)
            {
                
            }
        }
        public Step1PageViewModel(ISBDService isbdService)
        {
           
            _sbdService = isbdService;
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

            var BoardingPass = inputBuffer.ToString() == string.Empty
                ? CreateFakeBoardingPassData()
                : _sbdService.GetBoardingPassData(inputBuffer.ToString());
            inputBuffer.Clear();

            var Flight = _sbdService.GetFlightDetail(BoardingPass.FlightNumber);

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


        public BoardingPass CreateFakeBoardingPassData()
        {
            var BoardingPass = new BoardingPass();
            BoardingPass.DepartureAirportENG = "TSA";
            BoardingPass.ArrivalAirportENG = "MZG";
            BoardingPass.FlightNumber = "AE0381";
            BoardingPass.SeatNumber = "26A";
            BoardingPass.TicketNumber = "016";
            BoardingPass.PassengerName = "假資料FromDesignTime";
            BoardingPass.DepartureAirport = DataList.AirportNameDictionary[BoardingPass.DepartureAirportENG];
            BoardingPass.ArrivalAirport = DataList.AirportNameDictionary[BoardingPass.ArrivalAirportENG];
            return BoardingPass;
        }
    }
}