using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyek_PCS_toko
{
    class Cart
    {
        int idBarang;
        int jumlahBarang;

        public Cart(int idBarang, int jumlahBarang)
        {
            this.idBarang = idBarang;
            this.jumlahBarang = jumlahBarang;
        }
    }
}
