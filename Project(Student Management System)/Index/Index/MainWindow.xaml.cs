using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Threading;
using System.Windows.Threading;

namespace Index
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string user;
        public MainWindow()
        {
            InitializeComponent();
        }
        public static OleDbConnection connection()
        {
            OleDbConnection con;
               con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = data.mdb ");
            con.Open();
               return con;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
           log.Visibility = Visibility.Visible;
            register.Visibility = Visibility.Hidden;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval =TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }
        public void timer_tick(object sender, EventArgs e)
        {
            label4.Content = DateTime.Now.ToString("dd:MMM:yy ,  hh:mm:ss tt");
        }
        private void Click_NewUser(object sender, MouseButtonEventArgs e)
        {
            log.Visibility = Visibility.Hidden;
            register.Visibility = Visibility.Visible;
            label3.Content = "Register";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("Select * from admin where Username like '" +textBox_Copy3.Text+"' and Password like '"+textBox_Copy4.Text+"'",connection());
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                user = "Administrator";
                Main_page page = new Main_page();
                page.Show();
            }
           
            else
            {
                OleDbCommand cmd1 = new OleDbCommand("Select * from users where Username like '" + textBox_Copy3.Text + "' and Password like '" + textBox_Copy4.Text + "'", connection());
                OleDbDataReader read2 = cmd1.ExecuteReader();
                if (read2.Read())
                {
                    user = textBox_Copy3.Text;
                    Main_page page = new Main_page();
                    page.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or Password");
                }
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("Select Username from admin where Username like '" + textBox.Text + "'", connection());
            OleDbDataReader read3 = cmd.ExecuteReader();
            if (read3.Read())
            {
                MessageBox.Show("You Can't take this Username");
            }
            else
            {
                if (textBox_Copy.Password.ToString() == textBox_Copy1.Password.ToString())
                {
                    try
                    {
                        OleDbCommand cmd1 = new OleDbCommand("Insert into users values ('" + textBox.Text + "','" + textBox_Copy.Password.ToString() + "','" + textBox_Copy2.Text + "')", connection());
                        cmd1.ExecuteNonQuery();
                        register.Visibility = Visibility.Hidden;
                        log.Visibility = Visibility.Visible;
                    }
                    catch (Exception f)
                    {
                       
                    }
                }
                else
                {
                    MessageBox.Show("Password Mismatch");
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            register.Visibility = Visibility.Hidden;
            log.Visibility = Visibility.Visible;
        }
    }
}
