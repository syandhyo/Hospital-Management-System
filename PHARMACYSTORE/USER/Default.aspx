<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PHARMACYSTORE_USER_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>

    </td>
         <td><img src="../../images/pharmacy.jpg" height="630px" width="1180px" style="margin-top:-50px;margin-left:-25px;"></td>
</tr>
</table>

</asp:Content>

