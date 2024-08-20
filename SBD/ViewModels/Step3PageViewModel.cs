using Prism.Commands;
using Prism.Mvvm;

namespace SBD.ViewModels
{
    public class Step3PageViewModel : BindableBase
    {
        public Step3PageViewModel()
        {
            IsGettedData = true;
        }


        public bool IsGettedData { get; set; }

        private DelegateCommand _scanCommand;
        public DelegateCommand ScanCommand => _scanCommand ??= new DelegateCommand(ExcuteScanCommand);
        private void ExcuteScanCommand()
        {
            IsGettedData = true;
            RaisePropertyChanged(nameof(IsGettedData));

            
        }
    }
}
