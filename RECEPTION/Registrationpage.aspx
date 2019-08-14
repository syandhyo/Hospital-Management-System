<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Registrationpage.aspx.cs" Inherits="RECEPTION_Registrationpage" %>

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
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
        //On Page Load.
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

        //function findage() {
        //    var PresentDay = new Date();
        //    var dob = (new Date(document.getElementById("txtdob").value));
        //    var months = (PresentDay.getMonth() - dateOfBirth.getMonth() +
        //           (12 * (PresentDay.getFullYear() - dateOfBirth.getFullYear())));
        //    document.getElementById("txtage").value = Math.round(months / 12);
        //}
    </script>
        <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"rel="Stylesheet" type="text/css" />
    
                  <script type="text/javascript">
                      Sys.Application.add_load(function () {
                          $("[id$=txtcontno]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/Nonpobill.aspx/GetCustomers") %>',
                                      data: "{ 'prefix': '" + request.term + "'}",
                                      dataType: "json",
                                      type: "POST",
                                      contentType: "application/json; charset=utf-8",
                                      success: function (data) {
                                          response($.map(data.d, function (item) {
                                              return {
                                                  label: item.split('/ \s*/')[0]
                                              }
                                          }))
                                      },
                                      error: function (response) {
                                          alert(response.responseText);
                                      },
                                      failure: function (response) {
                                          alert(response.responseText);
                                      }
                                  });
                              },
                              select: function (e, i) {
                                  $("[id$=hfCustomerId]").val(i.item.val);
                              },
                              minLength: 1
                          });
                      });
    </script>
       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td align="left"><strong>Patient Registration</strong></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label> 
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label> 
        <asp:Label ID="LBLSLNO" runat="server" Text="Label" Visible="false"> </asp:Label>
        <asp:Label ID="lbluhid" runat="server" Text="Label" Visible="false"></asp:Label>
         </td>
</tr>
</table>
        <div style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Date & Time </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtDate" runat="server" CssClass="formdate" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right"> </td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtRegstno" runat="server" text="Registration No"  Visible="false"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"> </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtRegType" runat="server" Text="Registration Type" Visible="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtaginstbook" runat="server" Text="Against Booking" Visible="false"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><div style="border: thin solid #000000">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Booking Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>      
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Booking No. </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbookno" runat="server" OnTextChanged="txtbookno_TextChanged" AutoPostBack="true"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Booking Date & Time </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbookdate" runat="server" CssClass="formdate" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Booking For. </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbookfor" runat="server" Enabled="false"></asp:TextBox>
      
         </td>
       <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
      <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong> <asp:CheckBox ID="chkMlc" runat="server" OnCheckedChanged="chkMlc_CheckedChanged" AutoPostBack="true"/>MLC Case</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
            <div style="border: thin solid #000000" runat="server" id="mlcDiv" visible="false">
                    
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Case
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcase" runat="server" MaxLength="40" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Date</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdateMlc" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Police Station</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtPolicStaion" runat="server" MaxLength="40"></asp:TextBox>
     
         </td>
       <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
               
              <%--<div style="border: thin solid #000000">
            
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Patient
       </td>
    <td style="width:20%" align="left">
        
        
         </td>
    <td style="width:20%" align="right">ID</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtid" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
        </div>--%>

            <div style="border: thin solid #000000">
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>Against Card</td>
    <td style="width:20%" align="left">
        
     <asp:DropDownList ID="ddlaginCard" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlaginCard_SelectedIndexChanged">
         <asp:ListItem>YES</asp:ListItem>
           <asp:ListItem>NO</asp:ListItem>
     </asp:DropDownList>
         </td>
       <td style="width:20%" align="right"><asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Card No&nbsp;</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtCardno" runat="server" pattern="[0-9A-Za-z]+" title="Please input A Valid Number" MaxLength="12"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>       
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtTitle" runat="server" visible="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Card Name&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfirstname" runat="server" pattern="[A-Za-z0-9]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
  <%--  <table width="100%">
     <tr>
    <td style="width:20%" align="right">Middle Name</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmiddlnm" runat="server" ></asp:TextBox>
     
         </td>
       <td style="width:20%" align="right">Last Name</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtLastname" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%>
        </div>

            <div style="border: thin solid #000000">
                
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Patient Condition
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpatintcond" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbillopt" runat="server" Text="Billing Option" Visible="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"> <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>Patient Name:</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpname" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters"  ></asp:TextBox>
     
         </td>
       <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
           <asp:TextBox ID="txtinsurce" runat="server" Text="Insurance Co" Visible="false" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="lblcorpo" runat="server"  Visible="false"></asp:Label><asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>Insurance/Corporate</td>
    <td style="width:20%" align="left">
     <%--  ************** <asp:TextBox ID="txtselfCorp" runat="server" ></asp:TextBox>--%>
        <asp:DropDownList ID="dropselfcorp" runat="server">

        </asp:DropDownList>
     
         </td>
       <td style="width:20%" align="right">BPL Card No</td>
    <td style="width:20%" align="left">
           <asp:TextBox ID="txtbplno" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
            <div style="border: thin solid #000000">
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Refferal Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>     
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label9" runat="server" ForeColor="Red" Text="*"></asp:Label>From
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfrom" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Other Source Name</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtotSocenm" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Refferal Name</td>
    <td style="width:20%" align="left">
     <%--  *************** <asp:TextBox ID="txtrefname" runat="server" ></asp:TextBox>--%>
        <asp:DropDownList ID="dropreffname" runat="server"></asp:DropDownList>
         </td>
       <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       <%-- <asp:TextBox ID="txtreff2" runat="server" ></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
