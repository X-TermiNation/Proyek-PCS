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
    /// Interaction logic for menu.xaml
    /// </summary>
    public partial class menu : Window
    {
        public menu()
        {
            InitializeComponent();
        }

        private void btnmasterbarang_Click(object sender, RoutedEventArgs e)
        {
            masterbarang m = new masterbarang();
            this.Close();
            m.Show();
        }

        private void btnreport_Click(object sender, RoutedEventArgs e)
        {
            reportAdmin r = new reportAdmin();
            r.Show();
            this.Close();
        }

        private void formkat_Click(object sender, RoutedEventArgs e)
        {
            FormKatMerk fkm = new FormKatMerk();
            this.Close();
            fkm.Show();
        }
    }
}
