<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_op_consultancy_report.aspx.cs" Inherits="ACCOUNTS_account_op_consultancy_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script type="text/javascript">

        function Validate() {

            if (document.getElementById("<%=txtfdate.ClientID%>").value == "") {
                alert("From Date Is Required !");
                document.getElementById("<%=txtfdate.ClientID%>").focus();
                 return false;
             }

             if (document.getElementById("<%=txttdate.ClientID%>").value == "") {
                alert("To Date is Required !");
                document.getElementById("<%=txttdate.ClientID%>").focus();
                 return false;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <Triggers>
              <asp:PostBackTrigger ControlID="Button1" />
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

                  function SetDatePicker() {
                      $("[id$=txtfdate]").datepicker({
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
                  function SetDatePicker1() {
                      $("[id$=txttdate]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span>  OP Consultancy Report
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>OP Consultancy Report</u></b></h1>
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
                      <asp:TextBox ID="txtfdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>   
               
                         </div>
                        </div>
                      <!----  To Date :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>To Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txttdate" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                
                         </div>
                        </div>
                                     
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            
           <asp:Button ID="btnSubmit" runat="server" class="create" style="margin-left:5%;"  OnClientClick="return Validate();" OnClick="Button1_Click" Text="Submit" />

             &nbsp;&nbsp;
             
            <asp:Button ID="Button1" runat="server" Text="Export To Excel" class="update" OnClick = "ExportToExcel" Visible="False" Width="120px"/>
            
                         <br/><br/><br /><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                               OP Consultancy Report
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID" CssClass="table table-bordered" AllowPaging="True" 
           AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
            <Columns>                
                <asp:BoundField DataField="OPNo" HeaderText="OP No." HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sname" HeaderText="Doctor" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="Fee" HeaderText="Fee" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="CDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-CssClass="text-center"> 
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
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
</div>
        </div>
    </div>
             
              </strong>
             
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

