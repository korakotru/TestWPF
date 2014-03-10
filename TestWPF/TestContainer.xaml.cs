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
   
        public TestContainer()
        {
            InitializeComponent();

            gridPanelA.ShowGridLines = true;
            gridPanelA.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridPanelA.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ScrollViewer sv = new ScrollViewer();
            sv.Content = gridPanelA;

        }
        int columnA = 0;
        int numberOfObjects = 0;
        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            columnA = int.Parse(txtColumnSizeA.Text.Trim());
            numberOfObjects = int.Parse(txtNumberOfObjects.Text.Trim());

            List<Button> itemList = new List<Button>();
            for (int i = 0; i < numberOfObjects; i++)
            {
                itemList.Add(new Button() { Name="item"+i });
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
                    if (totalObjects == 0 )
                        break; 
                }
                item.Margin = new Thickness(10);
                item.Content = "[" + currentRow.ToString() + "]" + "[" + currentColumn.ToString() + "]"; 
                Grid.SetRow(item, currentRow);
                Grid.SetColumn(item, currentColumn);
                gridPanelA.Children.Add(item);

                currentColumn++; 
                --totalObjects;
            }
             
        }

        private void BuildGrid(int numberOfObjects, int column, Grid grid)
        {
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            ColumnDefinition cd;
            RowDefinition rd;
            int totalRow =  numberOfObjects/column  ;
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
                rd.Height = new GridLength( ( 50*width)/100  );
                grid.RowDefinitions.Add(rd);
            }

        }
      
    }
}
