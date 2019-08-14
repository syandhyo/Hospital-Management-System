<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_purchase_releaser.aspx.cs" Inherits="STOREKEEPER_store_purchase_releaser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick" ></asp:Timer>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
                              <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> 	Purchase Releaser	

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
        </div>
        <div class="card card-w-title">
        
				<h1 style="color:#0071bc;"><b><u>Purchase Release</u></b></h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             	
								PURCHASE REALESE
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdporelese" runat="server" AllowPaging="True" AllowSorting="True" PageSize="10" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered" DataKeyNames="GRNNO" OnPageIndexChanging="grdporelese_PageIndexChanging" OnSelectedIndexChanging="grdporelese_SelectedIndexChanging" >
            <Columns>
                <asp:BoundField DataField="GRNDATE" HeaderText="PurchaseDate" DataFormatString="{0:MM/dd/yy}" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="GRNNO" HeaderText="PurchaseNo." HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="PONO" HeaderText="PONo." HeaderStyle-CssClass="text-center"/>
                 <asp:BoundField DataField="PODATE" HeaderText="PO&nbsp;Date" HeaderStyle-CssClass="text-center"/>
                <%--<asp:BoundField DataField="STATUS" />--%>
              <%--  <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Select" Enabled="false" Height="35px" ImageUrl='<%# Eval("STATUS") %>' Width="110px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" HeaderText="Action" HeaderStyle-CssClass="text-center"/>
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
                                </div><br>
<br>
<br>
<br>
</div>
        </div>
    </div>
              </ContentTemplate>
     <Triggers>
              <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
          </Triggers>
     </asp:UpdatePanel>
</asp:Content>

