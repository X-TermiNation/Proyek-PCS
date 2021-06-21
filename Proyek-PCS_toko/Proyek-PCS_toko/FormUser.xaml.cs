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
        int jumlah = 1;
        int idbarang;
        int total;
        loggedUser user;
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        Random rnd = new Random();
        List<int> idBarang = new List<int>();
        List<Cart> cart = new List<Cart>();

        public FormUser(int id)
        {
            conn = MainWindow.conn;
            InitializeComponent();
            formUser.WindowState = WindowState.Maximized;
            user = new loggedUser(id);
            namaLabel.Content = $"Welcome, {user.namaCust}";
            saldoLabel.Content = $"Saldo : {user.saldo}";
            userId = id;
            saldo();
            itemReset();
            int[] arr = new int[3];
            

            arr[0] = rnd.Next(0, idBarang.Count());
            arr[1] = rnd.Next(0, idBarang.Count());
            arr[2] = rnd.Next(0, idBarang.Count());

            randomizeRec(arr[0], arr[1], arr[2]);
            tbjumlah.Text = jumlah.ToString();
        }
        private void loadData()
        {
            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();

            cmd.Connection = conn;
            cmd.CommandText = "SELECT BARANG.NAMA_BARANG AS \"NAMA BARANG\",MERK.NAMA_MERK ,KATEGORI.NAMA_KAT,STOK,HARGA FROM BARANG,MERK,KATEGORI WHERE BARANG.MERK = MERK.KODE_MERK AND BARANG.KATEGORI = KATEGORI.KODE_KAT ORDER BY BARANG.ID ASC";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dg_shop.ItemsSource = ds.DefaultView;
            conn.Close();
        }

        private void randomizeRec(int idBarang1, int idBarang2, int idBarang3)
        {
            List<Label> namaProduk = new List<Label>();
            List<Label> hrgProduk = new List<Label>();
            namaProduk.Add(namaproduk);
            namaProduk.Add(namaproduk2);
            namaProduk.Add(namaproduk3);

            hrgProduk.Add(hargalabel);
            hrgProduk.Add(hargalabel2);
            hrgProduk.Add(hargalabel3);


            OracleCommand cmd = new OracleCommand("SELECT NAMA_BARANG, HARGA FROM BARANG WHERE ID = :ID OR ID = :ID2 OR ID = :ID3", conn);
            cmd.Parameters.Add(":ID", idBarang1);
            cmd.Parameters.Add(":ID2", idBarang2);
            cmd.Parameters.Add(":ID3", idBarang3);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            int x = 0;
            while (reader.Read())
            {
                namaProduk[x].Content = reader[0].ToString();
                hrgProduk[x].Content = $"Harga: {reader[1].ToString()}";
                x++;
            }
            conn.Close();
        }

        private void itemReset()
        {
            OracleCommand cmd = new OracleCommand("SELECT ID FROM BARANG WHERE STOK > 0", conn);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
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
            if (tbsaldo.Text.Length != 0)
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
            gridcart.Visibility = Visibility.Hidden;
            gridshop.Visibility = Visibility.Hidden;
            btnmasukkeranjang.Visibility = Visibility.Hidden;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            gridsaldo.Visibility = Visibility.Hidden;
            gridhome.Visibility = Visibility.Visible;
            gridcart.Visibility = Visibility.Hidden;
            gridshop.Visibility = Visibility.Hidden;

            itemReset();
            int[] arr = new int[3];
            arr[0] = rnd.Next(0, idBarang.Count());
            arr[1] = rnd.Next(0, idBarang.Count());
            arr[2] = rnd.Next(0, idBarang.Count());

            randomizeRec(arr[0], arr[1], arr[2]);



        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count > 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Yakin ingin logout? Isi cart anda akan hilang jika anda logout!", "Logout Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    cart.Clear();
                    login lg = new login();
                    this.Close();
                    lg.ShowDialog();
                }
            }
            else
            {
                cart.Clear();
                login lg = new login();
                this.Close();
                lg.ShowDialog();
            }
        }

        private void shopButton_Click(object sender, RoutedEventArgs e)
        {
            loadData();
            datashophide();
            gridshop.Visibility = Visibility.Visible;
            gridsaldo.Visibility = Visibility.Hidden;
            gridhome.Visibility = Visibility.Hidden;
            gridcart.Visibility = Visibility.Hidden;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnmasukkeranjang_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(tbjumlah.Text) <= Convert.ToInt32(ds.Rows[dg_shop.SelectedIndex][3].ToString()))
            {
                getidbarang();
                cart.Add(new Cart(idbarang, Convert.ToInt32(tbjumlah.Text)));
                MessageBox.Show("Berhasil masuk ke cart");
                datashophide();
            }
            else
            {
                MessageBox.Show("Stok yang anda inginkan melebihi stok yang anda!");
            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            loadcart();
            gridshop.Visibility = Visibility.Hidden;
            gridsaldo.Visibility = Visibility.Hidden;
            gridhome.Visibility = Visibility.Hidden;
            gridcart.Visibility = Visibility.Visible;
        }

        //shop
        private void datashopshow()
        {
            lbharga.Visibility = Visibility.Visible;
            lbnamabarang.Visibility = Visibility.Visible;
            lbjml.Visibility = Visibility.Visible;
            tbjumlah.Visibility = Visibility.Visible;
            btnmin.Visibility = Visibility.Visible;
            btnplus.Visibility = Visibility.Visible;
            btnmasukkeranjang.Visibility = Visibility.Visible;
        }

        private void datashophide()
        {
            lbharga.Visibility = Visibility.Hidden;
            lbnamabarang.Visibility = Visibility.Hidden;
            lbjml.Visibility = Visibility.Hidden;
            tbjumlah.Visibility = Visibility.Hidden;
            btnmin.Visibility = Visibility.Hidden;
            btnplus.Visibility = Visibility.Hidden;
            btnmasukkeranjang.Visibility = Visibility.Hidden;
        }

        private void dg_shop_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dg_shop.SelectedIndex != -1)
            {
                jumlah = 1;
                datashopshow();
                lbnamabarang.Content = "Nama barang : " + ds.Rows[dg_shop.SelectedIndex][0].ToString();
                lbharga.Content = "Harga perbarang:" + ds.Rows[dg_shop.SelectedIndex][4].ToString();
                tbjumlah.Text = jumlah.ToString();
            }

        }

        private void btnplus_Click(object sender, RoutedEventArgs e)
        {
            if (dg_shop.SelectedIndex != -1)
            {
                if (jumlah < Convert.ToInt32(ds.Rows[dg_shop.SelectedIndex][3].ToString()))
                {
                    jumlah++;
                    tbjumlah.Text = jumlah.ToString();

                }
            }
        }

        private void getidbarang()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select ID from BARANG where NAMA_BARANG= '{ds.Rows[dg_shop.SelectedIndex][0]}'";
            idbarang = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }

        private void btnmin_Click(object sender, RoutedEventArgs e)
        {
            if (dg_shop.SelectedIndex != -1)
            {
                if (jumlah > 0)
                {
                    jumlah--;
                    tbjumlah.Text = jumlah.ToString();
                }
            }

        }

        //cart
        private void loadcart()
        {
            lbdatacart.Visibility = Visibility.Hidden;
            if (cart.Count > 0)
            {
                ds = new DataTable();
                OracleCommand cmd = new OracleCommand();
                da = new OracleDataAdapter();
                for (int i = 0; i < cart.Count; i++)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT NAMA_BARANG,HARGA,'{cart[i].JumlahBarang}' AS JUMLAH FROM BARANG WHERE ID = {cart[i].IdBarang}";

                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    dgkeranjang.ItemsSource = ds.DefaultView;
                    conn.Close();
                }
            }
            else
            {
                dgkeranjang.ItemsSource = null;
            }
            total = 0;
            for (int i = 0; i < dgkeranjang.Items.Count; i++)
            {
                total += Convert.ToInt32(ds.Rows[i][2].ToString()) * Convert.ToInt32(ds.Rows[i][1].ToString());
            }
            labeltotalharga.Content = total.ToString();
            
        }
      
        private void dgkeranjang_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Yakin ingin menghapus", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                cart.RemoveAt(dgkeranjang.SelectedIndex);
                loadcart();
            }
        }

        private void btnbeli_Click(object sender, RoutedEventArgs e)
        {
            if (user.saldo >= total)
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO H_BELI VALUES(-1, :IDCUST, :TOTAL, TO_DATE(SYSDATE, 'DD-MON-YY'))", conn);
                cmd.Parameters.Add(":IDCUST", userId);
                cmd.Parameters.Add(":TOTAL", total);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                for (int i = 0; i < dgkeranjang.Items.Count; i++)
                {
                    cmd = new OracleCommand("INSERT INTO D_BELI VALUES(-1, :IDBARANG, :JUMLAH, :SUBTOTAL)", conn);
                    cmd.Parameters.Add(":IDBARANG", cart[i].IdBarang);
                    cmd.Parameters.Add(":JUMLAH", cart[i].JumlahBarang);
                    cmd.Parameters.Add(":SUBTOTAL", Convert.ToInt32(ds.Rows[i][1].ToString()) * Convert.ToInt32(cart[i].JumlahBarang));
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                cart.Clear();
                loadcart();
                user.resetSaldo();
                saldoLabel.Content = $"Saldo : {user.saldo}";
            }
            else
            {
                MessageBox.Show("Saldo tidak cukup! Silahkan Top-up terlebih dahulu");
            }
        }

       
    }
}