<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_purchase.aspx.cs" Inherits="STOREKEEPER_store_purchase" %>

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
                $(function () {
                    SetDatePicker2();
                });

                //On UpdatePanel Refresh.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker2();
                        }
                    });
                };

                function SetDatePicker2() {
                    $("[id$=txtpodate]").datepicker({
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
                          $("[id$=txtpono]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/store_purchase.aspx/GetCustomers") %>',
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
         <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
          <script type="text/javascript">
              Sys.Application.add_load(function () {
                  $("[id$=txtMaterial]").autocomplete({
                      source: function (request, response) {
                          $.ajax({

                              url: '<%=ResolveUrl("~/STOREKEEPER/store_purchase.aspx/GetCustomers1") %>',
                              data: "{ 'prefix1': '" + request.term + "'}",
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
                          $("[id$=HiddenField1]").val(i.item.val);
                      },
                      minLength: 1
                  });
              });
             
                </script>

            
                            <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> <B>Goods Receive Notes</B>
                    </div>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblEditgrd" runat="server" Text="" Visible="false"></asp:Label>
            <asp:TextBox ID="txtContrtno" runat="server" Visible="false"></asp:TextBox>
             <asp:HiddenField ID="hdnprice" runat="server" />
          <asp:HiddenField ID="hdngst" runat="server" />
          <asp:HiddenField ID="hdnhsncd" runat="server" />
          <asp:HiddenField ID="hdnqty" runat="server" />
            <asp:Label ID="lblsgst" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblcgst" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblamt" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblgstamnt" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbltotalamt" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbligst" runat="server" Text="0.00" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----GRN No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        &nbsp;GRN No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtgrnno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                    </div>
                  <!----GRN Date :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>&nbsp;GRN Date :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtgrndate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            <div class="ui-grid-row">
                <!----Type :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Type :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Type</asp:ListItem>
                             <asp:ListItem Value="1">Direct</asp:ListItem>
                             <asp:ListItem Value="2">Normal</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <!----Vendor :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>&nbsp;Vendor :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:DropDownList ID="dropVendor" runat="server" OnSelectedIndexChanged="dropVendor_SelectedIndexChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" TabIndex="1" AutoPostBack="True" >
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
                      
                    </div>
                  <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtstatecode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="False"></asp:TextBox>
                    </div>
                </div>
              <div class="ui-grid-row">
                    <!---Invoice No :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>&nbsp;Invoice No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtinvoiceno" runat="server" patern="[a-zA-Z0-9]+" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Alphanumeric Value" TabIndex="2"></asp:TextBox>
           
                    </div>
                   <!----PO No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        PO No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtpono" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="TRUE" TabIndex="3" ></asp:TextBox>
                    </div>
                
                    <!----PO No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <asp:Button ID="btnview" runat="server" Text="View" CssClass="search" OnClick="btnview_Click" TabIndex="4" />
                    </div>
                </div>
              <div class="ui-grid-row">
                    <!---Invioce Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>&nbsp;Invoice Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtinvoicedate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" OnTextChanged="txtinvoicedate_TextChanged" AutoPostBack="True" TabIndex="5"></asp:TextBox>
           
                    </div>
                 <!----PO Date :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       PO Date :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtpodate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" onkeydown="return false;" onpaste ="return false;"></asp:TextBox> 
                    </div>
                </div>
            
             	
             <div id="directdiv" runat="server" visible="false">
                 
                 <hr />
                 
            <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
                
                 <div class="ui-grid-row">
                    <!---Item Name :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Item Name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                             placeholder="Item" pattern="^[A-Za-z -]+$" ToolTip="Enter Item Here" AutoPostBack="true" TabIndex="6" OnTextChanged="txtMaterial_TextChanged"></asp:TextBox>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                    </div>
                 <!----Unit :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Unit :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Unit Here" pattern="^[a-zA-Z0-9_]*" TabIndex="7"></asp:TextBox> 
                    </div>
                     <!---Quantity :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Quantity :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtqty" runat="server" placeholder="Quantity" ToolTip="Enter Quantity Here" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true" OnTextChanged="txtqty_TextChanged" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" TabIndex="8"></asp:TextBox>
           
                    </div>
                 <!----button---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Button ID="btnAdd" runat="server" OnClientClick="return Validate();" Text="ADD" CssClass="search" TabIndex="9" OnClick="btnAdd_Click"/>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     
                    </div>
                </div>
                 <%--<div class="ui-grid-row">
                    
                </div>--%>
                 <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None"  Width="100%" OnRowDeleting="grdMaterial_RowDeleting" TabIndex="10">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl&nbsp;No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item&nbsp;Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORD.QTY" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>--%>
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50"  CssClass="GridTextBox" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" AutoPostBack="true" Text='<%#Eval("QTY")%>' OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HSNCODE" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_hsn" runat="server" Text='<%#Eval("HSNCODE")%>'></asp:Label>
                    </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRICE" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_unit" runat="server" Width="50" Text='<%#Eval("UNIT")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                    
                                         <asp:TemplateField HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_cgst" runat="server" Text='<%#Eval("CGST")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                         <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_sgst" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTAMT" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_gstamt" runat="server" Text='<%#Eval("GSTAMT")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lbl_totalamount" runat="server" Text='<%#Eval("TOTALAMOUNT")%>'></asp:Label>
                        <%--<asp:TextBox ID="txt_value" runat="server"  Width="50" CssClass="GridTextBox" AutoPostBack="true" ReadOnly="true" Text='<%#Eval("VALUE")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Action" />
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
                 <div class="ui-grid-row">
                    <!---Total Price :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total Price :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lblpriceadd" runat="server" Text="0.00"></asp:Label>
                    </div>
                     <!---Total GST Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total GST Amount:
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lbltotalgstadd" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Total Discount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lbltotaldicadd" runat="server" Text="0.00"></asp:Label>
           
                    </div>
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Grand Total :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lbltotaladd" runat="server" style="color: #D20000; font-weight: 700" Text="0.00"></asp:Label>
                    </div>
                    </div>
              <%--  <div class="ui-grid-row">
                    
                    
                    </div>--%>
                 </div>
            <div id="normaldiv" runat="server" visible="false">
               
                <hr />
                <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
                
                <asp:GridView ID="grvpurchsItem" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="grvpurchsItem_RowDeleting"  ShowFooter="True" style="text-align: right" TabIndex="11">
            <Columns>
              
                <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                       
                        <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                       
                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged1" AutoPostBack="true" Checked="FALSE"/>
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
                        <asp:TextBox  ID="txt_Qty" runat="server" Width="50" pattern="[0-9]+([,\.][0-9]+)?" OnTextChanged="txt_Qty_TextChanged" AutoPostBack="true" Text='0.00' value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
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
               <asp:CommandField ShowDeleteButton="FALSE" HeaderText="Action" />
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
        <%-----------------------------------------%>
          <asp:GridView ID="grvpuritemTemp" runat="server" Visible="false" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVOnRowDataBound"  ShowFooter="True" style="text-align: right">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                       
                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"  AutoPostBack="true" Checked='<%#Eval("Isselected")%>'/>
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
                        <asp:Label ID="lbl_gstamt" runat="server" Text='<%#Eval("GSTMAT")%>'></asp:Label>
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

                <div class="ui-grid-row">
                    <!---Total Price :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total Price :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lbltotalprice" runat="server" Text="0.00"></asp:Label>
           
                    </div>
                      <!---Total GST Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total GST Amount:
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lblgstamt" runat="server" Text="0.00"></asp:Label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Total Discount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lbltotaldisc" runat="server" Text="0.00"></asp:Label>
           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Grand Total :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="lblgrandtotal" runat="server" style="color: #D20000; font-weight: 700" Text="0.00"></asp:Label>
                    </div>
              
                </div>
            </div>
                   </div>
				
				<hr>
					
                 <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Btncreate_Click" Text="Create" TabIndex="12" />
        <asp:Button ID="btnupdate" runat="server" CssClass="update"  Text="Update" Visible="False" OnClick="btnupdate_Click" TabIndex="13"/>
        <asp:Button ID="btndelete" runat="server" CssClass="create"  Text="Delete" Visible="False" OnClick="btndelete_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" TabIndex="14"/>
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click"  Text="Cancel" TabIndex="15" />
                        <br />
						<br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            GOODS RECEIVE DETAILS
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvPurchase" runat="server" Width="1060" AutoGenerateColumns="False" DataKeyNames="GRNNO"  AllowPaging="True"
             AllowSorting="True"   CellPadding="4" 
             OnSelectedIndexChanging="grvPurchase_SelectedIndexChanging" OnPageIndexChanging="grvPurchase_PageIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="GRNNO" HeaderText="GRN&nbsp;NO" />
                 <asp:BoundField DataField="GRNDATE" HeaderText="GRN&nbsp;DATE" />
                  <asp:BoundField DataField="INVOICENO" HeaderText="INVOICE&nbsp;NO" />
                 <asp:BoundField DataField="INVOICEDATE" HeaderText="INVOICE&nbsp;DATE" />
                 <asp:BoundField DataField="NAME1" HeaderText="VENDOR&nbsp;NAME" />
                <%-- <asp:BoundField DataField="INVOICENO" HeaderText="PARTY&nbsp;NAME" />
                 <asp:BoundField DataField="INVOICENO" HeaderText="PARTY&nbsp;NAME" />
                 <asp:BoundField DataField="INVOICENO" HeaderText="PARTY&nbsp;NAME" />--%>
                <%--<asp:CommandField  ShowSelectButton="false"  SelectText="&nbsp;&nbsp;&nbsp;Edit"/>--%>
            </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#F5F7FB" />
              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
              <SortedDescendingCellStyle BackColor="#E9EBEF" />
              <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                                </div><br>

<br>
</div>
        </div>
    </div>
            </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

