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
        private DateTime data;
        private int id_Usuario;

        public Usuario(int id_Usuario, string nome, string sobreNome, DateTime data, string email, string senha)
        {
            this.id_Usuario = id_Usuario;
            this.nome = nome;
            this.sobreNome = sobreNome;
            this.data = data;
            this.email = email;
            this.senha = senha;
        }

        public int Id_Usuario { get => id_Usuario; set => id_Usuario = value; }
        public string Nome { get => nome; set => nome = value; }
        public string SobreNome { get => sobreNome; set => sobreNome = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public DateTime Data { get => data; set => data = value; }

        internal static Usuario Logar(String userName, String password)
        {
            try
            {
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
