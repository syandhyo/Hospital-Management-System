<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_staff_search.aspx.cs" Inherits="RECEPTION_reception_staff_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Staff Search
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Staff Search</u></b></h1>
                
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  By Dr Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>By Dr Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoComplete="off" pattern="[A-Z a-z]+" title="Please Enter Alphabets"></asp:TextBox>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="search" OnClick="btnName_Click" />
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
                            
                            <!-- -->
                            
                            <div class="ui-grid-row">
                                <!---- By Designation :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>By Designation :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropdesg" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    </asp:DropDownList>
                                </div>
                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                   <asp:Button ID="Button2" runat="server" Text="Search" CssClass="search" OnClick="btnDesg_Click" />
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- By Department :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>By Department :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    </asp:DropDownList>
                                </div>
                                <!---- Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Button ID="Button3" runat="server" Text="Search" CssClass="search" OnClick="btnDept_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            </div>
                   </div>
                   
				<br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    Staff List
                                </div>
                        <div class="ui-datatable-tablewrapper">
                           <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="1060" DataKeyNames="id" CssClass="table table-bordered" 
                               CellPadding="4" ForeColor="#333333" GridLines="None" >
                               <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                 <asp:BoundField DataField="Sname" HeaderText="Name" HeaderStyle-CssClass="text-center" > 
                                 <HeaderStyle CssClass="text-center" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center">
                                 <HeaderStyle CssClass="text-center" />
                                 </asp:BoundField>
                                <asp:BoundField DataField="Contact" HeaderText="Contact" HeaderStyle-CssClass="text-center">                
                                 <HeaderStyle CssClass="text-center" />
                                 </asp:BoundField>
                                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="false" />--%>
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
                        </div>
                              
<br/>
<br/>
<br />
</div>
        </div>
    </label>
    </strong>
    </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

