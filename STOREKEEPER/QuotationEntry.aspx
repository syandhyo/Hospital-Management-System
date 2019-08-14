<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="QuotationEntry.aspx.cs" Inherits="STOREKEEPER_QuotationEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              
                <div style="background-color: #FFFFFF">
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
        //        maxDate: '0',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '0',
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
            $("[id$=txtdate]").datepicker({
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
            $("[id$=txtQuotDate]").datepicker({
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
                          $("[id$=txtRfqNo]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/QuotationEntry.aspx/GetCustomers") %>',
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
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
       
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left" class="auto-style1"><strong>Quotation Entry</strong></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"><asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label></td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
         </td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Quotation No :</td>
    <td style="width:20%; text-align: left;" align="left">
         <asp:TextBox ID="txtQuotNo" runat="server" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
         Quotation Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtQuotDate" runat="server" CssClass="formdate" Enabled="false"></asp:TextBox>&nbsp;
      
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>                
                    <table width="100%">
     <tr>
    <td style="width:20%" align="right">RFQ No :</td>
    <td style="width:20%;" align="left">
           <asp:TextBox ID="txtRfqNo" runat="server" ></asp:TextBox>&nbsp;
         <asp:Button ID="btn_search" runat="server" BackColor="#66CCFF" Text="Search" OnClick="btn_search_Click" />
        <asp:HiddenField ID="hfCustomerId" runat="server" />
         </td>
    <td style="width:20%; text-align: right;" align="right">
         Reff Date :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtdate" runat="server" CssClass="formdate"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right">Vendor name :</td>
    <td style="width:20%; text-align: left;" align="left">
        <asp:DropDownList ID="ddvendrNm" runat="server" Enabled="false">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
       
         </td>
    <td style="width:20%" align="right">
        State Code :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtStatecd" runat="server" MaxLength="2" pattern="[0-9]+" MinLength="2" ></asp:TextBox>&nbsp;
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="left">
        &nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
          <td style="width:60%" align="center">
    <asp:GridView ID="grvquonItem" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="100%">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
               <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                      <%#Container.DisplayIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkQuot" runat="server"  OnCheckedChanged="chkQuot_CheckedChanged" AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HSNCODE">
                    <ItemTemplate>
                      <%-- <asp:TextBox  ID="txt_hsncode" runat="server" Width="50" Text='<%#Eval("HSNCODE") %>'></asp:TextBox>--%>
                         <asp:TextBox  ID="txt_hsncode" runat="server" Width="50" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="UNIT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                        <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="PRICE">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                        <%--<asp:TextBox ID="txt_price" runat="server" Width="50"   AutoPostBack="true" OnTextChanged="txt_price_TextChanged" Text='<%#Eval("PRICE") %>'></asp:TextBox>--%>
                        <asp:TextBox ID="txt_price" runat="server" Width="60"   AutoPostBack="true" OnTextChanged="txt_price_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text="0" MaxLength="10" title="Please Enter Numeric Value"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>           
              
            
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_Amt" runat="server"  Enabled="false" Width="50" Text="0" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST">
                    <ItemTemplate>
                         <%--<asp:TextBox ID="TextBox1" runat="server"  Width="50" AutoPostBack="true" Text='<%#Eval("GST") %>' OnTextChanged="txt_price_TextChanged"></asp:TextBox>--%>
                        <asp:TextBox ID="txt_cgst" runat="server"  Width="50"  Text="0" AutoPostBack="true" OnTextChanged="txt_price_TextChanged" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST">
                    <ItemTemplate>
                         <%--<asp:TextBox ID="TextBox2" Enabled="false" runat="server"  Width="50" Text='<%#Eval("GST") %>'   AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>--%>
                        <asp:TextBox ID="txt_sgst" Enabled="false" runat="server"  Width="50" Text="0" AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_igst" runat="server" Enabled="false" Width="50" Text="0" AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTAMT">
                    <ItemTemplate>
                     <%--<asp:TextBox ID="TextBox3" runat="server"  Width="50" Text='<%#Eval("GSTAMT") %>' Enabled="false" ></asp:TextBox>--%>
                        <asp:TextBox ID="txt_gstamt" runat="server"  Width="50" Text="0" Enabled="false" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="TextBox3" runat="server"  Width="50" Text='<%#Eval("TOTAMT") %>'  Enabled="false"></asp:TextBox>--%>
                        <asp:TextBox ID="txt_TotAmt" runat="server"  Width="50" Text="0"  Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
             
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
              <%----------------------------%>
              <asp:GridView ID="grvquItemTemp" runat="server" Visible="false" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" ShowFooter="True">
              <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
               <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                      <%#Container.DisplayIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkQuot" runat="server" Checked='<%#Eval("CHKSEL")%>' OnCheckedChanged="chkQuot_CheckedChanged" AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HSNCODE">
                    <ItemTemplate>
                       <asp:TextBox  ID="txt_hsncode" runat="server" Width="50" Text='<%#Eval("HSNCODE")%>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="UNIT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                        <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="PRICE">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_price" runat="server" Width="50" Text='<%#Eval("PRICE")%>'  AutoPostBack="true" OnTextChanged="txt_price_TextChanged" pattern="[0-9]+([,\.][0-9]+)?"  MaxLength="10" title="Please Enter Numeric Value"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>           
              
            
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_Amt" runat="server"  Enabled="false" Width="50" Text='<%#Eval("AMOUNT")%>' ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_cgst" runat="server" Text='<%#Eval("CGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_cgst" runat="server"  Width="50" Text='<%#Eval("CGST")%>' AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_sgst" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_sgst" Enabled="false" runat="server"  Width="50" Text='<%#Eval("SGST")%>' AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_igst" runat="server" Enabled="false" Width="50" Text='<%#Eval("IGST")%>' AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTAMT">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_gstamt" runat="server" Text='<%#Eval("GSTAMT")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_gstamt" runat="server"  Width="50" Text='<%#Eval("GSTAMT")%>' Enabled="false" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_totalamount" runat="server" Text='<%#Eval("TOTALAMOUNT")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_TotAmt" runat="server"  Width="50" Text='<%#Eval("TOTAMT")%>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
             
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
    <td style="width:20%" align="left">
        
    </td>
</tr>
</table>
                    <div runat="server" id="divGrdTot" visible="false">
                    <table width="100%">
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
</table>
                        </div>
                    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm" Visible="False" OnClick="btnupdate_Click" />&nbsp;
         <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning" Visible="False"  OnClick="btndelete_Click"  OnClientClick="return confirm('Are you sure you want to delete this item?');"/>
         <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" />
       
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblEditgrd" runat="server" Text="" Visible="false"></asp:Label></td>
</tr>
</table>
                    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">
          <asp:GridView ID="grdquton" runat="server" AllowPaging="True" AllowSorting="True" PageSize="10" DataKeyNames="QUTIONID" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanging="grdquton_SelectedIndexChanging" OnPageIndexChanging="grdquton_PageIndexChanging">
            <Columns>
                  <asp:BoundField DataField="QUOTIONDATE" HeaderText="QUTATION&nbsp;DATE" />
                <asp:BoundField DataField="QUTIONID" HeaderText="QUTATION&nbsp;ID" />
                <asp:BoundField DataField="NAME1" HeaderText="VENDOR&nbsp;NAME" />
                  <asp:BoundField DataField="REFFDATE" HeaderText="REFF&nbsp;DATE" />
               
                
                <asp:CommandField HeaderText="ACTION" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
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
        </div>
          </ContentTemplate></asp:UpdatePanel></div>
</asp:Content>

