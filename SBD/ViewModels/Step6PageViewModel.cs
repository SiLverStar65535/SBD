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

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private void ExcuteScanCommand()
        {




            ApplicationCommands.NavigateCommand.Execute(NavigatePath.Step2PageView);
        }

    }
}
