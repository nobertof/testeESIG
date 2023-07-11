<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEBFORMS._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">Pessoas <a runat="server" href="~/" class="btn btn-success">+ Adicionar</a></h3>
    <asp:GridView 
        ID="gridView" 
        runat="server" 
        AutoGenerateColumns="False" 
        AllowPaging="true" 
        OnPageIndexChanging="GridView_PageIndexChanging" 
        CssClass="tabela" 
        PagerStyle-CssClass="pagination" PagerStyle-PageButtonCssClass="pagination-link">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
        </Columns>
    </asp:GridView>
    


</asp:Content>
