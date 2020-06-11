using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1.Modelo
{
    public class Controle
    {
        public bool tem = false;
        public String mensagem = "";
        public static string esteUser;
        //SqlCommand cmd = new SqlCommand();
        public bool Acessar (String login, String senha)
        {
            esteUser = login;
            DaoComandos loginDao = new DaoComandos();
            tem = loginDao.verificarLogin(login, senha);
            if (!loginDao.mensagem.Equals("ok"))
            {
                this.mensagem = loginDao.mensagem;
            }
            return tem;
        }

        public String Cadastrar (String userName, String senha, String confSenha)
        {
            this.tem = false;
            DaoComandos cadastroDao = new DaoComandos();
            this.mensagem = cadastroDao.cadastrar(userName, senha, confSenha);
            if (cadastroDao.tem)
            {
                this.tem = true;
            }
            return mensagem;
        }
        public string[] pegaUsers()
        {
            DaoComandos usersDao = new DaoComandos();
            string[] users = usersDao.listaUsers(esteUser);
            if (users == null)
            {
                MessageBox.Show("nenhum usuario encontrado");
            }
            return users;
        }
        public String pegaIP(String username)
        {
            DaoComandos pegaIp = new DaoComandos();
            String ip = pegaIp.pegaip(username);
            return ip;
        }
    }
}
