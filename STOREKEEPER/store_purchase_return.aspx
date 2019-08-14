<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_purchase_return.aspx.cs" Inherits="STOREKEEPER_store_purchase_return" %>

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
                </script>
                 <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
                <script type="text/javascript">
                    Sys.Application.add_load(function () {
                        $("[id$=txtitemname]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/STOREKEEPER/store_purchase_return.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Purchase Return
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
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Purchase Return</u></b></h1>
            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----Vendor :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Vendor :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:DropDownList ID="dropVendor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" OnSelectedIndexChanged="dropVendor_SelectedIndexChanged">
                                </asp:DropDownList>
                    </div>
                  <!----Statecode :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Statecode :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtstatecode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                    </div>
                 <asp:TextBox ID="txtdstatecode" runat="server" Enabled="False" Visible="False">21</asp:TextBox>
                </div>
                        <br><br>
					    <hr>
				   <h1 style="color:#0071bc;"><b><u>Return Details</u></b></h1>
                  <div class="ui-grid-row">
                    <!----MRN NO :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        MRN NO :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:TextBox ID="txtpono" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                    </div>
                  <!----Date Of Issue :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Date Of Issue :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtdateofissue" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                    <br><br>
			        <hr>
			
			      <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
               <div class="ui-grid-row">
                    <!----Item Name :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Item Name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtitemname" runat="server"  OnTextChanged="txtname_TextChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True"></asp:TextBox>
                                <asp:HiddenField ID="hfCustomerId" runat="server" />
                    </div>
                  <!----HSN Code:----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        HSN Code :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txthsncode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" Enabled="False"></asp:TextBox>
                    </div>
                </div>
                  <div class="ui-grid-row">
                    <!----Quantity :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Quantity :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtopening" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" OnTextChanged="txtopening_TextChanged" pattern="[0-9]+([,\.][0-9]+)?"
                                    title="Please Enter numeric value" value="0"
                                    onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}">

                       </asp:TextBox>
                    </div>
                  <!----Rate :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Rate :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txtpprice" runat="server" Enabled="true" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" title="Please Enter numeric value" value="0"
                                    onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                    </div>
                </div>
                  <div class="ui-grid-row">
                    <!----Unit :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Unit :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txtunit" runat="server" AutoPostBack="True" MaxLength="10" OnTextChanged="txtopening_TextChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" title="Please Enter numeric value" value="0"
                                    onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                    </div>
                 
                </div>
                  <div class="ui-grid-row">
                    <!----Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Amount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtamount" runat="server" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" title="Please Enter numeric value" value="0"
                                    onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                    </div>
                  <!----CGST % :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        CGST % :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtcgst" runat="server" Enabled="False" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                    </div>
                </div>
                  <div class="ui-grid-row">
                    <!----SGST % :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        SGST % :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtSgst" runat="server" Enabled="False" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                    </div>
                  <!----IGST % :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       IGST % :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtIGST" runat="server" Enabled="False" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                    </div>
                </div>
                  <div class="ui-grid-row">
                    <!----GST Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        GST Amount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtgstamount" runat="server" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0"></asp:TextBox>
                    </div>
                  <!----Total Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total Amount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:TextBox ID="txttotalamount" runat="server" Enabled="False" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0"></asp:TextBox>
                    </div>
                      <div class="ui-panelgrid-cell ui-grid-col-2">
                           <asp:Button ID="Button3" runat="server" OnClick="Button1_Click" Text="Add" CssClass="search" />
                          </div>
                </div>
                 <asp:GridView ID="grvStudentDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" ShowFooter="True" Style="text-align: right">
                                    <Columns>
                                        <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                                        <%--<asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>
                        <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

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
                                                <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                                                <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
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
                  <div class="ui-grid-row">
                    <!----Total Price :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total Price :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
                    </div>
                 
                </div>
                  <div class="ui-grid-row">
                    <!----Total GST Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total GST Amount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
                    </div>
                  <!----Grand Total :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Grand Total :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:Label ID="lblgrandtotal" runat="server" Style="color: #D20000; font-weight: 700" Text="0"></asp:Label>
                    </div>
                </div>
                 </div>
                </div>
				<br><br>
				<hr>
				<br>
                 <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Btncreate_Click" Text="Create" />
                                <asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />
                                <asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
                                &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br><br><br>
						
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                             <asp:GridView ID="GridView1" runat="server" Visible="False">
                                </asp:GridView>
                        
<br>
<br>
<br>
</div>
        </div>
    </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

