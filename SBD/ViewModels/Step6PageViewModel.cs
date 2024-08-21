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
    public class Step6PageViewModel : BindableBase
    {
        public Step6PageViewModel()
        {
        }

    
        private DelegateCommand _nextLuggageCommand;
        public DelegateCommand NextLuggageCommand => _nextLuggageCommand ??= new DelegateCommand(ExcuteNextLuggageCommand);
        private void ExcuteNextLuggageCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step3PageView);
        }

        private DelegateCommand _finishedCommand;
        public DelegateCommand FinishedCommand => _finishedCommand ??= new DelegateCommand(ExcuteFinishedCommand);
        private void ExcuteFinishedCommand()
        {
            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step1PageView);
        }
    }
}
