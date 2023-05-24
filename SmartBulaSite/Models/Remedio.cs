using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Models
{
    public class Remedio
    {
        //Criando conexão
        static MySqlConnection con = new MySqlConnection(
            "server=ESN509VMYSQL;database=db_smart_bula_v2;user id=aluno; password=Senai1234");

        //Criando variaveis
        private int idMedicamento;
        private string bula, resumoBula, principioAtivo, contraIndicacao, recomendadoPara;

        //Criando Construtor 
        public Remedio(int idMedicamento, string bula, string resumoBula, string contraIndicacao, string recomendadoPara, string principioAtivo)
        {
            this.idMedicamento = idMedicamento;
            this.bula = bula;
            this.resumoBula = resumoBula;
            this.contraIndicacao = contraIndicacao;
            this.recomendadoPara = recomendadoPara;
            this.principioAtivo = principioAtivo;
        }

        //Criando Encapsulamento
        public int IdMedicamento { get => idMedicamento; set => idMedicamento = value; }
        public string Bula { get => bula; set => bula = value; }
        public string ResumoBula { get => resumoBula; set => resumoBula = value; }
        public string ContraIndicacao { get => contraIndicacao; set => contraIndicacao = value; }
        public string RecomendadoPara { get => recomendadoPara; set => recomendadoPara = value; }
        public string PrincipioAtivo { get => principioAtivo; set => principioAtivo = value; }

        internal static Remedio BuscarRemedio(string principio_ativo)
        {
            try
            {
                if (!(con.State == System.Data.ConnectionState.Open))
                    con.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM medicamento WHERE principio_ativo = @principio_ativo", con);
                qry.Parameters.AddWithValue("@principio_ativo", principio_ativo);

                Remedio remedio = null;// Cria um remedio vazio, para receber o remedio do banco.

                MySqlDataReader leitor = qry.ExecuteReader();// Executa o script de busca de remedio do banco.

                if (leitor.Read())
                {
                    remedio = new Remedio(
                        int.Parse(leitor["id_Medicamento"].ToString()),
                        leitor["bula"].ToString(),
                        leitor["resumo_bula"].ToString(),
                        leitor["contra_indicacao"].ToString(),
                        leitor["recomendado_para"].ToString(),
                        leitor["principio_ativo"].ToString()
                        ); //preenche o remedio, que estava vazio.
                }
                else //Caso ele não consiga ler, retorna um remedio vazio. 
                {
                    con.Close();
                    return null;
                }
                con.Close();
                return remedio;
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                Console.WriteLine(e);
                return null;
            }
        }
    }
}
