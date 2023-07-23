using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEBFORMS.App_Data;

namespace WEBFORMS
{
    public partial class _Default : Page
    {
        protected void ExibirAlerta(string titulo, string mensagem, string tipo)
        {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", $"showAlert('{titulo}', '{mensagem}', '{tipo}');", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (gridView != null)
            {
            PessoaModel pessoa = new PessoaModel();
            gridView.DataSource = pessoa.GetList();
            gridView.DataBind();
            }
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (gridView != null)
            {
            gridView.PageIndex = e.NewPageIndex;
            gridView.DataBind();
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
            }else if(e.CommandName == "Excluir")
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
    }
}