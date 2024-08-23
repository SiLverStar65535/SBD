using System.Text;
using System.Windows.Input;
using DataLibrary;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase
    {
        #region Constructors
        public Step1PageViewModel()
        {
            if (App.IsDesignTime)
            {
            
            }
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
 
            ScandedString = inputBuffer.ToString();

            if (string.IsNullOrEmpty(ScandedString))
                return;

            RaisePropertyChanged(nameof(ScandedString));

            var ScandedStringList = ScandedString.Split(',');
            var boardingPassInfo = ScandedStringList[0];
            var boardingPassPassengerName = ScandedStringList[1];

            var BoardingPass = new BoardingPass
            {
                PassengerName = boardingPassPassengerName,
                DepartureAirport = boardingPassInfo.Substring(31, 3),
                ArrivalAirport = boardingPassInfo.Substring(33, 3),
                FlightNumber = boardingPassInfo.Substring(36, 7),
                SeatNumber = boardingPassInfo.Substring(49, 3),
                TicketNumber = boardingPassInfo.Substring(53, 3),
            };
            inputBuffer.Clear();

            var parameters = new NavigationParameters
            {
                { nameof(BoardingPass), BoardingPass }
            };
            var NaviInfo = new NaviInfo
            {
                RegionName = RegionNames.ContentRegion,
                NaviViewName = NavigatePath.Step2PageView,
                NavigationParameters = parameters
            };
            ApplicationCommands.NavigateCommand.Execute(NaviInfo);
        }
        #endregion
    }
}