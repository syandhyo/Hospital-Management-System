<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Provisional_Bill.aspx.cs" Inherits="RECEPTION_Provisional_Bill" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function Validate() {
              if (document.getElementById("<%=txtspid.ClientID%>").value == "") {
                  alert("* Please!!Enter The IPD No..");
                 document.getElementById("<%=txtspid.ClientID%>").focus();
                 return false;
             }
         }
    </script>
    <script type="text/javascript">
        function Validate1() {
            if (document.getElementById("<%=TXTUHID.ClientID%>").value == "") {
                alert("* Please!!Enter The UHID No..");
                  document.getElementById("<%=TXTUHID.ClientID%>").focus();
                 return false;
             }
         }
    </script>
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
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         
       <div>
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">Provisional Bill</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
           <div id="div1" runat="server">

            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By IPD NO.:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtspid" runat="server" BackColor="#CCFFFF" ></asp:TextBox>
         &nbsp;<asp:Button ID="btnshow" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" OnClientClick="return Validate();"/>
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By UHID.:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="TXTUHID" runat="server" BackColor="#CCFFFF" ></asp:TextBox>
         &nbsp;<asp:Button ID="Button1" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnUHID_Click" OnClientClick="return Validate1();"/>
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
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

