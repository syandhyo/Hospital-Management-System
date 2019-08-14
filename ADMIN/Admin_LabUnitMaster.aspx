<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_LabUnitMaster.aspx.cs" Inherits="ADMIN_Admin_LabUnitMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Labrotory <span>/ </span>Lab Unit Master
                </div>
               
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>
            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong />
                </div>
                <div class="card card-w-title">
                    <br>
                    <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                            <!----  Unit name No. : ----->
                            <div class="ui-grid-row">
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Unit Name. : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtunitname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" size="20" Style="margin-left: 0.7%" ></asp:TextBox>

                                </div>
                                 
                               
                            </div>
                           
                           
                        </div>
                    </div>



                  <br />
                    <br />

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="create" OnClick="btnSubmit_Click" style="margin-left:5%;"/>
                    &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />
                    &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                    <br>
                    <br>  <br>
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        

                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Ambulance Entry
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LAB_UNIT_ID" Width="1060"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True"
                                AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSorting="GridView1_Sorting" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LAB_UNIT_NAME" HeaderText="Unit Name" SortExpression="Unit name" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    
                                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center" >
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
                        <br />
                        <br />
                        <br />
                        <br />

                    </div>
                </div>

            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

