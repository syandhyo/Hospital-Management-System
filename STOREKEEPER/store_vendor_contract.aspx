<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_vendor_contract.aspx.cs" Inherits="STOREKEEPER_store_vendor_contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function Validate() {

         }
    </script>
    <script type="text/javascript">

        function ValidateCret() {

            if (document.getElementById("<%=txtdate.ClientID%>").value == "") {
                alert("Date Field Is Required !");
                document.getElementById("<%=txtdate.ClientID%>").focus();
                return false;
            }
        }
    </script>
     <script type = "text/javascript">

         function SetTarget() {

             document.forms[0].target = "_blank";

         }
        </script>
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

    </script>
             <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
             <script type="text/javascript">
                 Sys.Application.add_load(function () {
                     $("[id$=txtQuotaion]").autocomplete({
                         source: function (request, response) {
                             $.ajax({
                                 url: '<%=ResolveUrl("~/STOREKEEPER/Vendor_Contract.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Vendor Contract
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
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Vendor Contract</u></b></h1>
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                    </div>
                 <!----Contract Id :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Contract Id :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtInvoice" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"  Text=""></asp:TextBox>
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Vendor :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Vendor :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:DropDownList ID="ddVendor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" >
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
                    </div>
                
                </div>
					
					    <hr>
				   <h1 style="color:#0071bc;"><b><u>Quotation Details</u></b></h1>
              <div class="ui-grid-row">
                    <!---Quotation No :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Quotation No :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtQuotaion" runat="server"  Text="" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
         <asp:HiddenField ID="hfCustomerId" runat="server" />
           
                    </div>
                 <!----Date :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <asp:button id="btnadd" runat="server" CssClass="search"  onclientclick="return validate();" text="View" OnClick="btnadd_Click" />
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       
                    </div>
                </div>
           
			   <hr>
			     <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
            <asp:GridView ID="grdVendor" runat="server" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
              <AlternatingRowStyle BackColor="White" />
            <Columns>
             
               <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                      <%#Container.DisplayIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkQuot" runat="server" Checked='<%# Eval("CHKSEL") %>' OnCheckedChanged="chkQuot_CheckedChanged"  AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HSNCODE">
                    <ItemTemplate>
                          <asp:Label ID="lbl_hsncode" runat="server" Text='<%#Eval("HSNCODE")%>'></asp:Label>
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
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50" AutoPostBack="true" Text='<%#Eval("QUANTITY")%>'></asp:TextBox>
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
                      
                       <%-- <asp:TextBox ID="txt_Amt" runat="server"  Enabled="false" Width="50" Text="0" ></asp:TextBox>--%>
                          <asp:Label ID="lbl_Amt" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST">
                    <ItemTemplate>
                      <%--  <asp:TextBox ID="txt_cgst" runat="server"  Width="50" Text="0" AutoPostBack="true"></asp:TextBox>--%>
                         <asp:Label ID="lbl_cgst" runat="server" Text='<%#Eval("CGST")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST">
                    <ItemTemplate>
                      <%--  <asp:TextBox ID="txt_sgst" Enabled="false" runat="server"  Width="50" Text="0" AutoPostBack="true" ></asp:TextBox>--%>
                         <asp:Label ID="lbl_sgst" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST">
                    <ItemTemplate>
                       <%-- <asp:TextBox ID="txt_igst" runat="server" Enabled="false" Width="50" Text="0" AutoPostBack="true"></asp:TextBox>--%>
                         <asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTAMT">
                    <ItemTemplate>
                      <%--  <asp:TextBox ID="txt_gstamt" runat="server"  Width="50" Text="0" Enabled="false" ></asp:TextBox>--%>
                         <asp:Label ID="lbl_gstamt" runat="server" Text='<%#Eval("GSTAMT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT">
                    <ItemTemplate>
                     <%--   <asp:TextBox ID="txt_TotAmt" runat="server"  Width="50" Text="0" Enabled="false"></asp:TextBox>--%>
                         <asp:Label ID="lbl_totamt" runat="server" Text='<%#Eval("TOTAMT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                 
               
                <%--<asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>--%>
            </Columns>
              <EditRowStyle BackColor="#7C6F57" />
              <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#E3EAEB" />
             <SelectedRowStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
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
                        Total Gst Amount :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
                    </div>
                 <!----Grand Total :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget">Grand Total :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:Label ID="lblgrandtotal" runat="server" style="color: #D20000; font-weight: 700" Text="0"></asp:Label>    
                    </div>
                </div>
            </div>
                   </div>
				<br><br>
				<hr>
					<br>
                  <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" OnClientClick="return ValidateCret();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
        <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="create" Visible="False"   OnClick="btndelete_Click"  OnClientClick="return confirm('Are you sure you want to delete this item?');"/>
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                         <br><br><br>
						
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            VENDOR CONTRACT
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdVendContrct" runat="server" Width="1060" AllowPaging="True" AllowSorting="True" DataKeyNames="CONTRACTID" AutoGenerateColumns="False" CellPadding="4" OnSelectedIndexChanging="grdVendContrct_SelectedIndexChanging" OnPageIndexChanging="grdVendContrct_PageIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:BoundField DataField="DATE" HeaderText="DATE" />
                  <asp:BoundField DataField="CONTRACTID" HeaderText="CONTRACT&nbsp;NO" />
                <asp:BoundField DataField="NAME1" HeaderText="VENDOR&nbsp;NAME" />
                <asp:BoundField DataField="QUTIONNO" HeaderText="QUOTATION&nbsp;NO" />                
               
                
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

