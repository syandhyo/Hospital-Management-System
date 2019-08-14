<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Adminhome.aspx.cs" Inherits="ADMIN_Adminhome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="route-bar" style="display:none;">

        <ul class="route-bar-menu">
            <li class="search-item">
                <i class="fa fa-search"></i>
                <input type="text" placeholder="Search..." />
            </li>
            <li>
                <a href="#" data-tooltip="Notifications">
                    <i class="fa fa-globe"></i>
                </a>
            </li>
            <li>
                <a href="#" data-tooltip="Calendar">
                    <i class="fa fa-calendar"></i>
                </a>
            </li>
            <li>
                <a href="#" data-tooltip="Help">
                    <i class="fa fa-life-saver"></i>
                </a>
            </li>
        </ul>
        <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
    </div>
  
      <div class="layout-main-content">
        <input type="hidden" name="j_idt79" value="j_idt79" />
        <img src="../bootstraptemplate/images/admin-background.jpg" width="100%" height="auto">
        <div class="ui-fluid">
        </div>
        <input type="hidden" name="javax.faces.ViewState" id="j_id1:javax.faces.ViewState:130" value="-6706802491831525269:5066372470393129965" autocomplete="off" />
    </div>
</asp:Content>

