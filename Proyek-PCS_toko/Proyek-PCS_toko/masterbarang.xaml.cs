using System;
using System.Collections.Generic;
using System.Data;
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
namespace Proyek_PCS_toko
{
    /// <summary>
    /// Interaction logic for masterbarang.xaml
    /// </summary>
    public partial class masterbarang : Window
    {
        OracleConnection conn;
        DataTable ds;
        OracleDataAdapter da;
        int idbarang;
        string kodekat, kodemerk;
        public masterbarang()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadData();
            isimerk();
            isikategori();
            normalmode();
        }

        private void editmode()
        {
            btninsert.IsEnabled = false;
            btnupdate.IsEnabled = true;
            btndelete.IsEnabled = true;
            btnclear.IsEnabled = true;
        }

        private void normalmode()
        {
            tbid.Text = "";
            tbnama_barang.Text = "";
            tbstok.Text = "";
            tbharga.Text = "";
            cbkat.SelectedIndex = -1;
            cbmerk.SelectedIndex = -1;
            btninsert.IsEnabled = true;
            btnupdate.IsEnabled = false;
            btndelete.IsEnabled = false;
            btnclear.IsEnabled = false;
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
            dg_barang.ItemsSource = ds.DefaultView;
            conn.Close();

        }

        private void isikategori()
        {
            cbkat.Items.Clear();
            OracleCommand cmd = new OracleCommand($"select * from KATEGORI ORDER BY KODE_KAT", conn);
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cbkat.Items.Add(reader.GetString(1));
            }
            cbkat.SelectedIndex = -1;
            conn.Close();
        }

        private void isimerk()
        {
            cbmerk.Items.Clear();
            OracleCommand cmd = new OracleCommand($"select * from MERK", conn);
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cbmerk.Items.Add(reader.GetString(1));
            }
            cbmerk.SelectedValuePath = "Name";
            cbmerk.SelectedIndex = -1;
            conn.Close();
        }

        private void btncari_Click(object sender, RoutedEventArgs e)
        {
            string key = "";
            if (radionama.IsChecked == true)
            {
                key = "NAMA_BARANG";
            }
            else if (radiomerk.IsChecked == true)
            {
                key = "MERK";
            }
            else if (radiokategori.IsChecked == true)
            {
                key = "KATEGORI";
            }
            else if (radiosemua.IsChecked == true)
            {
                key = "semua";
            }
            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();
            if (key == "semua")
            {
                loadData();
            }
            else
            {
                if (textcari.Text != "")
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT ID ,NAMA_BARANG AS \"NAMA BARANG\",MERK,KATEGORI,STOK,HARGA FROM BARANG where "+key.ToUpper()+ " LIKE '%" + textcari.Text.ToUpper() + "%' ORDER BY ID ASC";
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteReader();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    dg_barang.ItemsSource = ds.DefaultView;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("keyword tidak boleh kosong");
                }
            }  
        }

        private void getid()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select MAX(ID)+1 from BARANG";
            idbarang = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }

        private void getkodekat()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select KODE_KAT from KATEGORI where NAMA_KAT = '{cbkat.SelectedItem}'";
            kodekat = cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        private void getkodemerk()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select KODE_MERK from MERK where NAMA_MERK = '{cbmerk.SelectedItem}'";
            kodemerk = cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            normalmode();
        }


        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            if (tbnama_barang.Text.Length != 0 || tbharga.Text.Length != 0 || tbstok.Text.Length !=0)
            {
                if(cbkat.SelectedIndex != -1 || cbmerk.SelectedIndex != -1)
                {
                    getkodekat();
                    getkodemerk();
                    conn.Open();
                    using (OracleTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            OracleCommand cmd = new OracleCommand($"insert into BARANG values({Convert.ToInt32(tbid.Text.ToString())},'{tbnama_barang.Text}','{kodemerk}','{kodekat}',{Convert.ToInt32(tbstok.Text)},{Convert.ToInt32(tbharga.Text)})", conn);
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                            conn.Close();
                            MessageBox.Show("Insert berhasil");
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            conn.Close();
                            MessageBox.Show(ex.Message);
                        }
                    }
                    conn.Close();
                    loadData();
                    normalmode();
                }
                else
                {
                    MessageBox.Show("Pilih combobox terlebih dahulu");
                }
            }
            else
            {
                MessageBox.Show("isi textbox terlebih dahulu");
            }
        }

        private void tbnama_barang_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(btninsert.IsEnabled==true)
            {
                if (tbnama_barang.Text.Length != 0)
                {
                    getid();
                    tbid.Text = idbarang.ToString();
                }
                else
                {
                    tbid.Text = "";
                }
            }
        }

        private void tbstok_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tString = tbstok.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Please enter a valid number");
                    tbstok.Text = "";
                    return;
                }

            }
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (tbnama_barang.Text.Length != 0 || tbharga.Text.Length != 0 || tbstok.Text.Length != 0)
            {
                if (cbkat.SelectedIndex != -1 || cbmerk.SelectedIndex != -1)
                {
                    getkodekat();
                    getkodemerk();
                    conn.Open();
                    using (OracleTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            OracleCommand cmd = new OracleCommand($"update BARANG set NAMA_BARANG = '{tbnama_barang.Text}',MERK = '{kodemerk}',KATEGORI = '{kodekat}',STOK = {Convert.ToInt32(tbstok.Text)},HARGA = {Convert.ToInt32(tbharga.Text)} where ID = {Convert.ToInt32(tbid.Text)}", conn);
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                            conn.Close();
                            MessageBox.Show("Update berhasil");
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            conn.Close();
                            MessageBox.Show(ex.Message);
                        }
                    }
                    conn.Close();
                    loadData();
                    normalmode();
                }
                else
                {
                    MessageBox.Show("Pilih combobox terlebih dahulu");
                }
            }
            else
            {
                MessageBox.Show("isi textbox terlebih dahulu");
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            using (OracleTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    OracleCommand cmd = new OracleCommand($"delete from barang where ID = {Convert.ToInt32(tbid.Text)}", conn);
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    conn.Close();
                    MessageBox.Show("delete berhasil");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    conn.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            conn.Close();
            loadData();
            normalmode();
        }

        private void dg_barang_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dg_barang.SelectedIndex != -1)
            {
                editmode();
                tbid.Text = ds.Rows[dg_barang.SelectedIndex][0].ToString();
                tbnama_barang.Text = ds.Rows[dg_barang.SelectedIndex][1].ToString();
                tbstok.Text = ds.Rows[dg_barang.SelectedIndex][4].ToString();
                tbharga.Text = ds.Rows[dg_barang.SelectedIndex][5].ToString();
                cbkat.SelectedIndex = cbkat.Items.IndexOf(ds.Rows[dg_barang.SelectedIndex][3]);
                cbmerk.SelectedIndex = cbmerk.Items.IndexOf(ds.Rows[dg_barang.SelectedIndex][2]);
            }
        }

        private void tbharga_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tString = tbharga.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Please enter a valid number");
                    tbharga.Text = "";
                    return;
                }

            }
        }
    }
}
