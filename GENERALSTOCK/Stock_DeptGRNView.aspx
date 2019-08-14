<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="Stock_DeptGRNView.aspx.cs" Inherits="GENERALSTOCK_Stock_DeptGRNView" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Reorder Report
                    </div>
            </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Product Expiry Report</u></b></h1>
				
			 <asp:GridView ID="grdDeptgrn" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" OnSelectedIndexChanging="grdDeptgrn_SelectedIndexChanging" >
            <Columns>
               
                <asp:BoundField DataField="MRNO" HeaderText="MR&nbsp;NUMBER" />
                <asp:BoundField DataField="MINDATE" HeaderText="DATE" DataFormatString="{0:MM/dd/yyyy}"/> 
                <asp:BoundField DataField="DeptName" HeaderText="DEPARTMENT&nbsp;NAME" />
              
               <%-- <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Select" Enabled="false" Height="35px" ImageUrl='<%# Eval("STATUS") %>' Width="110px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" HeaderText="ACTION"/>
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

        </div>
                    </div>
            </ContentTemplate>
         <Triggers>
              <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
          </Triggers>
        </asp:UpdatePanel>
</asp:Content>

