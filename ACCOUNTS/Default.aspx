<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ACCOUNTS_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="layout-main-content">
        <input type="hidden" name="j_idt79" value="j_idt79" />
        <img src="../images/account-background.jpg" width="100%" height="auto">
        <div class="ui-fluid">
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <input type="hidden" name="javax.faces.ViewState" id="j_id1:javax.faces.ViewState:130" value="-6706802491831525269:5066372470393129965" autocomplete="off" />
    </div>
</asp:Content>