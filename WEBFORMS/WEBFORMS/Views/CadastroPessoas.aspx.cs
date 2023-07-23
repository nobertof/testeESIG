using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEBFORMS.App_Data;
using WEBFORMS.Dtos.CargoDto;
using WEBFORMS.Dtos.PessoaDto;
using WEBFORMS.Views.PessoaDto;

namespace WEBFORMS
{
    public partial class CadastroPessoas : Page
    {

        protected void ExibirAlerta(string titulo, string mensagem, string tipo)
        {
            if (tipo == "success")
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", $"showAlert('{titulo}', '{mensagem}', '{tipo}'); setTimeout(()=>window.location.href = 'ListaPessoas.aspx',2000);", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", $"showAlert('{titulo}', '{mensagem}', '{tipo}');", true);
        }
        private void CarregaDadosPessoa(long id)
        {
            try
            {
                //buscando a pessoa pelo seu id
                PessoaModel pessoa = new PessoaModel();
                PessoaItemDto pessoaBuscada = pessoa.GetItem(id);

                //Preenchendo os campos com os valores buscados da pessoa
                txtNome.Text = pessoaBuscada.Nome;
                txtEmail.Text = pessoaBuscada.Email;
                ddlCargo.SelectedValue = pessoaBuscada.Cargo_Id;
                txtTelefone.Text = pessoaBuscada.Telefone;
                txtDataNascimento.Text = pessoaBuscada.Data_Nascimento;
                txtCEP.Text = pessoaBuscada.CEP;
                txtPais.Text = pessoaBuscada.Pais;
                txtCidade.Text = pessoaBuscada.Cidade;
                txtEndereco.Text = pessoaBuscada.Endereco;



            }
            catch (Exception ex)
            {
                ExibirAlerta("Aviso!", ex.Message, "error");
            }
        }

        private void CarregarCargos()
        {
            //pegando os cargos do banco de dados
            CargoModel cargo = new CargoModel();
            List<CargoListDto> listaCargos = cargo.GetList();

            //Limpando os itens do dropdownlist caso exista alguma coisa
            ddlCargo.Items.Clear();

            // Adicionando os cargos ao dropdownlist
            ddlCargo.Items.Add(new ListItem("Selecione", ""));
            foreach (CargoListDto itemCargo in listaCargos)
            {
                ddlCargo.Items.Add(new ListItem(itemCargo.Nome.ToString(), itemCargo.Id.ToString()));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCargos();
                if (Request.QueryString["id"] != null)
                {
                    CarregaDadosPessoa(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }


        protected void btnCadastrarClick(object sender, EventArgs e)
        {
            try
            {
                PessoaItemDto novaPessoa = new PessoaItemDto()
                {
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    Cargo_Id = ddlCargo.SelectedValue,
                    Telefone = txtTelefone.Text,
                    Data_Nascimento = DateTime.ParseExact(txtDataNascimento.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"),
                    CEP = txtCEP.Text,
                    Pais = txtPais.Text,
                    Cidade = txtCidade.Text,
                    Endereco = txtEndereco.Text
                };

                //Conferindo se a pessoa esta sendo editada ou cadastrada como nova
                if (Request.QueryString["id"] != null)
                {
                    novaPessoa.Id = Convert.ToInt32(Request.QueryString["id"]);
                }

                PessoaModel pessoa = new PessoaModel();
                ExibirAlerta("Sucesso!", pessoa.InsertAndUpdate(novaPessoa), "success");

            }
            catch (Exception ex)
            {
                ExibirAlerta("Aviso!", ex.Message, "error");
            }
        }


        protected void txtCEP_TextChanged(object sender, EventArgs e)
        {
            //Deixando o cep sem a o traço
            string cep = txtCEP.Text.Replace("-", "");

            //Conferindo se o tamanho do cep está no limite
            if (cep.Length == 8 && int.TryParse(cep, out _))
            {
                //Ajustando URL da requisição
                string url = $"https://viacep.com.br/ws/{cep}/json/";

                //Realizando a requisição ao via cep para carregar os dados
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        string json = wc.DownloadString(url);

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        ViaCepResult result = serializer.Deserialize<ViaCepResult>(json);
                        if (result.Erro)
                        {
                            throw new ArgumentException("Cep não encontrato!");
                        }

                        txtPais.Text = "Brasil";
                        txtCidade.Text = result.Localidade;
                        txtEndereco.Text = result.Logradouro + ", " + result.Bairro;
                    }
                    catch (Exception ex)
                    {
                        //Exibe erro na tela caso o CEP não seja encontrato
                        ExibirAlerta("Aviso!", ex.Message, "error");
                    }
                }
            }
        }
    }
}