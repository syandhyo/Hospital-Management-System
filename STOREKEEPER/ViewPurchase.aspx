<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="ViewPurchase.aspx.cs" Inherits="STOREKEEPER_Purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            $("[id$=txtgrndate]").datepicker({
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
            $("[id$=txtinvoicedate]").datepicker({
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
                          $("[id$=txtpono]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/Purchase.aspx/GetCustomers") %>',
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
       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td align="left"><strong>Purchase Releaser</strong></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
         </td>
</tr>
</table>
        <div style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Vendor :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropVendor" runat="server" AutoPostBack="false" Enabled="false">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Statecode :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstatecode" runat="server" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><div style="border: thin solid #000000">
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
    <td style="width:20%" align="right">Purchase No. :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtgrnno" runat="server" Enabled="false"></asp:TextBox>
         </td>
       <td style="width:20%" align="right">Purchase Date :-&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtgrndate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Invoice No. :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtinvoiceno" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Invoice Date :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtinvoicedate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">PO No. :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpono" runat="server" Enabled="TRUE" ></asp:TextBox>
        <asp:Button ID="btnview" runat="server" Text="View" OnClick="btnview_Click" />
         </td>
       <td style="width:20%" align="right">PO Date :-&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpodate" runat="server" CssClass="formdate" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
            <div style="border-style: none solid solid solid; border-width: thin; border-color: #000000;">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Item Info</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td align="center">
        <asp:GridView ID="grvStudentDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" 
            OnRowDataBound="GVOnRowDataBound"  ShowFooter="True" style="text-align: right">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                <asp:TemplateField HeaderText="SlNO">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="SELECT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                       
                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" Checked='<%#Eval("Isselected")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
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
                <asp:TemplateField HeaderText="ORD.QTY">
                    <ItemTemplate>
                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                        <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="REC.QTY">
                    <ItemTemplate>
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50" pattern="[0-9]+([,\.][0-9]+)?" OnTextChanged="txt_price_TextChanged" AutoPostBack="true" Text='<%#Eval("RECQTY")%>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="REM.QTY">
                    <ItemTemplate>
                         <asp:Label ID="lbl_remqty" runat="server" Text='<%#Eval("REMQTY")%>'></asp:Label>
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
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT">
                    <ItemTemplate>
                        <asp:Label ID="lbl_totalamount" runat="server" Text='<%#Eval("TOTALAMT")%>'></asp:Label>
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
    <td style="width:20%; text-align: right;" align="left">Total Discount :-</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:Label ID="lbltotaldisc" runat="server" Text="0"></asp:Label>
        </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Total GST Amount :-</td>
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
                </div>
            <div>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Btncreate_Click" Text="Create" />
        <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm"  Text="Update" Visible="False" />
        <asp:Button ID="btndelete" runat="server" CssClass="btn btn-warning btn-sm"  Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" Text="Cancel" />
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
    <td  align="center" class="auto-style1">
         <%--<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                 <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" />
                  <asp:BoundField DataField="PARTY" HeaderText="PARTY&nbsp;NAME" />
                <asp:CommandField  ShowSelectButton="true"  SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>
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
        </asp:GridView>--%>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>
        </div>
    </div></ContentTemplate></asp:UpdatePanel>
</asp:Content>

