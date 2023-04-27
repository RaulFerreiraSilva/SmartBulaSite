using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Models
{
    public class Remedio
    {

        static MySqlConnection con = new MySqlConnection(
            "server=ESN509VMYSQL;database=db_smart_bula;user id=aluno; password=Senai1234");

        private int idMedicamento;
        private string bula, resumoBula, principioAtivo;


        public Remedio(int idMedicamento, string bula, string resumoBula, string principioAtivo)
        {
            this.idMedicamento = idMedicamento;
            this.bula = bula;
            this.resumoBula = resumoBula;
            this.principioAtivo = principioAtivo;
        }

        public int IdMedicamento { get => idMedicamento; set => idMedicamento = value; }
        public string Bula { get => bula; set => bula = value; }
        public string ResumoBula { get => resumoBula; set => resumoBula = value; }
        public string PrincipioAtivo { get => principioAtivo; set => principioAtivo = value; }

        internal static String BuscarRemedio(string principio_ativo)
        {
            try
            {
                con.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM medicamento WHERE principio_ativo = @principio_ativo", con);
                qry.Parameters.AddWithValue("@principio_ativo", principio_ativo);

                Remedio remedio = null;

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    remedio = new Remedio(
                        int.Parse(leitor["id_Medicamento"].ToString()),
                        leitor["bula"].ToString(),
                        leitor["resumo_bula"].ToString(),
                        leitor["principio_ativo"].ToString()
                        );
                }
                con.Close();
                return remedio.ToString();
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                Console.WriteLine(e);
                return "Remedio não encontrado!";
            }
        }
    }
}
