<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="Radio_Resultrequbill.aspx.cs" Inherits="RADIOLOGY_Radio_Resultrequbill" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
       <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
            <asp:Label ID="lblSesion" runat="server" Text="Label" Visible="false"></asp:Label> 
       
    </td>
</tr>
</table>
          <table width="100%" >
        <tr>
            <td style="padding-left:140px;"> <asp:Button ID="btnPrint" runat="server" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click" /></td>
        </tr>
    </table>
  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None"/>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

