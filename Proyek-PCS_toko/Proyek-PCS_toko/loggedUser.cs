using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;

namespace Proyek_PCS_toko
{
    class loggedUser
    {
        OracleConnection conn;
        int userId;
        public string userName { get; set; }
        public string namaCust { get; set; }
        public string email { get; set; }
        public string noTelp { get; set; }
        public int saldo { get; set; }
        public loggedUser(int userId)
        {
            this.userId = userId;
            conn = MainWindow.conn;

            OracleCommand cmd = new OracleCommand("SELECT USERNAME, NAMA_CUST, EMAIL, NO_TELP,SALDO FROM CUSTOMER WHERE ID = :ID", conn);
            cmd.Parameters.Add(":ID", userId);
            conn.Close();
            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                this.userName = reader[0].ToString();
                this.namaCust = reader[1].ToString();
                this.email = reader[2].ToString();
                this.noTelp = reader[3].ToString();
                this.saldo = Convert.ToInt32(reader[4].ToString());
            }
            conn.Close();
        }
    }
}
