using System.Text;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SBD.Domain.Interface;
using SBD.Domain.Models;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive { get; } = false;
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

            var BoardingPass = inputBuffer.ToString() == string.Empty 
                ? _dataProvider.CreateFakeBoardingPassData( ) 
                : _dataProvider.GetBoardingPassData(inputBuffer.ToString());
            inputBuffer.Clear();

            var Flight = _dataProvider.GetFlightDetail(BoardingPass.FlightNumber);

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