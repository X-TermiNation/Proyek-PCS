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

namespace Proyek_PCS_toko
{
    /// <summary>
    /// Interaction logic for reportAdmin.xaml
    /// </summary>
    public partial class reportAdmin : Window
    {
        public reportAdmin()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reportCRV.Owner = Window.GetWindow(this);
            ReportView rpt = new ReportView();
            rpt.SetDatabaseLogon(MainWindow.userId, MainWindow.pass, MainWindow.source, "");
            reportCRV.ViewerCore.ReportSource = rpt;
        }
    }
}
