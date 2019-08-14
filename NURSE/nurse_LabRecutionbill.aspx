<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_LabRecutionbill.aspx.cs" Inherits="NURSE_nurse_LabRecutionbill" %>
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
                       <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Lab Bill
                    </div>
                  </div>
                  
                   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>
                   
                <div class="layout-main-content">

        <div class="ui-fluid"><strong>
            <asp:Label ID="LBLUHID" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="lblid" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtdate" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server"  Visible="false"> </asp:Label>
            <asp:Label ID="LBPAIDAMT" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
                     <div class="card card-w-title">
          <table width="100%" >
        <tr>
            <td style="padding-left:140px;"> <asp:Button ID="btnPrint" runat="server" BackColor="Yellow" Text="Print"  OnClick="btnPrint_Click" /></td>
        </tr>
    </table>
  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None"/>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                         </div>
                      </div>
                 
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

