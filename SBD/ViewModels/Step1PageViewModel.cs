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

   

        private DelegateCommand _keyDownCommand2;
        public DelegateCommand KeyDownCommand2 => _keyDownCommand2 ??= new DelegateCommand(ExcuteKeyDownCommand2);
        private void ExcuteKeyDownCommand2( )
        {


        }

    }
}
//if (args.Key == Key.Return)
//{

//    var temp = ScandedString;


//    //ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step2PageView);
//}