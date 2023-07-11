using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using WEBFORMS.Views.PessoaDto;

namespace WEBFORMS.App_Data
{
    public class PessoaModel
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;

        public List<PessoaListDto> GetList(long page, long quantity_per_page)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<PessoaListDto> pessoasList = new List<PessoaListDto>();
                    string sql = "select * from Pessoa order by id";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Obter os dados de cada coluna da pessoa
                                int id = Convert.ToInt32(reader["ID"]);
                                string nome = reader["NOME"].ToString();

                                pessoasList.Add(
                                    new PessoaListDto() {
                                        Id=id,
                                        Nome = nome
                                    });


                            }
                                connection.Close();
                                return pessoasList;
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }
    }
}