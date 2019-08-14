<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_document_upload.aspx.cs" Inherits="RECEPTION_reception_document_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
               
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> Document Upload
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

    <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid">
            
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Document Upload</u></b></h1>
					
                <div class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                            <div id="div1" runat="server">
                            <div class="ui-grid-row">
                                <!----  Search By IPDNO :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    
                                    <label class="ui-outputlabel ui-widget">Search By IPDNO :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtspid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z0-9]+$" MaxLength="8" minlength="6" title="Please enter The IPD Number EX:IP0001"></asp:TextBox>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshow" runat="server"  CssClass="update" Text="Show" OnClick="btnshow_Click" />
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
                                </div>
                            <div id="div2" runat="server" >
                                <div class="ui-grid-row">
                                <!----  Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    
                                    <label class="ui-outputlabel ui-widget">Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                    
                                </div>

                                <!----  Date----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>

                            </div>
                                <div class="ui-grid-row">
                                <!----  IPD No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    
                                    <label class="ui-outputlabel ui-widget">IPD No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblip" runat="server" Text=""></asp:Label>
                                </div>

                                <!----  Image Upload :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Image Upload :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:FileUpload ID="fluImg" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  type="file" Width="168px"/>
                                </div>

                            </div>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="create"  Text="Submit" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                    </div>
				  
                   <br><br>
				  
				<h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
							  
<br>
<br>
<br>
                               

</div>
        </div>
    </div>
        
</asp:Content>

