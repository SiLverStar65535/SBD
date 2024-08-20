using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Regions;

namespace SBD
{
    public static class ApplicationCommands
    {
        public static CompositeCommand NavigateCommand = new CompositeCommand();
       
    }
    public class MainWindowViewModel
    {
        private readonly IRegionManager _regionManager;
        public MainWindowViewModel()
        {
           
        }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand => _navigateCommand ??= new DelegateCommand<string>(ExecuteNavigateCommand);
        private void ExecuteNavigateCommand(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
    }
}
