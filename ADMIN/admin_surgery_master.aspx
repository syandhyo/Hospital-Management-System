<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_surgery_master.aspx.cs" Inherits="ADMIN_admin_surgery_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/</span>OT<span>/</span>Surgery Master
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

        <div class="ui-fluid"><strong/>
        </div>
        <div class="card card-w-title"><br/>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----  Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtname" runat="server" Enabled="true" pattern="^[A-Z a-z -]+$" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
               
                         </div>
                        </div>
                      <!----  Type-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Surgery Type :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="droptype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" >
        </asp:DropDownList>
             
                         </div>
                        </div>
                      <!----  Duration -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Duration :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdur" runat="server" MaxLength="15" minlength="11"
                       class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
               
                         </div>
                        </div>
                      <!----  State Code-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>OT Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                <asp:DropDownList ID="dropot" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" >
        </asp:DropDownList>
                         </div>
                            <br>    
                 <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                        </div>
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            <asp:Button ID="btncreate" runat="server" Text="Create" class="create" style="margin-left:12%;" OnClick="btncreate_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:12%;"  OnClick="btnupdate_Click" Visible="False" />
            &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                          <br />
                        <br /><br />
                        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Daily Test Wise Consuption
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="gridstore" runat="server" AutoGenerateColumns="False" Width="1060" PageSize="5" EmptyDataText="No Record Is There" OnSorting="gridstore_Sorting" DataKeyNames="sl" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="gridstore_SelectedIndexChanging" OnPageIndexChanging="gridstore_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                            <ItemTemplate>
                                                <%#Container.DisplayIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField DataField="NAME" HeaderText="NAME" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OT" HeaderText="OT NAME" HeaderStyle-CssClass="text-center" SortExpression="PROCE" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="TYPE" HeaderText="SUGERY TYPE" HeaderStyle-CssClass="text-center" SortExpression="PROCE" >
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
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