<div style="border: thin solid #000000">
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong> Complaint Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>     
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label10" runat="server" ForeColor="Red" Text="*"></asp:Label>Present Complaint
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpcompl" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Review</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtreviw" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">Department Name</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdeptnm" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" ></asp:TextBox>
     
         </td>
       <td style="width:20%" align="right">Doctor Name</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtdoctnm" runat="server" pattern="[a-z A-Z]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
            <div style="border: thin solid #000000">
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong> Patient's Contact Information</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>     
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*"></asp:Label>Nationality
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtnation" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Passport/ID</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpasspt" runat="server" ></asp:TextBox>
         </td>

    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label12" runat="server" ForeColor="Red" Text="*"></asp:Label>Gender</td>
    <td style="width:20%" align="left">
       <%-- <asp:TextBox ID="txtgender" runat="server" ></asp:TextBox>--%>   
        <asp:DropDownList ID="dropgender" runat="server">
            <asp:ListItem>Select Gender</asp:ListItem>
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
            <asp:ListItem>Transgender</asp:ListItem>
        </asp:DropDownList>  
         </td>
       <td style="width:20%" align="right">Blood Group</td>
    <td style="width:20%" align="left">
       <%-- <asp:TextBox ID="txtBlgrp" runat="server" ></asp:TextBox>--%>
         <asp:DropDownList ID="dropblogrp" runat="server">
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
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label25" runat="server" ForeColor="Red" Text="*" ></asp:Label>Date Of Birth</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdob" runat="server" CssClass="formdate" OnTextChanged="txtdob_TextChanged" AutoPostBack="true" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>     
         </td>
       <td style="width:20%" align="right"><asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label>Age</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtage" runat="server" Enabled="false" ></asp:TextBox> 
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
               <%--  <table width="100%">
     <tr>
    <td style="width:20%" align="right"> Years
       </td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtyears" runat="server" Width="80px"></asp:TextBox>         
         </td>
       <td style="width:20%" align="right"> Month</td>
    <td style="width:20%" align="left">
      <asp:TextBox ID="txtmonth" runat="server" Width="80px"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"> Days   </td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtdays" runat="server" Width="80px"></asp:TextBox> 
         </td>
       <td style="width:20%" align="right">Area</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtarea" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%>

                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*"></asp:Label>Address</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ></asp:TextBox>    
         </td>
       <td style="width:20%" align="right"><asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*"></asp:Label>District</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdistrict" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label16" runat="server" ForeColor="Red" Text="*"></asp:Label>State</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstate" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" ></asp:TextBox>     
         </td>
       <td style="width:20%" align="right">Telephone No</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttelphone" runat="server" onkeypress="return onlyNos(event);" MaxLength="13" MinLength="10"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right">Mobile No</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmobno" runat="server" onkeypress="return onlyNos(event);" MaxLength="11" MinLength="10"></asp:TextBox>     
         </td>
       <td style="width:20%" align="right">Email Id</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemailid" runat="server" type="email" MaxLength="40" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label17" runat="server" ForeColor="Red" Text="*"></asp:Label>Charges</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcharge" runat="server"   MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox> 
         </td>
       <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>
            <div style="border: thin solid #000000">
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong> Guardian's Information</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>     
                 <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><asp:Label ID="Label18" runat="server" ForeColor="Red" Text="*"></asp:Label>
        <asp:CheckBox ID="chkMinrPatent" runat="server" OnCheckedChanged="chkMinrPatent_CheckedChanged" AutoPostBack="true"/>Minor Patient</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
        <div runat="server" id="gurdndiv" visible="false">
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"></asp:Label>Guardian Name
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtguardname" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" MaxLength="40"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>Relation With Patient</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtrelpatnt" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters"  MaxLength="40"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"></asp:Label>Address</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtAdressInf" runat="server" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
     
         </td>
       <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       <%--  <asp:TextBox ID="txtAreaInf" runat="server" ></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label22" runat="server" ForeColor="Red" Text="*"></asp:Label>District
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdistrictInf" runat="server" pattern="[a-z A-Z_]+" title="Please input only letters" MaxLengthl="40"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label23" runat="server" ForeColor="Red" Text="*"></asp:Label>State</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstateInf" runat="server" pattern="[a-z A-Z]+" title="Please input only letters" MaxLength="40"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%" align="right">Telephone No
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtphInf" runat="server" onkeypress="return onlyNos(event);" MaxLength="13" MinLength="10"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label24" runat="server" ForeColor="Red" Text="*"></asp:Label>Mobile No</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmobnoInf" runat="server" MaxLength="10" MinLength="10" pattern="[789][0-9]{9}" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%" align="right">Email Id
       </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemailInf" runat="server" type="email" MaxLength="40" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Registration Made By</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtregMade" runat="server" Enabled="false" pattern="[a-z A-Z]+" title="Please input only letters" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
   </div>
        </div>    
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" Text="Create" OnClick="btncreate_Click"/>
        <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm"  Text="Update" Visible="False" OnClick="btnupdate_Click"/>
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm"  Text="Cancel" OnClick="btncancel_Click"/>
         </td>
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
      
         </td>
