<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_bed_transfer.aspx.cs" Inherits="RECEPTION_reception_bed_transfer" %>

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
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span>Bed Transfer
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
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
            
            <asp:Label ID="lblinsurance" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Bed Transfer</u></b></h1>
              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Search By IPD NO.:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                Search By IPD NO.:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtip" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                    
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshowip" runat="server" CssClass="search" Text="Show" OnClick="btnshowip_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
                            
                            <div class="ui-grid-row">
                                <!---- Search By Bed Number:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                Search By Bed &nbsp;&nbsp;&nbsp;Number:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    </asp:DropDownList>
                                </div>
                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshowbed" runat="server" CssClass="search" Text="Show" OnClick="btnshowbed_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
			<hr>
			
			  <h1 style="color:#0071bc;"><b><u>Current Bed data</u></b></h1>
				            <div class="ui-grid-row">
                                <!----  Search Item :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">IPDNO. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpid" runat="server" Text=""></asp:Label>
                                </div>

                                <!----  PATIENT NAME :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">PATIENT NAME :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                </div>

                            </div>
                            
                            <div class="ui-grid-row">
                                <!---- WARD NAME. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        WARD NAME. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblward" runat="server" Text="" ></asp:Label>
                                </div>
                                <!----  BED NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        BED NO. : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblbed" runat="server" Text=""></asp:Label>
                                </div>

                            </div>
                    <br><br>
				       <h1 style="color:#0071bc;"><b><u>Transfer Bed data</u></b></h1> 
				    <div class="ui-grid-row">
                                <!----  ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>WARD NAME. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="dropward" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" OnSelectedIndexChanged="dropward_SelectedIndexChanged" Width="178px"></asp:DropDownList>
                                    
                                </div>

                                <!---- DATE :----->
                                <div class="ui-panelgrid-cell ui-grid-col-1">
                                    <label class="ui-outputlabel ui-widget">DATE :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>

                            </div>
                            
                            <div class="ui-grid-row">
                                <!---- BED NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>BED NO. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropbed" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px">

                                    </asp:DropDownList>
                                </div>
                                <!----  Item Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                         </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            </div>
                  </div>
				
				 <br/><br />
				    <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="SUBMIT" style="margin-left:5%;"/>
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="CANCEL" />

                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                            
<br/>
<br/>
<br/>
</div>
        </div>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

