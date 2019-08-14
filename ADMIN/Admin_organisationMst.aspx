<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_organisationMst.aspx.cs" Inherits="ADMIN_Admin_organisationMst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
     <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i> <span>/ </span>Organization Master
                    </div>
   
         <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />
  <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="ui-fluid" style="height:468px;" >
                <div class="ui-g">
                    <div class="ui-g-12">
                        <div class="card card-w-title" style="background-image:url(../bootstraptemplate/images/Organization_bg.png); height:440px" >
                            
                            <div class="ui-g-12 ui-md-12 ui-lg-4 ui-fluid">
                                <div id="j_idt112" class="ui-panel ui-widget ui-widget-content ui-corner-all resolution-center" data-widget="widget_j_idt112">
                                    <div id="j_idt112_header" class="ui-panel-titlebar ui-widget-header ui-helper-clearfix ui-corner-all">
                                        <span class="ui-panel-title">Organization Master </span></div>
                                    <div id="j_idt112_content" class="ui-panel-content ui-widget-content">
                                        <label id="j_idt113" class="ui-outputlabel ui-widget" for="email">* Name :	</label>
                                       <%-- <input id="name" name="name" type="text" placeholder="4S CARE HOSPITAL" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false">--%>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                        <label id="Label1" class="ui-outputlabel ui-widget" for="email">* Phone :	</label>
                                        <%--<input id="phone" name="phone" type="text" placeholder="06712523454" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false">--%>
                                         <asp:TextBox ID="txtphone" runat="server"  pattern="[0-9]+([,\.][0-9]+)?" MaxLength="11" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        <label id="Label2" class="ui-outputlabel ui-widget" for="email">* GST IN :	</label>
                                        <%--<input id="nomber" name="nomber" type="text" placeholder="21JUJI8O" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false">--%>
                                        <asp:TextBox ID="txtgst" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-zA-Z0-9]+"></asp:TextBox>
                                        <label id="Label3" class="ui-outputlabel ui-widget" for="email">* State Code :</label>
                                        <%--<input id="Text1" name="nomber" type="text" placeholder="21" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false">--%>
                                         <asp:TextBox ID="txtstatecode" runat="server"  pattern="[0-9]+([,\.][0-9]+)?" MaxLength="2" CssClass="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                        <label id="j_idt114" class="ui-outputlabel ui-widget" for="message">* Address :	</label>
                                       <%-- <textarea id="message" name="message" cols="20" rows="5" placeholder="Write your address..." maxlength="2147483647" class="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false" aria-multiline="true"> 

                                        </textarea>--%>
                                         <asp:TextBox ID="txtaddress" runat="server" CssClass="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all" Enabled="true" TextMode="MultiLine" ></asp:TextBox>
                                        <%--<button id="j_idt115" name="j_idt115" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false">
                                            <span class="ui-button-text ui-c">Update</span></button>--%>
                                        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" OnClick="btnupdate_Click"/>
                                        <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" OnClick="btncreate_Click" />
                                    </div></div>
                </div>
                          <br><br><br><br><br><br><br><br><br><br><br><br>


                                  <div id="j_idt79:j_idt125" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-shadow ui-hidden-container"><div class="ui-dialog-titlebar ui-widget-header ui-helper-clearfix ui-corner-top"></div>
                                    <div class="ui-dialog-content ui-widget-content"><div id="j_idt79:j_idt126" class="ui-panelgrid ui-widget ui-panelgrid-blank">
                                      <div class="ui-panelgrid-footer ui-widget-header"></div>
                                    </div></div></div><script id="j_idt79:j_idt125_s" type="text/javascript">$(function () { PrimeFaces.cw("Dialog", "dlg", { id: "j_idt79:j_idt125", draggable: false, resizable: false, hideEffect: "scale", responsive: true }); });</script>
                        </div>
                    </div>
                    </div>
            </div>
                </div>
               </ContentTemplate></asp:UpdatePanel>
</asp:Content>

