<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_testwiseconsuption.aspx.cs" Inherits="ADMIN_admin_testwiseconsuption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $("[id$=txtname]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/ADMIN/admin_testwiseconsuption.aspx/GetCustomers") %>',
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Laboratory<span>/ </span>Daily Test Wise Consuption
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
                        <asp:Label ID="lblitem" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lblslno" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtid" runat="server" CssClass="" Visible="false"></asp:TextBox>
                        
                        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                            <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <div class="ui-grid-row">
                                    <!----  For The Date ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget" style="margin-left: 1%;">For The Date : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" ></asp:TextBox>
                                    </div>
                                    <!----  Serial No. ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Serial No. :</label>

                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtserialno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="" Enabled="False" ></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4" style="margin-left:150px;">
                                        <asp:Button ID="btnrate" runat="server" Text="Rate" CssClass="search" OnClick="btnrate_Click" />&nbsp;<asp:Button ID="btnasset" runat="server" Text="Asset" OnClick="btnasset_Click" CssClass="search" />
                                    </div>

                                </div>

                                <div class="ui-grid-row">
                                    <!----  Procedure ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Procedure :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="dropprocedure" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <hr />

                                <div class="ui-grid-row">
                                    <!----  Item Name  ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Item Name : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtname" runat="server" placeholder="Item" ToolTip="Enter Item Here" pattern="^[A-Za-z -0-9]+$" MaxLength="50" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                                    </div>
                                     <!---- Unit  ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Unit : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                       <asp:DropDownList ID="dropunit" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Style="width: 180px;"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!---- Quantity  ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Quantity : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtqty" runat="server" placeholder="Quantity" ToolTip="Enter Quantity Here" pattern="[0-9]+([,\.][0-9]+)?" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <asp:Button ID="btnAdd" runat="server" OnClientClick="return Validate();" CssClass="add" Text="ADD" OnClick="btnAdd_Click" />
                                    </div>

                                </div>


                            </div>
                        </div>

                        <br />
                        <table width="100%">
                            <tr>
                                <td style="width: 20%" align="right"></td>
                                <td style="width: 60%" align="center">
                                    <asp:GridView ID="grdMaterial" runat="server" Width="1060" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grdMaterial_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item&nbsp;Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Unitname" runat="server" Text='<%#Eval("UOM_NAME")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Unit" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("UOM_ID")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" HeaderText="Action" />
                                        </Columns>
                                        <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                                <td style="width: 20%" align="center"></td>
                            </tr>
                        </table>
                        <hr />
                        <br />
                        <%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:9%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
                        <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" OnClientClick="return ValidateCret();" Style="margin-left: 5%;" />
                        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click" />
                        &nbsp;&nbsp;<%--<button id="Button2" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                        <br />
                        <br /><br />
                        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Daily Test Wise Consuption
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" EmptyDataText="No Record Is There" OnSorting="GridView1_Sorting" DataKeyNames="SERIAL_NO" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                            <ItemTemplate>
                                                <%#Container.DisplayIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIAL_NO" HeaderText="SERIAL_NO" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROCE" HeaderText="PROCEDURE" HeaderStyle-CssClass="text-center" SortExpression="PROCE" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center" >
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
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

