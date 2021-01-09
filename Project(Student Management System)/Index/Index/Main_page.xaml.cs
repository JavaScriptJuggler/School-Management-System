using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.OleDb;
using System.Data;
using System.Windows.Threading;

namespace Index
{
    /// <summary>
    /// Interaction logic for Main_page.xaml
    /// </summary>
    public partial class Main_page : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();
       
        public static DispatcherTimer timer1 = new DispatcherTimer();
        public Main_page()
        {
            InitializeComponent();
        }
        public void combobox_Control()
        {
            ComboBox[] combo = new ComboBox[] { comboBox_Copy2, comboBox_Copy, comboBox_Copy1 };
            foreach (ComboBox com in combo)
            {
                com.Items.Add("Select");
                com.Items.Add("Yes");
                com.Items.Add("No");
                com.SelectedIndex = 0;
            }
            comboBox.Items.Add("Select");
            comboBox.Items.Add("CCA");
            comboBox.Items.Add("CCPA");
            comboBox.Items.Add("CACPA");
            comboBox.Items.Add("CSE");
            comboBox.SelectedIndex = 0;
        }
        string roll_no = "";
        int total_fees = 0;
        public void insert_data()
        {
            ComboBox[] combo = new ComboBox[] { comboBox_Copy2, comboBox_Copy, comboBox_Copy1 };
            if (comboBox_Copy.SelectedIndex != 0 && comboBox_Copy1.SelectedIndex != 0 && comboBox_Copy2.SelectedIndex != 0 && textBox.Text != "" && textBox_Copy.Text != "" && textBox_Copy1.Text != "" && textBox_Copy2.Text != "" && textBox_Copy3.Text != "" && textBox_Copy5.Text != "")
            {
                string name = textBox.Text;
                string batch = comboBox.SelectedItem.ToString();
                string rollno = textBox_Copy.Text;
                string add = textBox_Copy1.Text;
                string library = comboBox_Copy.SelectedItem.ToString();
                string bus = comboBox_Copy1.SelectedItem.ToString();
                int libfees = Convert.ToInt32(textBox_Copy2.Text);
                int trnsfees = Convert.ToInt32(textBox_Copy3.Text);
                string scholar = comboBox_Copy2.Text;
                int total = Convert.ToInt32(textBox_Copy5.Text);
                OleDbCommand cmd = new OleDbCommand("insert into std_record values('" + name + "','" + batch + "','" + rollno + "','" + add + "','" + library + "','" + bus + "','" + libfees + "','" + trnsfees + "','" + scholar + "','" + total + "')", MainWindow.connection());
                cmd.ExecuteNonQuery();
                if (comboBox_Copy.SelectedIndex == 1)
                {
                    string card = "L-" + rollno;
                    OleDbCommand cmd1 = new OleDbCommand("insert into library_department (Student_Name,Roll_no,Batch,Card_no) values('" + name + "','" + rollno + "','" + batch + "','" + card + "')", MainWindow.connection());
                    cmd1.ExecuteNonQuery();
                    OleDbCommand cmd2 = new OleDbCommand("insert into fees (Student_Name,Roll_no,Batch,Total) values('" + name + "','" + rollno + "','" + batch + "','" + total + "')", MainWindow.connection());
                    cmd2.ExecuteNonQuery();
                }
                if (comboBox_Copy1.SelectedIndex == 1)
                {
                    string trans_card = "Bus-" + rollno;
                    OleDbCommand cmd1 = new OleDbCommand("insert into Transportation_department (Student_Name,Roll_no,Batch,Bus_Card_No) values('" + name + "','" + rollno + "','" + batch + "','" + trans_card + "')", MainWindow.connection());
                    cmd1.ExecuteNonQuery();
                }
                timer.Start();
                MessageBox.Show("Record Entired");
            }
            else { MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            record.Visibility = Visibility.Hidden;
            label2_CopyP.Visibility = Visibility.Hidden;
            label2_Copy1P.Visibility = Visibility.Hidden;
            label2_Copy2P.Visibility = Visibility.Hidden;
            label1.Content = MainWindow.user;
            Label[] lbl = new Label[] { label2, label2_Copy1, label2, label2_Copy };

            combobox_Control();
            //insert.Visibility = Visibility.Visible;
            //Canvas[] canva = new Canvas[] { Delete};
            //foreach (Canvas can in canva)
            //{
            //    can.Visibility = Visibility.Hidden;
            //}
            OleDbCommand cmd = new OleDbCommand("select Username from users where Username like '" + label1.Content + "'", MainWindow.connection());
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                Label[] side_menu = new Label[] {  label5_Copy };
                foreach (Label lb in side_menu)
                {
                    lb.IsEnabled = false;
                    lb.Foreground = new SolidColorBrush(Color.FromRgb(126, 126, 126));
                }
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            delete();

            OleDbCommand cmd1 = new OleDbCommand("select * from Transportation_department", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adpt.Fill(ds1);
            dataGrid2.ItemsSource = ds1.Tables[0].DefaultView;


            OleDbCommand cmd2 = new OleDbCommand("select * from library_department", MainWindow.connection());
            OleDbDataAdapter adpt1 = new OleDbDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            adpt.Fill(ds2);
            dataGrid1.ItemsSource = ds2.Tables[0].DefaultView;
            timer.Stop();
        }
        private void clear()
        {
            TextBox[] text = new TextBox[] { textBox, textBox_Copy, textBox_Copy1, textBox_Copy2, textBox_Copy3, textBox_Copy5 };
            foreach (TextBox t in text)
            {
                t.Clear();
            }
            ComboBox[] combo = new ComboBox[] { comboBox, comboBox_Copy, comboBox_Copy1, comboBox_Copy2 };
            foreach (ComboBox cb in combo)
            {
                cb.SelectedIndex = 0;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            insert_data();
            clear();

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] text = new TextBox[] { textBox, textBox_Copy, textBox_Copy1, textBox_Copy2, textBox_Copy3, textBox_Copy5 };
            ComboBox[] combo = new ComboBox[] { comboBox, comboBox_Copy2, comboBox_Copy, comboBox_Copy1 };
            foreach (TextBox txt in text)
            {
                txt.Text = "";
            }
            foreach (ComboBox com in combo)
            {
                com.SelectedIndex = 0;
            }
        }

        private void click(object sender, MouseButtonEventArgs e)
        {
            label5.Background = new SolidColorBrush(Colors.Green);
            label5.Foreground = new SolidColorBrush(Colors.Yellow);
            record.Visibility = Visibility.Hidden;
            Label[] label_list = new Label[] { label5_Copy };
            foreach (Label lb in label_list)
            {
                lb.Background = new SolidColorBrush(Colors.WhiteSmoke);
                lb.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
        private void click1(object sender, MouseButtonEventArgs e)
        {

            label5_Copy.Background = new SolidColorBrush(Colors.Green);
            label5_Copy.Foreground = new SolidColorBrush(Colors.Yellow);
            record.Visibility = Visibility.Visible;
            delete();
            label3.Content = "Review / Delete Record";
            label4.Content = "See the details of student & Can also delete unnessary recods";
            Label[] label_list = new Label[] { label5 };
            foreach (Label lb in label_list)
            {
                lb.Background = new SolidColorBrush(Colors.WhiteSmoke);
                lb.Foreground = new SolidColorBrush(Colors.Green);
            }
        }


        public void delete()
        {
            //OleDbCommand cmd = new OleDbCommand("Select * from std_record", MainWindow.connection());
            OleDbDataAdapter data = new OleDbDataAdapter("Select * from std_record", MainWindow.connection());
            DataSet ds = new DataSet();
            data.Fill(ds);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;


        }

        private void change(object sender, TextChangedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from std_record where [Roll_no] like '%" + textBox1.Text + "%' OR [Student_name] like'%" + textBox1.Text + "%' OR [Batch] like '%" + textBox1 + "%'", MainWindow.connection());
            OleDbDataAdapter data = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            data.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGrid.ItemsSource = dt.DefaultView;
        }
        string delete_key;
        private void Selection_Change(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView selected_row = gd.SelectedItem as DataRowView;
            if (selected_row != null)
            {
                button1.IsEnabled = true;
                delete_key = selected_row["Roll_no"].ToString();
            }
            else { button1.IsEnabled = false; delete_key = ""; }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Sure to delete Record ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (rs == MessageBoxResult.OK)
            {
                OleDbCommand cmd = new OleDbCommand("delete from std_record where Roll_no like '" + delete_key + "'", MainWindow.connection());
                cmd.ExecuteNonQuery();
                OleDbCommand cmd1 = new OleDbCommand("delete from fees where Roll_no like '" + delete_key + "'", MainWindow.connection());
                cmd1.ExecuteNonQuery();
                OleDbCommand cmd3 = new OleDbCommand("delete from library_department where Roll_no like '" + delete_key + "'", MainWindow.connection());
                cmd3.ExecuteNonQuery();
                OleDbCommand cmd4 = new OleDbCommand("delete from Transportation_department where Roll_no like '" + delete_key + "'", MainWindow.connection());
                cmd4.ExecuteNonQuery();
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void label2_MouseEnter(object sender, MouseEventArgs e)
        {
            Label[] lb_array = new Label[] { label2, label2_Copy, label2_Copy1, label2_Copy2 };
            Label lb = (Label)sender;
            foreach (Label lbl in lb_array)
            {
                if (lbl.Content == lb.Content)
                {
                    lbl.Background = new SolidColorBrush(Color.FromRgb(84, 161, 84));
                    lbl.Foreground = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    if (lbl.IsEnabled != false)
                    {
                        lbl.Background = new SolidColorBrush(Colors.Transparent);
                        lbl.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }
            }
        }

        private void label2_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd1 = (DataGrid)sender;
            DataRowView dr = gd1.SelectedItem as DataRowView;
            if (dr != null)
            {
                label8.Content = "Student Name: " + dr["Student_Name"].ToString();
                textBox2.Text = dr["Roll_no"].ToString();
                textBox2_Copy.Text = dr["Batch"].ToString();
                textBox2_Copy1.Text = dr["Return_Date"].ToString();
                textBox2_Copy2.Text = dr["Book1"].ToString();
                //label8_Copy4.Content = "Card Number: " + dr["Card_no"].ToString();
                textBox2_Copy3.Text = dr["Card_no"].ToString();
                textBox2_Copy5.Text = dr["Take_Date"].ToString();
                textBox2_Copy4.Text = "";
                label8_Copy9.Content = dr["Status"].ToString();
                if (label8_Copy9.Content.ToString() != "Recived")
                    label8_Copy9.Foreground = new SolidColorBrush(Colors.Red);
                else
                    label8_Copy9.Foreground = new SolidColorBrush(Colors.Green);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string res = "Deactive";
            string no_res = "Active";
            string old_book = "", new_book;
            OleDbCommand cmd1 = new OleDbCommand("select * from Book_list where bookName ='" + textBox2_Copy4.Text + "'", MainWindow.connection());
            OleDbDataReader read = cmd1.ExecuteReader();
            if (read.Read())
            {
                if (read.GetValue(1).ToString() == res)
                {
                    MessageBox.Show("Book is not available !!!");
                }
                else
                {
                    read.Close();
                    OleDbCommand cmd2 = new OleDbCommand("select * from library_department where Card_no='" + textBox2_Copy3.Text + "'", MainWindow.connection());
                    OleDbDataReader read1 = cmd2.ExecuteReader();
                    if (read1.Read())
                    {
                        string stat1 = "Pending";
                        old_book = read1.GetValue(4).ToString();
                        OleDbCommand cmd3 = new OleDbCommand("update Book_list set stat ='" + no_res + "' where bookName='" + old_book + "'", MainWindow.connection());
                        cmd3.ExecuteNonQuery();
                        OleDbCommand cmd = new OleDbCommand("Update library_department set Book1='" + textBox2_Copy4.Text + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd.ExecuteNonQuery();
                        OleDbCommand cmd4 = new OleDbCommand("update Book_list set stat ='" + res + "' where bookName='" + textBox2_Copy4.Text + "'", MainWindow.connection());
                        cmd4.ExecuteNonQuery();
                        OleDbCommand cmd8 = new OleDbCommand("Update library_department set Status='" + stat1 + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd8.ExecuteNonQuery();
                        label9.Content = "Renewed Sucessful !";
                        string time = DateTime.Now.ToString("dd MMM yyyy");
                        string re = "";
                        OleDbCommand cmd9 = new OleDbCommand("Update library_department set Take_Date='" + time + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd9.ExecuteNonQuery();
                        OleDbCommand cmd10 = new OleDbCommand("Update library_department set Return_Date='" + re + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd10.ExecuteNonQuery();
                    }

                }
            }
            else
            {
                if (textBox2_Copy4.Text == "")
                {
                    OleDbCommand cmd2 = new OleDbCommand("select * from library_department where Card_no='" + textBox2_Copy3.Text + "'", MainWindow.connection());
                    OleDbDataReader read1 = cmd2.ExecuteReader();
                    if (read1.Read())
                    {
                        string stat = "Recived";
                        old_book = read1.GetValue(4).ToString();
                        OleDbCommand cmd5 = new OleDbCommand("update Book_list set stat ='" + no_res + "' where bookName='" + old_book + "'", MainWindow.connection());
                        cmd5.ExecuteNonQuery();
                        OleDbCommand cmd6 = new OleDbCommand("Update library_department set Book1='" + textBox2_Copy4.Text + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd6.ExecuteNonQuery();
                        OleDbCommand cmd7 = new OleDbCommand("Update library_department set Status='" + stat + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd7.ExecuteNonQuery();
                        string time = DateTime.Now.ToString("dd MMM yyyy");
                        string take = "";
                        OleDbCommand cmd9 = new OleDbCommand("Update library_department set Return_Date='" + time + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd9.ExecuteNonQuery();
                        OleDbCommand cmd10 = new OleDbCommand("Update library_department set Take_Date='" + take + "' where Card_no like '" + textBox2_Copy3.Text + "'", MainWindow.connection());
                        cmd10.ExecuteNonQuery();
                    }
                }

            }
            TextBox[] text = new TextBox[] { textBox2, textBox2_Copy, textBox2_Copy1, textBox2_Copy2, textBox2_Copy3, textBox2_Copy3, textBox2_Copy4 };
            foreach (TextBox tx in text)
            {
                tx.Clear();
            }
            timer3.Interval = TimeSpan.FromMilliseconds(1000);
            timer3.Tick += Timer3_Tick;
            timer3.Start();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from library_department", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid1.ItemsSource = ds.Tables[0].DefaultView;
            timer3.Stop();
        }

        private void library_Loaded(object sender, RoutedEventArgs e)
        {
            //textBox2_Copy4.AutoWordSelection
        }

        private void change1(object sender, TextChangedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from library_department where [Roll_no] like '%" + textBox1_Copy.Text + "%' or [Card_no] like'%" + textBox1_Copy.Text + "%'", MainWindow.connection());
            OleDbDataAdapter data = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            data.Fill(ds);
            DataTable dt = ds.Tables[0];
            dataGrid1.ItemsSource = dt.DefaultView;
        }

        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string rand_roll = "";
            int[] roll_array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                rand_roll = rand_roll + roll_array[rand.Next(roll_array.Length)];
            }
            if (comboBox.SelectedIndex != 0)
            {
                roll_no = comboBox.SelectedItem.ToString() + rand_roll;
                textBox_Copy.Text = roll_no;
                string batch = comboBox.SelectedItem.ToString();
                if (batch == "CCA")
                    total_fees += 6000;
                else if (batch == "CCPA")
                    total_fees += 8000;
                else if (batch == "CACPA")
                    total_fees += 13500;
                else if (batch == "CSE")
                    total_fees += 21000;

                textBox_Copy5.Text = Convert.ToString(total_fees);
            }
            else
            {
                textBox_Copy.Text = "";
                textBox_Copy5.Text = "";
            }
        }

        bool changed1 = false;
        bool changed = false;
        bool changer = false;
        private void comboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (comboBox_Copy.SelectedIndex == 1)
            {
                changed = true;
                total_fees = total_fees + 100;
                textBox_Copy2.Text = "100";
                textBox_Copy5.Text = Convert.ToString(total_fees);
            }
            else
            {
                if (changed == true)
                {
                    changed = false;
                    total_fees = total_fees - 100;
                    textBox_Copy2.Text = Convert.ToString(0);
                    textBox_Copy5.Text = Convert.ToString(total_fees);
                }
                else
                {
                    textBox_Copy2.Text = Convert.ToString(0);

                }
            }

        }

        private void comboBox_Copy1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (comboBox_Copy1.SelectedIndex == 1)
            {
                changed1 = true;
                total_fees = total_fees + 500;
                textBox_Copy3.Text = "500";
                textBox_Copy5.Text = Convert.ToString(total_fees);
            }
            else if (comboBox_Copy1.SelectedIndex == 2)
            {
                if (changed1 == true)
                {
                    changed1 = false;
                    total_fees = total_fees - 500;
                    textBox_Copy3.Text = Convert.ToString(0);
                    textBox_Copy5.Text = Convert.ToString(total_fees);
                }
                else
                {
                    textBox_Copy3.Text = Convert.ToString(0);
                }
            }
        }

        private void comboBox_Copy2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (comboBox_Copy2.SelectedIndex == 1)
            {
                changer = true;
                total_fees -= 1200;
                textBox_Copy5.Text = Convert.ToString(total_fees);
            }
            else
            {
                if (changer == true)
                {
                    total_fees += 1200;
                    textBox_Copy5.Text = Convert.ToString(total_fees);
                }
            }
        }

        private void textBox_Copy3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Book_List bl = new Book_List();
            bl.Show();
        }

        private void library_Loaded_1(object sender, RoutedEventArgs e)
        {
            timer1.Interval = TimeSpan.FromMilliseconds(500);
            timer1.Tick += Timer1_Tick;

            OleDbCommand cmd = new OleDbCommand("select * from library_department", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid1.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            textBox2_Copy4.Text = Book_List.bookname;
            timer1.Stop();
        }

        private void label2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid[] lb = new Grid[] { label2P, label2_CopyP, label2_Copy1P, label2_Copy2P,/* label2_Copy3P, label2_Copy31P*/ };
            Label leb = (Label)sender;
            string grid_name = leb.Name + "P";
            foreach (Grid gd in lb)
            {
                if (gd.Name == grid_name)
                {
                    gd.Visibility = Visibility.Visible;
                }
                else
                {
                    gd.Visibility = Visibility.Hidden;
                }
            }
        }
        public void installment_fun(int amount, string field, string roll, int total_now)
        {
            OleDbCommand cmd = new OleDbCommand("update fees set '" + field + "'='" + amount + "' where Roll_no='" + roll + "'", MainWindow.connection());
            cmd.ExecuteNonQuery();
            OleDbCommand cmd1 = new OleDbCommand("update fees set Total='" + total_now + "' where Roll_no='" + roll + "'", MainWindow.connection());
            cmd1.ExecuteNonQuery();
        }
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from fees where Roll_no='" + textBox3.Text + "'", MainWindow.connection());
            OleDbDataReader read = cmd.ExecuteReader();
            TextBox[] tx = new TextBox[] { textBox4, textBox4_Copy, textBox4_Copy1, textBox4_Copy2 };
            if (read.Read())
            {
                textBox4.Text = read.GetValue(0).ToString();
                textBox4_Copy.Text = read.GetValue(1).ToString();
                textBox4_Copy1.Text = read.GetValue(2).ToString();
                textBox4_Copy2.Text = read.GetValue(3).ToString();
                int fee = ((25 * Convert.ToInt32(textBox4_Copy2.Text) / 100));
                if (read.GetValue(4).ToString() != "0" && read.GetValue(5).ToString() != "0" && read.GetValue(6).ToString() != "0" && read.GetValue(7).ToString() != "0")
                {
                    textBox4_Copy3.Text = "Paid";
                    button5.IsEnabled = false;
                    CheckBox[] ch = new CheckBox[] { c4, c5, c6, c7 };
                    foreach (CheckBox c in ch)
                    {
                        c.IsEnabled = false;
                        c.IsChecked = true;
                    }
                }
                else
                {
                    textBox4_Copy3.Text = Convert.ToString(fee);
                    button5.IsEnabled = true;
                    for (int i = 4; i < 8; i++)
                    {
                        if (read.GetValue(i).ToString() == "0")
                        {
                            
                        }
                        else
                        {
                            if (i == 4)
                            {
                                c4.IsEnabled = false;
                                c4.IsChecked = true;
                            }
                            else if (i == 5)
                            {
                                c5.IsEnabled = false;
                                c5.IsChecked = true;
                            }
                            else if (i == 6)
                            {
                                c6.IsEnabled = false;
                                c6.IsChecked = true;
                            }
                            else if (i == 7)
                            {
                                c7.IsEnabled = false;
                                c7.IsChecked = true;
                            }
                        }
                    }
                }

            }

            else
            {
                MessageBox.Show("Invalid Roll Number...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from fees where Roll_no='" + textBox3.Text + "'", MainWindow.connection());
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                int fee = ((25 * Convert.ToInt32(textBox4_Copy2.Text) / 100));
                if (read.GetValue(4).ToString() != "0" && read.GetValue(5).ToString() != "0" && read.GetValue(6).ToString() != "0" && read.GetValue(7).ToString() != "0")
                {
                   // textBox4_Copy3.Text = "Paid";
                }
                else
                {
                    textBox4_Copy3.Text = Convert.ToString(fee);
                    for (int i = 4; i < 8; i++)
                    {
                        if (read.GetValue(i).ToString() == "0")
                        {
                            string field = i + "f";
                            OleDbCommand cmd1 = new OleDbCommand("update fees set " + field + "='" + fee + "' where Roll_no='" + textBox4_Copy.Text + "'", MainWindow.connection());
                            cmd1.ExecuteNonQuery();
                            break;
                        }
                    }
                }

            }
            textBox4.Text = "";
            textBox4_Copy.Text = "";
            textBox4_Copy1.Text = "";
            textBox4_Copy2.Text = "";
            textBox4_Copy3.Text = "";
            textBox3.Text = "";
        }

        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from Transportation_department where Bus_Card_No like '%"+textBox6.Text+"%'", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid2.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void label2_Copy2P_Loaded(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from Transportation_department", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid2.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd1 = (DataGrid)sender;
            DataRowView dr = gd1.SelectedItem as DataRowView;
            if (dr != null)
            {
                textBox5.Text = dr["Student_Name"].ToString();
                textBox5_Copy.Text = dr["Roll_no"].ToString();
                textBox5_Copy1.Text = dr["Bus_Card_No"].ToString();
                textBox5_Copy2.Text = Convert.ToString(500);
                if (dr["fine"].ToString() == "Yes")
                {
                    checkBox.IsChecked = true;
                    checkBox.IsEnabled = false;
                }
                if (dr["fine"].ToString() == "No" || dr["fine"].ToString() == "")
                {
                    checkBox.IsChecked = false;
                    checkBox.IsEnabled = true;
                }

                if (dr["Fees"].ToString() != "" && dr["Fees"].ToString() != "0")
                {
                    MessageBox.Show("Fees Paid","Info",MessageBoxButton.OK,MessageBoxImage.Information);
                    button6.IsEnabled = false;
                }
                if (dr["Fees"].ToString() == "" || dr["Fees"].ToString() == "0")
                {
                    button6.IsEnabled = true;
                }

            }
        }
        string decisition = "";
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from Transportation_department where Bus_Card_No='" + textBox5_Copy1.Text + "'", MainWindow.connection());
            OleDbDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                if (read.GetValue(4).ToString() != "0" && read.GetValue(4).ToString() != "")
                {

                }
                else
                {
                    OleDbCommand cmd1 = new OleDbCommand("update Transportation_department set Fees='" + textBox5_Copy2.Text + "' where Bus_Card_No='" + textBox5_Copy1.Text + "'", MainWindow.connection());
                    cmd1.ExecuteNonQuery();
                    OleDbCommand cmd2 = new OleDbCommand("update Transportation_department set Fine='" + decisition + "' where Bus_Card_No='" + textBox5_Copy1.Text + "'", MainWindow.connection());
                    cmd2.ExecuteNonQuery();
                }
                timer2.Interval = TimeSpan.FromMilliseconds(1000);
                timer2.Tick += Timer2_Tick;
                timer2.Start();
            }
            else
            {
                MessageBox.Show("Card Number Not Found !!!");
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select * from Transportation_department", MainWindow.connection());
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGrid2.ItemsSource = ds.Tables[0].DefaultView;
            timer2.Stop();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            decisition = "Yes";
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            decisition = "No";
        }
       
    }
}
