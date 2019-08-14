<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_warehousemaster.aspx.cs" Inherits="ADMIN_admin_warehousemaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Phramacy<span>/ </span>Warehouse Master 
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
             <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
        </div>
        <div class="card card-w-title"><br>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                     <!---- Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-1">
                             <label class="ui-outputlabel ui-widget">  <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Name:</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtname" runat="server" ToolTip="name" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" pattern="^[a-z A-Z 0-9_]*" size="20" AutoComplete="off" Style="margin-left:-30px;"></asp:TextBox>
                         </div>
                        </div>
                       <!---- Name-----> 
                       <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-1">
                             <label class="ui-outputlabel ui-widget">Is Active :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:CheckBox ID="CheckBox1" runat="server" Style="margin-left:-33px;" />
                         </div>
                        </div>


                    </div>
                </div>
       
              
                              <script id="j_idt116_s" type="text/javascript">$(function () {
    PrimeFaces.cw("SelectManyCheckbox", "widget_j_idt116", {
        id: "j_idt116"
    }
                 );
}
                                                                        );
                </script>
                  <br>
                  

                 <%-- <button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px;margin-left:8%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button> --%>
             <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create"  style="width:80px;margin-left:8%;" OnClick="btncreate_Click"/>
            <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" style="width:80px;margin-left:8%;" Visible="False" OnClick="btnupdate_Click" />

                  &nbsp;&nbsp;
            <%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%>
            <asp:Button ID="btncancel" runat="server" CssClass="cancel" Text="Cancel" style="width:80px;background-color:#e71a33; border-color:#b11124;" OnClick="btncancel_Click"/>

                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    Warehouse Master
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvwarehouse" runat="server" AutoGenerateColumns="False" DataKeyNames="WARE_ID" OnSorting="grvwarehouse_Sorting" Width="1060"  
            AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="grvwarehouse_SelectedIndexChanging" OnRowDataBound="grvwarehouse_RowDataBound"  OnRowDeleting="grvwarehouse_RowDeleting"
            OnPageIndexChanging="grvwarehouse_PageIndexChanging"
           CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
            <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="WAREHOUSE" Visible="true" SortExpression="WARE_NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("WARE_NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
               <asp:TemplateField HeaderText="ISACTIVE" Visible="true" >
            <ItemTemplate>
                 <asp:Label ID="lblactive" runat="server" Text='<%#Eval("WARE_STATUS")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
      <%--  <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="false" SelectText="&nbsp;&nbsp;&nbsp;Delete" 
            HeaderText="ACTION" />--%>
                   <asp:CommandField ShowDeleteButton="TRUE" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" DeleteText="&nbsp;&nbsp;&nbsp; Hide"
                     HeaderText="Action" HeaderStyle-CssClass="text-center">

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
                                </div><br>
<br>
<br>
<br>
</div>
        </div>

              </strong>

              </ContentTemplate></asp:UpdatePanel>
</asp:Content>

