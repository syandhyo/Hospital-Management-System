<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_financialyear.aspx.cs" Inherits="ADMIN_Admin_financialyear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i> <span>/ </span>Add Financial Year
                    </div>

   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>
    <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />

            <div class="ui-fluid">
                <div class="ui-g">
                    <div class="ui-g-12">
                        <div class="card card-w-title" style="background-image:url(../bootstraptemplate/images/financialyear_bg.png)" >
                            <h1><strong>Add Financial Year</strong></h1>
                          <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                              <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                            <div class="ui-grid-row">

                           <%-- <select style="width:15%;height:28px; margin-left:0.3%;">
					  <option value="">----No Doctor----</option>
					  <option value="Dr. Rajesh Kumar sahoo">Dr. Rajesh Kumar sahoo</option>
					  <option value="Dr. Lalit Kumar">Dr. Lalit Kumar</option>
					  <option value="Dr. Gourav">Dr. Gourav</option>
					</select>--%>
                                  <asp:DropDownList ID="DropDownList1" runat="server" style="width:15%;height:28px; margin-left:0.3%;">
                                       <asp:ListItem>Select Year</asp:ListItem>
                                        <asp:ListItem>2017-2018</asp:ListItem>
                                        <asp:ListItem>2018-2019</asp:ListItem>
                                        <asp:ListItem>2019-2020</asp:ListItem>
                                        <asp:ListItem>2020-2021</asp:ListItem>
                                        <asp:ListItem>2022-2023</asp:ListItem>
                                        <asp:ListItem>2024-2025</asp:ListItem>
                                    </asp:DropDownList>

                            </div>
                                  <br>
                                  </div></div>
                                  <div style="width:120px; margin-left:10px">
                                     <%-- <button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false"><span class="ui-button-text ui-c">save/modify</span></button>--%>
                                      <asp:Button ID="Button1" runat="server" Text="Save/Modify"  CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" tooltip="Click Here" type="button" role="button" aria-disabled="false" Height="26px"/>
                                  
                                  </div>
                                  
                                  
                                  
                                  <br><br><br><br><br><br><br><br><br><br><br><br><br>

                                  <div id="j_idt79:j_idt125" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-shadow ui-hidden-container"><div class="ui-dialog-titlebar ui-widget-header ui-helper-clearfix ui-corner-top"></div>
                                    <div class="ui-dialog-content ui-widget-content"><div id="j_idt79:j_idt126" class="ui-panelgrid ui-widget ui-panelgrid-blank">
                                      <div class="ui-panelgrid-footer ui-widget-header"></div>
                                    </div></div></div><script id="j_idt79:j_idt125_s" type="text/javascript">$(function () { PrimeFaces.cw("Dialog", "dlg", { id: "j_idt79:j_idt125", draggable: false, resizable: false, hideEffect: "scale", responsive: true }); });</script>
                        </div>
                    </div>
                    </div>
            </div>
                </div>
</asp:Content>

