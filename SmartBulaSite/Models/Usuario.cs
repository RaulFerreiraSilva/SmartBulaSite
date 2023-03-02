using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Models
{
    public class Usuario
    {
        static MySqlConnection con = new MySqlConnection(
            "server=ESN509VMYSQL;database=php;user id=aluno; password=Senai1234");
        private string nome, sobreNome, email, password;
        private DateTime data;

        public Usuario(string nome, string sobreNome, string email, string password, DateTime data) {
            this.nome = nome;
            this.sobreNome = sobreNome;
            this.email = email;
            this.password = password;
            this.data = data;
        }

        internal static List<Usuario> Listar() {
            try {
                con.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM Pessoas", con);

                List<Usuario> lista = new List<Usuario>();

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read()) {
                    lista.Add(new Usuario(
                        leitor["nome"].ToString(),
                        leitor["sobreNome"].ToString(),
                        leitor["email"].ToString(),
                        leitor["password"].ToString(),
                        DateTime.Parse(leitor["data"].ToString())
                        ));
;
                }
                con.Close();
                return lista;
            } catch (Exception e) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }


    }
}
