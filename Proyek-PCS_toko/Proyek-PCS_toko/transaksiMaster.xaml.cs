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
        DataTable datasaldo;
        Boolean mtran = true;
        int idcus,idsaldo,saldo;
        string namacus;
        public transaksiMaster()
        {
            InitializeComponent();
            conn = MainWindow.conn;
            loadDataTransaksi();
            loadDataSaldo();

            mtransaksi();
        }

        private void mtransaksi()
        {
            gridmastertransaksi.Visibility = Visibility.Visible;
            gridsaldo.Visibility = Visibility.Collapsed;
        }

        private void msaldo()
        {
            gridmastertransaksi.Visibility = Visibility.Collapsed;
            gridsaldo.Visibility = Visibility.Visible;
        }

        private void loadDataTransaksi()
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

        private void loadDataSaldo()
        {
            datasaldo = new DataTable();
            OracleCommand cmd = new OracleCommand();
            da = new OracleDataAdapter();
            cmd.Connection = conn;
            cmd.CommandText = "select rq.ID,c.NAMA_CUST,rq.SALDOREQ from REQ_SALDO rq,CUSTOMER c where rq.ID_CUST=c.ID";
            conn.Open();
            cmd.ExecuteReader();
            da.SelectCommand = cmd;
            da.Fill(datasaldo);
            dgvsaldo.ItemsSource = datasaldo.DefaultView;
            conn.Close();
        }

        private void getidcus()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = $"select ID from CUSTOMER where NAMA_CUST='{namacus}'";
            idcus = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();   
        }

        private void deletereqsaldo()
        {
            conn.Close();
            conn.Open();
            using (OracleTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    OracleCommand cmd = new OracleCommand($"delete from REQ_SALDO where ID = {idsaldo}", conn);
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
        }

        private void btnreject_Click(object sender, RoutedEventArgs e)
        {
            deletereqsaldo();
            loadDataSaldo();
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            menu mn = new menu();
            this.Close();
            mn.ShowDialog();
        }

        private void btnchange_Click(object sender, RoutedEventArgs e)
        {
            if (mtran==true)
            {
                btnchange.Content = "Goto Transaksi";
                mtran = false;
                msaldo();
            }
            else if(mtran==false)
            {
                btnchange.Content = "Goto Request Saldo";
                mtran = true;
                mtransaksi();
            }

        }

        private void btnaccept_Click(object sender, RoutedEventArgs e)
        {
            Boolean fail = false;
            getidcus();
            conn.Open();
            using (OracleTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    OracleCommand cmd = new OracleCommand($"update CUSTOMER set SALDO = SALDO+{saldo} where ID={idcus}", conn);
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
                    fail = true;
                }
            }
            conn.Close();
            if(!fail)
                deletereqsaldo();
            loadDataSaldo();
        }

        private void dgvsaldo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbgetsaldo.Text= datasaldo.Rows[dgvsaldo.SelectedIndex][0].ToString() + " - " + datasaldo.Rows[dgvsaldo.SelectedIndex][1].ToString() + " - " + datasaldo.Rows[dgvsaldo.SelectedIndex][2].ToString();
            namacus = datasaldo.Rows[dgvsaldo.SelectedIndex][1].ToString();
            saldo = Convert.ToInt32(datasaldo.Rows[dgvsaldo.SelectedIndex][2].ToString());
            idsaldo = Convert.ToInt32(datasaldo.Rows[dgvsaldo.SelectedIndex][0].ToString());
        }
    }
}
