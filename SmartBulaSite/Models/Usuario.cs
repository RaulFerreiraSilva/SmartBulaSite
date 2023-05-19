﻿using MySql.Data.MySqlClient;
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
        private int id_favorito;
        private int FK_USUARIO_id_usuario;
        private int FK_MEDICAMENTO_id_Medicamento;

        public Usuario(int id_Usuario, string nome, string sobreNome, DateTime dataNasc, string email, string senha) {
            this.id_Usuario = id_Usuario;
            this.nome = nome;
            this.sobreNome = sobreNome;
            this.dataNasc = dataNasc;
            this.email = email;
            this.senha = senha;
        }

        public Usuario(int id_favorito, int fK_USUARIO_id_usuario, int fK_MEDICAMENTO_id_Medicamento) {
            this.id_favorito = id_favorito;
            FK_USUARIO_id_usuario = fK_USUARIO_id_usuario;
            FK_MEDICAMENTO_id_Medicamento = fK_MEDICAMENTO_id_Medicamento;
        }

        public int Id_Usuario { get => id_Usuario; set => id_Usuario = value; }
        public string Nome { get => nome; set => nome = value; }
        public string SobreNome { get => sobreNome; set => sobreNome = value; }
        public DateTime DataNasc { get => dataNasc; set => dataNasc = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }

        public static Boolean favoritar(int id_Usuario, int id_Medicamento) {
            try {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();

                MySqlCommand checkQuery = new MySqlCommand("SELECT FK_USUARIO_id_usuario FROM MENDICAMENTO_FAVORITO WHERE FK_USUARIO_id_usuario = @id_usuario AND FK_MEDICAMENTO_id_Medicamento = @id_medicamento", con);
                checkQuery.Parameters.AddWithValue("@id_usuario", id_Usuario);
                checkQuery.Parameters.AddWithValue("@id_medicamento", id_Medicamento);
                object result = checkQuery.ExecuteScalar();

                if (result != null) // Se o like já existe
                {
                    MySqlCommand deleteQuery = new MySqlCommand("DELETE FROM MENDICAMENTO_FAVORITO WHERE FK_USUARIO_id_usuario = @FK_USUARIO_id_usuario AND FK_MEDICAMENTO_id_Medicamento = @id_medicamento", con);
                    deleteQuery.Parameters.AddWithValue("@FK_USUARIO_id_usuario", id_Usuario);
                    deleteQuery.Parameters.AddWithValue("@id_medicamento", id_Medicamento);
                    deleteQuery.ExecuteNonQuery();
                    return false;
                }

                MySqlCommand query = new MySqlCommand("INSERT INTO MENDICAMENTO_FAVORITO (FK_USUARIO_id_usuario, FK_MEDICAMENTO_id_Medicamento) VALUES (@id_usuario, @id_medicamento)", con);
                query.Parameters.AddWithValue("@id_usuario", id_Usuario);
                query.Parameters.AddWithValue("@id_medicamento", id_Medicamento);
                MySqlDataReader reader = query.ExecuteReader();

                con.Close();
                return true;
            } catch (Exception ex) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static List<Remedio> listaFavoritar(int id_Usuario) {
            try {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();
                MySqlCommand query = new MySqlCommand("SELECT id_medicamento, bula, resumo_bula, principio_ativo, FK_USUARIO_id_usuario FROM medicamento INNER JOIN mendicamento_favorito ON FK_USUARIO_id_usuario = @id_usuario; ", con);
                query.Parameters.AddWithValue("@id_usuario", id_Usuario);
                MySqlDataReader reader = query.ExecuteReader();
                List<Remedio> lista = new List<Remedio>();

                while (reader.Read()) {
                    lista.Add(new Remedio(
                        int.Parse(reader["id_medicamento"].ToString()),
                        reader["bula"].ToString(),
                        reader["resumo_bula"].ToString(),
                        reader["principio_ativo"].ToString()
                       ));
                }
                con.Close();
                return lista;

            } catch (Exception ex) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal String Salvar(Usuario user) {
            try {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();
                MySqlCommand qry = new MySqlCommand(
                    "INSERT INTO usuario (nome, sobreNome, dataNasc, email, senha) VALUES (@nome, @sobreNome, @dataNasc, @email, @senha)", con);
                qry.Parameters.AddWithValue("@nome", user.nome);
                qry.Parameters.AddWithValue("@sobreNome", user.sobreNome);
                qry.Parameters.AddWithValue("@dataNasc", user.dataNasc);
                qry.Parameters.AddWithValue("@email", user.email);
                qry.Parameters.AddWithValue("@senha", user.senha);

                qry.ExecuteNonQuery();
                user = Logar(user.nome, user.senha);
                con.Close();
                return "Sucesso, Cadastrado";
            } catch (Exception e) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return "Houve um Erro: " + e;
            }
        }


        internal static Usuario Logar(String email, String password) {
            try {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();

                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM usuario WHERE email = @email and senha=@senha", con);
                qry.Parameters.AddWithValue("@email", email);
                qry.Parameters.AddWithValue("@senha", password);

                Usuario user = null;

                MySqlDataReader leitor = qry.ExecuteReader();


                if (leitor.Read()) {
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
                    return null;

                con.Close();

                return user;
            } catch (Exception e) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                return null;
            }
        }

        internal static String Editar(String email, String senha, String senhaNova) {
            try {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();
                Usuario user = null;
                MySqlCommand qry = new MySqlCommand(
                    "UPDATE usuario SET senha = @senhaNova WHERE email = @email and senha = @senha", con);
                qry.Parameters.AddWithValue("@email", email);
                qry.Parameters.AddWithValue("@senha", senha);
                qry.Parameters.AddWithValue("@senhaNova", senhaNova);

                if (qry.ExecuteNonQuery() > 0)
                    user = Logar(email, senhaNova);
                else
                    return "Ocorreu um Erro no edit, Usuario não encontrado";

                con.Close();
                return "Edit Realizado com Sucesso";
            } catch (Exception e) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return "Ocorreu um Erro no edit: " + e;
            }
        }
        internal String Excluir() {
            try {
                con.Open();
                MySqlCommand qry = new MySqlCommand(
                    "DELETE FROM usuario WHERE nome = @nome and senha=@senha", con);
                qry.Parameters.AddWithValue("@nome", this.nome);
                qry.Parameters.AddWithValue("@sobreNome", this.sobreNome);

                qry.ExecuteNonQuery();
                con.Close();
                return "Excluido com Sucesso";
            } catch (Exception e) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return "Houve um erro: " + e;
            }
        }
    }
}
