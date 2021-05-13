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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.DataAccess.Client;
using System.Data;


namespace Proyek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static OracleConnection conn;
        String source, userId, pass;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            source = sourceTB.Text;
            userId = UsernameTB.Text;
            pass = PassTB.Text;
            try
            {
                conn = new OracleConnection("Data Source = " + source + "; User ID = " + userId + "; password = " + pass);
                conn.Open();
                conn.Close();
                LoginForm lf = new LoginForm();
                this.Close();
                lf.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
    }
}
