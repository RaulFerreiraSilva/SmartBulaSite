﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Models
{
    public class Alergia
    {
        static MySqlConnection con = new MySqlConnection(
          "server=ESN509VMYSQL;database=db_smart_bula;user id=aluno; password=Senai1234");
        private string tipo_Alergia;
        private int id_Alergia;
        private int id_alergia_user;
        private int FK_ALERGIA_id_alergia;
        private int FK_USUARIO_id_usuario;

        public Alergia(string tipo_Alergia, int id_Alergia) {
            this.Tipo_Alergia = tipo_Alergia;
            this.Id_Alergia = id_Alergia;
        }

        public Alergia(int id_alergia_user, int fK_ALERGIA_id_alergia, int fK_USUARIO_id_usuario) {
            this.id_alergia_user = id_alergia_user;
            FK_ALERGIA_id_alergia = fK_ALERGIA_id_alergia;
            FK_USUARIO_id_usuario = fK_USUARIO_id_usuario;
        }

        public string Tipo_Alergia { get => tipo_Alergia; set => tipo_Alergia = value; }
        public int Id_Alergia { get => id_Alergia; set => id_Alergia = value; }
        public int Id_alergia_user { get => id_alergia_user; set => id_alergia_user = value; }
        public int FK_ALERGIA_id_alergia1 { get => FK_ALERGIA_id_alergia; set => FK_ALERGIA_id_alergia = value; }
        public int FK_USUARIO_id_usuario1 { get => FK_USUARIO_id_usuario; set => FK_USUARIO_id_usuario = value; }

        public String cadastrarAlergia() {
            try {
                con.Open();
                MySqlCommand query = new MySqlCommand("INSERT INTO alergia (tipo_alergia) VALUES(@tipo_alergia)", con);
                query.Parameters.AddWithValue("@tipo_alergia", this.tipo_Alergia);
                MySqlDataReader reader = query.ExecuteReader();


                con.Close();
                return "Alergia Cadastrada com sucesso!";
            } catch (Exception ex) {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                Console.WriteLine(ex.Message);
                return "Erro ao cadastrar: " + ex;
            }
        }

        public Boolean alergiaUsuario(int id_Usuario) {
            try {
                con.Open();
                MySqlCommand query = new MySqlCommand("INSERT INTO alergia_usuario (FK_ALERGIA_id_alergia, FK_USUARIO_id_usuario) VALUES(@id_alergia,@id_usuario)", con);
                query.Parameters.AddWithValue("@id_usuario", id_Usuario);
                query.Parameters.AddWithValue("@id_alergia", this.id_Alergia);
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

        internal static List<Alergia> listar() {
            try {
                con.Open();
                MySqlCommand query = new MySqlCommand("SELECT * FROM Alergia", con);
                List<Alergia> lista = new List<Alergia>();
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read()) {
                    lista.Add(new Alergia(
                        reader["tipo_alergia"].ToString(),
                        int.Parse(reader["id_alergia"].ToString())
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

        internal static List<Alergia> listarAlergiaUsuario(int usuarioId) {
            try {
                con.Open();
                MySqlCommand query = new MySqlCommand("SELECT * FROM alergia_usuario where FK_USUARIO_id_usuario = @FK_USUARIO_id_usuario ", con);
                query.Parameters.AddWithValue("@FK_USUARIO_id_usuario", usuarioId);
                List<Alergia> lista = new List<Alergia>();
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read()) {
                    lista.Add(new Alergia(
                        int.Parse(reader["id_alergia_user"].ToString()),
                        int.Parse(reader["fK_ALERGIA_id_alergia"].ToString()),
                        int.Parse(reader["fK_USUARIO_id_usuario"].ToString())
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
    }
}
