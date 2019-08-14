<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="account_damage_assets_entry.aspx.cs" Inherits="ACCOUNTS_account_damage_assets_entry" %>

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


                     $('.fromdate').datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Damage Assets Entry                    </div>
    
                    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
             <h1 style="color:#0071bc;"><b><u>Damage Assets Entry</u></b></h1>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>  
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                  
                           
                            <!-- -->
                        
                            <div class="ui-grid-row">
                                <!---- Assets :----->
                                <div class="ui-panelgrid-cell ui-grid-col-1">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Assets :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropassets" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    </asp:DropDownList>
                                </div>
                                <!----  Date  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-1">
                                    <label class="ui-outputlabel ui-widget">
                                       Date : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        Enabled="False"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Quantity ----->
                                <div class="ui-panelgrid-cell ui-grid-col-1">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Quantity :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                  <asp:TextBox ID="txtqty" runat="server" pattern="[0-9]+" MaxLength="3" title="Please Enter numeric value" 
                                      class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                      

                                       
                                </div>
                                <!---- Status  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-1">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Status :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtstatus" runat="server" pattern="^[A-Za-z -]+$" title="Plz enter only Alphabets" 
                             class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>

                            </div>
                           
                           
                        </div>
                    </div>

            
                <br>
            <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:120px;"><span class="ui-button-text ui-c">Update</span></button>--%>
            <asp:Button ID="btncreate" runat="server" Text="Submit" class="create" style="margin-left:5%;" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:12%;"  OnClick="Button2_Click" Visible="False" />
             &nbsp;&nbsp;
            <asp:Button ID="btncancel" runat="server" class="cancel" OnClick="Button3_Click" Text="Cancel"  />
                         <br/><br/><br /><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                  <div class="ui-datatable-header ui-widget-header ui-corner-top">
                               DAMAGE ASSETS ENTRY
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="id" OnRowDeleting="gvDetails_RowDeleting" 
            OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" AllowSorting="True" 
                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" 
                CssClass="table table-bordered" OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None" >
                <AlternatingRowStyle BackColor="White" />
            <Columns>    
                <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                                        <ItemTemplate>
                                          <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>             
                <asp:BoundField DataField="Assets" HeaderText="Assets" HeaderStyle-CssClass="text-center" SortExpression="Assets">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Status" HeaderText="Status" />  
                <asp:BoundField DataField="Adate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>           
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

