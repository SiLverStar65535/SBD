using System.Windows.Controls;
using System.Windows.Input;

namespace SBD.Views
{
    /// <summary>
    /// Step1View.xaml 的互動邏輯
    /// </summary>
    public partial class Step1PageView : UserControl
    {
        public Step1PageView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e) => Focus();
    }
}
