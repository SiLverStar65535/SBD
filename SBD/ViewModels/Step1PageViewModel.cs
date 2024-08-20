using Prism.Commands;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step1PageViewModel : BindableBase
    {
        public Step1PageViewModel()
        {
        }

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private void ExcuteScanCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step2PageView);
        }

    }
}
