<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_duty_roster_report.aspx.cs" Inherits="RECEPTION_reception_duty_roster_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(
    <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <Triggers>
              <asp:PostBackTrigger ControlID="Button1" />
         </Triggers>
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
                           SetDatePicker1();
                       }
                   });
               };
               function SetDatePicker() {
                   $("[id$=txtfdate]").datepicker({
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
               //var prm = Sys.WebForms.PageRequestManager.getInstance();
               //if (prm != null) {
               //    prm.add_endRequest(function (sender, e) {
               //        if (sender._postBackSettings.panelsToUpdate != null) {
               //            SetDatePicker1();
               //        }
               //    });
               //};
               function SetDatePicker1() {
                   $("[id$=txttdate]").datepicker({
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
                    <i class="fa fa-home"></i><span>/ </span>Reports <span>/ </span>Duty Roster Report
                </div>

            </div>

            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong>
                         <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>

                </div>

                <div class="card card-w-title">
                    <h1 style="color: #0071bc;"><b><u>Duty Roster Report</u></b></h1>
             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                  <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            From Date :
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtfdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>

                        </div>
                       
                    </div>
                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            To Date :
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">

                            <asp:TextBox ID="txttdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                        </div>
                       
                    </div>
                    <div class="ui-grid-row">
                       
                        <div class="ui-panelgrid-cell ui-grid-col-1">
                            <asp:Button ID="btncreate" runat="server" CssClass="search" OnClick="Button1_Click" Text="Show" style="margin-left:5%;" />
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                           <asp:Button ID="Button1" runat="server" Text="Export To Excel" CssClass="search"  
                               OnClick="ExportToExcel" Visible="False" width="117px" style="margin-left:1%;"/>
                        </div>
                    </div>

            </div>
                 </div>
                    
                    <h1>&nbsp;</h1>
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        <div class="ui-grid-row" style="text-align: right; margin-bottom: 10px;">
                            
                        </div>
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Duty Roster Detail
                        </div>
                        <div class="ui-datatable-tablewrapper">

                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="id" AllowPaging="True" PageSize="10" Width="1060" 
                                OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Sname" HeaderText="Staff" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Ward" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                    <asp:BoundField DataField="FDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="TDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>--%>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                               <%-- <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />--%>
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>

                    </div>
                </div>


            </div>

            </strong>

        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>

