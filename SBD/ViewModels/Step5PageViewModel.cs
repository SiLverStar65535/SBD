using Prism.Commands;
using Prism.Mvvm;
using SBD.Provider;

namespace SBD.ViewModels
{
    public class Step5PageViewModel : BindableBase
    {
        public Step5PageViewModel()
        {
        }

   
        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step3PageView);
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteCancelCommand);
        private void ExcuteCancelCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step6PageView);
        }
    }
}
