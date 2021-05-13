using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyek
{
    class user
    {
        int id;
        string username;
        string nama;
        string email;
        string notelp;
        string password;
        int saldo;

        public user(int id, string username, string nama, string email, string notelp, string password, int saldo)
        {
            this.id = id;
            this.username = username;
            this.nama = nama;
            this.email = email;
            this.notelp = notelp;
            this.password = password;
            this.saldo = saldo;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Email { get => email; set => email = value; }
        public string Notelp { get => notelp; set => notelp = value; }
        public string Password { get => password; set => password = value; }
        public int Saldo { get => saldo; set => saldo = value; }
    }
}
