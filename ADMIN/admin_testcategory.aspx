<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_testcategory.aspx.cs" Inherits="ADMIN_admin_testcategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/</span>Laboratory<span>/ </span> Test Category
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
         <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>

             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                     <div class="ui-grid-row">
                    <!----  Name-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Category Name :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>     
                    </div>
                  
                </div>
                 
                 </div>
             </div>

    <br>
                  <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:5%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
            <asp:Button ID="btncreate" runat="server" Text="Create" Class="create" OnClick="Button1_Click" Style="margin-left:5%;" />
        <asp:Button ID="btnupdate" runat="server" Text="Update" Class="update"  OnClick="Button2_Click" Visible="False" />
                   &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" style="width:80px" OnClick="Button3_Click" />
                         <br><br><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <%--<label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            <select id="j_idt79:j_idt131_reflowDD" name="j_idt79:j_idt131_reflowDD" class="ui-reflow-dropdown ui-state-default" autocomplete="off">
                            	<option value="0_0">Id Ascending</option>
                                <option value="0_1">Id Descending</option>
                                <option value="1_0">Year Ascending</option>
                                <option value="1_1">Year Descending</option>
                                <option value="2_0">Brand Ascending</option>
                                <option value="2_1">Brand Descending</option>
                                <option value="3_0">Color Ascending</option>
                                <option value="3_1">Color Descending</option>
                              </select>--%>
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                               Test Category
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                         DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting" OnSorting="GridView1_Sorting" EmptyDataText="No records has been added." 
                                        AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
                                        OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" HeaderStyle-CssClass="text-center">
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:CommandField>
                                    </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                      <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                      <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                      <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                      <RowStyle BackColor="#EFF3FB" />
                                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                      <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                      <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                      <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                      <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                </div><br/>
<br/>
<br/>
<br/>
</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

