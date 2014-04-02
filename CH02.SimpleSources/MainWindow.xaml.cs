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

namespace CH02.SimpleSources
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Resources.Add("brush2", new SolidColorBrush(
 Color.FromRgb(200, 10, 150)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Modify the brush resource
            var brush = (LinearGradientBrush)this.Resources["brush1"];
            brush.GradientStops.Add(new GradientStop(Colors.Blue, .5));

            rect2.Fill = (SolidColorBrush)this.Resources["brush2"];
        }


    }
}
