<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_preg_reciept.aspx.cs" Inherits="RECEPTION_reception_preg_reciept" %>
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
                       <i class="fa fa-home"></i><span>/ </span> Out Patient <span> / </span> Registration Receipt
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title">
				 <h1 style="color:#203a5a;"><b><center>REGISTRATION RECEIPT</center></b>

                     <br />
           <asp:Button ID="btnPrint" runat="server" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click"  />
                     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None"/>
        
                         </div>
                       
                     </div>
                     
                  
                    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

