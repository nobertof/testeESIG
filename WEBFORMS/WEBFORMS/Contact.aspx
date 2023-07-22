<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WEBFORMS.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">+ Adicionar pessoa</h3>
     <div class="row">
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="txtNome" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox1" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox2" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox3" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox4" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox5" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox6" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox7" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Label runat="server" Text="Nome:" />
            <asp:TextBox runat="server" ID="TextBox8" />
         </div>
         <div class="col-md-12 col-lg-4">
            <asp:Button runat="server" ID="btnEnviar" Text="Enviar" OnClick="btnCadastrarClick" />
         </div>
        </div>
</asp:Content>
