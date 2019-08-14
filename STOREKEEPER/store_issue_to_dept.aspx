<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_issue_to_dept.aspx.cs" Inherits="STOREKEEPER_store_issue_to_dept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                            <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> 	Issue Department	

                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblEditgrd" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
				<h1 style="color:#0071bc;"><b><u>Issue Department	</u></b></h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             	
								ISSUE DEPARTMENT
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView2" runat="server" AllowPaging="True" Width="1060px" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yy}" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="ID" HeaderText="MR&nbsp;No." HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <%--<asp:BoundField DataField="STATUS" />--%>
              <%--  <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Select" Enabled="false" Height="35px" ImageUrl='<%# Eval("STATUS") %>' Width="110px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" HeaderText="Action" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:CommandField>
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
<br>
<br>
<br>
</div>
        </div>
    </div>
                            </strong>
            </ContentTemplate>
        <Triggers>
              <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
          </Triggers>
        </asp:UpdatePanel>
</asp:Content>

