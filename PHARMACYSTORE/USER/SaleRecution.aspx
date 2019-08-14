<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="SaleRecution.aspx.cs" Inherits="PHARMACYSTORE_USER_SaleRecution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type = "text/javascript">

         function SetTarget() {

             document.forms[0].target = "_blank";

         }
        </script>
    
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #009F50;
        }
        .auto-style2 {
            text-decoration: underline;
            color: #000000;
        }
        .auto-style3 {
            color: #000000;
        }
        .auto-style4 {
            color: #0970C4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <div>
                   <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
   
         


    <script type="text/javascript">
        //Sys.Application.add_load(function () {
        //    $('.formdate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        maxDate: '0',
        //        yearRange: "c-75:c+10",
        //    });
        //});

    </script>
      <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
                  <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
          <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
            <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>
   <script type="text/javascript">
       Sys.Application.add_load(function () {
           $("[id$=txtname]").autocomplete({
               source: function (request, response) {
                   $.ajax({
                       url: '<%=ResolveUrl("~/PHARMACYSTORE/USER/Sale.aspx/GetCustomers") %>',
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

       $(function () {
           SetDatePicker();
       });
       //On UpdatePanel Refresh.
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       if (prm != null) {
           prm.add_endRequest(function (sender, e) {
               if (sender._postBackSettings.panelsToUpdate != null) {
                   SetDatePicker();
               }
           });
       };

       function SetDatePicker() {
           $("[id$=txtinvdate]").datepicker({
               dateFormat: 'dd-mm-yy',
               showOn: 'button',
               buttonImageOnly: true,
               dateFormat: 'dd-mm-yy',
               changeMonth: true,
               changeYear: true,
               maxDate: '0',

               yearRange: "c-75:c+10",
               buttonImage: 'img/calendar.png'

           });
       }
    </script>
   <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
         <asp:Label ID="lblrecution" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Sale Entry</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:TextBox ID="txtcstatecode" runat="server" Visible="False">21</asp:TextBox>
         </td>
</tr>
</table>
          </div>
         
 
        <div style="border: 2px solid black;">
       <table width="100%" style="border: 1px solid #000000">
     <tr>
    <td style="width:20%" align="right" class="auto-style2"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Sale To :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droprtype" runat="server" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="droprtype_SelectedIndexChanged">
            <asp:ListItem >OUTPATIENT</asp:ListItem>
            <asp:ListItem >INPATEINT</asp:ListItem>
            <asp:ListItem >ONCOUNTER</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style3"><strong>Party Info</strong></td>
    <td style="width:20%; text-align: right;" align="left" class="auto-style3">IPDNO/OPDNO :</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:TextBox ID="txtIPNO" runat="server" CssClass="form-control input-sm m-bot15" Enabled="true" OnTextChanged="txtIPNO_TextChanged"></asp:TextBox>
         </td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Patient Name: </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpartyname" runat="server" CssClass="form-control input-sm m-bot15" Enabled="true" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Invoice No :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="TXTID" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
        State Code :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtstatecode" runat="server" CssClass="form-control input-sm m-bot15" Enabled="false" Text="21" ></asp:TextBox></td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Invoice Date :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtinvdate" runat="server" CssClass=" formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
              
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
         
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     
 
        <div style="border: 2px solid #000000">

             <table width="100%" style="border: 1px solid #000000">
     <tr>
    <td style="width:20%" align="right" class="auto-style2"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style3"><strong>Item Info</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtcomp" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="True"  Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Item Name :</td>
    <td style="width:20%; text-align: left;" align="right">
        <%--  <asp:TextBox ID="txtname" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="True" OnTextChanged="txtname_TextChanged"></asp:TextBox>--%>
        <asp:DropDownList ID="txtname" runat="server" OnSelectedIndexChanged="txtname_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
    <asp:HiddenField ID="hfCustomerId" runat="server" />
          </td>
    <td style="width:20%; text-align: right;" align="left">Hsn No :</td>
    <td style="width:20%; text-align: left;" align="center">
          <asp:TextBox ID="txthsncode" runat="server" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
         <td style="width:20%" align="right"></td>
</tr>
</table> <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">Batchno :</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:TextBox ID="txtbatchno" runat="server" CssClass="form-control input-sm m-bot15" Text="0" Enabled="true" OnTextChanged="txtbatchno_TextChanged"></asp:TextBox>
        Press Enter
         </td>
    <td style="width:20%; text-align: right;" align="left">Category :</td>
    <td style="width:20%; text-align: left;" align="center">
          <asp:DropDownList ID="dropcate" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False">
        </asp:DropDownList>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table>
         <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">&nbsp;Unit :</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:DropDownList ID="droppurchaseunit" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False">
              <asp:ListItem>PCS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;Price :</td>
    <td style="width:20%; text-align: left;" align="center">
         <asp:TextBox ID="txtpprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table>
         
         <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">&nbsp;
        <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Qty :</td>
    <td style="width:25%; text-align: left;" align="right">
        <asp:TextBox ID="txtopening" runat="server" Text="0" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" AutoPostBack="True" OnTextChanged="txtopening_TextChanged" Width="68px"></asp:TextBox><asp:Label ID="lblstock" runat="server" ForeColor="Red" Text=""></asp:Label>
        
    </td>
    <td style="width:15%; text-align: right;" align="left">Expiry Date :</td>
    <td style="width:20%; text-align: left;" align="center">
          <asp:DropDownList ID="dropexpiry" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="true" OnSelectedIndexChanged="dropexpiry_SelectedIndexChanged">
        </asp:DropDownList>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table><table id="Table1" width="100%" visible="false" runat="server">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">Discount(%) :</td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:TextBox ID="txtdisc" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" AutoPostBack="True" OnTextChanged="txtdisc_TextChanged"></asp:TextBox>
    </td>
    <td style="width:20%; text-align: right;" align="left">Discount Amount :</td>
    <td style="width:20%; text-align: left;" align="center">
         <asp:TextBox ID="txtdiscamt" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table><table width="100%">
     <tr>
  
    <td style="width:20%; text-align: right;" align="left">Amount :</td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:TextBox ID="txtamount" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
    
    <td style="width:20%; text-align: right;" align="left">CGST (%):</td>
    <td style="width:20%; text-align: left;" align="center">
         <asp:TextBox ID="txtcgst" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
           <td style="width:20%" align="right"></td>
</tr>
</table>
         <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">SGST (%):</td>
    <td style="width:20%; text-align: left;" align="right"> 
         <asp:TextBox ID="txtSgst" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: right;" align="left">IGST (%):</td>
    <td style="width:20%; text-align: left;" align="center">
         <asp:TextBox ID="txtIGST" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table>
         <table width="100%">
     <tr>
  
    <td style="width:20%; text-align: right;" align="left">GST Amount :</td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:TextBox ID="txtgstamount" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: right;" align="left">Total Amount :</td>
    <td style="width:20%; text-align: left;" align="center">
         <asp:TextBox ID="txttotalamount" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
           <td style="width:20%; text-align: left;" align="right">
               <asp:ImageButton ID="ImageButton1" runat="server" BorderColor="Black" Height="32px" ImageUrl="~/PHARMACYSTORE/USER/img/Add.png" Width="43px" OnClick="ImageButton1_Click" />
         </td>
</tr>
</table>
         <table width="100%">
     <tr>
   
    <td align="center">

        <asp:GridView ID="grvStudentDetails" runat="server" 
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" 
                GridLines="None"  style="text-align: right" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" >
    <Columns>
       <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
         <asp:TemplateField HeaderText="Sno">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_slno" runat="server" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="COMPANY">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_lblcompany" runat="server" Text='<%#Eval("COMPANY")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="NAME">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="HSNCODE">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_hsn" runat="server" Text='<%#Eval("HSNCODE")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="BATCHNO">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_batchno" runat="server" Text='<%#Eval("BATCHNO")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField  HeaderText="CATEGORY">
            <ItemTemplate>
                <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                 <asp:Label ID="lbl_category" runat="server" Text='<%#Eval("CATEGORY")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="EXP DATE">
            <ItemTemplate>
                <asp:Label ID="lbl_exp" runat="server" Text='<%#Eval("EXP")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="UNIT">
            <ItemTemplate>
                <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                 <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRICE">
            <ItemTemplate>
               <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_unit" runat="server" Width="50" Text='<%#Eval("UNIT")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="QTY">
            <ItemTemplate>
                <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="DISC">
            <ItemTemplate>
               <asp:Label ID="lbl_disc" runat="server" Text='<%#Eval("DISC")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_rate" runat="server" Width="50" OnTextChanged="txt_rate_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("RATE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="DISCAMT">
            <ItemTemplate>
               <asp:Label ID="lbl_discamt" runat="server" Text='<%#Eval("DISCAMT")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_rate" runat="server" Width="50" OnTextChanged="txt_rate_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("RATE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="AMOUNT">
            <ItemTemplate>
                 <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CGST">
            <ItemTemplate>
                 <asp:Label ID="lbl_cgst" runat="server" Text='<%#Eval("CGST")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="SGST">
            <ItemTemplate>
                 <asp:Label ID="lbl_sgst" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="IGST">
            <ItemTemplate>
                 <asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="GSTAMT">
            <ItemTemplate>
                 <asp:Label ID="lbl_gstamt" runat="server" Text='<%#Eval("GSTAMT")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="TOTAL AMT">
            <ItemTemplate>
                 <asp:Label ID="lbl_totalamount" runat="server" Text='<%#Eval("TOTALAMOUNT")%>'></asp:Label>
                 <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
           
            <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
                
                <%--<asp:Button ID="ButtonAdd" runat="server" 
                        Text="Add New Row" OnClick="ButtonAdd_Click" BackColor="SteelBlue" ForeColor="White"/>--%>
                 
            </FooterTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#E3EAEB" />
    <EditRowStyle BackColor="#7C6F57" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>
    </td>
   
       
          
</tr>
</table>


        </div>
        
        
        
       <table width="100%">
     <tr>
    <td style="width:30%" align="right">
&nbsp;&nbsp;&nbsp;</td>
    <td style="width:10%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left">Total Price :</td>
    <td style="width:20%; text-align: right;" align="center">
        <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left">&nbsp;Discamt :</td>
    <td style="width:20%; text-align: right;" align="center">
        <asp:TextBox ID="lbldiscamt" runat="server" OnTextChanged="lbldiscamt_TextChanged" Text="0" pattern="[0-9]+([,\.][0-9]+)?" Width="51px"></asp:TextBox> </td>
</tr>
</table>
        <table width="100%">
     <tr>
    
    <td style="width:70%" align="center">

         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-sm"  OnClick="Btndelete_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />

    </td>
    <td style="width:10%" align="left">Total Amount :</td>
    <td style="width:20%; text-align: right;" align="center">
        <asp:Label ID="lbltotalamt" runat="server" Text="0"></asp:Label>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       <asp:Label ID="UHID" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left">GST Amount:</td>
    <td style="width:20%; text-align: right;" align="center">
        <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left" class="auto-style3">Grand Total :</td>
    <td style="width:20%; text-align: right;" align="center">
        <asp:Label ID="lblgrandtotal" runat="server" Text="0" style="color: #D20000; font-weight: 700"></asp:Label>
         </td>
</tr>
</table>
              <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left" class="auto-style3">Paid Amount :</td>
    <td style="width:20%; text-align: right;" align="center">
        <asp:TextBox ID="txtpaidamt" runat="server" OnTextChanged="txtpaidamt_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text="0" Width="51px"></asp:TextBox>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:13%" align="left" class="auto-style3">Balance/Refundable :</td>
    <td style="width:17%; text-align: right;" align="center">
        <asp:Label ID="lblbalanceamt" runat="server" Text="0" style="color: #D20000; font-weight: 700"></asp:Label>
         </td>
</tr>
</table>
        <div style="border: 1px solid #000000">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; font-weight: 700;" align="center" class="auto-style4">Invoice History</td>
    <td style="width:20%" align="right"></td>
    
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" PageSize="10" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
               
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                 <asp:BoundField DataField="ID" HeaderText="INVOICE" />
                  <asp:BoundField DataField="PARTY" HeaderText="PARTY&nbsp;NAME" />
                <asp:CommandField  ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>
               <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
            </Columns>
             <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
              <FooterStyle BackColor="White" ForeColor="#000066" />
              <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
              <RowStyle ForeColor="#000066" />
              <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F1F1F1" />
              <SortedAscendingHeaderStyle BackColor="#007DBB" />
              <SortedDescendingCellStyle BackColor="#CAC9C9" />
              <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">
        <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
  
        
    </ContentTemplate></asp:UpdatePanel></div>
</asp:Content>

