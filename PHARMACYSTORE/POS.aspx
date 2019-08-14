<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="POS.aspx.cs" Inherits="PHARMACYSTORE_POS" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <script type="text/javascript">

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
                         buttonImage: '../images/calendar.png'

                     });
                 }
    </script>
             <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
            <script type="text/javascript">
                Sys.Application.add_load(function () {
                    $("[id$=txtname]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("~/PHARMACYSTORE/pharmacy_sale.aspx/GetCustomers") %>',
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
             <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Sale
                    </div>
             </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                     <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtcstatecode" runat="server" Visible="False">21</asp:TextBox>
            <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
             <asp:TextBox ID="txtcomp" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="True" OnTextChanged="txtname_TextChanged" Visible="False"></asp:TextBox>
        </div>
        <div class="card card-w-title">
					
					 <h1 style="color:#203a5a;"><b><center>SALE ENTRY</center></b>
                         <h1></h1>
                         <h1></h1>
                         <hr>
                         <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                 <div class="ui-grid-row">
                                     <!---- Party Name ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                         Sale To :
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="droprtype" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="droprtype_SelectedIndexChanged" Width="180">
                                             <asp:ListItem>OUTPATIENT</asp:ListItem>
                                             <asp:ListItem>INPATEINT</asp:ListItem>
                                             <asp:ListItem>ONCOUNTER</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         IPDNO/OPDNO :
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtIPNO" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" OnTextChanged="txtIPNO_TextChanged"></asp:TextBox>
                                     </div>
                                 </div>
                                 <hr>
                                 <br>
                                 <h1 style="color:#0071bc;"><b><u>Party Info</u></b></h1>
                                 <br />
                                 <div class="ui-grid-row">
                                     <!---- Party Name ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Party Name:
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtpartyname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                     <!---- Invoice No----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Invoice No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="TextBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="15" MinLength="6" pattern="^[A-Z0-9_]*"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!----  State Code :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         State Code :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtstatecode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false" Text="21" ></asp:TextBox>
                                     </div>
                                     <!----Invoice Date----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Invoice Date :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtinvdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <br>
                                 <hr />
                                 <br>
                                 <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- Item Name : :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Item Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtname" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                         <asp:HiddenField ID="hfCustomerId" runat="server" />
                                     </div>
                                     <!---- Hsn No :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Hsn No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txthsncode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" Text="0"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Batchno :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Batchno :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropbatch" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
                                         <%--<asp:TextBox ID="txtbatchno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false" OnTextChanged="txtbatchno_TextChanged" Text="0"></asp:TextBox>--%>
                                     </div>
                                     <!---- Category :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Category :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropcate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" Width="180">
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!----  Unit :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Unit :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="droppurchaseunit" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" Width="180">
                                             <asp:ListItem>PCS</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <!----Price----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Price :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpprice" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Quantity :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Quantity :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtopening" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" OnTextChanged="txtopening_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                         <asp:Label ID="lblstock" runat="server" ForeColor="Red"></asp:Label>
                                     </div>
                                     <!----  Expiry Date :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label6" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Expiry Date :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropexpiry" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!----  Discount(%) :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Discount(%) :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdisc" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" OnTextChanged="txtdisc_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                     <!----Discount Amount :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Discount Amount :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdiscamt" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!----  Amount :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Amount :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtamount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                     <!---- CGST (%):----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         CGST (%):</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtcgst" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- SGST (%):----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         SGST(%) :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtSgst" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                     <!----  IGST (%):----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         IGST(%) :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtIGST" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- GST Amount :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         GST Amount :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtgstamount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                     <!---- Total Amount :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Total Amount :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txttotalamount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-1">
                                         <asp:ImageButton ID="ImageButton1" runat="server" BorderColor="Black" Height="32px" ImageUrl="~/PHARMACYSTORE/USER/img/Add.png" OnClick="ImageButton1_Click" Width="43px" />
                                     </div>
                                 </div>
                                 
                                 <br>
                                 <br></br>
                                 <br></br>
                                 </br>
                                 
                                 </br>
                                 </hr>
                                 <br />
                             </div>
                         </div>
                         <div class="ui-datatable-tablewrapper">
                             <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                 <asp:GridView ID="grvStudentDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" ShowFooter="True" style="text-align: right">
                                     <Columns>
                                         <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                                         <asp:TemplateField HeaderText="Sno">
                                             <ItemTemplate>
                                                 <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                                                 <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="COMPANY">
                                             <ItemTemplate>
                                                 <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                                                 <asp:Label ID="lbl_lblcompany" runat="server" Text='<%#Eval("COMPANY")%>'></asp:Label>
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
                                         <asp:TemplateField HeaderText="CATEGORY">
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
                                         <asp:TemplateField HeaderText="UNIT">
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
                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                     <RowStyle BackColor="#EFF3FB" />
                                     <EditRowStyle BackColor="#2461BF" />
                                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                     <AlternatingRowStyle BackColor="White" />
                                     <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                     <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                     <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                     <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                 </asp:GridView>
                             </asp:Panel>
                         </div>
                         <hr>
                         <br>
                         <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" />
                         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />
                         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
                         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br />
                         <br />
                        
                          <div id="Div1" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="Div2" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Total Price :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Discamt :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:TextBox ID="lbldiscamt" runat="server" OnTextChanged="lbldiscamt_TextChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" pattern="[0-9]+([,\.][0-9]+)?" Width="51px"></asp:TextBox>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Total Amount :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lbltotalamt" runat="server" Text="0"></asp:Label>
                                 <asp:Label ID="UHID" runat="server" Text="0" Visible="false"></asp:Label>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 GST Amount:</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Grand Total :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lblgrandtotal" runat="server" style="color: #D20000; font-weight: 700" Text="0"></asp:Label>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Paid amount :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:TextBox ID="txtpaidamt" runat="server" OnTextChanged="txtpaidamt_TextChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" Text="0" Width="51px"></asp:TextBox>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Balance/Refundable :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lblbalanceamt" runat="server" Text="0" style="color: #D20000; font-weight: 700"></asp:Label>
                             </div>
                         </div>
                        </div>
                    </div>
                         <br/>
                         <br/>
                         <hr>
                         <br/>
                         <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                             <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                 INVOICE HISTORY
                             </div>
                             <div class="ui-datatable-tablewrapper">
                                 <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                     <AlternatingRowStyle BackColor="White" />
            <Columns>
               
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                 <asp:BoundField DataField="ID" HeaderText="INVOICE" />
                  <asp:BoundField DataField="PARTY" HeaderText="PARTY&nbsp;NAME" />
               <%-- <asp:CommandField  ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>--%>
               <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
            </Columns>
             <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                     <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
              <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F5F7FB" />
              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
              <SortedDescendingCellStyle BackColor="#E9EBEF" />
              <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                                 <asp:GridView ID="GridView1" runat="server" Visible="False">
                                 </asp:GridView>
                             </div>
                         </div>
                         
                         <br>
                        
                         
                         </hr>
                         
                     
    </div>
             </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
       <div class="box box-primary" style="border: solid 1px #1FDA9A">
        <div class="box-header with-border" style="background-color: #1FDA9A; color: white">
            <h3 class="box-title">Rack Availability</h3>
            <div class="box-tools pull-right">
            <button class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip" title="Collapse"><i class="fa fa-minus"></i></button>
            <button class="btn btn-box-tool" data-widget="remove" data-toggle="tooltip" title="Remove"><i class="fa fa-times"></i></button>
            </div>
        </div>
        <div class="box-body">
            <div style="width: 89%; float: left;">
                <%
                    Connect obj = new Connect();
                    DataSet dsRoomTypes = obj.RetDataset("SELECT SELF_ID,SELF_NM FROM SELF_HDR");
                        DataTable dtRoomTypes = dsRoomTypes.Tables[0];
                        for (int i = 0; i < dtRoomTypes.Rows.Count; i++)
                        {%>
                            <%
                            int rtypCode = int.Parse(dtRoomTypes.Rows[i][0].ToString());
                            DataSet dsRoomNames = obj.RetDataset("SELECT a.SELF_DTL_ID,a.RACK_NO,a.STATUS,isnull(c.NAME,0) as NAME FROM SELF_DTL a  left join  ITEM_LOCATION b on a.SELF_DTL_ID=CELL_ID  left join ITEM_TABLE c on b.ITEM_ID= c.slno WHERE SELF_ID=" + rtypCode);
                            DataTable dtRoomNames = dsRoomNames.Tables[0];
                            string[] color = { "#fff", "#26b13e", "#f5e10a", "#ff6a00" };
                            if (dtRoomNames.Rows.Count > 0)
                            {
                                %>
                                <div style="width: 100%; border: 1px solid blue; font-size: 15px; font-weight: bold ;margin-bottom: 12px">
                                    <div style="width: 100% ;text-align: center; background-color: #b6dfff; color: black"><%=dtRoomTypes.Rows[i][1]%></div>
                                    <table class="table table-bordered" style="width: 100%; margin-bottom: 0px;">
                                        <%
                                        int tdWidth = 100/dtRoomNames.Rows.Count;
                                        for (int j = 0; j < dtRoomNames.Rows.Count; j++)
                                        {%>
                                                      <%
                                            DataSet dsItems = obj.RetDataset("SELECT a.SELF_DTL_ID,a.RACK_NO,a.STATUS,isnull(c.NAME,0) as NAME FROM SELF_DTL a  left join  ITEM_LOCATION b on a.SELF_DTL_ID=CELL_ID  left join ITEM_TABLE c on b.ITEM_ID= c.slno where NAME='PLKL'");
                            DataTable dtItems = dsItems.Tables[0];
                             %>
                                        <%
                                           // for (int k = 0; k < dtItems.Rows.Count; k++)
                                            if (dtRoomNames.Rows[j][3].ToString() == "PLKL")
                            {
                                %>
                                         
                                                <td width="<%=tdWidth %>%" align="center" style="border: 1px solid black; background-color: red"><%=dtRoomNames.Rows[j][1].ToString()%></td>  

                                         <%}
                                           else
                                           {   %>
                                            <td width="<%=tdWidth %>%" align="center" style="border: 1px solid black; background-color: rgb(255, 255, 255)"><%=dtRoomNames.Rows[j][1].ToString()%></td>  
                                            <% }
                                           
                                           
                                             %>
                                            <%}    
                                        %>
                                    </table>
                                </div>                            
                            <%}
                        }
                    %>
                
            </div>
     <%--      <div style="width: 10%; height: 150px; padding: 5px; float: right; border: solid 1px #ff6a00;">
                <table>
                    <tr>
                        <td><label class="tds" style="border:1px solid black; background-color: #fff"></label></td><td style="vertical-align:middle">Available</td>
                    </tr>
                    <tr>
                        <td><label class="tds" style="border:1px solid black; background-color: #26b13e"></label></td><td>Checked In</td>
                    </tr>
                    <tr>
                        <td><label class="tds" style="border:1px solid black; background-color: #f5e10a"></label></td><td>Booked</td>
                    </tr>
                    <tr>
                        <td><label class="tds" style="border:1px solid black; background-color: #f24444"></label></td><td>Blocked</td>
                    </tr>
                </table>
            </div>--%>
        </div>
    </div>
</asp:Content>

