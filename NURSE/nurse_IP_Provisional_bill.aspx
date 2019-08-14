<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_IP_Provisional_bill.aspx.cs" Inherits="NURSE_nurse_IP_Provisional_bill" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Provisional/Final Bill
                    </div>
    
                </div>
    <div class="layout-main-content" >
        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        <input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid">
            <strong>
        </div>
        <div class="card card-w-title" style="min-height:450px;">
            <h1 style="color: #0071bc;"><b><u>Provisional Bill</u></b></h1>

            <br>

            
            
            <asp:Button ID="btnPrint" runat="server" CssClass="search" Text="Print" OnClick="btnPrint_Click" Visible="false" />
            <div class="ui-grid-row" style="margin-top:5px;">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" BorderColor="Red" BorderStyle="None" ToolbarStyle-BackColor="#E2E4DE" ToolbarStyle-BorderColor="#FF5050" />
            </div>
                
        </div>
    </div>
              </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

