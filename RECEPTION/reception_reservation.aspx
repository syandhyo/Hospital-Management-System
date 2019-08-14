<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_reservation.aspx.cs" Inherits="RECEPTION_reception_reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(
    <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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

                $(function () {
                    SetDatePicker2();
                });

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
          
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>In Patient <span>/ </span>Reservation
                </div>

            </div>


            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong>
                          <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtpatientid" runat="server" Visible="false" Text="Patient Id :"></asp:TextBox>
                </div>
                <div class="card card-w-title">

                    <h1 style="color: #203a5a;"><b><center>RESERVATION/BOOKING</center> </b> 
                        <h1></h1>
                        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                            <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> Booking No. :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtbookingno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ReadOnly="true" style="background-color:lightgrey;"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> Date :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ReadOnly="true" style="background-color:lightgrey;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget"><span style="color:red">*</span>Booking For :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtbookingfor" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text=""></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget"><span style="color:red">*</span>Name :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <hr>
                                <h1 style="color: #0071bc;"><b><u>Patient&#39;s Contact &amp; Other Information</u></b></h1>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget"><span style="color:red">*</span>Gender :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="dropgender" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px">
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Date of Birth :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdateofbirth" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Blood Group :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="dropbloodgroup" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px">
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
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"><span style="color:red">*</span>Address :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtaddress" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Columns="22" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> District :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdistrict" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> Pin :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtpin" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="6" minLength="6" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> State : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtstate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Telephone No : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txttelno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="10" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget"><span style="color:red">*</span>Mobile No. :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtmobileno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="10" pattern="[789][0-9]{9}"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> Email Id :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtemailid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" type="email"></asp:TextBox>
                                    </div>
                                </div>
                                    <br />
                                <hr>
                                <h1 style="color: #0071bc;"><b><u>Refferal Details</u></b></h1>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">From :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtfrom" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Referal Name : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtrefname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                    </div>
                                </div>
                                    <br />
                                <hr>
                                <h1 style="color: #0071bc;"><b><u>Booking Details</u></b></h1>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Present Complaint :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtcomplaint" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Columns="22" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> Consulting Department : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="dropdept" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="dropdept_SelectedIndexChanged" Width="178px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Doctor Name :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="dropdoctor" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Operation/Treatment :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txttreatment" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                       <label class="ui-outputlabel ui-widget"> <span style="color:red">*</span>Room Type :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="droproomtype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px">
                                            <asp:ListItem>A/C</asp:ListItem>
                                            <asp:ListItem>Non A/C</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">Operation Date
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtoperationdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        Days For Stay
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdays" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Maxlength="2" Minlength="1" pattern="[0-9]+"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <span style="color:red">*</span>Booking For Date
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtbookingdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                    <br />
                                <hr>
                                <br>
                                <div class="ui-grid-row">
                                    <div class="ui-panelgrid-cell ui-grid-col-3">
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-6">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="Button1_Click" Text="Submit" />
                                        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="search" OnClick="Button2_Click" Text="Update" Visible="False" />
                                        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-3">
                                    </div>
                                </div>
                                <br>
                                <br>
                                <br>
                                <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                                    <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                        RESERVATION
                                    </div>
                                    <div class="ui-datatable-tablewrapper">
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" Width="1060" 
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="BOOKING_NO" HeaderStyle-CssClass="text-center" HeaderText="Booking No">
                                                <HeaderStyle CssClass="text-center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BOOKING_FOR" HeaderStyle-CssClass="text-center" HeaderText="Booking For">
                                                <HeaderStyle CssClass="text-center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NAME" HeaderStyle-CssClass="text-center" HeaderText="Name">
                                                <HeaderStyle CssClass="text-center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BOOKING_DATE" DataFormatString="{0:dd/MM/yy}" HeaderStyle-CssClass="text-center" HeaderText="Date">
                                                <HeaderStyle CssClass="text-center" />
                                                </asp:BoundField>
                                                <asp:CommandField HeaderStyle-CssClass="text-center" HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true">
                                                <HeaderStyle CssClass="text-center" />
                                                </asp:CommandField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                  </div>
                                    <input id="j_idt79:j_idt131_selection" autocomplete="off" name="j_idt79:j_idt131_selection" type="hidden" value=""></input>
                                        
                                        <br></br>
                                    </input>
                                   
                                </div>
                               
                                <h1></h1>
                            </div>
                        </div>
                        </strong> </h1>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

