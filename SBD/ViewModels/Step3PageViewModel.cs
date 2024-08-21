using Prism.Commands;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step3PageViewModel : BindableBase
    {
        public Step3PageViewModel()
        {
            IsGettedData = false;
        }


        public bool IsGettedData { get; set; }

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private void ExcuteScanCommand()
        {
            IsGettedData = true;
            RaisePropertyChanged(nameof(IsGettedData));
        }

        private DelegateCommand _againCommand;
        public DelegateCommand AgainCommand => _againCommand ??= new DelegateCommand(ExcuteAgainCommand);
        private void ExcuteAgainCommand()
        {
            IsGettedData = false;
            RaisePropertyChanged(nameof(IsGettedData));
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteConfirmCommand);
        private void ExcuteConfirmCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step4PageView);
        }

    }
}
