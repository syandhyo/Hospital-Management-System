<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_QuotationView.aspx.cs" Inherits="STOREKEEPER_store_QuotationView" %>

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
               <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> 	PO Releaser	

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
          <asp:Label ID="lblQotation" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
                <div class="card card-w-title">
                    <h1 style="color:#0071bc;"><b><u>Quotation Entry</u></b></h1>
                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
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
                <div class="ui-panelgrid-cell ui-grid-col-1">
                      <asp:Button ID="btn_search" runat="server" CssClass="search" Text="Search"  />
                      <asp:HiddenField ID="hfCustomerId" runat="server" />
                    </div>
                </div>   
                <div class="ui-grid-row">
                   
                 <!----Quotation No :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Quotation No :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtQuotNo" runat="server" Text="0" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                </div>   
                              <div class="ui-grid-row">
                    <!----Vendor name :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Vendor name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:DropDownList ID="ddvendrNm" runat="server" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                  <!----Reff Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Reff Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                </div>   
                              <div class="ui-grid-row">
                    <!----State Code :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       State Code :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtStatecd" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                 <!----Quotation Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Quotation Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtQuotDate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                </div>  
                 <asp:GridView ID="grvquotion" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" ShowFooter="True" style="text-align: right">
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
                        <asp:CheckBox ID="chkQuot" runat="server" Checked='<%#Eval("CHKSEL") %>'  AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HSNCODE">
                    <ItemTemplate>
                       <asp:TextBox  ID="txt_hsncode" runat="server" Text='<%#Eval("HSNCODE")%>'  Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
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
                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QUANTITY")%>'></asp:Label>
                        <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="PRICE">
                    <ItemTemplate>
                       <%-- <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_price" runat="server" Width="50" Text='<%#Eval("PRICE")%>' AutoPostBack="true" ></asp:TextBox>
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
                        <asp:TextBox ID="txt_cgst" runat="server"  Width="50" Text='<%#Eval("CGST")%>' AutoPostBack="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_sgst" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_sgst" Enabled="false" runat="server"  Width="50" Text='<%#Eval("SGST")%>' AutoPostBack="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST">
                    <ItemTemplate>
                        <%--<asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_igst" runat="server" Enabled="false" Width="50" Text='<%#Eval("IGST")%>' AutoPostBack="true" ></asp:TextBox>
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
                    <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" Visible="false"/>

        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" Visible="false"/>
                    </div>
                </div>
                    </div>
                <br />
                <br />
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

