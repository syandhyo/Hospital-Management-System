<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_patient_registration.aspx.cs" Inherits="RECEPTION_reception_patient_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
               <script type = "text/javascript">

                   function SetTarget() {

                       document.forms[0].target = "_blank";

                   }


        </script>
    <div>
       
    <script type="text/javascript">
        
        $(function () {
            SetDatePicker();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
                }
            });
        };
        function SetDatePicker() {
            $("[id$=txtdateMlc]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: '../images/calendar.png'

            });
        }
        $(function () {
            SetDatePicker1();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker1();
                }
            });
        };
        function SetDatePicker1() {
            $("[id$=txtdob]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: '../images/calendar.png'

            });
        }

    </script>
        
    
                 
     <div class="route-bar">
         <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label> 
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label> 
        <asp:Label ID="LBLSLNO" runat="server" Text="Label" Visible="false"> </asp:Label>
        <asp:Label ID="lbluhid" runat="server" Text="Label" Visible="false"></asp:Label>
         <asp:TextBox ID="txtRegstno" runat="server" text="Registration No"  Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtRegType" runat="server" Text="Registration Type" Visible="false"></asp:TextBox>
         <asp:TextBox ID="txtaginstbook" runat="server" Text="Against Booking" Visible="false"></asp:TextBox>
         <asp:Label ID="lblcorpo" runat="server"  Visible="false"></asp:Label>
         <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
         <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
         <asp:TextBox ID="txtTitle" runat="server" visible="false"></asp:TextBox>
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Out Patient <span> / </span> PATIENT REGISTRATION
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
        </div>
        <div class="card card-w-title">
					
					 <h1 style="color:#203a5a;"><b><center>	PATIENT REGISTRATION</center></b>
                         <h1></h1>
                         <hr/>
                         <br/>
                         <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                 <div class="ui-grid-row">
                                     <!----  Date & Time :	----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Date &amp; Time :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtDate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!----  Button----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                 </div>
                                 <br/>
                                 <br/>
                                 <hr/>
                                 <h1 style="color:#0071bc;"><b><u>Booking No.</u>:</b></h1>
                                 <div class="ui-grid-row">
                                     <!----  Date & Time :	----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Booking No. :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtbookno" runat="server" AutoComplete="off"  AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtbookno_TextChanged"></asp:TextBox>
                                     </div>
                                     <!----  Button----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Booking Date &amp; Time :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtbookdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false" ></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Booking For :	----->
                                     <div class="ui-panelgrid-cell ui-grid-col-6">
                                         <label class="ui-outputlabel ui-widget">
                                         Booking For :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtbookfor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false" ></asp:TextBox>
                                     </div>
                                     <!----  MLC Case----->
                                     
                                     <div class="ui-panelgrid-cell ui-grid-col-6">
                                         <asp:CheckBox ID="chkMlc" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnCheckedChanged="chkMlc_CheckedChanged" style="margin-left: 17%;"/>
                                         MLC Case</strong>
                                     </div>
                                 </div>
                                 <div id="mlcDiv" runat="server" visible="false">
                                     <hr />
                                     <div class="ui-grid-row">
                                         <!---- *Case :	----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                             Case :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtcase" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="40"></asp:TextBox>
                                         </div>
                                         <!----  *Date----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                             Date:</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtdateMlc" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" CssClass="formdate" onkeydown="return false;" onpaste="return false;" style="border-radius: 3px;height:20px;width:170px;"></asp:TextBox>
                                         </div>
                                     </div>
                                     <div class="ui-grid-row">
                                         <!---- Police Station :	----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                             Police Station :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtPolicStaion" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="50"></asp:TextBox>
                                         </div>
                                         <!----  MLC Case----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                         </div>
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <br>
                                 <div class="ui-grid-row">
                                     <!---- Against Card :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Against Card :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="ddlaginCard" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="ddlaginCard_SelectedIndexChanged" Width="180">
                                             <asp:ListItem Value="0">YES</asp:ListItem>
                                             <asp:ListItem>NO</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <!----  Card No----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Card No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtCardno" runat="server" AutoComplete="off"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="12" pattern="[0-9A-Za-z]+" title="Please input A Valid Number"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                     <!----  Card Name----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Card Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtfirstname" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[A-Za-z0-9]+" title="Please input only letters"></asp:TextBox>
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <br>
                                 <div class="ui-grid-row">
                                     <!---- Patient Name:----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                         Patient First Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpname" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                         <asp:TextBox ID="txtinsurce" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Insurance Co" Visible="false"></asp:TextBox>
                                     </div>
                                      <!---- Patient Name:----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                         Patient Last Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtplastnm" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                         
                                     </div>
                                     </div>
                                  <div class="ui-grid-row">
                                     <!----Patient Condition: ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Patient Condition :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpatintcond" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                         <asp:TextBox ID="txtbillopt" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="Billing Option" Visible="false"></asp:TextBox>
                                     </div>
                                           <!---- Insurance/Corporate----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                        
                                         Insurance/Corporate :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropselfcorp" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                
                                     <!----  BPL Card No----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label10" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         BPL Card No:</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtbplno" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Refferal Details</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- From----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label8" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         From :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtfrom" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                     </div>
                                     <!---- Other Source Name----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Other Source Name</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtotSocenm" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Refferal Name----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Refferal Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropreffname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                                     </div>
                                     <!----  Card No----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Complaint Details</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- Present Complaint----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label11" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Present Complaint :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpcompl" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!---- Review----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Review :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtreviw" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Department Name---->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Department Name</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                          <asp:DropDownList ID="ddldept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                          <%--               <asp:TextBox ID="txtdeptnm" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>--%>
                                     </div>
                                     <!---- Doctor Name----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Doctor Name</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                            <asp:DropDownList ID="ddldoctor" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
