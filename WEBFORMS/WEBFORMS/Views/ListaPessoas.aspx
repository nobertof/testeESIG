<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaPessoas.aspx.cs" Inherits="WEBFORMS._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="title">Pessoas <a runat="server" href="CadastroPessoas.aspx" class="btn btn-success">+ Adicionar</a></h3>
    <asp:GridView 
        ID="gridView" 
        runat="server" 
        AutoGenerateColumns="False" 
        AllowPaging="true" 
        OnPageIndexChanging="GridView_PageIndexChanging" 
        OnRowCommand="GridView1_RowCommand"
        CssClass="table" 
        PagerStyle-CssClass="pagination-data" 
        PagerStyle-PageButtonCssClass="pagination-link" PageSize="6"
        >
        <Columns>
             <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>

                <asp:LinkButton 
                    ID="editRow" 
                    runat="server" 
                    CommandName="Editar" 
                    CommandArgument='<%# Eval("ID") %>'
                    Text="📝" 
                    CssClass="btn btn-warning mt-0" 
                    />
                <asp:LinkButton 
                    ID="LinkButton1" 
                    runat="server" 
                    CommandName="Excluir" 
                    CommandArgument='<%# Eval("ID") %>'
                    Text="🗑️" 
                    CssClass="btn btn-danger" 
                    />

            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
        </Columns>
    </asp:GridView>



</asp:Content>
