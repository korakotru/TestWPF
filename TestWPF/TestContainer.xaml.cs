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
    /// Interaction logic for TestContainer.xaml
    /// </summary>
    public partial class TestContainer : Window
    {
        #region ============== Declaration ====================
        int columnA = 0;
        int numberOfObjects = 0;

        double FirstXPos, FirstYPos, FirstArrowXPos, FirstArrowYPos;
        object MovingObject;
        Line Path1, Path2, Path3, Path4;
        Rectangle FirstPosition, CurrentPosition;
        #endregion

        public TestContainer()
        {
            InitializeComponent();

            gridPanelA.ShowGridLines = true;
            gridPanelA.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridPanelA.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ScrollViewer sv = new ScrollViewer();
            sv.Content = gridPanelA;

        }

        private void RootWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
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

            int currentRow = 0, currentColumn = 0;
            int totalObjects = itemList.Count;
            foreach (Button item in itemList)
            {
                if (currentColumn >= columnA)
                {
                    ++currentRow;
                    currentColumn = 0;
                    if (totalObjects == 0)
                        break;
                }
                item.Margin = new Thickness(10);
                item.Content = "[" + currentRow.ToString() + "]" + "[" + currentColumn.ToString() + "]";
                Grid.SetRow(item, currentRow);
                Grid.SetColumn(item, currentColumn);
                gridPanelA.Children.Add(item);

                /*
                Binding drag and drop event handler to control 
                */
                //item.PreviewMouseLeftButtonDown += item_PreviewMouseLeftButtonDown;
                //item.PreviewMouseMove += item_PreviewMouseMove;
                //item.PreviewMouseLeftButtonUp += item_PreviewMouseLeftButtonUp;
                 
                // Update counter for next loop
                currentColumn++;
                --totalObjects;
            }
        }


        #region ============= Drag and Drop event handler =============
        void item_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        } 
        void item_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == MovingObject)
            {
                (sender as Control).SetValue(LeftProperty, e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20);
                (sender as Control).SetValue(TopProperty, e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20);
            }
        } 
        void item_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FirstXPos = e.GetPosition(sender as Control ).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            FirstArrowXPos = e.GetPosition((sender as Control).Parent as Control).X - FirstXPos - 20 ;
            FirstArrowYPos = e.GetPosition((sender as Control).Parent as Control).Y - FirstYPos - 20;
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

        }

       

    }
}
