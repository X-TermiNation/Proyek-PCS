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

namespace Proyek_PCS_toko
{
    /// <summary>
    /// Interaction logic for FormKatMerk.xaml
    /// </summary>
    public partial class FormKatMerk : Window
    {
        OracleConnection conn;
        OracleDataAdapter da;
        DataTable dataKat;
        DataTable dataMerk;
        public FormKatMerk()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadData();
        }

        private void loadData()
        {
            dataKat = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();

            cmd.Connection = conn;
            cmd.CommandText = "SELECT KODE_KAT AS \"Kode Kategori\", NAMA_KAT AS \"Nama Kategori\" FROM KATEGORI ORDER BY NAMA_KAT ASC";

            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(dataKat);
            dgvKat.ItemsSource = dataKat.DefaultView;
            conn.Close();

            dataMerk = new DataTable();
            cmd.CommandText = "SELECT KODE_MERK AS \"Kode Merk\", NAMA_MERK AS \"Nama Merk\" FROM MERK ORDER BY NAMA_MERK ASC";
            conn.Close();
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(dataMerk);
            dgvMerk.ItemsSource = dataMerk.DefaultView;
            conn.Close();
        }
        int katIndex = -1;
        private void dgvKat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgvKat.SelectedIndex != -1)
            {
                dgvMerk.SelectedIndex = -1;
                kodekatTb.Text = dataKat.Rows[dgvKat.SelectedIndex][0].ToString();
                namakatTb.Text = dataKat.Rows[dgvKat.SelectedIndex][1].ToString();
                inskBtn.IsEnabled = false;
                updkBtn.IsEnabled = true;
                delkBtn.IsEnabled = true;
            }
        }

        private void dgvMerk_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvMerk.SelectedIndex != -1)
            {
                dgvKat.SelectedIndex = -1;
                kodemerkTb.Text = dataMerk.Rows[dgvMerk.SelectedIndex][0].ToString();
                namamerkTb.Text = dataMerk.Rows[dgvMerk.SelectedIndex][1].ToString();
                insmBtn.IsEnabled = false;
                updmBtn.IsEnabled = true;
                delmBtn.IsEnabled = true;
            }
        }

        private void inskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (katReq)
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO KATEGORI VALUES(:KODEKAT, :NAMAKAT)", conn);
                cmd.Parameters.Add(":KODEKAT", kodekatTb.Text);
                cmd.Parameters.Add(":NAMAKAT", namakatTb.Text);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadData();
                kodekatTb.Text = "";
                namakatTb.Text = "";
                dgvKat.SelectedIndex = -1;
                inskBtn.IsEnabled = true;
                updkBtn.IsEnabled = false;
                delkBtn.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Nama kategori tidak memenuhi syarat");
            }
        }
        bool katReq = false;
        private void namakatTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgvKat.SelectedIndex < 0)
            {
                if (namakatTb.Text.Length >= 3)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT AUTOGENKODEKAT('{namakatTb.Text.Substring(0, 3).ToUpper()}') FROM DUAL";
                    conn.Close();
                    conn.Open();
                    kodekatTb.Text = cmd.ExecuteScalar().ToString();
                    conn.Close();
                    katReq = true;
                }
                else
                {
                    kodekatTb.Text = "";
                    katReq = false;
                }
            }
        }

        private void clrkBtn_Click(object sender, RoutedEventArgs e)
        {
            kodekatTb.Text = "";
            namakatTb.Text = "";
            dgvKat.SelectedIndex = -1;
            inskBtn.IsEnabled = true;
            updkBtn.IsEnabled = false;
            delkBtn.IsEnabled = false;
        }

        private void clrmBtn_Click(object sender, RoutedEventArgs e)
        {
            kodemerkTb.Text = "";
            namamerkTb.Text = "";
            dgvMerk.SelectedIndex = -1;
            insmBtn.IsEnabled = true;
            updmBtn.IsEnabled = false;
            delmBtn.IsEnabled = false;
        }
        bool merkReq = false;
        private void namamerkTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dgvMerk.SelectedIndex < 0)
            {
                if (namamerkTb.Text.Length >= 3)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT AUTOGENKODEMERK('{namamerkTb.Text.Substring(0, 3).ToUpper()}') FROM DUAL";
                    conn.Close();
                    conn.Open();
                    kodemerkTb.Text = cmd.ExecuteScalar().ToString();
                    conn.Close();
                    merkReq = true;
                }
                else
                {
                    kodemerkTb.Text = "";
                    merkReq = false;
                }
            }
        }

        private void insmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (merkReq)
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO MERK VALUES(:KODEMERK, :NAMAMERK)", conn);
                cmd.Parameters.Add(":KODEMERK", kodemerkTb.Text);
                cmd.Parameters.Add(":NAMAMERK", namamerkTb.Text);
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadData();
                kodemerkTb.Text = "";
                namamerkTb.Text = "";
                dgvMerk.SelectedIndex = -1;
                insmBtn.IsEnabled = true;
                updmBtn.IsEnabled = false;
                delmBtn.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Nama merk tidak memenuhi syarat");
            }
        }

        private void delkBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM KATEGORI WHERE KODE_KAT=:KODEKAT", conn);
                cmd.Parameters.Add(":KODEKAT", dataKat.Rows[dgvKat.SelectedIndex][0].ToString());
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadData();
                kodekatTb.Text = "";
                namakatTb.Text = "";
                dgvKat.SelectedIndex = -1;
                inskBtn.IsEnabled = true;
                updkBtn.IsEnabled = false;
                delkBtn.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Kategori sudah terisi di barang lain");
            }
            
        }

        private void delmBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM MERK WHERE KODE_MERK=:KODEMERK", conn);
                cmd.Parameters.Add(":KODEMERK", dataMerk.Rows[dgvMerk.SelectedIndex][0].ToString());
                conn.Close();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadData();
                kodemerkTb.Text = "";
                namamerkTb.Text = "";
                dgvMerk.SelectedIndex = -1;
                insmBtn.IsEnabled = true;
                updmBtn.IsEnabled = false;
                delmBtn.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Merk sudah terisi di barang lain");
            }
        }

        private void btnkembali_Click(object sender, RoutedEventArgs e)
        {
            masterbarang mb = new masterbarang();
            this.Close();
            mb.ShowDialog();
        }
    }
}
