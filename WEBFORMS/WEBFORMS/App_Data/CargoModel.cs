using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBFORMS.Dtos.CargoDto;
using WEBFORMS.Views.PessoaDto;

namespace WEBFORMS.App_Data
{
    public class CargoModel
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;

        public List<CargoListDto> GetList()
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<CargoListDto> cargosList = new List<CargoListDto>();
                    string sql = "select * from Cargo order by id";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Obter os dados de cada coluna dos cargos
                                int id = Convert.ToInt32(reader["ID"]);
                                string nome = reader["NOME"].ToString();

                                cargosList.Add(
                                    new CargoListDto()
                                    {
                                        Id = id,
                                        Nome = nome
                                    });


                            }
                            connection.Close();
                            return cargosList;
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