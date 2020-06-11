using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO
{
    class DaoComandos
    {
        public bool cadExiste = false;
        public bool tem = false;
        public String mensagem;
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr;

        public bool verificarLogin (String login, String senha)
        {
            // Comandos sql para verifcar se usuario existe no banco
            cmd.CommandText = "select * from APS5user where username = @login and senha = @senha";
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@senha", senha);
            try
            {
                cmd.Connection = con.conectar();
                dr = cmd.ExecuteReader();              
                if (dr.HasRows)
                {
                    dr.Close();
                    if (salvaip(login))
                    {
                        tem = true;
                        mensagem = "ok";
                    }                  
                }
                con.desconectar();
                dr.Close();
            }
            catch (MySqlException e)
            {

                this.mensagem = "Erro com o banco de dados " + e.Message;
            }
            return tem;
        }
        public string cadastrar (String userName, String senha, String confSenha)
        {
            // Comandos para inserir usuarios no Banco de dados
            if (senha.Equals(confSenha))
            {
                cmd.CommandText = "select * from APS5user where username = @userName";
                cmd.Parameters.AddWithValue("@userName", userName);
                try
                {
                    cmd.Connection = con.conectar();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        cadExiste = true;
                    }
                    con.desconectar();
                    dr.Close();
                }
                catch (MySqlException)
                {
                    this.mensagem = "erro ao procurar?";
                }

                if (!cadExiste)
                {
                    tem = false;
                    cmd.CommandText = " insert into APS5user values (@teste, @senha, 1);";
                    cmd.Parameters.AddWithValue("@teste", userName);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    try
                    {
                        cmd.Connection = con.conectar();
                        cmd.ExecuteNonQuery();
                        con.desconectar();
                        this.mensagem = "Cadastrado com sucesso";
                        tem = true;
                    }
                    catch (MySqlException e)
                    {
                        this.mensagem = "Erro ao cadastrar" + e.Message;
                    }
                }
                else
                {
                    this.mensagem = "Esse Username já está cadastrado, tente outro!";
                }
            }
            else
            {
                this.mensagem = "Senhas não são iguais, digite novamente";
            }
            
            return mensagem;
        }

        public string[] listaUsers(String username)
        {
            string[] lista = null;
            int linha = 0;
            cmd.CommandText = "select username, ip from APS5user where ip <> null and username <> @username";
            cmd.Parameters.AddWithValue("@username", username);
            try
            {
                cmd.Connection = con.conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista[linha] = dr.ToString();
                    linha++;
                }
                con.desconectar();
                dr.Close();
                

            }
            catch (MySqlException)
            {
                this.mensagem = "Erro com o banco de dados";
            }
            return lista;
        }
        public String pegaip (String username)
        {
            String ip = "";
            cmd.CommandText = "select ip from APS5user where username = @username";
            cmd.Parameters.AddWithValue("@username", username);
            try
            {
                cmd.Connection = con.conectar();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    ip = dr.ToString();
                    
                }
                con.desconectar();
                dr.Close();
            }
            catch (MySqlException)
            {
                this.mensagem = "Erro com o banco de dados";
            }
            return ip;
        }
        public bool salvaip (String username)
        {
            bool ok = false;
            string nome = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostAddresses(nome);
            cmd.CommandText = "Update APS5user set ip = @ip where username = @username";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@ip", ip);
            try
            {
                cmd.Connection = con.conectar();
                cmd.ExecuteNonQuery();
                con.desconectar();
                //dr.Close();
                ok = true;
            }
            catch (MySqlException e)
            {
                this.mensagem = "Erro ao salvar ip " + e.Message;
            }
            return ok;
        }
    }
}
