<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_bedmatrix.aspx.cs" Inherits="ADMIN_admin_bedmatrix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Ward Master <span> / </span>Bed Matrix
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
        	<div class="col-md-12">
                <div class="col-md-6">
               
                     <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                     <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>

             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                    <div class="ui-grid-row">
                    <!----  Select Ward -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Department :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="dropdept_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                       
                 </div>
                     <div class="ui-grid-row">
                    <!----  Select Ward -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Ward :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropward" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="True">
                        </asp:DropDownList>
                    </div>
                        <!----  show bed button-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <asp:Button ID="btnshow" runat="server" CssClass="search" OnClick="btnshow_Click" Width="100" Text="Show Beds" />
                           
                    </div>
                  
                 </div>
                 <div class="ui-grid-row">
                    <!----  Text box -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                       
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtbed"  runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" Text="0" pattern="[0-9]+"></asp:TextBox>
                    </div>
                        <!----  Addbed Button -----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                     <asp:Button ID="btnaddbed" runat="server" CssClass="update" OnClick="btnaddbed_Click" Text="Add Beds" Width="100" Visible="TRUE" />
                           
                    </div>
                  
                 </div>
                 <div class="ui-grid-row">
                    <!----  Text  -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget">No.of Occupeid Bed :<asp:Label ID="lbloccupaidbed" runat="server" Text="0" style="color: #CC3300"></asp:Label></label>
                    </div>
                   
                 </div>
                     <div class="ui-grid-row">
                    <!----  Text  -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> No.of Available Bed :<asp:Label ID="lblavailablebed" runat="server" Text="0" style="color: #009999"></asp:Label></label>
                    </div>
                   
                 </div>


                </div>
               </div>

                    <br/><br/>
                   
         
    <div class="ui-datatable-tablewrapper">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060"
         OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" 
         CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
             <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="NAME" SortExpression="NAME" HeaderText="NAME" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                 <HeaderStyle CssClass="text-center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="BEDNO" SortExpression="BEDNO" HeaderText="BEDNO" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                  <HeaderStyle CssClass="text-center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                  <asp:BoundField DataField="STATUS" SortExpression="STATUS" HeaderText="STATUS" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle CssClass="text-center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true"  HeaderText="ACTION" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle CssClass="text-center" />
                <ItemStyle HorizontalAlign="Center" />
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
        </asp:GridView></td>
    
</div>
                    <br />
       			
                 <br/><br/>
               
                
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           <%-- <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            <select id="j_idt79:j_idt131_reflowDD" name="j_idt79:j_idt131_reflowDD" class="ui-reflow-dropdown ui-state-default" autocomplete="off">
                            	<option value="0_0">Id Ascending</option>
                                <option value="0_1">Id Descending</option>
                                <option value="1_0">Year Ascending</option>
                                <option value="1_1">Year Descending</option>
                                <option value="2_0">Brand Ascending</option>
                                <option value="2_1">Brand Descending</option>
                                <option value="3_0">Color Ascending</option>
                                <option value="3_1">Color Descending</option>
                              </select>--%>
                               </div>
                                <br/>
<br/>
<br/>
<br/>
</div>
        </div>
            </div>
                </div>
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

