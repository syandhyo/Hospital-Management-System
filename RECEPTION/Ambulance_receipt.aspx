<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Ambulance_receipt.aspx.cs" Inherits="RECEPTION_Ambulance_receipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #div1 {
            background-color: #FFFFFF;
        }
        .auto-style1 {
            text-decoration: underline;
        }
        .auto-style2 {
            width: 21%;
        }
    </style>
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("div1").innerHTML;
            var printWindow = window.open('', '', 'height=200,width=400');
            printWindow.document.write('<html><head><title>AmbulanceReceipt</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="div1" style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/ADMIN/img/PNHLOGO.jpg" Width="60px" />
         </td>
    <td class="text-center"><b>Panda Nursing Home, Dhenkanal, Odisha</b></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
</table>
        <table width="100%" style="border-top-color: #000000">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Ambulance Receipt</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><strong>Date:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="right"><strong>From:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><strong>Patient Name:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="right"><strong>To:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><strong>Attendent Name:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td>
    <td align="right" class="auto-style2"><strong>Km: </strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><strong>Contact No:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="right"><strong>Fee:</strong></td>
    <td style="width:20%" align="left">
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center">&nbsp;</td>
</tr>
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center">&nbsp;</td>
</tr>
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="left"><strong>Authorised Signatory</strong></td>
    <td style="width:20%" align="center">&nbsp;</td>
</tr>
</table>        
    </div>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
     
         <asp:Button ID="Button1" runat="server" Text="PRINT" onclientclick="PrintDiv()"/>
          
     
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
</asp:Content>

