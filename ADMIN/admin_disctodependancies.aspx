<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_disctodependancies.aspx.cs" Inherits="ADMIN_admin_disctodependancies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Discount<span>/ </span>Discount To Staff Dependancy
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
			<asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>

              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                   <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----  Disc% In Pharmacy-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label13" runat="server" Text="*" ForeColor="#CC0000"> </asp:Label>Disc% In Pharmacy :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          
                             <asp:TextBox ID="txtdiscpharmacy" runat="server" pattern="[0-9]+([,\.][0-9]+)?" 
        MaxLength="11" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
                         </div>
                        </div>
                        <!---- Disc% In Lab  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Disc% In Lab :	</label>
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                             <asp:TextBox ID="txtdisclab" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
                              </div>
                        </div>

                        <!---- Disc% In Room Charge  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Disc% In Room  &nbsp;&nbsp;Charge :</label>
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtroomcharge" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" 
        MaxLength="2"  value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
                              </div>
                        </div>

                        <!---- Disc% In Others  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                              <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Disc% In Others :</label>
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                              <asp:TextBox ID="discothers" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
                              </div>
                        </div>
                  
                </div>
            </div>
<br>
                 
                
                 <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px;margin-left:14%;"><span class="ui-button-text ui-c">Update</span></button>--%>
                 <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Button ID="btncreate" runat="server" Text="Update" class="update" style="margin-left:14%;margin-top: 10px;" OnClick="Button1_Click" />
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            

</div>
</div>
        </div>
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

