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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (gridView != null)
            {
            PessoaModel pessoa = new PessoaModel();
            gridView.DataSource = pessoa.GetList(gridView.PageIndex+1, 10);
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
    }
}