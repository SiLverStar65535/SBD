using System.Text;
using System.Windows.Input;
using System.Xml;
using DataLibrary;
using Prism.Commands;
using Prism.Mvvm;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase 
    {
        public Step1PageViewModel()
        {

            ScandedString = "M1CE/SHI               6JAWL7 TSAMZGAE 0381 275Y026A0016 34C>5180  3275BAE              2A             0 AE                        N,,";
            var temp = ScandedString.Split(',');
            var boardingPassData = new BoardingPassData
            {
                PassengerName = null,
                FlightNumber = ScandedString.Substring(36, 7),
                SeatNumber = ScandedString.Substring(49, 3),
                DepartureAirport = ScandedString.Substring(31, 3),
                ArrivalAirport = ScandedString.Substring(33, 3),
                DepartureTime = null,
                ArrivalTime = null,
                BoardingGate = null,
                TicketNumber = ScandedString.Substring(53, 3),
                LuggageList = null,
            };


        }

        private readonly StringBuilder inputBuffer = new();
        public string ScandedString { get; set; }


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

            string[] PassengerNamewords = ScandedString.Split(',');
            var temp = ScandedString.Split(',').ToString();
            var boardingPassData = new BoardingPassData
            {
                PassengerName = ScandedString.Split(',').ToString(),
                FlightNumber = ScandedString.Substring(36, 7),
                SeatNumber = ScandedString.Substring(49, 3),
                DepartureAirport = ScandedString.Substring(31, 3),
                ArrivalAirport = ScandedString.Substring(33, 3),
                DepartureTime = null,
                ArrivalTime = null,
                BoardingGate = null,
                TicketNumber = ScandedString.Substring(53, 3),
                LuggageList = null,
            };
            inputBuffer.Clear();
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step2PageView);
        }
    }
}