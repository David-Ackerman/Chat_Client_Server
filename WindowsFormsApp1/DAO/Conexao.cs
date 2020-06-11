using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.DAO
{
    public class Conexao
    {
        
        MySqlConnection con = new MySqlConnection();
        public Conexao()
        {
            con.ConnectionString = @"Database=bdapsdavid2019;Password=DAvi-BD86;Server=db4free.net;User=david_ackerman;old guids=true";

        }
        public MySqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
