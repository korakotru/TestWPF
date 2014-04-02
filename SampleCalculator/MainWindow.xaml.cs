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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
        }
         
        string _opt;
        int? _firstNum = null , _secNum = null ;
        private void OnKeyPressed(object sender, RoutedEventArgs e)
        {
            int digit;
            string content = ((Button)e.Source).Content.ToString();
            txtResult.Text = txtResult.Text +" "+ content;
            if (int.TryParse(content,out digit))
            {
                if (_firstNum == null)
                    _firstNum = digit;
                else
                    _secNum = digit;
            }
            else
            {
                if (content == "=")
                {
                    // Calculate
                    Calculate();
                    _firstNum = _secNum = null;
                    _opt = string.Empty;
                }
                else
                {
                    _opt = content; // set operation
                }
            }
        }

        private void Calculate()
        {
            string result = "" ;
            if (_firstNum != null  && _secNum != null  )
            {
                switch (_opt)
                {
                    case "+": result = (_firstNum + _secNum).ToString();
                        break;
                    case "-": result = (_firstNum - _secNum).ToString();
                        break;
                    case "*": result = (_firstNum * _secNum).ToString();
                        break;
                    case "/": result = (_firstNum /_secNum).ToString();
                        break;
                    default: result = string.Empty;
                        break;
                }
            }
            else if (_firstNum == null && _secNum == null)
            {
                result = string.Empty;
            }
            else
            {
                result = _firstNum == null ? _secNum.ToString() : _firstNum.ToString();
            }
            txtResult.Text = result;
        }
    }
}
