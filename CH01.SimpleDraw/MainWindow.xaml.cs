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

namespace CH01.SimpleDraw
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

        Point _pos;
        bool _isDrawing;
        Brush _stroke = Brushes.Black;

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // To handling the source of event (Drawing on canvas or pick the color)
            var rect = e.Source as Rectangle;
            if (rect != null)
            {
                // Color picker mode
                _stroke = rect.Fill;
            }
            else
            {
                // Drawing mode
                _isDrawing = true;
                _pos = e.GetPosition(_root);
                _root.CaptureMouse(); // Force capture of the mouse to this element.
            }
         
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                Line line = new Line();
                line.X1 = _pos.X;
                line.Y1 = _pos.Y;
                // Continue to drawing line
                _pos = e.GetPosition(_root);
                line.X2 = _pos.X;
                line.Y2 = _pos.Y;
                line.Stroke = _stroke;
                line.StrokeThickness = 1;
                _root.Children.Add(line); // add the line drawing to canvas container
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            // When the mouse button is released , just revert things to normal.
            _isDrawing = false;
            _root.ReleaseMouseCapture(); // releases the mouse capture , if this element held the capture.
        }
         
    }
}
