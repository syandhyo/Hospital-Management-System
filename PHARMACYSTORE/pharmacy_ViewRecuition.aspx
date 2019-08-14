<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="pharmacy_ViewRecuition.aspx.cs" Inherits="PHARMACYSTORE_pharmacy_ViewRecuition" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> View Requisation
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
                     <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
          <asp:Label ID="lblsid" runat="server" Text="Label" Visible="false"></asp:Label>
                   </div>
        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                 INVOICE HISTORY
                             </div>
                             <div class="ui-datatable-tablewrapper">
                                 <asp:GridView ID="grdRecution" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-bordered" DataKeyNames="INDENT_NO" OnPageIndexChanging="grdRecution_PageIndexChanging" OnSelectedIndexChanging="grdRecution_SelectedIndexChanging" ForeColor="#333333" GridLines="None" Width="1075px">
                                     <AlternatingRowStyle BackColor="White" />
            <Columns>
               
                <asp:BoundField DataField="INDENT_NO" HeaderText="ID" />
                <asp:BoundField DataField="DeptName" HeaderText="DEPARTMENT&nbsp;NAME" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-CssClass="text-center"> 
              
                 <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
              
                 <asp:BoundField DataField="IPNO" HeaderText="IPNO" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" HeaderText="Action"/>
            </Columns>
                                     <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                                 </div>
             </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

