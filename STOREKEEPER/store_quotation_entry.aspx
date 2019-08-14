<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_quotation_entry.aspx.cs" Inherits="STOREKEEPER_store_quotation_entry" %>

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
 <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $("[id$=txtRfqNo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/STOREKEEPER/store_quotation_entry.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Quotation Entry
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
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Quotation Entry</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
            <div class="ui-grid-row">
                    <!----Quotation No :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Quotation No :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtQuotNo" runat="server" value="0" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" ></asp:TextBox>
                    </div>
                  <!---Reff Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Reff Date :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>     
                    </div>
                </div>
             <div class="ui-grid-row">
                    <!----RFQ No :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        RFQ No :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtRfqNo" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                 <!-------->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <asp:Button ID="btn_search" runat="server" CssClass="search" Text="Search" OnClick="btn_search_Click" />
        <asp:HiddenField ID="hfCustomerId" runat="server" />
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                            
                    </div>
                </div>
            <br />
            <hr />
            <br />
            
            <div class="ui-grid-row">
                    <!----Quotation Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Quotation Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtQuotDate" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                    </div>
                
                </div>
            <div class="ui-grid-row">
                    <!----Vendor name :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>Vendor name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="ddvendrNm" runat="server" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Width="180">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                 <!---State Code :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>State Code :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtStatecd" runat="server" MaxLength="2" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+" MinLength="2" ></asp:TextBox>    
                    </div>
                </div>
            
          
                    <asp:GridView ID="grvquonItem" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="100%">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
               <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                <asp:TemplateField HeaderText="Slno" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <%#Container.DisplayIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkQuot" runat="server"  OnCheckedChanged="chkQuot_CheckedChanged" AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HSNCODE" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <%-- <asp:TextBox  ID="txt_hsncode" runat="server" Width="50" Text='<%#Eval("HSNCODE") %>'></asp:TextBox>--%>
                         <asp:TextBox  ID="txt_hsncode" Enabled="false" runat="server" Width="70" Text='<%#Eval("HSNCODE") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="NAME" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="UNIT" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                        <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="PRICE" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                        <%--<asp:TextBox ID="txt_price" runat="server" Width="50"   AutoPostBack="true" OnTextChanged="txt_price_TextChanged" Text='<%#Eval("PRICE") %>'></asp:TextBox>--%>
                        <asp:TextBox ID="txt_price" runat="server" Width="60"   AutoPostBack="true" OnTextChanged="txt_price_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text="0" MaxLength="10" title="Please Enter Numeric Value"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>           
              
            
                <asp:TemplateField HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_amount" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_Amt" runat="server"  Enabled="false" Width="50" Text="0" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <%--<asp:TextBox ID="TextBox1" runat="server"  Width="50" AutoPostBack="true" Text='<%#Eval("GST") %>' OnTextChanged="txt_price_TextChanged"></asp:TextBox>--%>
                        <asp:TextBox ID="txt_cgst" runat="server" Enabled="false"  Width="50"  Text='<%#Eval("CGST") %>' AutoPostBack="true" OnTextChanged="txt_price_TextChanged" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <%--<asp:TextBox ID="TextBox2" Enabled="false" runat="server"  Width="50" Text='<%#Eval("GST") %>'   AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>--%>
                        <asp:TextBox ID="txt_sgst" Enabled="false" runat="server"  Width="50" Text='<%#Eval("SGST") %>' AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_igst" runat="server" Enabled="false" Width="50" Text="0" AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTAMT" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                     <%--<asp:TextBox ID="TextBox3" runat="server"  Width="50" Text='<%#Eval("GSTAMT") %>' Enabled="false" ></asp:TextBox>--%>
                        <asp:TextBox ID="txt_gstamt" runat="server"  Width="50" Text="0" Enabled="false" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT" ItemStyle-HorizontalAlign="Center">
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
                        <asp:TextBox ID="txt_cgst" runat="server" Enabled="false"  Width="50" Text='<%#Eval("CGST")%>' AutoPostBack="true" OnTextChanged="txt_price_TextChanged"></asp:TextBox>
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
            <div runat="server" id="divGrdTot" visible="false">
               <br />
                <hr />
                <br />
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
                    <!----Total Gst Amount :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Total Gst Amount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
                    </div>
                 </div>
                       <div class="ui-grid-row">
                    <!----Grand Total :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Grand Total :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:Label ID="lblgrandtotal" runat="server" style="color: #D20000; font-weight: 700" Text="0"></asp:Label>
                    </div>
                 </div>
                    </div>
                </div>

                </div>
            <br><br><br>
					   
				
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click" />&nbsp;
         <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="create" Visible="False"  OnClick="btndelete_Click"  OnClientClick="return confirm('Are you sure you want to delete this item?');"/>
         <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                         <br><br>
						 <hr>
						 <br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           <asp:Label ID="lblEditgrd" runat="server" Text="" Visible="false"></asp:Label>
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            QUOTATION ENTRY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdquton" runat="server" AllowPaging="True" Width="1060" AllowSorting="True" DataKeyNames="QUTIONID" AutoGenerateColumns="False" CellPadding="4" OnSelectedIndexChanging="grdquton_SelectedIndexChanging" OnPageIndexChanging="grdquton_PageIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                  <asp:BoundField DataField="QUOTIONDATE" HeaderText="QUTATION&nbsp;DATE" />
                <asp:BoundField DataField="QUTIONID" HeaderText="QUTATION&nbsp;ID" />
                <asp:BoundField DataField="NAME1" HeaderText="VENDOR&nbsp;NAME" />
                  <asp:BoundField DataField="REFFDATE" HeaderText="REFF&nbsp;DATE" />
               
                
                <asp:CommandField HeaderText="ACTION" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
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
<br>
<br>
</div>
        </div>
    </div>
             </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

