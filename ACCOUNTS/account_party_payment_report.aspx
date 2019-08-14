<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_party_payment_report.aspx.cs" Inherits="ACCOUNTS_account_party_payment_report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                  Sys.Application.add_load(function () {

                      $('.formdate').datepicker({
                          dateFormat: 'dd-mm-yy',
                          changeMonth: true,
                          changeYear: true,
                          minDate: '-75Y',
                          yearRange: "c-75:c+10",

                      });
                      $('.todate').datepicker({
                          dateFormat: 'dd-mm-yy',
                          changeMonth: true,
                          changeYear: true,
                          minDate: '-75Y',
                          yearRange: "c-75:c+10",
                      });
                  });
    </script>
                <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span>  Party Payment Report
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Payment Report</u></b></h1>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
             <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----Select Vendor----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-1.5">
                             <label class="ui-outputlabel ui-widget">Select Vendor :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                <asp:DropDownList ID="dropvendor" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                    Width="180"></asp:DropDownList>
                         </div>
                        </div>
                                                          
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            
            <asp:Button ID="btncreate" runat="server" class="create" style="margin-left:10%;" OnClick="Button1_Click" Text="Show" />
          &nbsp;
             &nbsp;&nbsp;
                               
                         <br/><br/><br />
             <div class="ui-datatable-header ui-widget-header ui-corner-top" style="height: 25px;padding: 7px;width:100%;">
                              Payment Report
                            </div>
            <br/>
            <asp:Button ID="btnPrint" runat="server" Visible="false" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click"  />
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
<br />
<br />
                                                                                               
                            

</div>
        </div>
    </div>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

