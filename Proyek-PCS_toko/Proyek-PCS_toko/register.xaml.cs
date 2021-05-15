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

namespace Proyek_PCS_toko
{
    /// <summary>
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Window
    {
        public register()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            adddata();
        }
        OracleConnection conn;
        Boolean checkus = true;
        List<user> us = new List<user>();
        int maxidcus;

        private void adddata()
        {
            OracleCommand cmd = new OracleCommand($"select * from CUSTOMER", conn);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                us.Add(new user(Convert.ToInt32(reader[0]), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), Convert.ToInt32(reader[6])));
            }
            conn.Close();
        }

        private void getid()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select MAX(ID)+1 from CUSTOMER";
            maxidcus = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }

        private void checkusername()
        {
            for (int i = 0; i < us.Count; i++)
            {
                if (tbusername.Text.ToUpper() == us[i].Username.ToUpper())
                {
                    checkus = false;
                }
            }
        }

        private void btnregister_Click(object sender, RoutedEventArgs e)
        {
            if(tbusername.Text.Length != 0 || tbnama.Text.Length != 0  || tbnotelp.Text.Length!=0 || tbpassword.Password.Length!=0 || tbemail.Text.Length!=0)
            {
                checkusername();
                if(tbemail.Text.Contains("@") && tbemail.Text.Contains(".com"))
                {
                    if(checkus)
                    {
                        getid();
                        conn.Open();
                        using (OracleTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                OracleCommand cmd = new OracleCommand($"insert into CUSTOMER values ({maxidcus},'{tbusername.Text}','{tbnama.Text}','{tbemail.Text}','{tbnotelp.Text}','{tbpassword.Password}',0)", conn);
                                cmd.ExecuteNonQuery();
                                trans.Commit();
                                conn.Close();
                                MessageBox.Show("berhasil register");
                                tbusername.Text = "";
                                tbnama.Text = "";
                                tbnotelp.Text = "";
                                tbpassword.Password = "";
                                tbemail.Text = "";
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                conn.Close();
                                MessageBox.Show("Username,email dan no telp sudah terdaftar");
                            }
                        }
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username sudah terdaftar");
                        tbusername.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("format email salah");
                }
            }
            else
            {
                MessageBox.Show("Data tidak boleh kosong");
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            login lg = new login();
            this.Close();
            lg.ShowDialog();
        }

        private void tbnotelp_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tString = tbnotelp.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Please enter a valid number");
                    tbnotelp.Text = "";
                    return;
                }

            }
        }
    }
}
