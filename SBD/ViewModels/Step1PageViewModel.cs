using System.Text;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase
    {
        public Step1PageViewModel()
        {
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
           
            if (string.IsNullOrEmpty( ScandedString))
                return;

            inputBuffer.Clear();
            RaisePropertyChanged(nameof(ScandedString));


            //var boardingPassData = new BoardingPassData
            //{
            //    PassengerName = null,
            //    FlightNumber = ScandedString.Substring(36, 7),
            //    SeatNumber = ScandedString.Substring(49, 3),
            //    DepartureAirport = ScandedString.Substring(31, 3),
            //    ArrivalAirport = ScandedString.Substring(33, 3),
            //    DepartureTime = null,
            //    ArrivalTime = null,
            //    BoardingGate = null,
            //    TicketNumber = ScandedString.Substring(53, 3),
            //    LuggageList = null
            //};

            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step2PageView);
        }
    }
}