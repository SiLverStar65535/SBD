using System.ComponentModel;
using SBD.enumPool;

namespace SBD.Provider
{
    public class StaticData
    {
        private static string _currentStep = "1";
        public static string CurrentStep
        {
            get => _currentStep;
            set
            {
                if (_currentStep == value) return;
                _currentStep = value;
                OnStaticPropertyChanged(nameof(CurrentStep));
            }
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        protected static void OnStaticPropertyChanged(string propertyName) => StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));


    }
}
