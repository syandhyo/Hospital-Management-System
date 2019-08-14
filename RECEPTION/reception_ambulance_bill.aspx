<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_ambulance_bill.aspx.cs" Inherits="RECEPTION_reception_ambulance_bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
               <script type="text/javascript">
                   function PrintDiv() {
                       var divContents = document.getElementById("div1").innerHTML;
                       var printWindow = window.open('', '', 'height=600,width=900');
                       printWindow.document.write('<html><head><title>AmbulanceReceipt</title>');
                       printWindow.document.write('</head><body >');
                       printWindow.document.write(divContents);
                       printWindow.document.write('</body></html>');
                       printWindow.document.close();
                       printWindow.print();
                   }
    </script>
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
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Patient Bill
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u></u></b></h1>
					
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                             <div id="div1" style="border: thin solid #000000">
        
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/ADMIN/img/logo-big.png" Width="60px" />
         </td>
    <td class="text-center"><b>Zemusi Hospital, Odisha</b></td>
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
        <asp:Label ID="Label1" runat="server" ></asp:Label></td>
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
         <br />
    <td align="center">
     
         <asp:Button ID="Button1" runat="server" CssClass="create" Text="PRINT" onclientclick="PrintDiv()"/>
          
     
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                           
                           </div>
                      </div>
                   
					
				<h1>&nbsp;</h1>
                         
        </div>
    </div>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

