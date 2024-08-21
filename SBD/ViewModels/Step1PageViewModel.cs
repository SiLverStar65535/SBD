using System.Windows.Ink;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase
    {
        public Step1PageViewModel()
        {
            
        }


        public string ScandedString { get; set; }
        private DelegateCommand<TextCompositionEventArgs> _previewTextInputCommand;
        public DelegateCommand<TextCompositionEventArgs> PreviewTextInputCommand => _previewTextInputCommand ??= new DelegateCommand<TextCompositionEventArgs>(ExcutePreviewTextInputCommand);
        private void ExcutePreviewTextInputCommand(TextCompositionEventArgs args)
        {

            ScandedString = args.Text;
        }
        private DelegateCommand<KeyEventArgs> _previewTextInputCommand;
        public DelegateCommand<KeyEventArgs> PreviewTextInputCommand => _previewTextInputCommand ??= new DelegateCommand<KeyEventArgs>(ExcutePreviewTextInputCommand);
        private void ExcutePreviewTextInputCommand(TextCompositionEventArgs args)
        {

            ScandedString = args.Text;
        }

    }
}
//if (args.Key == Key.Return)
//{

//    var temp = ScandedString;


//    //ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step2PageView);
//}