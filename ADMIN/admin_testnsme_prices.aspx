<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_testnsme_prices.aspx.cs" Inherits="ADMIN_admin_testnsme_prices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Laboratory<span>/ </span>Rate For Test
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
                        <asp:Label ID="lbltestid" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lblslno" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtid" runat="server" CssClass="" Visible="false"></asp:TextBox>

                        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                            <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <div class="ui-grid-row">
                                     <!----  TestName. ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Procedure. :</label>

                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="dropprocedure" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <!----  For The Date ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget" style="margin-left: 1%;">Date : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" ></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4" style="margin-left:150px;">
                                        <asp:Button ID="btnasset" runat="server" Text="Asset" CssClass="search" OnClick="btnasset_Click" />&nbsp;<asp:Button ID="btnconsumption" runat="server" Text="Material" OnClick="btnconsumption_Click" CssClass="search" />
                                    </div>

                                </div>

                                <div class="ui-grid-row">
                                    <!----  Procedure ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Price :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtprice" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" text="0" ></asp:TextBox>
                                    </div>

                                </div>
                               



                            </div>
                        </div>

                        <br />
                        
                        
                        <br />
                        <%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:9%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
                        <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click"  Style="margin-left: 5%;" />
                       <%-- &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click" />--%>
                        &nbsp;&nbsp;<%--<button id="Button2" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                        <br />
                        <br /><br />
                        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Test Rates
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" EmptyDataText="No Record Is There" OnSorting="GridView1_Sorting" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                            <ItemTemplate>
                                                <%#Container.DisplayIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Date" HeaderText="DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INV" HeaderText="PROCEDURE" HeaderStyle-CssClass="text-center" SortExpression="INV" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Price" HeaderText="PRICE" HeaderStyle-CssClass="text-center" SortExpression="Price" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                       <%-- <asp:CommandField ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="false" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:CommandField>--%>
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

