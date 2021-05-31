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
using System.Data;
using System.Text.RegularExpressions;


namespace Proyek_PCS_toko
{
    /// <summary>
    /// Interaction logic for FormUser.xaml
    /// </summary>
    public partial class FormUser : Window
    {
        int userId;
        int idregsaldo;
        loggedUser user;
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        public FormUser(int id)
        {
            conn = MainWindow.conn;
            InitializeComponent();
            user = new loggedUser(id);
            namaLabel.Content = $"Welcome, {user.namaCust}";
            saldoLabel.Content = $"Saldo : {user.saldo}";
            userId = id;
            saldo();
        }

        private void saldo()
        {
            cblistpembayaran.Items.Add("BCA");
            cblistpembayaran.Items.Add("OVO");
            cblistpembayaran.Items.Add("GOPAY");
            cblistpembayaran.Items.Add("DANA");
            cblistpembayaran.Items.Add("BNI");
        }

        private void tbsaldo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, @"^[^0-9\d\s]+$");
        }

        private void getidreqsaldo()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select MAX(ID)+1 from REQ_SALDO";
            idregsaldo = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }

        private void btnisisaldo_Click(object sender, RoutedEventArgs e)
        {
            if(tbsaldo.Text.Length !=0)
            {
                getidreqsaldo();
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand($"insert into REQ_SALDO values({idregsaldo},{userId},{Convert.ToInt32(tbsaldo.Text)})", conn);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        conn.Close();
                        MessageBox.Show("Berhasil Req saldo ke admin");
                        tbsaldo.Text = "";
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
                conn.Close();
            }
        }
    }
}
