<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_database_backup.aspx.cs" Inherits="ADMIN_admin_database_backup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>

   <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i> <span>/ </span>Database Backup
                    </div>

   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="" Visible="false"> </asp:Label>

<input type="hidden" name="j_idt79" value="j_idt79" />

            <div class="ui-fluid">
                <div class="ui-g">
                    <div class="ui-g-12">
                        <div class="card card-w-title" style="background-image:url(../bootstraptemplate/images/backup-bg.png);">
                            <h1><strong>Database Backup</strong></h1>
							<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;"><div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                           

                                 <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Drive :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:DropDownList ID="DropDownList1" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
                         </div>
                        </div>

                                <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Folder :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="DropDownList2" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                        </asp:DropDownList>
                         </div>
                        </div>
                                  
                                  </div>
							</div>
                                  <div style="width:50px; margin-left:10px">
                                    <%--  <button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false"><span class="ui-button-text ui-c">Start</span></button>--%>
                                      
                                      <asp:Button ID="btn_start" runat="server" OnClick="Button1_Click" CssClass="create" Text="Start" Width="57px" />

                                  </div>
                                  <img src="../bootstraptemplate/images/backup-1.png">
                                  <div id="j_idt79:j_idt125" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-shadow ui-hidden-container"><div class="ui-dialog-titlebar ui-widget-header ui-helper-clearfix ui-corner-top"></div>
                                    <div class="ui-dialog-content ui-widget-content">
                                        <div id="j_idt79:j_idt126" class="ui-panelgrid ui-widget ui-panelgrid-blank">
                                      <div class="ui-panelgrid-footer ui-widget-header"></div>
                                    </div></div></div><script id="j_idt79:j_idt125_s" type="text/javascript">$(function () { PrimeFaces.cw("Dialog", "dlg", { id: "j_idt79:j_idt125", draggable: false, resizable: false, hideEffect: "scale", responsive: true }); });</script>
                        </div>
                    </div>
                    </div>
            </div>

                </div>
              
              </ContentTemplate></asp:UpdatePanel>
</asp:Content>

