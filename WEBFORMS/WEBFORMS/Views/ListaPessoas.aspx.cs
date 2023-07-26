using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEBFORMS.App_Data;
using WEBFORMS.Views.PessoaDto;

namespace WEBFORMS
{
    public partial class _Default : Page
    {
        protected void ExibirAlerta(string titulo, string mensagem, string tipo)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", $"showAlert('{titulo}', '{mensagem}', '{tipo}');", true);
        }
        private DataTable ajustarListaDePessoas(List<PessoaListDto> pessoas)
        {
            DataTable dataTable = new DataTable();

            // Definir as colunas do DataTable
            dataTable.Columns.Add("Id", typeof(string));
            dataTable.Columns.Add("Nome", typeof(string));
            dataTable.Columns.Add("Cargo", typeof(string));
            dataTable.Columns.Add("Salario", typeof(string));

            // Adicionar as linhas ao DataTable com os valores do objeto Pessoa
            foreach (PessoaListDto pessoa in pessoas)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = pessoa.Id;
                row["Nome"] = pessoa.Nome;
                row["Cargo"] = pessoa.Cargo;
                row["Salario"] = pessoa.Salario;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        private void BindGridViewData()
        {
            if (ViewState["GridViewData"] != null)
            {
                // Se os dados da consulta estão armazenados no ViewState, utilize-os para popular a GridView
                gridView.DataSource = (DataTable)ViewState["GridViewData"];
                gridView.DataBind();
            }
            else
            {
                // Realize a consulta normalmente e armazene os dados no ViewState
                PessoaModel pessoa = new PessoaModel();
                DataTable listaPessoas = ajustarListaDePessoas(pessoa.GetList());
                gridView.DataSource = listaPessoas;
                ViewState["GridViewData"] = listaPessoas;
                gridView.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && gridView != null)
            {
                BindGridViewData();
            }
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (gridView != null)
            {
                gridView.PageIndex = e.NewPageIndex;
                BindGridViewData();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // pegando o id que esta sendo passado como argumento da tabela
                string id = e.CommandArgument.ToString();

                // Redirecionando para a pagina de cadastro/ edição de pessoas
                Response.Redirect("CadastroPessoas.aspx?id=" + id);
            }
            else if (e.CommandName == "Excluir")
            {
                // pegando o id da pessoa que vai ser removida
                string id = e.CommandArgument.ToString();
                PessoaModel pessoa = new PessoaModel();
                ExibirAlerta("Sucesso!", pessoa.Remove(Convert.ToInt32(id)), "success");
                if (gridView != null)
                {
                    gridView.DataSource = pessoa.GetList();
                    gridView.DataBind();
                }
            }
        }

        protected async void CalcularSalarios(object sender, EventArgs e)
        {
            try
            {

                PessoaModel pessoa = new PessoaModel();
                string resultado = await pessoa.CalcularSalarios();
                ExibirAlerta("Sucesso!", resultado, "success");
                if (gridView != null)
                {
                    gridView.DataSource = pessoa.GetList();
                    gridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExibirAlerta("Aviso!", ex.Message, "error");
            }
            finally
            {
                //Escondendo loading na tela
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideLoading", "hideLoading();", true);
            }
        }

        protected async void PesquisarPessoa(object sender, EventArgs e)
        {
            try
            {
                PessoaModel pessoa = new PessoaModel();
                if (gridView != null)
                {
                    List<PessoaListDto> pessoas = pessoa.GetPessoaByNome(txtNome.Text);
                    DataTable listaPessoas = ajustarListaDePessoas(pessoas.Any() ? pessoas : pessoa.GetList());
                    gridView.DataSource = listaPessoas;
                    ViewState["GridViewData"] = listaPessoas;
                    gridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExibirAlerta("Aviso!", ex.Message, "error");
            }
        }
    }
}