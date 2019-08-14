<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_daily_expense.aspx.cs" Inherits="ACCOUNTS_account_daily_expense" %>

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
                          dateFormat: 'yy-mm-dd',
                          changeMonth: true,
                          changeYear: true,
                          minDate: '-75Y',
                          yearRange: "c-75:c+10",

                      });
                      $('.todate').datepicker({
                          dateFormat: 'yy-mm-dd',
                          changeMonth: true,
                          changeYear: true,
                          minDate: '-75Y',
                          yearRange: "c-75:c+10",
                      });
                  });
    </script>
                <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Daily Expense
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Daily Expense</u></b></h1>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!---- Procedure Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Procedure Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         
               <asp:TextBox ID="txtexptype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                   pattern="^[A-Za-z ]+$" title="Please enter Alphabet"></asp:TextBox>
                         </div>
                        </div>
                      <!----  Amount-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Amount :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                      
                <asp:TextBox ID="txtamount" runat="server"  pattern="[0-9]+([,\.][0-9]+)?" title="Plz enter only Numeric" value="0"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                         </div>
                        </div>
                      <!----  Date-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdate" runat="server"  Enabled="False"   CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                     
               
                         </div>
                        </div>
                
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="create" style="margin-left:5px;" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:12%;"  OnClick="Button2_Click" Visible="False" />
             &nbsp;&nbsp;
            <asp:Button ID="btncancel" runat="server" class="cancel" OnClick="Button3_Click" Text="Cancel"  />
                         <br/><br/><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                  <div class="ui-datatable-header ui-widget-header ui-corner-top">
                               DAILY EXPENSE
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" Width="1060"
                                    AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
                                    OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" 
                                     CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                    <AlternatingRowStyle BackColor="White" />
            <Columns> 
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                                        <ItemTemplate>
                                          <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                <asp:BoundField DataField="ExpType" HeaderText="Expense Type" HeaderStyle-CssClass="text-center" SortExpression="ExpType">
                 <HeaderStyle CssClass="text-center" />
                 </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="text-center">
                 <HeaderStyle CssClass="text-center" />
                 </asp:BoundField>
                <asp:BoundField DataField="EDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center">                                  
                 <HeaderStyle CssClass="text-center" />
                 </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
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
                               

</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

