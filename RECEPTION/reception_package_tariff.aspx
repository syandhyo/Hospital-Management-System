<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_package_tariff.aspx.cs" Inherits="RECEPTION_reception_package_tariff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
         <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Reception Package tariff
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

     <div class="layout-main-content">

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Package Tariff</u></b></h1>
			
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
							   <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    PACKAGE TARIFF
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"  CssClass="table table-bordered" 
                                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                         <asp:BoundField DataField="ProcedureName" HeaderText="Procedure Name" HeaderStyle-CssClass="text-center"> 
                                         <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="Tariff" HeaderText="Tariff" HeaderStyle-CssClass="text-center">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:BoundField>
                                        <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="false" HeaderText="Action"/>--%>
                                    </Columns>
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
                                </div><br>
                               
  <br/>
        <br/>
        <br/>
        <br/>                        
         <br/>
        <br/>  
        <br />
</div>
        </div>
    </div>
              
              </strong>
              
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

