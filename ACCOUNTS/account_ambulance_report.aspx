<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_ambulance_report.aspx.cs" Inherits="ACCOUNTS_account_ambulance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script type="text/javascript">

        function Validate() {

            if (document.getElementById("<%=txt_Fdate.ClientID%>").value == "") {
                 alert("Ambulance From Date Is Required !");
                 document.getElementById("<%=txt_Fdate.ClientID%>").focus();
                 return false;
             }

             if (document.getElementById("<%=txt_Todate.ClientID%>").value == "") {
                 alert("Ambulance To Date is Required !");
                 document.getElementById("<%=txt_Todate.ClientID%>").focus();
                 return false;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <Triggers>
              <asp:PostBackTrigger ControlID="btn_Download" />
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
                      $("[id$=txt_Fdate]").datepicker({
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
                      $("[id$=txt_Todate]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span>  Ambulance Report
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Ambulance Report</u></b></h1>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
             <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----From Date-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>From Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_Fdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>   
               
                         </div>
                        </div>
                      <!----  To Date :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>To Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txt_Todate" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                
                         </div>
                        </div>
                                     
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            
            <asp:Button ID="btnShow" runat="server" class="create"  Text="Show" style="margin-left:10%;" 
        OnClientClick="return Validate();" OnClick="btnShow_Click"  />
&nbsp;
             &nbsp;&nbsp;
             <asp:Button ID="btn_Download" runat="server" class="update"  Text="Download" Visible="false" OnClick="btn_Download_Click"  />
           
            
                         <br/><br/><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                          
                                  <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Ambulance Report
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                
                        <asp:GridView ID="grd_ambulance" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="id" AllowPaging="True" 
           AllowSorting="True"   CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" 
                             OnPageIndexChanging="grd_ambulance_PageIndexChanging"
                            PageSize="10" >
                            <AlternatingRowStyle BackColor="White" />
            <Columns> 
                <asp:BoundField DataField="ADate" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}"/> 
                 <asp:BoundField DataField="id" HeaderText="Sl No" />
                 <asp:BoundField DataField="ORGID" HeaderText="Organisation Id" />
                <asp:BoundField DataField="PatientName" HeaderText="NAME" />               
                 <asp:BoundField DataField="AttendentName" HeaderText="ATTENDENT NAME" />
                <asp:BoundField DataField="ContactNo" HeaderText="CONTACT NO" />  
                 <asp:BoundField DataField="From" HeaderText="FROM" />
                <asp:BoundField DataField="To" HeaderText="TO" />
                <%-- <asp:BoundField DataField="To" HeaderText="PATIENT ID" />
                <asp:BoundField DataField="PatientName" HeaderText="NAME" />--%>
                 <asp:BoundField DataField="ApproxKm" HeaderText="APPROX KM" />
                <asp:BoundField DataField="Fee" HeaderText="FEE" />
                 <asp:BoundField DataField="AmbulanceNo" HeaderText="AMBULANCE NO" />
               
                
               <%-- <asp:TemplateField HeaderText="DOWNLOAD">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbn_download" runat="server" OnClick="lbn_download_Click" href='<%#Eval("FILEUPLOAD") %>' >Download</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
               <%-- <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />--%>
            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                           <%-- <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
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
             
              </strong>
             
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

