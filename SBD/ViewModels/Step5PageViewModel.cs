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
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step5PageView);
        }
    }
}
