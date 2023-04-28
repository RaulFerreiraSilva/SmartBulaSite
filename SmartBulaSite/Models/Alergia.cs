using MySql.Data.MySqlClient;
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

        public Alergia(string tipo_Alergia, int id_Alergia)
        {
            this.Tipo_Alergia = tipo_Alergia;
            this.Id_Alergia = id_Alergia;
        }

        public string Tipo_Alergia { get => tipo_Alergia; set => tipo_Alergia = value; }
        public int Id_Alergia { get => id_Alergia; set => id_Alergia = value; }

        public String cadastrarAlergia()
        {
            try
            {
                con.Open();
                MySqlCommand query = new MySqlCommand("INSERT INTO alergia (tipo_alergia) VALUES(@tipo_alergia)", con);
                query.Parameters.AddWithValue("@tipo_alergia", this.tipo_Alergia);
                MySqlDataReader reader = query.ExecuteReader();


                con.Close();
                return "Alergia Cadastrada com sucesso!";
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                Console.WriteLine(ex.Message);
                return "Erro ao cadastrar: " + ex;
            }
        }

        public Boolean alergiaUsuario(int id_Usuario)
        {
            try
            {
                con.Open();
                MySqlCommand query = new MySqlCommand("INSERT INTO alergia_usuario (FK_ALERGIA_id_alergia, FK_USUARIO_id_usuario) VALUES(@id_alergia,@id_usuario)", con);
                query.Parameters.AddWithValue("@id_usuario", id_Usuario);
                query.Parameters.AddWithValue("@id_alergia", this.id_Alergia);
                MySqlDataReader reader = query.ExecuteReader();


                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}
