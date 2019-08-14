<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_progress_report.aspx.cs" Inherits="NURSE_nurse_progress_report" %>
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Progress Report
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
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>	Progress Report</u></b></h1>
                
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Select Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Select Bed No :</label>
                         
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropbedno" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged">
                                 </asp:DropDownList>
                                </div>

                                <!---- IPDNO:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">IPDNO :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:Label ID="lblipno" runat="server"></asp:Label>
                                </div>

                            </div>
                           
                            <div class="ui-grid-row">
                                <!---- Gender :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Gender :</label>
                                       
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:Label ID="lblgender" runat="server"></asp:Label>
                                </div>
                                <!----   Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                         Name :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                  <asp:Label ID="lblname" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Bed No :</label>
                                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:Label ID="lblbedno" runat="server"></asp:Label>
                                </div>
                                <!---- Age :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                       Age :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblage" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Dr.----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Dr.</label>
                                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:DropDownList ID="dropdr" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                </asp:DropDownList>
                                </div>
                                <!--------->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                      </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     
                                </div>

                            </div>
                        </div>
                   </div>
				   <br><br><br>
				   
             		 <asp:Button ID="btngenerate" runat="server" CssClass="search" OnClick="Button3_Click" Text="Generate" />
                
                
                            
                               <asp:Button ID="btnPrint" runat="server" Visible="false" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click"  /></td>
       <br />
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None"/>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
<br/>


        </div>
        </div>
 </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

