<%@ Page Title="" Language="C#" MasterPageFile="~/ASSET/MasterPage.master" AutoEventWireup="true" CodeFile="return_mst.aspx.cs" Inherits="ASSET_return_mst" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Master <span> / </span> Daily Expense
                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Return</u></b></h1>
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
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Lblissno" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Issuse Number :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         
               <asp:TextBox ID="txtissno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                  </asp:TextBox>
                         </div>
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Lblbedno" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Bed Number :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         
               <asp:TextBox ID="txtbdno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                  </asp:TextBox>
                         </div>
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Lblrtrn" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Returned Item :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         
               <asp:TextBox ID="txtrtrn" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                  </asp:TextBox>
                         </div>
                        </div>
                               
                  </div>
             </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="create" style="margin-left:5px;"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:12%;" Visible="False" />
             &nbsp;&nbsp;
            <asp:Button ID="btncancel" runat="server" class="cancel" Text="Cancel"  />
                         <br/><br/><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                  <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Return
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="GrdVw1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" Width="1060"
                                    AllowPaging="True" AllowSorting="True" 
                                    CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                    <AlternatingRowStyle BackColor="White" />
            <Columns> 
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                                        <ItemTemplate>
                                          <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                <asp:BoundField DataField="CAT_NAME" HeaderText="Category Name" HeaderStyle-CssClass="text-center" SortExpression="Cat Name">--%>
                 <%--<HeaderStyle CssClass="text-center" />--%>
                <%-- </asp:BoundField>--%>
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

