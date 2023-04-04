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
            "server=ESN509VMYSQL;database=db_smart_bula;user id=aluno; password=Senai1234");
        private string nome, sobreNome, email, senha;
        private DateTime dataNasc;
        private int id_Usuario;

        public Usuario(int id_Usuario, string nome, string sobreNome, DateTime dataNasc, string email, string senha)
        {
            this.id_Usuario = id_Usuario;
            this.nome = nome;
            this.sobreNome = sobreNome;
            this.dataNasc = dataNasc;
            this.email = email;
            this.senha = senha;
        }

        public int Id_Usuario { get => id_Usuario; set => id_Usuario = value; }
        public string Nome { get => nome; set => nome = value; }
        public string SobreNome { get => sobreNome; set => sobreNome = value; }
        public DateTime DataNasc { get => dataNasc; set => dataNasc = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
      


        internal Usuario Salvar()
        {
            try
            {
                con.Open();
                Usuario user = null;
                MySqlCommand qry = new MySqlCommand(
                    "INSERT INTO usuario (nome, sobreNome, dataNasc, email, senha) VALUES (@nome, @sobreNome, @dataNasc, @email, @senha)", con);
                qry.Parameters.AddWithValue("@nome", this.nome);
                qry.Parameters.AddWithValue("@sobreNome",this.sobreNome);
                qry.Parameters.AddWithValue("@dataNasc", this.dataNasc);
                qry.Parameters.AddWithValue("@email", this.email);
                qry.Parameters.AddWithValue("@senha", this.senha);

                qry.ExecuteNonQuery();
                user = Logar(this.nome, this.senha);
                con.Close();
                return user;
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }


        internal static Usuario Logar(String userName, String password)
        {
            try
            {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();

                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM usuario WHERE nome = @nome and senha=@senha", con);
                qry.Parameters.AddWithValue("@nome", userName);
                qry.Parameters.AddWithValue("@senha", password);

                Usuario user = null;

                MySqlDataReader leitor = qry.ExecuteReader();


                if (leitor.Read())
                {
                    user = new Usuario(
                          int.Parse(leitor["id_usuario"].ToString()),
                          leitor["nome"].ToString(),
                          leitor["sobreNome"].ToString(),
                          DateTime.Parse(leitor["dataNasc"].ToString()),
                          leitor["email"].ToString(),
                          leitor["senha"].ToString()
                          );
                }


                user.Senha = leitor.GetString("senha");

                if (!user.Senha.Equals(password))
                    return user;

                con.Close();

                return user;
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                return null;
            }
        }

        internal static Usuario Editar(int idUser, String userName, String lastName, DateTime data, String email, String password)
        {
            try
            {
                con.Open();
                Usuario user = null;
                MySqlCommand qry = new MySqlCommand(
                    "UPDATE usuario SET nome = @nome, sobreNome = @sobreNome, dataNasc = @dataNasc, email = @email, senha = @senha WHERE id_usuario = @id", con);
                qry.Parameters.AddWithValue("@id", idUser);
                qry.Parameters.AddWithValue("@nome", userName);
                qry.Parameters.AddWithValue("@sobreNome", lastName);
                qry.Parameters.AddWithValue("@dataNasc", data);
                qry.Parameters.AddWithValue("@email", email);
                qry.Parameters.AddWithValue("@senha", password);

                qry.ExecuteNonQuery();

                if (qry.ExecuteNonQuery() > 0)
                    user = Logar(userName, password);
                else
                    return null;

                con.Close();
                return user;
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }
        internal static Usuario Excluir(String userName, String password)
        {
            try
            {
                con.Open();

                Usuario user = null;

                MySqlCommand qry = new MySqlCommand(
                    "DELETE FROM usuario WHERE nome = @nome and senha=@senha", con);
                qry.Parameters.AddWithValue("@nome", userName);
                qry.Parameters.AddWithValue("@senha", password);

                qry.ExecuteNonQuery();
                con.Close();
                return user;
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }
    }
}
