<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Reservation.aspx.cs" Inherits="RECEPTION_Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        //Sys.Application.add_load(function () {


        //    $('.formdate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",
        //    });
        //});
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
       
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>     
    </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Reservation/Booking</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
             
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">

        <div style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Booking No.&nbsp; :</td>
    <td style="width:20%" align="left">        
        <asp:TextBox ID="txtbookingno" runat="server" Enabled="False"></asp:TextBox>
    </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:25%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="False" ></asp:TextBox>
         </td>
    <td style="width:15%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Booking For :</td>
    <td style="width:20%" align="left">
        
         <asp:TextBox ID="txtbookingfor" runat="server" Text=""></asp:TextBox>
        
                </td>
    <td align="right" class="auto-style3"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Name :</td>
    <td style="width:20%" align="left">        
        
         <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
    <td style="width:20%" align="center"></td>
</tr>
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpatientid" runat="server" Visible="false" Text="Patient Id :"></asp:TextBox>
         </td>
    <td align="right" class="auto-style3">&nbsp; </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>    
</tr>
</table>--%>
        <div>
        
            <div style="border: thin solid #000000">
            <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">--%>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="center" class="auto-style1"><strong>Patient&#39;s Contact &amp; Other Information</strong></td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Gender :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropgender" runat="server">
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Date of Birth :-</td>
    <td style="width:25%" align="left">
        <asp:TextBox ID="txtdateofbirth" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:15%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Blood Group :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropbloodgroup" runat="server">
            <asp:ListItem>O+ve</asp:ListItem>
            <asp:ListItem>O-ve</asp:ListItem>
            <asp:ListItem>A+ve</asp:ListItem>
            <asp:ListItem>A-ve</asp:ListItem>
            <asp:ListItem>B+ve</asp:ListItem>
            <asp:ListItem>B-ve</asp:ListItem>
            <asp:ListItem>AB+ve</asp:ListItem>
            <asp:ListItem>AB-ve</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Address :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">District :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdistrict" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Pin :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpin" runat="server" MaxLength="6" minLength="6" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">State :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstate" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Telephone No :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttelno" runat="server" MinLength="10" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
         </td>
   <td style="width:20%" align="right"><asp:Label ID="Label7" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Mobile No.:-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmobileno" runat="server" MinLength="10" MaxLength="10" pattern="[789][0-9]{9}"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <table width="100%">
     <tr>
    <td style="width:20%" align="right">Email Id :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemailid" runat="server" type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        &nbsp;</td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>    
    </tr>
</table> --%>              
                </div>
        <div style="border: thin solid #000000">
            <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">--%>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>&nbsp;Referal Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
       <table width="100%">
     <tr>
    <td style="width:20%" align="right">From . :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfrom" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
       <td style="width:20%" align="right">Referal Name :-&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtrefname" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>
    </tr>
</table>--%>
                 </div>
        <div style="border: thin solid #000000">
            <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="left">--%>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>&nbsp;Booking Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Present Complaint :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcomplaint" runat="server" TextMode="MultiLine"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Consulting Department :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdept" runat="server" style="width:130px" AutoPostBack="true" OnSelectedIndexChanged="dropdept_SelectedIndexChanged">
          
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Doctor Name :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdoctor" runat="server">
         
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Operation/Treatment :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttreatment" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Room Type :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droproomtype" runat="server">
            <asp:ListItem>A/C</asp:ListItem>
            <asp:ListItem>Non A/C</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Operation Date :</td>
    <td style="width:25%" align="left">
        <asp:TextBox ID="txtoperationdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:15%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Days For Stay :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdays" runat="server" pattern="[0-9]+" Maxlength="2" Minlength="1"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Booking For Date :</td>
    <td style="width:25%" align="left">
        <asp:TextBox ID="txtbookingdate" runat="server" CssClass="formdate"  onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
        `</td>
    <td style="width:15%" align="center"></td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>
</tr>
</table>--%>

        </div>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Submit" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    </tr>
</table>
            
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="center">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="10" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" >
            <Columns> 
                <asp:BoundField DataField="BOOKING_NO" HeaderText="Booking No" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="BOOKING_FOR" HeaderText="Booking For" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="NAME" HeaderText="Name" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="BOOKING_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yy}" HeaderStyle-CssClass="text-center"/>                                  
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>
            </Columns>
              <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="left"></td>
</tr>
</table>


            </td>
    <td style="width:20%" align="right"></td>    
</tr>
</table>
        
        
              </ContentTemplate></asp:UpdatePanel> 
</asp:Content>

