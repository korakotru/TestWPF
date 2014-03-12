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

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for TestForm.xaml
    /// </summary>
    public partial class TestForm : Window
    {
        double FirstXPos, FirstYPos, FirstArrowXPos, FirstArrowYPos;
        object MovingObject;
        Line Path1, Path2, Path3, Path4;
        Rectangle FirstPosition, CurrentPosition;

        public TestForm()
        {
            InitializeComponent();

            // Binding Event
            item.PreviewMouseLeftButtonDown += item_PreviewMouseLeftButtonDown;
            item.PreviewMouseMove += item_PreviewMouseMove;
            item.PreviewMouseLeftButtonUp += item_PreviewMouseLeftButtonUp;
        }
        #region ============= Drag and Drop event handler =============
        void item_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        void item_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == MovingObject)
            {
                (sender as Control).SetValue(Canvas.LeftProperty, e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20);
                (sender as Control).SetValue(Canvas.TopProperty, e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20);
            }
        }
        void item_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FirstXPos = e.GetPosition(sender as Control).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            FirstArrowXPos = e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20;
            FirstArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20;
        }
        #endregion
    }
}
