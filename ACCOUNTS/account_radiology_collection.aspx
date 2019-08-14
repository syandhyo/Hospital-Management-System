<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_radiology_collection.aspx.cs" Inherits="ACCOUNTS_account_radiology_collection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <Triggers>
              <asp:PostBackTrigger ControlID="Button2" />
         </Triggers>
          <ContentTemplate>
              <script type="text/javascript">
                  document.onkeydown = function (e) {
                      e.preventDefault();
                  }

                  $(function () {
                      SetDatePicker();
                  });

                  $(function () {
                      SetDatePicker1();
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
                      $("[id$=txtfrom]").datepicker({
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

                  //On UpdatePanel Refresh.
                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                  if (prm != null) {
                      prm.add_endRequest(function (sender, e) {
                          if (sender._postBackSettings.panelsToUpdate != null) {
                              SetDatePicker1();
                          }
                      });
                  };

                  function SetDatePicker1() {
                      $("[id$=txtto]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span>  Radiology Collection
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Radiology Collection Report  </u></b></h1>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
             <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                   
                     <!----Patient Type -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Patient Type :</label>	
                            <asp:CheckBox ID="CheckBox2" runat="server" Enabled="TRUE" AutoPostBack="True" OnCheckedChanged="CheckBox2_SelectedIndexChanged">
                     </asp:CheckBox>
                          
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-6">
                               <asp:DropDownList ID="drppatient" runat="server" Width="180"
                                    class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="false" style="margin-left: 26px;">
                                    <asp:ListItem>In Patient</asp:ListItem>
                                    <asp:ListItem>Out Patient</asp:ListItem>
                               </asp:DropDownList>
               
                         </div>
                        </div>

                     
                     <!----Corporate /Insurance -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2.5">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Corporate /Insurance:</label>	
                             <asp:CheckBox ID="CheckBox1" runat="server" Enabled="TRUE" AutoPostBack="True" OnCheckedChanged="CheckBox1_SelectedIndexChanged"> 
                             </asp:CheckBox>
                     
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:DropDownList ID="dropinsurance" runat="server" Width="180"
                                    class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="false"></asp:DropDownList>
               
                         </div>
                        </div>
                        <!----From Date-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>From Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-6">
                      <asp:TextBox ID="txtfrom" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="margin-left: 25px;"></asp:TextBox>   
               
                         </div>
                        </div>
                      <!----  To Date :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>To Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-6">
                      <asp:TextBox ID="txtto" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="margin-left: 25px;"></asp:TextBox>
                
                         </div>
                        </div>
                                     
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            
             <asp:Button ID="Show" runat="server" class="create" Text="Show" Width="68px" OnClick="Show_Click" style="margin-left:5%;"/>
        
            &nbsp; 
             &nbsp;&nbsp;
               
                <asp:Button ID="Button2" runat="server" class="update" Text="Export To Excel" OnClick="Button2_Click" Visible="false"  Width="118px" />
                         <br/><br/><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                  <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Radiology Collection Report
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID" CellPadding="4" GridLines="None" AllowPaging="True"
                             OnPageIndexChanging="GridView1_PageIndexChanging"  ShowFooter="True" OnRowDataBound="GridView1_RowDataBound1" ForeColor="#333333" >
          <%--  <Columns>
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                <asp:BoundField DataField="USERID" HeaderText="USER" />
                <asp:BoundField DataField="CAMOUNT" HeaderText="AMOUNT" DataFormatString="{0:N2}"/>
            </Columns>--%>
            <Columns>     
                 <asp:BoundField DataField="DATE" HeaderText="Date of Admission" />
                 <asp:BoundField DataField="OPDNO" HeaderText="OutPatientId/InPatientID" Visible="false" />
                <asp:BoundField DataField="outid" HeaderText="OutPatientId" Visible="false" />
                <asp:BoundField DataField="VN" HeaderText="InPatientID" Visible="false"/>
                <asp:BoundField DataField="NAME" HeaderText="Name Of Patient" />

                 <%--<asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>  --%>
                         
                 <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                        <asp:Label ID="lblamount" runat="server" Text='<%# Eval("TOTAMT") %>'/>
                                                        </ItemTemplate>
                                                    <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" />
                                                </FooterTemplate>
                                                        </asp:TemplateField>
                  <asp:BoundField DataField="CNAME" HeaderText="Insurance / Corporate" HeaderStyle-CssClass="text-center"  >
                 <HeaderStyle CssClass="text-center" />
                 </asp:BoundField>
                <%--<asp:BoundField DataField="RGFEE" HeaderText="Admission Fee" HeaderStyle-CssClass="text-center" DataFormatString="{0:N2}"/>--%>
              
                 <%--<asp:BoundField DataField="TDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"/> --%>
                <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>--%>
            </Columns>
            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

                            </div>
                               


</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

