<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="Item_detail_Entry.aspx.cs" Inherits="PHARMACYSTORE_Item_detail_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
     <script type="text/javascript">
         //On Page Load.
         $(function () {
             SetDatePicker();
         });

         $(function () {
             SetDatePicker1();
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
             $("[id$=txtexpirydate]").datepicker({
                 dateFormat: 'dd-mm-yy',
                 showOn: 'button',
                 buttonImageOnly: true,
                 dateFormat: 'dd-mm-yy',
                 changeMonth: true,
                 changeYear: true,
                 minDate: '0',

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
                                url: '<%=ResolveUrl("~/PHARMACYSTORE/pharmacy_purchase.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Purchase
                    </div>
 
                </div>

                <div class="layout-main-content">
        <div class="ui-fluid">
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                     <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtcstatecode" runat="server" Visible="False">21</asp:TextBox>
            <asp:TextBox ID="txtcomp" runat="server" AutoPostBack="True" Visible="False"></asp:TextBox>
        </div>
        <div class="card card-w-title">
					
					 <h1 style="color:#203a5a;"><b><center>Detail Entry</center></b>
                         <h1></h1>
                         <hr>
                         <h1 style="color:#0071bc;"><b><u>Party Info</u></b></h1>
                         <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                             
                               
                                 <div class="ui-grid-row">
                                     <!---- Item Name : :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                        Item Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropitem" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="dropitem_SelectedIndexChanged"></asp:DropDownList>
                                     </div>
                                    <!---- Hsn No :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Hsn No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txthsncode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" Enabled="False"></asp:TextBox>
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
                                     </div>
                                      <!----Expiry----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">Expiry Date </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                          <asp:DropDownList ID="dropexp"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" runat="server"></asp:DropDownList>
                                     </div>
                                 </div>
                                
                                 <div class="ui-grid-row">
                                   <!---- Total Quantity :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                        Total Quantity :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                          <asp:TextBox ID="txtqty" runat="server" Text="0" pattern="[0-9]+([,\.][0-9]+)?"
                                               MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True"
                                               ></asp:TextBox>
                                     </div>
                                      <!----Expiry----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">Select Cell </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                          <asp:DropDownList ID="dropcell"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" runat="server"></asp:DropDownList>
                                     </div>
                                 </div>
                                
                                
                                 
                                 </hr>
                         
                                 </div>
                         </div>
         
                         <br>

                         <br>
                         <hr>
                         <br>
                         <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="btncreate_Click" Text="Create" />
                         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />
                         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
            <br />
                         <br />
                        
          <hr />
                         <br>
                         <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                             <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                LOCATION DETAIL
                             </div>
                             <div class="ui-datatable-tablewrapper">
                                 <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="LOC_ID"  AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" 
                                     OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnRowDeleting="GridView2_RowDeleting"
                                     OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                     <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="RACK_NO" HeaderText="CELL" />
                 <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                  <asp:BoundField DataField="NAME" HeaderText="ITEM NAME" />
                <asp:CommandField  ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowDeleteButton="true"  HeaderText="ACTION"/>
            </Columns>
                                     <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#F5F7FB" />
              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
              <SortedDescendingCellStyle BackColor="#E9EBEF" />
              <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                                 <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
                             </div>
                             
                         </div>
                        
                    
        </div>
    </div>
       </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

