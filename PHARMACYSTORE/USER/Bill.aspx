<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="PHARMACYSTORE_USER_Bill" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
   <script type="text/javascript">
       $(function () {
           $("[id$=txtname]").autocomplete({
               source: function (request, response) {
                   $.ajax({
                       url: '<%=ResolveUrl("~/PHARMACYSTORE/USER/Bill.aspx/GetCustomers") %>',
                       data: "{ 'prefix': '" + request.term + "'}",
                       dataType: "json",
                       type: "POST",
                       contentType: "application/json; charset=utf-8",
                       success: function (data) {
                           response($.map(data.d, function (item) {
                               return {
                                   label: item.split('/ \s*/')[0]
                               }
                           }))
                       },
                       error: function (response) {
                           alert(response.responseText);
                       },
                       failure: function (response) {
                           alert(response.responseText);
                       }
                   });
               },
               select: function (e, i) {
                   $("[id$=hfCustomerId]").val(i.item.val);
               },
               minLength: 1
           });
       });
    </script>
    <style type="text/css">
        .auto-style1 {
            color: #000000;
            text-decoration: underline;
        }
    </style>
     <script type="text/javascript">

         function ValidtSw() {

             if (document.getElementById("<%=txtinvoice.ClientID%>").value == "") {
                 alert("Invoice Field Is Required !");
                 document.getElementById("<%=txtinvoice.ClientID%>").focus();
                 return false;
             }
         }
             function ValidSerch() {
             if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                 alert("Name Field is Required !");
                 document.getElementById("<%=txtname.ClientID%>").focus();
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
    <td style="width:20%" align="left"> <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Sale Bill</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Select Invoice :</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtinvoice" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show"  OnClientClick="return ValidtSw();"/>
         &nbsp;</td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center">&nbsp;</td>
</tr>
</table><table width="100%" runat="server" visible="false">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Select Patient Name :</td>
    <td style="width:15%; text-align: left;" align="right">
        <asp:DropDownList ID="droppartyname" runat="server">
        </asp:DropDownList>
&nbsp;<asp:Button ID="Btnpart" runat="server" OnClick="Butparty_Click" Text="Show" />
         </td>
    <td style="width:25%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Search By Patient Name :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtname" runat="server" AutoPostBack="false"></asp:TextBox>
         <asp:HiddenField ID="hfCustomerId" runat="server" /> &nbsp;<asp:Button ID="Btnsearch" runat="server" OnClick="Btnsearch_Click" Text="Search" Width="67px" OnClientClick="return ValidSerch();"/>
         </td>
    <td style="width:20%" align="left">&nbsp; 
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Print" Width="61px" Visible="false" />
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
   
         
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="ID"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="NAME" />
                 <asp:BoundField DataField="DATE" HeaderText="DATE" />
                  <asp:BoundField DataField="ID" HeaderText="INVOICE_NO" />
                <asp:CommandField ShowDeleteButton="False" HeaderText="ACTION" ShowEditButton="False" ShowSelectButton="true" />
            </Columns>
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <table width="100%" >
        <tr>
            <td style="padding-left:140px;"> <asp:Button ID="btnPrint" runat="server" Visible="false" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click"  /></td>
        </tr>
    </table>
    <table width="100%" runat="server" id="tab1">
     <tr>
    <td style="text-align: center;" align="center">
       
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
       
         </td>
</tr>
</table>
    
  
</asp:Content>

