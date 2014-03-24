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
    /// Interaction logic for DragDrop.xaml
    /// </summary>
    public partial class DragDropTest : Window
    {
        double FirstXPos, FirstYPos, FirstArrowXPos, FirstArrowYPos;
        object MovingObject; // temp object during drag
        Line Path1, Path2, Path3, Path4; // use for drawing the line from start position to destination position.
        Rectangle FirstPosition, CurrentPosition;

        public DragDropTest()
        {
            InitializeComponent();

            // Custom assign event to control are contain in the dragable canvas.
            foreach (Control control in DesignCanvas.Children)
            {
                control.PreviewMouseLeftButtonDown += control_PreviewMouseLeftButtonDown;
                control.PreviewMouseMove += control_PreviewMouseMove;
                control.PreviewMouseLeftButtonUp += control_PreviewMouseLeftButtonUp;
                control.Cursor = Cursors.Hand;
            }

            // Setting up the Lines that we want to show the path of movement.
            List<Double> Dots = new List<double>();
            Dots.Add(1);
            Dots.Add(2);
            Path1 = new Line();
            Path2 = new Line();
            Path3 = new Line();
            Path4 = new Line();

            Path1.Width = Path2.Width = Path3.Width = Path4.Width = DesignCanvas.Width;
            Path1.Height = Path2.Height = Path3.Height = Path4.Height = DesignCanvas.Height;
            Path1.Stroke = Brushes.Red;
            Path2.Stroke = Brushes.Green;
            Path3.Stroke = Brushes.Blue;
            Path4.Stroke = Brushes.DarkGray;

            Path1.StrokeDashArray = new DoubleCollection(Dots);
            Path2.StrokeDashArray = new DoubleCollection(Dots);
            Path3.StrokeDashArray = new DoubleCollection(Dots);
            Path4.StrokeDashArray = new DoubleCollection(Dots);

            FirstPosition = new Rectangle();
            CurrentPosition = new Rectangle();
            FirstPosition.Stroke = CurrentPosition.Stroke = Brushes.Brown;
            FirstPosition.StrokeDashArray = new DoubleCollection(Dots);
            CurrentPosition.StrokeDashArray = new DoubleCollection(Dots);
             
            // Adding Lines to main designing panel(canvas)
            DesignCanvas.Children.Add(Path1);
            DesignCanvas.Children.Add(Path2);
            DesignCanvas.Children.Add(Path3);
            DesignCanvas.Children.Add(Path4);
            DesignCanvas.Children.Add(FirstPosition);
            DesignCanvas.Children.Add(CurrentPosition);

        }

        void control_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // MouseButtonState.Pressed 
            // In this event, we get current mouse position on the control to use it in the MouseMove event
            FirstXPos = e.GetPosition(sender as Control).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            FirstArrowXPos = e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20;
            FirstArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20;
            MovingObject = sender;
        }

        void control_PreviewMouseMove(object sender, MouseEventArgs e)
        {  
            // Drag event !
            // In this event, at first we check the mouse left button state. If it is pressed and event sender object is similar with our moving object, we can move our control with some effects.
            if (e.LeftButton == MouseButtonState.Pressed && sender == MovingObject)
            {
                // We start to moving objects with setting the lines position
                Path1.X1 = FirstArrowXPos;
                Path1.Y1 = FirstArrowYPos;
                Path1.X2 = e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20;
                Path1.Y2 = e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20;

                Path2.X1 = Path1.X1 + (MovingObject as Control).ActualWidth;
                Path2.Y1 = Path1.Y1;
                Path2.X2 = Path1.X2 + (MovingObject as Control).ActualWidth;
                Path2.Y2 = Path1.Y2;

                Path3.X1 = Path1.X1;
                Path3.Y1 = Path1.Y1 + (MovingObject as Control).ActualHeight;
                Path3.X2 = Path1.X2;
                Path3.Y2 = Path1.Y2 + (MovingObject as Control).ActualHeight;

                Path4.X1 = Path1.X1 + (MovingObject as Control).ActualWidth;
                Path4.Y1 = Path1.Y1 + (MovingObject as Control).ActualHeight;
                Path4.X2 = Path1.X2 + (MovingObject as Control).ActualWidth;
                Path4.Y2 = Path1.Y2 + (MovingObject as Control).ActualHeight;

                FirstPosition.Width = (MovingObject as Control).ActualWidth;
                FirstPosition.Height = (MovingObject as Control).ActualHeight;
                FirstPosition.SetValue(Canvas.LeftProperty, FirstArrowXPos);
                FirstPosition.SetValue(Canvas.TopProperty, FirstArrowYPos);

                CurrentPosition.Width = (MovingObject as Control).ActualWidth;
                CurrentPosition.Height = (MovingObject as Control).ActualHeight;
                CurrentPosition.SetValue(Canvas.LeftProperty, Path1.X2 );
                CurrentPosition.SetValue(Canvas.TopProperty, Path1.Y2);

                /*
                 * For changing the position of a control, we should use the SetValue method to setting
                 * the Canvas.LeftProperty and Canvas.TopProperty dependencies.
                 * 
                 * For calculating the current position of the control, we should do:
                 *  Current position of the mouse curso on the object parent - position of the control's parent.
                 */
                // Moving the position of the object.
                (sender as Control).SetValue(Canvas.LeftProperty, e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20);
                (sender as Control).SetValue(Canvas.TopProperty, e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20);
            }
        }

        void control_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // MouseButtonState.Released 
            // In the event, we should set the lines visibility to hidden
            MovingObject = null;
            Path1.Visibility = System.Windows.Visibility.Hidden;
            Path2.Visibility = System.Windows.Visibility.Hidden;
            Path3.Visibility = System.Windows.Visibility.Hidden;
            Path4.Visibility = System.Windows.Visibility.Hidden;
            FirstPosition.Visibility = System.Windows.Visibility.Hidden;
            CurrentPosition.Visibility = System.Windows.Visibility.Hidden;
        }
         
    }
}
