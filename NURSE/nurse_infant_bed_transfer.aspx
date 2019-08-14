<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_infant_bed_transfer.aspx.cs" Inherits="NURSE_nurse_infant_bed_transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                  
                  $(function () {
                      SetDatePicker();
                  });

                  //On UpdatePanel Refresh.
                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                  if (prm != null) {
                      prm.add_endRequest(function (sender, e) {
                          if (sender._postBackSettings.panelsToUpdate != null) {
                              SetDatePicker();
                          }
                      });
                  };
                  function SetDatePicker() {
                      $("[id$=txtdate]").datepicker({
                          dateFormat: 'dd-mm-yy',
                          showOn: 'button',
                          buttonImageOnly: true,
                          dateFormat: 'dd-mm-yy',
                          changeMonth: true,
                          changeYear: true,
                          maxDate: '0',

                          yearRange: "c-75:c+10",
                          buttonImage: '../images/calendar.png'

                      });
                  }
    </script>
              <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Infant Bed Transfer
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblinsurance" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Infant Bed Transfer</u></b></h1>
              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----Search By IP NO.:-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Search By IP NO.:</label>
                                	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            </asp:DropDownList> 

                          </div>
                        <!----  Button----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                          <asp:Button ID="btnshowbed" runat="server" Text="Show" CssClass="search" OnClick="btnshowbed_Click" />
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           
                        </div>
                       </div>

                    
                     <hr />
                    
                     <h1 style="color:#0071bc;"><b><u>Current Bed data</u></b></h1>
                    
                     <div class="ui-grid-row">
                                <!---- INFANT NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;INFANT NO. :</label>
                                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpid" runat="server" Text=""></asp:Label>
                                </div>
                                <!----  MOTHER ID :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        MOTHER ID :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- WARD NAME. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;WARD NAME. :</label>
                                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblward" runat="server" Text=""></asp:Label>
                                </div>
                                <!----BED NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        BED NO. :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblbed" runat="server" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- BUtton----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                   <asp:Button ID="btnrelease" runat="server" CssClass="search" OnClick="Btnrelease_Click" Text="RELEASE BED" Width="118px"/>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>
                                
                            </div>
                  
                     <hr />
                    
                     <h1 style="color:#0071bc;"><b><u>Transfer Bed data</u></b></h1>
                    
                            <div class="ui-grid-row">
                                <!---- Category----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>WARD NAME. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropward" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="True" OnSelectedIndexChanged="dropward_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!----  DATE :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>DATE :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtdate" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  BED NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label10" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>BED NO. ::</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:DropDownList ID="dropbed" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
                                </div>
                            </div>
                     </div>
                     </div>
				 <br>
				   <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="SUBMIT" Width="80px" Style="margin-left:5px;"/>
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="CANCEL" />
                         
                
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">


</div>
        </div>
    </div>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

