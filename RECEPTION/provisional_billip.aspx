<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="provisional_billip.aspx.cs" Inherits="RECEPTION_provisional_billip" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         
       <div>
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center" fontsize="16px"><strong>Provisional Bill</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
          
       </div>
   
    <div>
          <table width="100%" >
        <tr>
            <td style="padding-left:140px;"> <asp:Button ID="btnPrint" runat="server" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click" visible="false" /></td>
        </tr>
    </table>
        <table width="100%">
     <tr>
    <td style="width:100%" align="center"> 
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" BorderColor="Red" BorderStyle="None" ToolbarStyle-BackColor="#E2E4DE" ToolbarStyle-BorderColor="#FF5050"/>
    </td>
     
   
  
</tr>
</table>
        <table width="100%">
     <tr>
    <td align="center">
        &nbsp;</td>
</tr>
</table>
    </div>
</asp:Content>

