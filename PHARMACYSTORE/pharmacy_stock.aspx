<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="pharmacy_stock.aspx.cs" Inherits="PHARMACYSTORE_pharmacy_stock" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Stock
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
					 <h1 style="color:#0071bc;"><b><u>Stock Report</u></b></h1>
				<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                    <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                        <div class="ui-grid-row">
                            <!---- Search by name : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Search by name :
                                </label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                </asp:DropDownList>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-1">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" CssClass="search" />
                            </div>
                        </div>
                        <div class="ui-grid-row">
                            <!----Search By Patient Name : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Show All" CssClass="search" />
                            </div>
                             
                        </div>
                        </div>
                    </div>
				 <br/><br/><br/>
			
                  <table width="100%" >
        <tr>
            <td style="padding-left:140px;"> <asp:Button ID="btnPrint" runat="server" Visible="false" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click"  /></td>
        </tr>
    </table>
    <table width="100%">
     <tr>
    <td style="text-align: center;" align="center">
       
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
       
         </td>
</tr>
</table>          
                            
                                
<br/>
<br/>
<br/>
</div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

