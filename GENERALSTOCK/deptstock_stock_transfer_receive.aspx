<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_stock_transfer_receive.aspx.cs" Inherits="GENERALSTOCK_deptstock_stock_transfer_receive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 
      <asp:Timer ID="Timer1" runat="server" Interval="1000" ></asp:Timer>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
                    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span>Stock Transfer Receive
                    </div>
    
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Stock Transfer Receive</u></b></h1>
                   <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
							STOCK TRANSFER RECEIVE
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdDeptgrn" runat="server" AllowPaging="True" AllowSorting="True" PageSize="10" AutoGenerateColumns="False" OnPageIndexChanging="grdDeptgrn_PageIndexChanging" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" OnSelectedIndexChanging="grdDeptgrn_SelectedIndexChanging" >
            <Columns>
               
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="TRANSFER_NO" HeaderText="Transfer No" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-CssClass="text-center"/> 
                <asp:BoundField DataField="FromDept" HeaderText="From Dept" />
                <asp:BoundField DataField="ToDept" HeaderText="Dept To" />

                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" HeaderText="Action"/>
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
                                </div><br/>
<br/>
<br/>
<br/>
</div>
        </div>
    </div>
              </ContentTemplate>
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
          </Triggers>
 
      </asp:UpdatePanel>
</asp:Content>

