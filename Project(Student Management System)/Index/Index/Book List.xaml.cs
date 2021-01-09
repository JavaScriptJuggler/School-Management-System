using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Index
{
    /// <summary>
    /// Interaction logic for Book_List.xaml
    /// </summary>
    public partial class Book_List : Window
    {
        public static string bookname="";
        public Book_List()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from Book_list where bookName like '%" + textBox.Text + "%'", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }
        public void datagrid_view()
        {
            OleDbCommand cmd = new OleDbCommand("select * from Book_list", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid_view();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView gr = gd.SelectedItem as DataRowView;
            if (gr != null)
            {
                bookname = gr["bookName"].ToString();
                Main_page.timer1.Start();
                this.Close();
            }
        }
    }
}
