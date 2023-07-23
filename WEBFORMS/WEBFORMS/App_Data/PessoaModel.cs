using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.UI;
using WEBFORMS.Dtos.PessoaDto;
using WEBFORMS.Views.PessoaDto;

namespace WEBFORMS.App_Data
{
    public class PessoaModel
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;

        public List<PessoaListDto> GetList()
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<PessoaListDto> pessoasList = new List<PessoaListDto>();
                    string sql = "select p.ID, p.NOME,c.Nome CARGO, (select s.salario_bruto from pessoa_salario s where s.pessoa_id=p.id) SALARIO from pessoa p inner join cargo c on c.Id = p.cargo_Id order by p.ID";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Preenchendo a lista de pessoas
                                pessoasList.Add(
                                    new PessoaListDto()
                                    {
                                        Id = Convert.ToInt32(reader["ID"]),
                                        Nome = reader["NOME"].ToString(),
                                        Cargo = reader["CARGO"].ToString(),
                                        Salario = reader["SALARIO"].ToString() == "" ? "Não calculado" : reader["SALARIO"].ToString()
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
        public PessoaItemDto GetItem(long id)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<PessoaListDto> pessoasList = new List<PessoaListDto>();
                    string sql = $"select * from pessoa where id={id}";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            PessoaItemDto pessoaBuscada = null;
                            while (reader.Read())
                            {
                                pessoaBuscada = new PessoaItemDto()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString(),
                                    Cidade = reader["Cidade"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    CEP = reader["CEP"].ToString(),
                                    Endereco = reader["Endereco"].ToString(),
                                    Pais = reader["Pais"].ToString(),
                                    Usuario = reader["Usuario"].ToString(),
                                    Telefone = reader["Telefone"].ToString(),
                                    Data_Nascimento = Convert.ToDateTime(reader["Data_Nascimento"]).ToString("yyyy-MM-dd"),
                                    Cargo_Id = reader["Cargo_Id"].ToString()

                                };
                            }
                            connection.Close();
                            if (pessoaBuscada == null)
                            {
                                throw new ArgumentException("Pessoa não encontrada!");
                            }
                            return pessoaBuscada;
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }
        public string InsertAndUpdate(PessoaItemDto model)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<PessoaListDto> pessoasList = new List<PessoaListDto>();
                    string sql = "";

                    if (model.Id == null)
                    {
                        sql = "INSERT INTO pessoa (NOME, CIDADE, EMAIL, CEP, ENDERECO, PAIS, TELEFONE, DATA_NASCIMENTO, CARGO_ID) " +
                              "VALUES (:Nome, :Cidade, :Email, :CEP, :Endereco, :Pais, :Telefone, :Data_Nascimento, :Cargo_Id)";
                    }
                    else
                    {
                        sql = "UPDATE pessoa " +
                          "SET NOME = :Nome, CIDADE = :Cidade, EMAIL = :Email, CEP = :CEP, ENDERECO = :Endereco, PAIS = :Pais, " +
                          "TELEFONE = :Telefone, DATA_NASCIMENTO = :Data_Nascimento, CARGO_ID = :Cargo_Id " +
                          "WHERE ID = :Id";
                    }

                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {

                        //Substituindo as variaveis em seus devidos campos
                        command.Parameters.Add("Nome", OracleDbType.Varchar2).Value = model.Nome;
                        command.Parameters.Add("Cidade", OracleDbType.Varchar2).Value = model.Cidade;
                        command.Parameters.Add("Email", OracleDbType.Varchar2).Value = model.Email;
                        command.Parameters.Add("CEP", OracleDbType.Varchar2).Value = model.CEP;
                        command.Parameters.Add("Endereco", OracleDbType.Varchar2).Value = model.Endereco;
                        command.Parameters.Add("Pais", OracleDbType.Varchar2).Value = model.Pais;
                        command.Parameters.Add("Telefone", OracleDbType.Varchar2).Value = model.Telefone;
                        command.Parameters.Add("Data_Nascimento", OracleDbType.Date).Value = DateTime.ParseExact(model.Data_Nascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        command.Parameters.Add("Cargo_Id", OracleDbType.Varchar2).Value = model.Cargo_Id;


                        if (model.Id != null)
                        {
                            command.Parameters.Add("Id", OracleDbType.Varchar2).Value = model.Id.Value;

                            int rowsUpdated = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsUpdated > 0)
                            {
                                return "Atualização bem-sucedida!";
                            }
                            else
                            {
                                throw new ArgumentException("Nenhuma linha foi atualizada.");
                            }
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                            connection.Close();
                            return "Pessoa inserida";
                        }

                    }
                }
                catch (OracleException ex)
                {
                    throw new ArgumentException("Erro ao executar comando Oracle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Erro inesperado: " + ex.Message);
                }
            }
        }
        public string Remove(long id)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<PessoaListDto> pessoasList = new List<PessoaListDto>();
                    string sql = "";

                    sql = "delete from Pessoa WHERE ID = :Id";


                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {


                        command.Parameters.Add("Id", OracleDbType.Varchar2).Value = id;

                        command.ExecuteNonQuery();
                        connection.Close();
                        return "Pessoa removida com sucesso!";

                    }
                }
                catch (OracleException ex)
                {
                    throw new ArgumentException("Erro ao executar comando Oracle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Erro inesperado: " + ex.Message);
                }
            }
        }
        public async Task<string> CalcularSalarios()
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    List<PessoaListDto> pessoasList = this.GetList();


                    string sql = "select * from CALCULAR_SALARIO(:Id)";

                    foreach (PessoaListDto pessoa in pessoasList)
                    {
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {
                            command.Parameters.Add("Id", OracleDbType.Varchar2).Value = pessoa.Id;

                            DbDataReader reader = await command.ExecuteReaderAsync();
                            PessoaSalarioDto salario_pessoa = null;
                            while (reader.Read())
                            {
                                //Preenchendo a lista de pessoas
                                salario_pessoa = new PessoaSalarioDto()
                                {
                                    salario_liquido = reader["salario_liquido"].ToString(),
                                    salario_bruto = reader["salario_bruto"].ToString(),
                                    descontos = reader["descontos"].ToString() == "" ? "Não calculado" : reader["descontos"].ToString()
                                };
                            }
                            string checandoRegistroSalario = "select * from pessoa_salario where pessoa_id=:ID";

                            using (OracleCommand comandoChecandoRegistro = new OracleCommand(checandoRegistroSalario, connection))
                            {
                                comandoChecandoRegistro.Parameters.Add("ID", OracleDbType.Varchar2).Value = pessoa.Id.ToString();
                                DbDataReader readerSalario = await comandoChecandoRegistro.ExecuteReaderAsync();
                                string salario_pessoa_id = null;
                                while (readerSalario.Read())
                                {
                                    salario_pessoa_id = readerSalario["id"].ToString();
                                }
                                string insertOrUpdateSalario = "";

                                if (String.IsNullOrEmpty(salario_pessoa_id))
                                {
                                    insertOrUpdateSalario = "INSERT INTO pessoa_salario (PESSOA_ID, NOME, SALARIO_BRUTO, DESCONTOS, SALARIO_LIQUIDO) " +
                                         "VALUES (:Pessoa_id, :Nome, :Salario_bruto, :Descontos, :Salario_liquido)";
                                }
                                else
                                {
                                    insertOrUpdateSalario = "UPDATE pessoa_salario " +
                                      "SET PESSOA_ID = :Pessoa_id, NOME = :Nome, SALARIO_BRUTO = :Salario_bruto, DESCONTOS = :Descontos, SALARIO_LIQUIDO = :Salario_liquido WHERE ID = :Id";
                                }
                                using (OracleCommand inserindoDadosDeSalario = new OracleCommand(insertOrUpdateSalario, connection))
                                {
                                    inserindoDadosDeSalario.Parameters.Add("Pessoa_id", OracleDbType.Varchar2).Value = pessoa.Id.ToString();
                                    inserindoDadosDeSalario.Parameters.Add("Nome", OracleDbType.Varchar2).Value = pessoa.Nome;
                                    inserindoDadosDeSalario.Parameters.Add("Salario_bruto", OracleDbType.Varchar2).Value = salario_pessoa.salario_bruto;
                                    inserindoDadosDeSalario.Parameters.Add("Descontos", OracleDbType.Varchar2).Value = salario_pessoa.descontos;
                                    inserindoDadosDeSalario.Parameters.Add("Salario_liquido", OracleDbType.Varchar2).Value = salario_pessoa.salario_liquido;

                                    if (!String.IsNullOrEmpty(salario_pessoa_id))
                                    {
                                        inserindoDadosDeSalario.Parameters.Add("Id", OracleDbType.Varchar2).Value = salario_pessoa_id;

                                        int rowsUpdated = await inserindoDadosDeSalario.ExecuteNonQueryAsync();

                                        if (!(rowsUpdated > 0))
                                        {
                                            throw new ArgumentException("Erro ao atualizar salario.");
                                        }
                                    }
                                    else
                                    {
                                        await inserindoDadosDeSalario.ExecuteNonQueryAsync();
                                    }
                                }

                            }



                        }
                    }
                    connection.Close();
                    return "Salarios calculados com sucesso!";

                }
                catch (OracleException ex)
                {
                    throw new ArgumentException("Erro ao executar comando Oracle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Erro inesperado: " + ex.Message);
                }
            }
        }
    }
}