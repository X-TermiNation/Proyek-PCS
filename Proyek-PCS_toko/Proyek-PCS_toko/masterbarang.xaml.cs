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
        public masterbarang()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadData();
        }
        private void loadData()
        {

            ds = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();

            cmd.Connection = conn;
            cmd.CommandText = "SELECT ID ,NAMA_BARANG AS \"NAMA BARANG\",MERK ,KATEGORI,STOK,HARGA FROM BARANG ORDER BY id ASC";

            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dg_barang.ItemsSource = ds.DefaultView;
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
                    cmd.CommandText = "SELECT ID ,NAMA_BARANG AS \"NAMA BARANG\",MERK ,KATEGORI,STOK,HARGA FROM BARANG where "+key.ToUpper()+ " LIKE '%"+ textcari.Text +"%' ORDER BY ID ASC";
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
    }
}
