<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_nicu_admission.aspx.cs" Inherits="RECEPTION_reception_nicu_admission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <div class="route-bar">
                  
            <script type="text/javascript">
                //On Page Load.
                $(function () {
                    SetDatePicker();
                });

                $(function () {
                    SetDatePicker1();
                });
                $(function () {
                    SetDatePicker2();
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
                    $("[id$=txtbookingdate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }


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
                    $("[id$=txtoperationdate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }
                
                
                //On UpdatePanel Refresh.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker2();
                        }
                    });
                };
                function SetDatePicker2() {
                    $("[id$=txtdateofbirth]").datepicker({
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
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> NICU Admission
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>     
        </div>
        <div class="card card-w-title">
					
					 <h1 style="color:#203a5a;"><b><center>NICU ADMISSION</center></b>
                         <h1></h1>
                         <hr>
                         <br>
                         <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                 <div class="ui-grid-row">
                                     <!----  Date :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Date :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                     </div>
                                     <!----  Regn No. :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Regn No. :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtregn" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                     </div>
                                 </div>
                                 <!-- -->
                                 <div class="ui-grid-row">
                                     <!---- Patient Id :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Patient Id :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpatientid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!----  IP No. :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         IP No. :
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtipno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Name :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                     <!---- Mother Exit :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Mother Exit :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropmotherexit" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>Yes</asp:ListItem>
                                             <asp:ListItem>No</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Minor Patient :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Minor Patient :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtminorpatient" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!----Combine Bill with Mother :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Combine Bill with Mother :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropcombinebill" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>Yes</asp:ListItem>
                                             <asp:ListItem>No</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Patient Condition :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Patient Condition :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpatientcond" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!---- Emergency :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                        Emergency :
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtemergency" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Patient&#39;s Contact &amp; Other Information </u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- Gender :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Gender :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropgender" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>Male</asp:ListItem>
                                             <asp:ListItem>Female</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <!---- Date of Birth :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget"><asp:Label ID="Label6" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Date of Birth :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdateofbirth" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Blood Group :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Blood Group :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropbloodgroup" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>O+ve</asp:ListItem>
                                             <asp:ListItem>O-ve</asp:ListItem>
                                             <asp:ListItem>A+ve</asp:ListItem>
                                             <asp:ListItem>A-ve</asp:ListItem>
                                             <asp:ListItem>B+ve</asp:ListItem>
                                             <asp:ListItem>B-ve</asp:ListItem>
                                             <asp:ListItem>AB+ve</asp:ListItem>
                                             <asp:ListItem>AB-ve</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <!---- Address :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Address :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Address :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtaddress" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!---- District :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         District :
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdistrict" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- State :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         State :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtstate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                     <!---- Pin :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Pin :
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtpin" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="6" Minlength="6" pattern="[0-9]+"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Telephone No :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Telephone No :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txttelno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" Minlength="10" pattern="[0-9]{10,10}"></asp:TextBox>
                                     </div>
                                     <!---- Mobile No :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Mobile No.:
                                         </label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtmobileno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" Minlength="10" pattern="[0-9]{10,10}"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Email Id :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Email Id :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtemailid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" type="email"></asp:TextBox>
                                     </div>
                                     <!---- Emergency :----->
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
                                 <h1 style="color:#0071bc;"><b><u>Refferal Details</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- From . :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         From . :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtfrom" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                     <!---- Referal Name :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Referal Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtrefname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Booking Details</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!---- Present Complaint :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Present Complaint :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtcomplaint" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="170"></asp:TextBox>
                                     </div>
                                     <!---- Consulting Department :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Consulting Department :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                            AutoPostBack="true" OnSelectedIndexChanged="dropdept_SelectedIndexChanged" Width="180">
                                             
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Doctor Name :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Doctor Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropdoctor" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             
                                         </asp:DropDownList>
                                     </div>
                                     <!---- Operation/Treatment :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Operation/Treatment :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txttreatment" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Room Type :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Room Type :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="droproomtype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                             <asp:ListItem>A/C</asp:ListItem>
                                             <asp:ListItem>Non A/C</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                     <!---- Operation Date :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Operation Date :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtoperationdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!----Days For Stay :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Days For Stay :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtdays" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                     <!---- Booking For Date :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label8" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Booking For Date :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtbookingdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                                 </hr>
                                 
                             </div>
                         </div>
                         <br/>
                         <br/>
                         
                         <br/>
                         <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="Button1_Click" Text="Submit" />
                         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />
                         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br>
                         <br>
                         <br>
                         <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                             <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                 NICU ADMISSION
                             </div>
                             <div class="ui-datatable-tablewrapper">
                                 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
                                     <AlternatingRowStyle BackColor="White" />
                                     <Columns>
                                         <asp:BoundField DataField="REGN_NO" HeaderStyle-CssClass="text-center" HeaderText="REGN_NO No">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="IPNO" HeaderStyle-CssClass="text-center" HeaderText="IPNO">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="NAME" HeaderStyle-CssClass="text-center" HeaderText="Name">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="MOBILE" HeaderStyle-CssClass="text-center" HeaderText="MOBILE">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:BoundField>
                                         <asp:BoundField DataField="DATE" DataFormatString="{0:MM/dd/yy}" HeaderStyle-CssClass="text-center" HeaderText="Date">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:BoundField>
                                         <asp:CommandField HeaderStyle-CssClass="text-center" HeaderText="Action" SelectText="Edit" ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true">
                                         <HeaderStyle CssClass="text-center" />
                                         </asp:CommandField>
                                     </Columns>
                                     <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                     <EditRowStyle BackColor="#2461BF" />
                                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                     <RowStyle BackColor="#EFF3FB" />
                                     <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                     <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                     <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                     <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                     <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                 </asp:GridView>
                             </div>
                             
                         </div>
                        <br />
                         <br />
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

