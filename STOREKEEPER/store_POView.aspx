<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_POView.aspx.cs" Inherits="STOREKEEPER_store_POView" %>

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
             <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> 	PO 

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
             <asp:Label ID="lblPOId" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblPOView" runat="server" Text="" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
        
				<h1 style="color:#0071bc;"><b><u>Purchase Order(PO)</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----Vendor :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Vendor :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:DropDownList ID="dropVendor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"  AutoPostBack="True"  Enabled="false">
                    </asp:DropDownList>
                    </div>
                
                </div>     
                 <div class="ui-grid-row">
                    <!----Statecode :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Statecode :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtstatecode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                    </div>
                  <!----Ref No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Ref No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtrefno" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                <div class="ui-panelgrid-cell ui-grid-col-1">
                     <asp:Button ID="btnview" runat="server" Text="View" CssClass="search" />
                    </div>
                </div>    
                 <hr />
                  <h1 style="color:#0071bc;"><b><u>Order Details</u></b></h1>    
                  <div class="ui-grid-row">
                    <!----PO No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        PO No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtpono" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                    </div>
                  <!----Date Of Issue :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Date Of Issue :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtdateofissue" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                
                </div>    
                  <div class="ui-grid-row">
                    <!----Ref Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Ref Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtrefdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                    </div>
                </div>    
                 <hr />
                 <h1 style="color:#0071bc;"><b><u>Delivery Address</u></b></h1>    
                  <div class="ui-grid-row">
                    <!----Org Name :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Org Name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtorgname" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server"></asp:TextBox>
                    </div>
                  <!----Pin :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>Pin :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtpin" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                
                </div>    
                  <div class="ui-grid-row">
                    <!----City :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       City :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtcity" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                  <!----Phone :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Phone :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtphone" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="9876543526"></asp:TextBox>
                    </div>
                
                </div>    
                  <div class="ui-grid-row">
                    <!----State :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        State :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtstate" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server"></asp:TextBox>
                    </div>
                  <!----StateCode :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        StateCode :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtdstatecode" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server"></asp:TextBox>
                    </div>
                
                </div>    
                  <div class="ui-grid-row">
                    <!----Address :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Address :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtaddress" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server"></asp:TextBox>
                    </div>
                  <!----GSTIN :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        GSTIN :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtgstin" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server"></asp:TextBox>
                    </div>
                
                </div>    
                 <hr />
                 <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>  
                 <asp:GridView ID="grvpoView" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  ShowFooter="True" style="text-align: right">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                <asp:TemplateField HeaderText="SLNO">
                    <ItemTemplate>
                        <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="SELECT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                       
                        <asp:CheckBox ID="CheckBox1" runat="server"  Checked='<%#Eval("ischecked") %>'  CssClass="ChkBoxClass"  AutoPostBack="true"/>
                      
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
             
                 <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50" pattern="[0-9]+([,\.][0-9]+)?" Text='<%#Eval("QTY")%>'  AutoPostBack="true" ></asp:TextBox>
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
                        <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>Total Gst Amount :
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
                 <hr />
                  <h1 style="color:#0071bc;"><b><u>Terms & Conditions</u></b></h1>  
                 <div class="ui-grid-row">
                    <!----Duties &amp; Taxes :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Duties &amp; Taxes :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_term_condition" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Taxes will be extra as per Material category" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                  <!----Test Report :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Test Report :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txt_TEST_REPORT0" runat="server" Text="Test report is Mandatory for the material application" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                </div>
                 <div class="ui-grid-row">
                    <!----Delivery Term :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Delivery Term :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txt_DELIVERY_TERM" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Immediate"  TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                  <!----Inspection:----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Inspection:
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_INSPECTION0" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="In Your Scope" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>
                 <div class="ui-grid-row">
                    <!----Delivery Time&nbsp; :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Delivery Time&nbsp; :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_DELIVERY_TIME" runat="server" Text="NA" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                  <!----Waranty/Guarantee:----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Waranty/Guarantee:
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_WARANTY0" runat="server" Text="In Your Scope" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>

                 <div class="ui-grid-row">
                    <!----Freight :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Freight :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_FREIGHT" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Freight Extra" Width="170"></asp:TextBox>
                    </div>
                  <!----Licence &amp; Permit :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label27" runat="server" ForeColor="Red" Text="*"></asp:Label>Licence &amp; Permit :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_LICENCE_PERMIT0" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="NA" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>
                 <div class="ui-grid-row">
                    <!----Make :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Make :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_make" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="The make &amp; techinical specification of the material should be as per " Width="170"></asp:TextBox>
                    </div>
                  <!----Price &amp; Payment Terms :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Price &amp; Payment Terms :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_PRICE_PAYMENT" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="100% Payment shall be given with in 30days from delivery" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>
                 <div class="ui-grid-row">
                    <!----Packing &amp; Forwarding :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Packing &amp; Forwarding :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_PACKING_FORWARDING" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="InYour Scope" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                  <!----Acceptance :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Acceptance :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_ACCEPTANCE0" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="In the case of GOODS delivered by SUPPLIER not confirming with the PURCHASE ORDER whether by reason of not being of the quality or in the quantity or measurement stipulated or being unfit for the purpose for which they are required, PURCHASER shall have the right to reject such GOODS within a reasonable time of their delivery and inspection and to purchase else where and to claim for any additional expense incurred without any prejudice to any other right which PURCHASER may have against SUPPLIER. The making of any prior payments by PURCHASER shall not prejudice PURCHASER'S right of rejection." TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>
                 <div class="ui-grid-row">
                    <!----Transit Insurance :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Transit Insurance :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_TRANSIT_INSURANCE" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="InYour Scope" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                  <!----Termination :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Termination :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_TERMINATION0" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="In the event of any breach of any of the terms and conditions of the PURCHASE ORDER including failure to deliver by the due date, then PURCHASER without prejudice to any other rights, may terminate the PURCHASE ORDER and may return GOODS previously supplied under the PURCHASE ORDER for full credit by SUPPLIER. In the event of termination due to non-delivery or non-acceptance due to SUPPLIER'S breach of the terms and conditions hereof, SUPPLIER shall undertake to reimburse all monies paid by PURCHASER prior to the date of termination including all direct costs and expenses incured by PURCHASER arising from or in connection with the Termination." TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>

                 <div class="ui-grid-row">
                    <!----Loading &amp; UnLoading:----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Loading &amp; UnLoading:
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_LOADING_UNLOADING" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Loading is in your scope &amp; unloading at site in our scope" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                  <!----Installation &amp; Commissioning :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Installation &amp; Commissioning :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_INSTALLATION_COMMI1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Na" TextMode="MultiLine" Width="170"></asp:TextBox>
                    </div>
                
                </div>

                </div>
            </div> 
            <br><br>
				<hr>
				<br>
            <asp:Button ID="btncreate" runat="server" CssClass="create"  Text="Create" Visible="False"/>
        <asp:Button ID="btnupdate" runat="server" CssClass="update"  Text="Update"/>
        <asp:Button ID="btndelete" runat="server" CssClass="create"  Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" Text="Cancel" />
        </div>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

