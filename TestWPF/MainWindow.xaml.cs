using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable dtHistory { get; set; }

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            InitDataGrid();
        }
         
        private void InitDataGrid()
        {
            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Number 1";
            textColumn.Binding = new Binding("N1");
            gridHistory.Columns.Add(textColumn);

            textColumn = new DataGridTextColumn();
            textColumn.Header = "Number 2";
            textColumn.Binding = new Binding("N2");
            gridHistory.Columns.Add(textColumn);

            textColumn = new DataGridTextColumn();
            textColumn.Header = "Result";
            textColumn.Binding = new Binding("Result");
            gridHistory.Columns.Add(textColumn);
        }

        private void Plus()
        {
            decimal  result = 0;
            if (txtNum1.Text.Trim() != string.Empty && txtNum2.Text.Trim() != string.Empty)
            {
                try
                {
                    result = decimal.Parse(txtNum1.Text) + decimal.Parse(txtNum2.Text);
                    txtResult.Text = result.ToString();

                    // Save the result table.
                    SaveHistory(decimal.Parse(txtNum1.Text), decimal.Parse(txtNum2.Text), decimal.Parse(txtResult.Text));
                }
                catch (Exception)
                {
                    txtResult.Text = "Invalid Number";
                    ClearInputBox();
                }
            }
        }

        private void ClearInputBox()
        {
            txtNum1.Clear();
            txtNum2.Clear();
        }

        private void SaveHistory(decimal n1, decimal n2 , decimal result)
        {
            if (this.dtHistory == null )
            {
                dtHistory = new DataTable();
                CreateHistoryTable( dtHistory);
            }
            dtHistory.Rows.Add(n1,n2,result);

            BindHisotryGrid(this.dtHistory);
        }

        private void BindHisotryGrid(DataTable dataTable)
        {
            gridHistory.ItemsSource = dataTable.DefaultView;
        }

        private void CreateHistoryTable(DataTable dt)
        {
            dtHistory.Columns.Add("N1",typeof(decimal));
            dtHistory.Columns.Add("N2", typeof(decimal));
            dtHistory.Columns.Add("Result", typeof(decimal));
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        } 

        private void btnCal_Click(object sender, RoutedEventArgs e)
        {
            Plus();
        }

        private void txtNum1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Plus();
            }
        }

        private void txtNum2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Plus();
            }
        }
         
    }
}
