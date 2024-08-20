using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Windows.Threading;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step2PageViewModel : BindableBase
    {
        public Step2PageViewModel()
        {
        }

       
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(ExcuteCancelCommand);
        private void ExcuteCancelCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step1PageView);
        }

        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??= new DelegateCommand(ExcuteConfirmCommand);
        private void ExcuteConfirmCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step3PageView);
        }

    }
}
