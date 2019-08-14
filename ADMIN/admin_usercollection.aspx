<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_usercollection.aspx.cs" Inherits="ADMIN_admin_usercollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
        <ContentTemplate>
            <script type="text/javascript">
                //On Page Load.
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
                    $("[id$=txtfromdate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        maxDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../bootstraptemplate/images/calendar.png'

                    });
                }


                //On Page Load.
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
                    $("[id$=txttodate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        maxDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../bootstraptemplate/images/calendar.png'

                    });
                }

            </script>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>User Wise Collection Report
                </div>
               
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>

            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong />
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
                <div class="card card-w-title">
                    <br />
                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                            <!----  Select User----->
                            <div class="ui-grid-row">
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select User</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropuser" runat="server" width="179px" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <!---- From Date :----->
                            <div class="ui-grid-row">
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>From Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtfromdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        onkeydown="return false;" onpaste="return false;"></asp:TextBox>

                                </div>
                            </div>
                            <!---- To Date----->
                            <div class="ui-grid-row">
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>To Date:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txttodate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        onkeydown="return false;" onpaste="return false;"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />

                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" CssClass="add" style="margin-left:10%;" />
                    <asp:Button ID="Button2" runat="server" CssClass="search" Text="Export" Visible="false" OnClick="Button2_Click" />
                    <h1>&nbsp;</h1>
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                       
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            User Collections
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="1056" GridLines="None" ShowFooter="True" EmptyDataText="No Records" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="SLNo" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DATE" HeaderText="DATE" />
                                    <asp:BoundField DataField="USERID" HeaderText="USER" />
                                    <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                                    <asp:BoundField DataField="CAMOUNT" HeaderText="AMOUNT" DataFormatString="{0:N2}" />
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
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

                        </div>
                        <br />

                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

