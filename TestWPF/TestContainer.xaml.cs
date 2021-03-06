﻿using System;
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
using System.Drawing;
using System.Windows.Media.Effects;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for TestContainer.xaml
    /// </summary>
    public partial class TestContainer : Window
    {
        #region ============== Declaration ====================

        int columnA = 0;
        int numberOfObjects = 0;

        string tempStr = "";
        double FirstXPos, FirstYPos, FirstArrowXPos, FirstArrowYPos;
        object MovingObject;
        Line Path1, Path2, Path3, Path4;
        Rectangle FirstPosition, CurrentPosition;
        int previousRow;
        int previousCol; 

        double ctrlHeight, ctrlWidth; // use for set the moving control(temp object) size

        #endregion ============================================

        public TestContainer()
        {
            InitializeComponent();

            gridPanelA.ShowGridLines = true;
            gridPanelA.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridPanelA.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        }

        private void RootWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gridPanelB.AllowDrop = true;
            gridPanelB.DragEnter += gridPanelB_DragEnter;
        }

        void gridPanelB_DragEnter(object sender, DragEventArgs e)
        {
            (e.Source as Canvas).Background = Brushes.Yellow;
        } 

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            // Clean panel
            gridPanelA.Children.Clear();

            columnA = int.Parse(txtColumnSizeA.Text.Trim());
            numberOfObjects = int.Parse(txtNumberOfObjects.Text.Trim());

            // Create object and add to list
            List<Button> itemList = new List<Button>();
            for (int i = 0; i < numberOfObjects; i++)
            {
                itemList.Add(new Button() { Name = "item" + i });
            }

            // Set the layout 
            BuildGrid(numberOfObjects, columnA, gridPanelA);

            // Add the control into the GridPanel container
            int currentRow = 0, currentColumn = 0;
            int totalObjects = itemList.Count;
            Canvas canvasContainer;
            foreach (Button item in itemList)
            {
                if (currentColumn >= columnA)
                {
                    ++currentRow;
                    currentColumn = 0;
                    if (totalObjects == 0)
                        break;
                }

                item.Width = ctrlWidth;
                item.Height = ctrlHeight;
                item.Margin = new Thickness(10);
                item.Content = currentRow.ToString() + "," + currentColumn.ToString();
                DragDrop.DoDragDrop(item,item.Content,DragDropEffects.Move); 
                canvasContainer = new Canvas(); // ให้ control อยู่ใน canvas เพื่อให้สามารถ handler event mouse over ได้ เพื่อให้ สามารถ drop control ลงใน canvas ได้อย่างถูกต้อง
                canvasContainer.ToolTip = currentRow.ToString() + "c" + currentColumn.ToString(); // Set the name as index
                // Binding event to canvas container
                canvasContainer.AllowDrop = true; 
                canvasContainer.Children.Add(item);

                Grid.SetRow(canvasContainer, currentRow);
                Grid.SetColumn(canvasContainer, currentColumn);
                gridPanelA.Children.Add(canvasContainer);

                // Update counter for next loop
                currentColumn++;
                --totalObjects;
            }
            /*
               Binding drag and drop event handler to control 
               */
            foreach (Control ctrl in FindVisualChildren<Button>(gridPanelA))
            {
                BindDragDropEvent(ctrl);
            }
        } 
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        private void BindDragDropEvent(Control ctrl)
        {
            ctrl.PreviewMouseLeftButtonDown += item_PreviewMouseLeftButtonDown;
            ctrl.PreviewMouseMove += item_PreviewMouseMove;
            ctrl.PreviewMouseLeftButtonUp += item_PreviewMouseLeftButtonUp; 
        }
          
        #region ============= Drag and Drop event handler =============
        void item_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            // Get canvas is indexed  
            UIElementCollection controlCollection = gridPanelA.Children;
            foreach (Canvas canvasItem in controlCollection)
            {
                //
                // Do return dragging item to previous position container.
                //
                if (  String.Compare(canvasItem.ToolTip.ToString(), previousRow + "c" + previousCol) == 0)
                {
                    Control movingControl = sender as Control;
                    //canvasPanel.Children.Remove(movingControl);
                    (movingControl.Parent as Canvas).Children.Remove(movingControl);
                    canvasItem.Children.Add(movingControl);
                    movingControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    movingControl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    Canvas.SetLeft(movingControl,0);
                    Canvas.SetTop(movingControl, 0);  
                }

                //
                // Do Swap dragging item with dragover's item.
                //


                //
                // Do Move the dragging item to new location (new canvas container)
                //

                MovingObject = null; 
            } 
        }

        void item_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == MovingObject)
            {
                (sender as Control).SetValue(LeftProperty, e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 10); // -10 เพื่อชดเชยค่า margin ของ control ที่ set ไว้
                (sender as Control).SetValue(TopProperty, e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 10);

                lblMovingTxt.Content = (sender as Control).ToString();
                lblObjectPositionX.Content = e.GetPosition((sender as Control).Parent as Control).X;
                lblObjectPositionY.Content = e.GetPosition((sender as Control).Parent as Control).Y;
            }
        }

        void item_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FirstXPos = e.GetPosition(sender as Control).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            FirstArrowXPos = e.GetPosition((sender as Control).Parent as Control).X; //-FirstXPos - 20;
            FirstArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y;// -FirstYPos - 20;
            MovingObject = sender;

            //try
            //{
            string[] index = ((MovingObject as Control).Parent as Canvas).ToolTip.ToString().Split('c');
            lblPreviousRow.Content = previousRow = int.Parse(index[0]);
            lblPreviousCol.Content = previousCol = int.Parse(index[1]); 
            //}
            //catch (Exception)
            //{ 
            //}

            //*** remove object from grid (disconnect the control from container)
            ((MovingObject as Control).Parent as Canvas).Children.Remove(MovingObject as Control);

            //*** add control to canvas container
            canvasPanel.Children.Add(MovingObject as Control);
            (MovingObject as Control).PointFromScreen(Mouse.GetPosition(Application.Current.MainWindow));
        }
        #endregion

        private void BuildGrid(int numberOfObjects, int column, Grid grid)
        {
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            ColumnDefinition cd;
            RowDefinition rd;
            int totalRow = numberOfObjects / column;
            double width = grid.Width / column;
            // Create column
            for (int i = 0; i < column; i++)
            {
                cd = new ColumnDefinition();
                cd.Width = new GridLength(width);
                grid.ColumnDefinitions.Add(cd);
            }
            // Create row
            for (int i = 0; i < totalRow; i++)
            {
                rd = new RowDefinition();
                rd.Height = new GridLength((50 * width) / 100);
                grid.RowDefinitions.Add(rd);
            }

            ctrlWidth = width - 20;
            ctrlHeight = grid.RowDefinitions[0].Height.Value - 20; // 10 is margin
        } 
    }
}
