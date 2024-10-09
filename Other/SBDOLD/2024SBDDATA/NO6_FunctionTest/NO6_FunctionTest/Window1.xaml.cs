using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NO6_FunctionTest
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            name.NameValue = InputScopeNameValue.AlphanumericHalfWidth;
            input.Names.Add(name);

            //this.T1.Focus();
        }

    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private string KeyTOString(KeyEventArgs keyCode)
        {
            string Keystring = "";

          
             if (keyCode.Key == Key.D0)
            {
                
                    Keystring = "0";
            }
            else if (keyCode.Key == Key.D1)
            {
                Keystring = "1";
            }
            else if (keyCode.Key == Key.D2)
            {
                Keystring = "2";
            }
            else if (keyCode.Key == Key.D3)
            {
                Keystring = "3";
            }
            else if (keyCode.Key == Key.D4)
            {
                Keystring = "4";
            }
            else if (keyCode.Key == Key.D5)
            {
                Keystring = "5";
            }
            else if (keyCode.Key == Key.D6)
            {
                Keystring =  "6";
            }
             else if (keyCode.Key == Key.D7)
            {
                Keystring = "7";
            }
            else if (keyCode.Key == Key.D8)
            {
                Keystring ="8";
            }
            else if (keyCode.Key == Key.D9)
            {
                Keystring =  "9";
            }
            else if (keyCode.Key == Key.LeftShift)
            {
                Keystring = "";
            }
            else if (keyCode.Key == Key.Space)
            {
                Keystring = " ";
            }
            else if (keyCode.Key == Key.OemQuestion)
            {
                Keystring = "/";
            }
            else if (keyCode.Key == Key.OemPeriod)
            {
                Keystring = ">";
            }
            else 
            {
                Keystring = keyCode.Key.ToString();
            }

                         
            return Keystring;
 

        }

     

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void T1_KeyUp(object sender, KeyEventArgs e)
        {

           


        }
        string BoardingData = "";
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            e.Handled = true;
            string KeyTOStringValue = KeyTOString(e);
            if ("Return".Equals(KeyTOStringValue) == true)
            {

               
            }
            else
            {
                BoardingData = BoardingData+ KeyTOString(e);
                LB1.Content = LB1.Content + KeyTOString(e);
                T11.Text = T11.Text + KeyTOString(e); ;
            }
          
           
        }
           


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        InputScope input = new InputScope();
        InputScopeName name = new InputScopeName();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        
            this.InputScope = input;

            this.T11.Focus();
        }
    }
}
