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
            this.IdBarang = idBarang;
            this.JumlahBarang = jumlahBarang;
        }

        public int IdBarang { get => idBarang; set => idBarang = value; }
        public int JumlahBarang { get => jumlahBarang; set => jumlahBarang = value; }
    }
}
