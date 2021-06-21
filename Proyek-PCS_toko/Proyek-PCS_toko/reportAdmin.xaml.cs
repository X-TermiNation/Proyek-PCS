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
using System.Data;
using Oracle.DataAccess.Client;

namespace Proyek_PCS_toko
{
    /// <summary>
    /// Interaction logic for reportAdmin.xaml
    /// </summary>
    public partial class reportAdmin : Window
    {
        OracleConnection conn;
        DataTable ds, dt, pertanyaan;
        DataTable temp;
        OracleDataAdapter da;
        string kodemerk;
        string kodekat;
        string kodeNota;
        public reportAdmin()
        {
            InitializeComponent();
            getKat();
            getMerk();
            getNota();
        }
        void getMerk()
        {
            merkCB.Items.Add("All");
            conn = MainWindow.conn;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT NAMA_MERK from MERK ORDER BY KODE_MERK ASC";
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                merkCB.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }

        void getNota()
        {
            notaCB.Items.Add("All");
            conn = MainWindow.conn;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT NOMOR_NOTA from D_BELI ORDER BY NOMOR_NOTA ASC";
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                notaCB.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }
        void getKat()
        {
            katCB.Items.Add("All");
            conn = MainWindow.conn;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT NAMA_KAT from KATEGORI ORDER BY KODE_KAT ASC";
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                katCB.Items.Add(reader.GetString(0));
            }
            conn.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reportCRV.Owner = Window.GetWindow(this);
        }

        private void hargaReportRB_Checked(object sender, RoutedEventArgs e)
        {
            pertamaDATE.IsEnabled = false;
            duaDATE.IsEnabled = false;
            katCB.IsEnabled = true;
            merkCB.IsEnabled = true;
            notaCB.IsEnabled = false;
            submitBTN.IsEnabled = true;
        }

        private void stockReportRB_Checked(object sender, RoutedEventArgs e)
        {
            pertamaDATE.IsEnabled = false;
            duaDATE.IsEnabled = false;
            katCB.IsEnabled = false;
            merkCB.IsEnabled = false;
            notaCB.IsEnabled = true;
            submitBTN.IsEnabled = true;
        }

        private void pemasukanReportRB_Checked(object sender, RoutedEventArgs e)
        {
            pertamaDATE.IsEnabled = true;
            duaDATE.IsEnabled = true;
            katCB.IsEnabled = true;
            merkCB.IsEnabled = true;
            submitBTN.IsEnabled = true;
            notaCB.IsEnabled = false;
        }

        private void getcurrentkode()
        {
            if(katCB.Text != "All")
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = $"select KODE_KAT from KATEGORI where NAMA_KAT = '{katCB.Text}'";
                kodekat = (String)cmd.ExecuteScalar();
                conn.Close();
            }
            
            if(merkCB.Text != "All")
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = $"select KODE_MERK from MERK where NAMA_MERK = '{merkCB.Text}'";
                kodemerk = (String)cmd.ExecuteScalar();
                conn.Close();
            }
            if(notaCB.Text != "All")
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = $"select NOMOR_NOTA from D_BELI where NOMOR_NOTA = '{notaCB.Text}'";
                kodeNota = (string)cmd.ExecuteScalar();
                conn.Close();
            }
        }

        private void keluarBTN_Click(object sender, RoutedEventArgs e)
        {
            menu m = new menu();
            m.Show();
            this.Close();
        }

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            getcurrentkode();
            if (hargaReportRB.IsChecked == true)
            {
                ReportHarga rpt = new ReportHarga();
                if (katCB.Text.ToString() != "" &&  katCB.Text.ToString() != "All")
                {
                    rpt.SetParameterValue("katParam", kodekat);
                }
                else
                {
                    rpt.SetParameterValue("katParam", "1");
                }
                if (merkCB.Text.ToString() != "" && merkCB.Text.ToString() != "All")
                {
                    rpt.SetParameterValue("merkParam", kodemerk);
                }
                else
                {
                    rpt.SetParameterValue("merkParam", "1");
                }
                rpt.SetDatabaseLogon(MainWindow.userId, MainWindow.pass, MainWindow.source, "");
                reportCRV.ViewerCore.ReportSource = rpt;
            }
            else if (stockReportRB.IsChecked == true)
            {
                getcurrentkode();
                ReportStock rpt = new ReportStock();
                if (notaCB.Text.ToString() != "" && notaCB.Text.ToString() != "All")
                {
                    rpt.SetParameterValue("paramNota", kodeNota);
                }
                else
                {
                    rpt.SetParameterValue("paramNota", "1");
                }
                rpt.SetDatabaseLogon(MainWindow.userId, MainWindow.pass, MainWindow.source, "");
                reportCRV.ViewerCore.ReportSource = rpt;
            }
            else if (pemasukanReportRB.IsChecked == true)
            {
                getcurrentkode();
                pemasukanReport rpt = new pemasukanReport();
                if (pertamaDATE.DisplayDate == null || duaDATE.DisplayDate == null || katCB.Text.ToString() == "" || merkCB.Text.ToString() == "")
                {
                    MessageBox.Show("Fill BLank");
                }
                else
                {
                    rpt.SetParameterValue("keduaParam", duaDATE.DisplayDate);
                    rpt.SetParameterValue("pertamaParam", pertamaDATE.DisplayDate);
                    if (katCB.Text.ToString() != "" && katCB.Text.ToString() != "All")
                    {
                        rpt.SetParameterValue("katParam", kodekat);
                    }
                    else
                    {
                        rpt.SetParameterValue("katParam", "1");
                    }
                    if (merkCB.Text.ToString() != "" && merkCB.Text.ToString() != "All")
                    {
                        rpt.SetParameterValue("merkParam", kodemerk);
                    }
                    else
                    {
                        rpt.SetParameterValue("merkParam", "1");
                    }
                    if (katCB.Text.ToString() != "" && katCB.Text.ToString() != "All")
                    {
                        rpt.SetParameterValue("katParam", kodekat);
                    }
                    else
                    {
                        rpt.SetParameterValue("katParam", "1");
                    }
                    if (merkCB.Text.ToString() != "" && merkCB.Text.ToString() != "All")
                    {
                        rpt.SetParameterValue("merkParam", kodemerk);
                    }
                    else
                    {
                        rpt.SetParameterValue("merkParam", "1");
                    }
                    rpt.SetDatabaseLogon(MainWindow.userId, MainWindow.pass, MainWindow.source, "");
                    reportCRV.ViewerCore.ReportSource = rpt;
                }
            }
        }

        private void pemasukanReportCB_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
