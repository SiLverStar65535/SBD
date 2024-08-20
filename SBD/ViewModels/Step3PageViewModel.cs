using Prism.Commands;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step3PageViewModel : BindableBase
    {
        public Step3PageViewModel()
        {
        }


   

        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteCancelCommand);
        private void ExcuteCancelCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step4PageView);
        }
    }
}
