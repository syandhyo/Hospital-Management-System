<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_referral_settlement.aspx.cs" Inherits="ACCOUNTS_account_referral_settlement" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span>  Referral Settlement
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Referral Settlement</u></b></h1>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----Referral Name -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Referral Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
               <asp:DropDownList ID="droprefname" runat="server" AutoPostBack="true"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                   OnSelectedIndexChanged="droprefname_SelectedIndexChanged" Width="180"></asp:DropDownList>
                         </div>
                        </div>
                      <!---- Date :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtdate" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                
                         </div>
                        </div>
                      <div class="ui-grid-row">
                     <div id="divtotamt" runat="server" visible="false">
                        Total Amount : <asp:Label ID="lbltotamt" runat="server" Text=""></asp:Label>
                     Settlement Amt : <asp:TextBox ID="txtsatelment" runat="server" Width="60px"></asp:TextBox> 
                         </div>  
                          </div>          
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            <asp:Button ID="btncreate" runat="server" Text="Submit" class="create" style="margin-left:5%;" OnClick="btncreate_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update"  Text="Update" Visible="False" /> 
             &nbsp;&nbsp;
            <asp:Button ID="btncancel" runat="server" class="cancel" Text="Cancel" OnClick="btncancel_Click"/> 
            
                         <br/><br/><br /><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                         
                                  <div class="ui-datatable-header ui-widget-header ui-corner-top">
                               IP Patient Status
                            </div>
                            <div class="ui-datatable-tablewrapper">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060px" AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" />
            <Columns>  
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Pharmacy">
                    <ItemTemplate>
                        <asp:Label ID="lblpchg" runat="server" Text='<%#Eval("pchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Lab">
                    <ItemTemplate>
                        <asp:Label ID="lblichg" runat="server" Text='<%#Eval("lpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> <asp:TemplateField HeaderText="Bed">
                    <ItemTemplate>
                        <asp:Label ID="lblbchg" runat="server" Text='<%#Eval("bpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Radiology">
                    <ItemTemplate>
                        <asp:Label ID="lblrchg" runat="server" Text='<%#Eval("RpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Others">
                    <ItemTemplate>
                        <asp:Label ID="lblochg" runat="server" Text='<%#Eval("MpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("totamt")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>      
            
                <%--<asp:BoundField DataField="Adate" HeaderText="Date" DataFormatString="{0:MM/dd/yy}"/>          --%>  
               <%-- <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>--%>
            </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                                <asp:GridView ID="grdReffsatel" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" Width="1060"  AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                    <AlternatingRowStyle BackColor="White" />
            <Columns>  
                  
            
                <%--<asp:BoundField DataField="Adate" HeaderText="Date" DataFormatString="{0:MM/dd/yy}"/>          --%>  
               <%-- <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="REFFNAME">
                    <ItemTemplate>
                        <asp:Label ID="lblpchg" runat="server" Text='<%#Eval("REFFNAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                      <asp:TemplateField HeaderText="TOTAL&nbsp;AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="lblichg" runat="server" Text='<%#Eval("TOTAMT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="SETTLEMENT&nbsp;AMT">
                    <ItemTemplate>
                        <asp:Label ID="lblbchg" runat="server" Text='<%#Eval("SATELAMOUNT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                  
            
            </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
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

