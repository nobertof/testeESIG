<%@ Page Title="Cadastro de Pessoas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroPessoas.aspx.cs" Inherits="WEBFORMS.CadastroPessoas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titleCadastro">
        <a runat="server" href="ListaPessoas.aspx" class="btn btn-secondary"><span class="glyphicon glyphicon-arrow-left"></span>Voltar</a>
        <h3 class="title">+ Adicionar pessoa</h3>
    </div>
    <div class="row">
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="txtNome" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="E-mail:" />
            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Cargo:" />
            <asp:DropDownList ID="ddlCargo" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Telefone:" />
            <asp:TextBox runat="server" ID="txtTelefone" CssClass="form-control" />
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTelefone" Mask="(99)99999-9999" MaskType="Number" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Data de nascimento:" />
            <asp:TextBox runat="server" TextMode="Date" ID="txtDataNascimento" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Cep:" />
            <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control" OnTextChanged="txtCEP_TextChanged" AutoPostBack="true"></asp:TextBox>
            <ajaxToolkit:MaskedEditExtender ID="mascaraCEP" runat="server" TargetControlID="txtCEP" Mask="99999-999" MaskType="Number" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="País:" />
            <asp:TextBox runat="server" ID="txtPais" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Cidade:" />
            <asp:TextBox runat="server" ID="txtCidade" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Endereço:" />
            <asp:TextBox runat="server" ID="txtEndereco" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-lg-12 btnSubmitPessoa ">
            <asp:Button runat="server" ID="btnEnviar" Text="Salvar" OnClick="btnCadastrarClick" CssClass="btn btn-success" />
        </div>
    </div>
</asp:Content>
