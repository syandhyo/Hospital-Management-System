<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_assetsentry.aspx.cs" Inherits="ADMIN_admin_assetsentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/</span>Assets<span>/</span>Assets Entry
                </div>
               
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>

            <div class="layout-main-content">
                <form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
                    <input type="hidden" name="j_idt79" value="j_idt79" />

                    <div class="ui-fluid">
                        <strong>
                    </div>

                    <div class="card card-w-title">
                        <br>
                        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false">

                        </asp:Label><asp:Label ID="lblorgid" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                            <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <!----  Assets----->
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">

                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Assets : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtassets" runat="server" pattern="^[A-Za-z -]+$" MaxLength="50" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>

                                    </div>
                                </div>
                                <!----  Date----->
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Date :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdate" runat="server" onkeydown="return false;" onpaste="return false;" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>

                                    </div>
                                </div>
                                <!----  Quantity----->
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Quantity : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtqty" runat="server" CssClass="" pattern="[0-9]+" MaxLength="3" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>

                                    </div>
                                </div>

                            </div>
                        </div>



                        <%--    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false">

    </asp:Label><asp:Label ID="lblorgid" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>--%>
                        <%-- <div class="card card-w-title"><br>--%>
                        <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>

                        <br>

                        <asp:Button ID="btncreate" runat="server" class="create" Style="margin-left: 7%;" OnClick="Button1_Click" Text="Create" />

                        <asp:Button ID="btnupdate" runat="server" class="update" Style="margin-left: 6%;" OnClick="Button2_Click" Text="Update" Visible="False" />

                        &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" class="cancel" OnClick="Button3_Click" Text="Cancel" />
                        <br>
                        <br>
                        <br>
                        <br>
                        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Assets Entry
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060"
                                    DataKeyNames="id" OnRowDeleting="gvDetails_RowDeleting" OnRowDataBound="GridView1_RowDataBound" 
                                    AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
                                    OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" OnSorting="GridView1_Sorting" CellPadding="4" 
                                    ForeColor="#333333" GridLines="None" PageSize="10">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                            <ItemTemplate>
                                                <%#Container.DisplayIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Assets" HeaderText="Assets" HeaderStyle-CssClass="text-center" SortExpression="Assets">
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="ADate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>

                            </div>
                            <br>
                            <br>
                            <br>
                            <br>
                        </div>

                    </d>
            </div>
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

