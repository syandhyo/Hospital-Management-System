<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_po_report.aspx.cs" Inherits="STOREKEEPER_store_po_report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function openpopup() {
             window.open("store_pobill.aspx")
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
                       $("[id$=TextBox1]").datepicker({
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
                       $("[id$=TextBox2]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> PO Report
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
					 <h1 style="color:#0071bc;"><b><u>All PO Report</u></b></h1>
				
                           <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----Select from date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select from date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="TextBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                    </div>
                 </div>
                 <div class="ui-grid-row">
                    <!----Select to date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select to date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="TextBox2" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                    </div>
                 </div>
                 <div class="ui-grid-row">
                    <!----Select from date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="search" Text="Show" />
                    </div>
                 </div>
                 </div>
                </div>
            <br /><br />
                     <asp:GridView ID="grSearchPatient" runat="server" AutoGenerateColumns="False"  pagesize="10" AllowSorting="True"  CssClass="table table-bordered" >
            <Columns>                               
                  <asp:BoundField DataField="DATE" HeaderText="Date" />
                <asp:BoundField DataField="PONO" HeaderText="PO No." />
              <asp:BoundField DataField="NAME" HeaderText="Vendor" /> 
               <asp:TemplateField HeaderText="Select">
            <ItemTemplate >
                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openpopup();" OnClick="LinkButton1_Click">View&nbsp;PO&nbsp;Report</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField> 

                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
            </Columns>
                   <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
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
<br>
<br>
<br>

        </div>
    </div>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

