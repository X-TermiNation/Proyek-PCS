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
        public reportAdmin()
        {
            InitializeComponent();
            getKat();
            getMerk();
        }
        void getMerk()
        {
            conn = MainWindow.conn;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT KODE_MERK from MERK ORDER BY KODE_MERK ASC";
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                merkCB.Items.Add(reader[0]);
            }
            conn.Close();
        }
        void getKat()
        {
            conn = MainWindow.conn;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT KODE_KAT from KATEGORI ORDER BY KODE_KAT ASC";
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                katCB.Items.Add(reader[0]);
            }
            conn.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reportCRV.Owner = Window.GetWindow(this);
        }

        private void hargaReportRB_Checked(object sender, RoutedEventArgs e)
        {
            katCB.IsEnabled = true;
            merkCB.IsEnabled = true;
        }

        private void stockReportRB_Checked(object sender, RoutedEventArgs e)
        {
            katCB.IsEnabled = true;
            merkCB.IsEnabled = true;
        }

        private void pemasukanReportRB_Checked(object sender, RoutedEventArgs e)
        {
            katCB.IsEnabled = true;
            merkCB.IsEnabled = true;
        }

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            if(hargaReportRB.IsChecked == true)
            {
                ReportHarga rpt = new ReportHarga();
                if (katCB.Text.ToString() != "")
                {
                    rpt.SetParameterValue("katParam", katCB.Text.ToString());
                }
                else
                {
                    rpt.SetParameterValue("katParam", "1");
                }
                if (merkCB.Text.ToString() != "")
                {
                    rpt.SetParameterValue("merkParam", merkCB.Text.ToString());
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
                ReportStock rpt = new ReportStock();
                if (katCB.Text.ToString() != "")
                {
                    rpt.SetParameterValue("katParam", katCB.Text.ToString());
                }
                else
                {
                    rpt.SetParameterValue("katParam", "1");
                }
                if (merkCB.Text.ToString() != "")
                {
                    rpt.SetParameterValue("merkParam", merkCB.Text.ToString());
                }
                else
                {
                    rpt.SetParameterValue("merkParam", "1");
                }
                rpt.SetDatabaseLogon(MainWindow.userId, MainWindow.pass, MainWindow.source, "");
                reportCRV.ViewerCore.ReportSource = rpt;
            }
            else if (pemasukanReportRB.IsChecked == true)
            {
                pemasukanReport rpt = new pemasukanReport();
            }
        }

        private void pemasukanReportCB_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
