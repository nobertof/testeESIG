<%@ Page Title="Lista de pessoas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaPessoas.aspx.cs" Inherits="WEBFORMS._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">Pessoas </h3>
    <div class="optionsListaPessoas">
        <div class="pesquisa">
            <div >
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="txtNome" CssClass="form-control" />
                </div>
            <asp:Button runat="server" ID="Button1" Text="Pesquisar" OnClick="PesquisarPessoa" CssClass="btn btn-secondary" />
        </div>
        <div>
            <asp:Button runat="server" ID="btnEnviar" Text="Calcular Salários" OnClick="CalcularSalarios" CssClass="btn btn-secondary" />
            <a runat="server" href="CadastroPessoas.aspx" class="btn btn-success">+ Adicionar</a>
        </div>
    </div>
    <asp:GridView
        ID="gridView"
        runat="server"
        AutoGenerateColumns="False"
        AllowPaging="true"
        OnPageIndexChanging="GridView_PageIndexChanging"
        OnRowCommand="GridView1_RowCommand"
        CssClass="table"
        PagerStyle-CssClass="pagination-data"
        PagerStyle-PageButtonCssClass="pagination-link" PageSize="6">
        <Columns>
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>

                    <asp:LinkButton
                        ID="editRow"
                        runat="server"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("ID") %>'
                        CssClass="btn btn-warning mt-0">
                <span class="glyphicon glyphicon-pencil"></span>
                    </asp:LinkButton>
                    <asp:LinkButton
                        ID="LinkButton1"
                        runat="server"
                        CommandName="Excluir"
                        CommandArgument='<%# Eval("ID") %>'
                        CssClass="btn btn-danger">
                    <span class="glyphicon glyphicon-trash"></span>
                    </asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
            <asp:BoundField DataField="Salario" HeaderText="Salario" />
        </Columns>
    </asp:GridView>



</asp:Content>
