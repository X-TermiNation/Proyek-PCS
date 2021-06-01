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
        Random rnd = new Random();
        List<int> idBarang = new List<int>();
        public FormUser(int id)
        {
            conn = MainWindow.conn;
            InitializeComponent();
            user = new loggedUser(id);
            namaLabel.Content = $"Welcome, {user.namaCust}";
            saldoLabel.Content = $"Saldo : {user.saldo}";
            userId = id;
            saldo();
            itemReset();
            int rand = rnd.Next(0, idBarang.Count());
            randomizeRec(rand);
        }
        private void loadData()
        {
            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();

            cmd.Connection = conn;
            cmd.CommandText = "SELECT BARANG.ID ,BARANG.NAMA_BARANG AS \"NAMA BARANG\",MERK.NAMA_MERK ,KATEGORI.NAMA_KAT,STOK,HARGA FROM BARANG,MERK,KATEGORI WHERE BARANG.MERK = MERK.KODE_MERK AND BARANG.KATEGORI = KATEGORI.KODE_KAT ORDER BY id ASC";

            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dg_shop.ItemsSource = ds.DefaultView;
            conn.Close();
        }

        private void randomizeRec(int idBarang)
        {
            OracleCommand cmd = new OracleCommand("SELECT NAMA_BARANG, HARGA FROM BARANG WHERE ID = :ID", conn);
            cmd.Parameters.Add(":ID", idBarang);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                namaproduk.Content = reader[0].ToString();
                hargalabel.Content = $"Harga: {reader[1].ToString()}";
            }
            conn.Close();
        }

        private void itemReset()
        {
            OracleCommand cmd = new OracleCommand("SELECT ID FROM BARANG", conn);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                idBarang.Add(Convert.ToInt32(reader[0]));
            }
            conn.Close();
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

        private void btnsaldo_Click(object sender, RoutedEventArgs e)
        {
            gridhome.Visibility = Visibility.Hidden;
            gridsaldo.Visibility = Visibility.Visible;
            dg_shop.Visibility = Visibility.Hidden;
            btnlihatkeranjang.Visibility = Visibility.Hidden;
            btnmasukkeranjang.Visibility = Visibility.Hidden;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            gridsaldo.Visibility = Visibility.Hidden;
            gridhome.Visibility = Visibility.Visible;
            dg_shop.Visibility = Visibility.Hidden;
            btnlihatkeranjang.Visibility = Visibility.Hidden;
            btnmasukkeranjang.Visibility = Visibility.Hidden;
            itemReset();
            int rand = rnd.Next(0, idBarang.Count());
            randomizeRec(rand);
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            login lg = new login();
            this.Close();
            lg.ShowDialog();
        }

        private void shopButton_Click(object sender, RoutedEventArgs e)
        {
            dg_shop.Visibility = Visibility.Visible;
            loadData();
            btnlihatkeranjang.Visibility = Visibility.Visible;
            btnmasukkeranjang.Visibility = Visibility.Visible;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnmasukkeranjang_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnlihatkeranjang_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
