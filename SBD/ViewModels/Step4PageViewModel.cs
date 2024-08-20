using Prism.Commands;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step4PageViewModel : BindableBase
    {
        public Step4PageViewModel()
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
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step5PageView);
        }

    }
}
