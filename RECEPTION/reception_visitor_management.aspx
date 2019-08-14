<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_visitor_management.aspx.cs" Inherits="RECEPTION_reception_visitor_management" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }

        .auto-style2 {
            text-decoration: underline;
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

           
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>In Patient <span>/ </span>Visitor Management
                </div>

            </div>

            <div class="layout-main-content" id="div2" runat="server">

                <div class="ui-fluid">
                    <strong>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                </div>
                <div class="card card-w-title">
                    <h1 style="color: #0071bc;"><b><u>Visitor Management</u></b></h1>

     <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                               

                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-3">
                            <label class="ui-outputlabel ui-widget"> * Search By UHID : </label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtip" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z 0-9.-]+$" 
                                MaxLength="15" title="Please enter The UHID Number"></asp:TextBox>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-5">
                            <asp:Button ID="btnshowip" runat="server" Text="Show" CssClass="search" Width="70px"  OnClick="btnshowip_Click" />
                        </div>

                    </div>

                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-3">
                            <label class="ui-outputlabel ui-widget"> * Search By Bed Number :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:DropDownList ID="DropDownList1" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" style="width:178px;">
                            </asp:DropDownList>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-5">

                            <asp:Button ID="btnshowbed" runat="server" CssClass="search" Text="Show" Width="70px" OnClick="btnshowbed_Click" />
                        </div>

                    </div>

                    <br />

                        <hr>
                    &nbsp;&nbsp;
					
                    <div class="ui-grid-row">

                            <div class="ui-panelgrid-cell ui-grid-col-2">
                               <label class="ui-outputlabel ui-widget">  Visitor No. : </label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-10">
                                <asp:TextBox ID="lblvisitorno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" ReadOnly="true" style="border:0px;"></asp:TextBox>
                            </div>

                        </div>
                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget"> Date :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-10">

                            <asp:TextBox ID="txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ReadOnly="true"></asp:TextBox>

                        </div>

                    </div>
                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget"> UHID :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-10">

                            <%--<asp:Label ID="lbluhid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0"></asp:Label>--%>
                            <asp:TextBox ID="lbluhid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" ReadOnly="true" style="border:0px;"></asp:TextBox>
                        </div>

                    </div>
                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget">  Patient Name :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-10">
                            <asp:TextBox ID="lblpname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" ReadOnly="true" style="border:0px;"></asp:TextBox>
                            <%--<asp:Label ID="lblpname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0"></asp:Label>--%>
                        </div>

                    </div>
                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"> Bed No. : </label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-10">
                            <%--<asp:Label ID="lblbedno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0"></asp:Label>--%>
                            <asp:TextBox ID="lblbedno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" ReadOnly="true" style="border:0px;"></asp:TextBox>
                        </div>

                    </div>
                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget"> Vistor Name : </label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-10">

                            <asp:TextBox ID="txtvisitorname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>

                        </div>

                    </div>
                    <div class="ui-grid-row">

                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget"> Relation :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-10">

                            <asp:TextBox ID="txtrelation" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>

                        </div>

                    </div>
                 <br />
                    <div class="ui-grid-row">

                       
                        <div class="ui-panelgrid-cell ui-grid-col-4">

                            <asp:Button ID="Button1" runat="server" CssClass="create" OnClick="Button1_Click" Text="Save" style="margin-left:10%;"/>

                        </div>

                    </div>
</div>
         </div>
                    
                    <h1>&nbsp;</h1>
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

