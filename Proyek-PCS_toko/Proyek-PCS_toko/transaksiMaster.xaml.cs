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
    /// Interaction logic for transaksiMaster.xaml
    /// </summary>
    public partial class transaksiMaster : Window
    {
        OracleConnection conn;
        OracleDataAdapter da;
        DataTable dataTrans;
        public transaksiMaster()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadData();
        }

        private void loadData()
        {
            dataTrans = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT HB.NOMOR_NOTA AS \"Nomor Nota\",C.NAMA_CUST AS \"Nama Customer\",HB.TOTAL_PEMBELIAN AS \"Total Pembelian\" FROM H_BELI HB,CUSTOMER C WHERE C.ID = HB.ID_CUSTOMER GROUP BY C.NAMA_CUST,HB.NOMOR_NOTA,HB.TOTAL_PEMBELIAN ORDER BY C.NAMA_CUST ASC";
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(dataTrans);
            dgvTransaksi.ItemsSource = dataTrans.DefaultView;
            conn.Close();
        }
    }
}