</tr>
</table>
               <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:60%" align="center"> 
       <asp:GridView ID="grdRegtyp" runat="server" AutoGenerateColumns="False"  BackColor="White" DataKeyNames="ID"  BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AllowPaging="True" AllowSorting="True" PageSize="10" OnPageIndexChanging="grdRegtyp_PageIndexChanging" CellPadding="3" OnSelectedIndexChanging="grdRegtyp_SelectedIndexChanging" OnRowDataBound="grdRegtyp_RowDataBound" OnRowDeleting="grdRegtyp_RowDeleting">
           <Columns>
                <%--<asp:TemplateField Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lbltype" Visible="false" runat="server" Text='<%#Eval("TESTYPE")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>--%>
               <asp:BoundField HeaderText="OPDNO" DataField="ID" />
          

         <asp:TemplateField HeaderText="NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("PNAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="DATE&nbsp;OF&nbsp;BIRTH" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldob" runat="server" Text='<%#Eval("DOB")%>'></asp:Label>
               <%-- <asp:TextBox ID="" runat="server" Text='<%#Eval("DOB")%>' ></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
          
                   <asp:TemplateField HeaderText="DATE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldtime" runat="server" Text='<%#Eval("DATETIME")%>'></asp:Label>
             <%--   <asp:TextBox ID="lbldob" runat="server" Text='<%#Eval("DATETIME")%>' ></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
               <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action"/>
                     <asp:TemplateField HeaderText="Select" Visible="true" >
            <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" Visible="true" OnClick="LinkButton2_Click" Text="Print" OnClientClick="SetTarget();"></asp:LinkButton>
              </ItemTemplate>
        </asp:TemplateField>
       <%-- <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged"/>
            </ItemTemplate>
        </asp:TemplateField>--%>
              
    </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
      
    </td>
    <td style="width:20%" align="right"></td>   
</tr>
</table> 
            </div>

        </div>
    </div></ContentTemplate></asp:UpdatePanel>
</asp:Content>

