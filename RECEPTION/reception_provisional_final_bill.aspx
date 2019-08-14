<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_provisional_final_bill.aspx.cs" Inherits="RECEPTION_reception_provisional_final_bill" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Validate() {
            if (document.getElementById("<%=txtspid.ClientID%>").value == "") {
                alert("* Please!!Enter The IPD No..");
                document.getElementById("<%=txtspid.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function Validate1() {
            if (document.getElementById("<%=TXTUHID.ClientID%>").value == "") {
                alert("* Please!!Enter The UHID No..");
                document.getElementById("<%=TXTUHID.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        <input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid">
            <strong>
        </div>
        <div class="card card-w-title">
            <h1 style="color: #0071bc;"><b><u>Provisional Bill</u></b></h1>
  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

            <div class="ui-grid-row">

                <div class="ui-panelgrid-cell ui-grid-col-2">
                    * Search By IPD NO 
                </div>
                <div class="ui-panelgrid-cell ui-grid-col-3">
                    <asp:TextBox ID="txtspid" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="margin-left: 1%;"></asp:TextBox>
                </div>
                <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:Button ID="btnshow" runat="server" class="search" Text="Show" OnClick="btnshow_Click" OnClientClick="return Validate();"/>
                    </div>
            </div>
            			
				 
            <div class="ui-grid-row">

                <div class="ui-panelgrid-cell ui-grid-col-2">
                    * Search By UHID. 			
                </div>
                <div class="ui-panelgrid-cell ui-grid-col-3">
                    <asp:TextBox ID="TXTUHID" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="margin-left: 1%;"></asp:TextBox>
            
                </div>
                <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:Button ID="Button2" runat="server" class="search" Text="Show" OnClick="btnUHID_Click" OnClientClick="return Validate1();"/>
                    
                    </div>
            </div>
       
            <br />
            <asp:Button ID="btnPrint" runat="server" CssClass="search" Text="Print" OnClick="btnPrint_Click" Visible="false"/>
</div>
      </div>
            <br /><br />
           
            <div class="ui-grid-row" style="margin-top:5px;">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" BorderColor="Red" BorderStyle="None" ToolbarStyle-BackColor="#E2E4DE" ToolbarStyle-BorderColor="#FF5050" />
            </div>
              <br /><br /><br /> <br /><br /><br /> <br />  <br /><br />  
        </div>
    </div>

    
             </ContentTemplate></asp:UpdatePanel>

</asp:Content>

