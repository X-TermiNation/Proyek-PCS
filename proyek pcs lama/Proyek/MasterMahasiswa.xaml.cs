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

namespace Proyek
{
    /// <summary>
    /// Interaction logic for MasterMahasiswa.xaml
    /// </summary>
    public partial class MasterMahasiswa : Window
    {
        OracleConnection conn;
        DataTable dataSiswa;
        OracleDataAdapter da;
        public MasterMahasiswa()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadData();
        }

        private void loadData()
        {
            da = new OracleDataAdapter();
            dataSiswa = new DataTable();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = "SELECT NIS, NAMA AS \"Nama Siswa\", DECODE(JENIS_KELAMIN, 'L', 'Laki-laki', 'P', 'Perempuan') AS \"Jenis Kelamin\", TANGGAL_LAHIR AS \"Tgl Lahir\", DECODE(ID_JURUSAN, '1', 'IPA', '2', 'IPS') AS \"Jurusan\" FROM SISWA";
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(dataSiswa);
            dgSiswa.ItemsSource = dataSiswa.DefaultView;
            conn.Close();
        }

        private void insBtn_Click(object sender, RoutedEventArgs e)
        {
            string namaUser = namaTB.Text.ToString();
            string nisn = nisnTb.Text.ToString();
            string nis = nisTb.Text.ToString();
            string jk = "";
        }
    }
}
