<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="PO.aspx.cs" Inherits="STOREKEEPER_PO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        //        yearRange: "c-75:c+10",
        //    });
        //});

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
            $("[id$=txtdateofissue]").datepicker({
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
        $(function () {
            SetDatePicker1();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker1();
                }
            });
        };

        function SetDatePicker1() {
            $("[id$=txtrefdate]").datepicker({
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
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"rel="Stylesheet" type="text/css" />
    
                  <script type="text/javascript">
                      Sys.Application.add_load(function () {
                          $("[id$=txtrefno]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/PO.aspx/GetCustomers") %>',
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
        <style>
               .ChkBoxClass input {width:15px; height:15px; color:red}
        </style>
        <table width="100%">
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
    <td style="width:20%" align="center"><strong>Purchase Order (PO)</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
         <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
        <asp:Label ID="lbluid0" runat="server" Text="Label" Visible="false"> </asp:Label>
         </td>
</tr>
</table>
        <div style="border: thin solid #000000">
             <table width="100%">
     <tr>
    <td style="width:20%" align="right">PO No. :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpono" runat="server" Enabled="false"></asp:TextBox>
         </td>
       <td style="width:20%" align="right">Date  :-&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdateofissue" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Vendor :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropVendor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropVendor_SelectedIndexChanged" Enabled="true">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Statecode :-</td>
    <td style="width:20%" align="left"> <asp:TextBox ID="txtstatecode" runat="server" Enabled="False"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <div style="border: thin solid #000000">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Order Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
      <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Ref No. :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtrefno" runat="server"></asp:TextBox>
         <asp:Button ID="btnview" runat="server" Text="View" OnClick="btnview_Click" />
         </td>
    <td style="width:20%" align="right">Ref Date :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtrefdate" runat="server" CssClass="formdate" Enabled="true" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table></div>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Delivery Address</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Org Name :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtorgname" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Pin :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpin" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">City :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcity" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Phone :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtphone" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">State :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstate" runat="server"></asp:TextBox>
         </td>
   <td style="width:20%" align="right">StateCode :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdstatecode" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <table width="100%">
     <tr>
    <td style="width:20%" align="right">Address :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtaddress" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">GSTIN:-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtgstin" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="center">
        &nbsp;</td>
</tr>
</table></div>
        <div style="border-style: none solid solid solid; border-width: thin; border-color: #000000;">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Item Info</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td align="center">
        <asp:GridView ID="grvItemtDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVOnRowDataBound"  ShowFooter="True" style="text-align: right">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                <asp:TemplateField HeaderText="SLNO">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="SELECT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                       
                        <asp:CheckBox ID="CheckBox1" runat="server"  OnCheckedChanged="CheckBox1_CheckedChanged" CssClass="ChkBoxClass"  AutoPostBack="true"/>
                      
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
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50" pattern="[0-9]+([,\.][0-9]+)?" Text='<%#Eval("QTY")%>' OnTextChanged="txt_price_TextChanged" AutoPostBack="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="ORDER QTY">
                    <ItemTemplate>
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50" pattern="[0-9]+([,\.][0-9]+)?" Text='<%#Eval("QTY")%>' OnTextChanged="txt_price_TextChanged" AutoPostBack="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            
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
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT">
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
                <asp:CommandField ShowDeleteButton="false" />
            </Columns>
             <FooterStyle BackColor="#b70000" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#E3EAEB" />
            <EditRowStyle BackColor="#7C6F57" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#b70000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Total Price :-</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
         </td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="center">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Total Gst Amount :-</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
         </td>
    <td style="width:20%; text-align: right;" align="left">Grand Total :-</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:Label ID="lblgrandtotal" runat="server" style="color: #D20000; font-weight: 700" Text="0"></asp:Label>
         </td>
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
            <div style="border-style: solid solid none solid; border-width: thin; border-color: #000000;">
                <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Terms & Conditions</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Duties &amp; Taxes :-</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txt_term_condition" runat="server" style="margin-left: 0px" Text="Taxes will be extra as per Material category" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
    <td style="width:20%" align="right">Test Report :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_TEST_REPORT0" runat="server" Text="Test report is Mandatory for the material application" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Delivery Term :-</td>
    <td style="width:20%" align="left"> <asp:TextBox ID="txt_DELIVERY_TERM" runat="server" Text="Immediate" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
    <td style="width:20%" align="right">Inspection:-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_INSPECTION0" runat="server" Text="In Your Scope" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Delivery Time&nbsp; :-</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txt_DELIVERY_TIME" runat="server" Text="NA" Width="100%"></asp:TextBox></td>
    <td style="width:20%" align="right">Waranty/Guarantee:-</td>
      <td style="width:20%" align="left">
          <asp:TextBox ID="txt_WARANTY0" runat="server" Text="In Your Scope" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Freight :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_FREIGHT" runat="server" Text="Freight Extra" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Licence &amp; Permit :-</td>
     <td style="width:20%" align="left">
         <asp:TextBox ID="txt_LICENCE_PERMIT0" runat="server" Text="NA" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Make :-</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txt_make" runat="server" Text="The make &amp; techinical specification of the material should be as per " Width="100%"></asp:TextBox>
                                    </td>
    <td style="width:20%" align="right">Price &amp; Payment Terms :</td>
     <td style="width:20%" align="left">
         <asp:TextBox ID="txt_PRICE_PAYMENT" runat="server" Text="100% Payment shall be given with in 30days from delivery" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Packing &amp; Forwarding :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_PACKING_FORWARDING" runat="server" Text="InYour Scope" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Acceptance :-</td>
     <td style="width:20%" align="left">
         <asp:TextBox ID="txt_ACCEPTANCE0" runat="server" Text="In the case of GOODS delivered by SUPPLIER not confirming with the PURCHASE ORDER whether by reason of not being of the quality or in the quantity or measurement stipulated or being unfit for the purpose for which they are required, PURCHASER shall have the right to reject such GOODS within a reasonable time of their delivery and inspection and to purchase else where and to claim for any additional expense incurred without any prejudice to any other right which PURCHASER may have against SUPPLIER. The making of any prior payments by PURCHASER shall not prejudice PURCHASER'S right of rejection." TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Transit Insurance:-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_TRANSIT_INSURANCE" runat="server" Text="InYour Scope" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Termination :-</td>
   <td style="width:20%" align="left">
       <asp:TextBox ID="txt_TERMINATION0" runat="server" Text="In the event of any breach of any of the terms and conditions of the PURCHASE ORDER including failure to deliver by the due date, then PURCHASER without prejudice to any other rights, may terminate the PURCHASE ORDER and may return GOODS previously supplied under the PURCHASE ORDER for full credit by SUPPLIER. In the event of termination due to non-delivery or non-acceptance due to SUPPLIER'S breach of the terms and conditions hereof, SUPPLIER shall undertake to reimburse all monies paid by PURCHASER prior to the date of termination including all direct costs and expenses incured by PURCHASER arising from or in connection with the Termination." TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">    &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Loading &amp; UnLoading:-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_LOADING_UNLOADING" runat="server" Text="Loading is in your scope &amp; unloading at site in our scope" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Installation &amp; Commissioning :-</td>
     <td style="width:20%" align="left">
         <asp:TextBox ID="txt_INSTALLATION_COMMI1" runat="server" Text="Na" TextMode="MultiLine" Width="100%"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right"></td>
  <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
</tr>
</table>
            </div>
        </div>

        <div>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="lblEditgrd" runat="server" Text="" Visible="false"></asp:Label></td>
    <td align="center">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Btncreate_Click" Text="Create" />
        <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnupdate_Click" Text="Update" Visible="False" />
        <asp:Button ID="btndelete" runat="server" CssClass="btn btn-warning btn-sm" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td  align="center" >
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="PONO"  AllowPaging="True" AllowSorting="True"  CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" OnSelectedIndexChanging="GridView2_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="DATEOFISSUE" HeaderText="DATE" />
                 <asp:BoundField DataField="PONO" HeaderText="PONO" />
                  <asp:BoundField DataField="VNAME" HeaderText="VENDOR&nbsp;NAME" />
                   <asp:BoundField DataField="REFDATE" HeaderText="REF DATE" />
                 
               <%-- <asp:CommandField  ShowSelectButton="true"  SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>--%>
            </Columns>
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle ForeColor="#003399" BackColor="White" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div></ContentTemplate></asp:UpdatePanel>
</asp:Content>

