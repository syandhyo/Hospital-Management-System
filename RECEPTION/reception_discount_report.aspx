<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_discount_report.aspx.cs" Inherits="RECEPTION_reception_discount_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker1();
                        }
                    });
                };
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
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Discount Report
                    </div>
    
                </div>

    <div class="ui-fluid">
        <strong>
         <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
    </div>
    <div class="card card-w-title">
        <h1 style="color:#0071bc;"><b><u>Report For Discount</u></b></h1>

 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
     <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

        <div class="ui-grid-row">
            <div class="ui-panelgrid-cell ui-grid-col-1">
                From Date:
            </div>
            <div class="ui-panelgrid-cell ui-grid-col-4">
                 <asp:TextBox ID="txtfdate" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
            </div>
           
        </div>
        <div class="ui-grid-row">
            <div class="ui-panelgrid-cell ui-grid-col-1">
                To Date :
            </div>
            <div class="ui-panelgrid-cell ui-grid-col-4">
                <asp:TextBox ID="txttdate" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" ></asp:TextBox>
            </div>
           
        </div>
        <div class="ui-panelgrid-cell ui-grid-row">
          
            <div class="ui-panelgrid-cell ui-grid-col-1">
                
            <asp:Button ID="btnShow" runat="server" CssClass="search" Text="Show"  OnClick="btnShow_Click" />
                </div>
                 <div class="ui-panelgrid-cell ui-grid-col-2">
                  <asp:Button ID="Button1" runat="server" CssClass="search" Text="Export To Excel" OnClick = "ExportToExcel" Visible="False" Width="117px"/>
                </div>
                    
        </div>
  </div>
     </div>      	
        <h1>&nbsp;</h1>
        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
           
            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Discount Report Detail
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" width="1090" DataKeyNames="ID"  AllowPaging="True" 
                                AllowSorting="True"  CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4"
                                 ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
            <Columns>
                <%--<asp:BoundField DataField="ID" HeaderText="ID" />--%>
                <asp:BoundField DataField="PID" HeaderText="PID" />
                 <asp:BoundField DataField="NAME" HeaderText="NAME" />
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                  <asp:BoundField DataField="TOTALAMT" HeaderText="TOTAL&nbsp;AMT" />
                <asp:BoundField DataField="BALANCEAMT" HeaderText="BALANCE&nbsp;AMT" />
                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowSelectButton="true" HeaderText="ACTION"/>--%>
            </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
            <%--<SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />--%>
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                        </div>
            <br />
        </div>
    </div>
              

              
              </strong>
              </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