<%--                                         <asp:TextBox ID="txtdoctnm" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z]+" title="Please input only letters"></asp:TextBox>--%>
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Patient&#39;s Contact Information</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- Nationality----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label12" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Nationality :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtnation" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                     </div>
                                     <!----Passport/ID----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Passport/ID :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpasspt" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Gender----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label13" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Gender :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropgender" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>Select Gender</asp:ListItem>
                                             <asp:ListItem>Male</asp:ListItem>
                                             <asp:ListItem>Female</asp:ListItem>
                                             <asp:ListItem>Transgender</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <!---- Blood Group----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Blood Group :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropblogrp" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>Select Blood Group</asp:ListItem>
                                             <asp:ListItem>A+</asp:ListItem>
                                             <asp:ListItem>A-</asp:ListItem>
                                             <asp:ListItem>B+</asp:ListItem>
                                             <asp:ListItem>B-</asp:ListItem>
                                             <asp:ListItem>O+</asp:ListItem>
                                             <asp:ListItem>O-</asp:ListItem>
                                             <asp:ListItem>AB+</asp:ListItem>
                                             <asp:ListItem>AB-</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Date Of Birth----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label14" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Date Of Birth :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdob" runat="server" AutoComplete="off" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;" OnTextChanged="txtdob_TextChanged" ></asp:TextBox>
                                     </div>
                                     <!---- Age----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label19" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Age :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtage" runat="server" AutoComplete="off" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false">

                                   </asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Address----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label15" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Address :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtAddress" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" style="width: 170px;"></asp:TextBox>
                                     </div>
                                     <!---- District----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label20" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         District :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdistrict" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- State----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label16" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         State :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtstate" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                     </div>
                                     <!----Telephone No----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Telephone No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txttelphone" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="13" MinLength="10" onkeypress="return onlyNos(event);"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Mobile No----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Mobile No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtmobno" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="11" MinLength="10" onkeypress="return onlyNos(event);"></asp:TextBox>
                                     </div>
                                     <!---- Email Id----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Email Id :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtemailid" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="40" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" type="email"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Charges----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label18" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Charges :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtcharge" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="4" onblur="if(this.value==''){this.value='0'}" onclick="if(this.value=='0'){this.value=''}" pattern="[0-9]+([,\.][0-9]+)?" value="0"></asp:TextBox>
                                     </div>
                                     <!---- ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Guardian&#39;s Information</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- Minor Patient----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                         <asp:CheckBox ID="chkMinrPatent" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnCheckedChanged="chkMinrPatent_CheckedChanged" />
                                         Minor Patient</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                     <!---- ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                 </div>
                                 <div id="gurdndiv" runat="server" visible="false">
                                     <div class="ui-grid-row">
                                         <!---- Guardian Name----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                             Guardian Name :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtguardname" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="50" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                             
                                         </div>
                                         <!---- Relation With Patient----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label26" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                             Relation With Patient</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtrelpatnt" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="40" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                         </div>
                                     </div>
                                     <div class="ui-grid-row">
                                         <!---- Address----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label22" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                             Address :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtAdressInf" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="170px"></asp:TextBox>
                                         </div>
                                         <!---- ----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             </label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                         </div>
                                     </div>
                                     <div class="ui-grid-row">
                                         <!----District----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label23" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                             District :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtdistrictInf" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLengthl="40" pattern="[a-z A-Z_]+" title="Please input only letters"></asp:TextBox>
                                         </div>
                                         <!---- State----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label27" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                             State :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtstateInf" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="40" pattern="[a-z A-Z]+" title="Please input only letters"></asp:TextBox>
                                         </div>
                                     </div>
                                     <div class="ui-grid-row">
                                         <!---- Telephone No------>
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             Telephone No :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtphInf" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="13" MinLength="10" onkeypress="return onlyNos(event);"></asp:TextBox>
                                         </div>
                                         <!----Mobile No ----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             <asp:Label ID="Label24" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                             Mobile No :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtmobnoInf" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="10" pattern="[789][0-9]{9}"></asp:TextBox>
                                         </div>
                                     </div>
                                     <div class="ui-grid-row">
                                         <!---- Email Id----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             Email Id :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtemailInf" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="40" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" type="email"></asp:TextBox>
                                         </div>
                                         <!----Registration Made By ----->
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             </label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtregMade" runat="server" AutoComplete="off" Visible="false"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 </hr>
                              
                                 
                              
                                 </br>
                             </div>
                         </div>
                         <br>
                         <hr>
                         <br>
                         <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="btncreate_Click" Text="Create" style="margin-left:5%;"/>
                         <asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" style="margin-left:5%;"/>
                         &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                         <br>
                         <br><br />
                         <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                             <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                 RGISTRATION HISTORY
                             </div>
                             <div class="ui-datatable-tablewrapper">
                                 <asp:GridView ID="grdRegtyp" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Width="1060" 
                                     CellPadding="4" DataKeyNames="ID" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdRegtyp_PageIndexChanging" 
                                     OnRowDataBound="grdRegtyp_RowDataBound" OnRowDeleting="grdRegtyp_RowDeleting" OnSorting="grdRegtyp_Sorting" OnSelectedIndexChanging="grdRegtyp_SelectedIndexChanging">
                                     <AlternatingRowStyle BackColor="White" />
                                     <Columns>
                                         <%--<asp:TemplateField Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lbltype" Visible="false" runat="server" Text='<%#Eval("TESTYPE")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>--%>
                                         <asp:BoundField DataField="ID" HeaderText="OPDNO" SortExpression="ID" />
                                         <asp:TemplateField HeaderText="NAME" Visible="true" SortExpression="PNAME">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblinv" runat="server" Text='<%#Eval("PNAME")%>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DATE&nbsp;OF&nbsp;BIRTH" Visible="true">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbldob" runat="server" Text='<%#Convert.ToDateTime(Eval("DOB")).ToString("d")%>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DATE" Visible="true" SortExpression="DATETIME" >
                                             <ItemTemplate >
                                                 <asp:Label ID="lbldtime" runat="server" Text='<%#Convert.ToDateTime(Eval("DATETIME")).ToString("d")%>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:CommandField HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowDeleteButton="true" 
                                             ShowEditButton="False" ShowSelectButton="true" />
                                         <asp:TemplateField HeaderText="Select" Visible="true">
                                             <ItemTemplate>
                                                 <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();" 
                                                     Text="Print" Visible="true"></asp:LinkButton>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                     </Columns>
                                     <EditRowStyle BackColor="#2461BF" />
                                     <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                     <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                     <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                     <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                     <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                     <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                     <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                     <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                 </asp:GridView>
                             </div>
                            
                         </div>
                        
                         <h1></h1>
                        
                     </h1>
        </div>
                    </div>
                    </div>
                </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

