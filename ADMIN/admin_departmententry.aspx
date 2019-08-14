<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_departmententry.aspx.cs" Inherits="ADMIN_admin_departmententry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i> <span>/ </span>DEPARTMENT ENTRY
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />
    <asp:TextBox ID="txtid" runat="server"  Visible="false"></asp:TextBox>
     <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
     <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC" Visible="false"></asp:Label>
    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbl_branch_fy" runat="server" Text="Label" Visible="false"></asp:Label>
        <div class="ui-fluid">
            <strong></strong>
            
        </div>
        <div class="card card-w-title"><br/>
              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                         
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                             <!---- Category-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-1.5">
                             <label class="ui-outputlabel ui-widget">Department:</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                pattern="^[a-z A-Z0-9]+$" size="20" style="margin-left:0.5%"></asp:TextBox>
                          
                         </div>
                        </div>

                         </div>
                      </div>

<br/>
                  <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:9%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%> 
                   <asp:Button ID="btncreate" runat="server" class="create"
                         OnClick="Button1_Click" Text="Create" style="margin-left:5%;"/> 
                  &nbsp;
            <asp:Button ID="btnupdate" runat="server" class="update" 
               
                OnClick="Button2_Click" Text="Update" Visible="False" />
             &nbsp;&nbsp;
            <asp:Button ID="btncancel" runat="server" class="cancel"
                
                OnClick="Button3_Click" Text="Cancel" />
            <%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" 
                type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%>
                           <br/><br/><br /><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    Department Details
                                </div><div class="ui-datatable-tablewrapper">

                                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound"
             OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" PageSize="5"  EmptyDataText="No records has been added." OnSorting="GridView1_Sorting"
            OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"
             CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>                
                <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" ItemStyle-HorizontalAlign="center" HeaderStyle-CssClass="text-center">                         
                <HeaderStyle CssClass="text-center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
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

</div>
        </div>
    </div>
 </ContentTemplate></asp:UpdatePanel>
</asp:Content>

