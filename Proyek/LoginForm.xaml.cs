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
using Oracle.DataAccess.Client;

namespace Proyek
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public static string nmuser;
        public static string current_idus;
        OracleConnection conn;
        Boolean login = false;
        List<user> us = new List<user>();

        private void adddata()
        {
            OracleCommand cmd = new OracleCommand($"select * from CUSTOMER", conn);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                us.Add(new user(Convert.ToInt32(reader[0]),reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),reader.GetString(5),Convert.ToInt32(reader[6])));
            }
            conn.Close();
        }

        public LoginForm()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            adddata();
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbusername.Text.ToUpper() == "ADMIN" && tbpassword.Text.ToUpper() == "ADMIN")
            {
                MessageBox.Show("Berhasil login admin");
            }
            else
            {
                for (int i = 0; i < us.Count; i++)
                {
                    if(tbusername.Text.ToUpper() == us[i].Username.ToUpper())
                    {
                        if (tbpassword.Text.ToUpper() == us[i].Password.ToUpper())
                            login = true;
                        else
                            MessageBox.Show("password salah");
                    }
                }

                if(login)
                {
                    MessageBox.Show("Berhasil login customer");
                }
                else
                {
                    MessageBox.Show("Username tidak ada / Username salah");
                }
            }
        }
    }
}
