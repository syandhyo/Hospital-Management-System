<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="STOREKEEPER_VenderMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
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
            $("[id$=txtdate]").datepicker({
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
    <td style="text-align: center;" align="left" class="auto-style1"><strong>Vendor Master</strong></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
        <asp:Label ID="lblEditgrd" Visible="false" runat="server" Text="Label"></asp:Label></td>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;VendorId</td>
    <td style="width:20%; text-align: left;" align="left"><asp:TextBox ID="txtvendorid" runat="server"  Enabled="false"></asp:TextBox></td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;" Enabled="false"></asp:TextBox></td>
    <td style="width:20%" align="center"><asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label></td>
</tr>
</table>            
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label10" runat="server" style="color: #FF0000" Text="*"></asp:Label>Company Name Part I :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcname1" runat="server" pattern="^[A-Za-z ]+$"></asp:TextBox></td>
    <td style="width:20%" align="right"><asp:Label ID="Label11" runat="server" style="color: #FF0000" Text="*"></asp:Label>Address Line I :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtadd1" runat="server" TextMode="MultiLine" pattern="^[a-zA-Z0-9_]*"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Company Name Part II :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtcname2" runat="server" pattern="^[A-Za-z ]+$"></asp:TextBox></td>
    <td style="width:20%" align="right">Address Line II :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtadd2" runat="server" TextMode="MultiLine" pattern="^[a-zA-Z0-9_]*"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label5" runat="server" style="color: #FF0000" Text="*"></asp:Label>City :</td>
    <td style="width:20%; text-align: left; margin-left: 120px;" align="left">
        <asp:TextBox ID="txtcity" runat="server" pattern="^[A-Za-z ]+$"></asp:TextBox >
         </td>
    <td style="width:20%; text-align: right;" align="right">
         PIN Code :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpin" runat="server" MaxLength="6" MinLength="6" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" style="color: #FF0000" Text="*"></asp:Label>State :</td>
    <td style="width:20%; text-align: left;" align="left">
        <asp:TextBox ID="txtstate" runat="server" pattern="^[A-Za-z -]+$" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        Tel-No.:</td>
    <td style="width:20%" align="left">
       <asp:TextBox ID="txttelno" runat="server"  MaxLength="12" MinLength="7" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox></td>
    <td style="width:20%" align="center">
        </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" style="color: #FF0000" Text="*"></asp:Label>Mobile No :</td>
    <td style="width:20%; text-align: left;" align="left">
        <asp:TextBox ID="txtmobno" runat="server"  MaxLength="10" MinLength="10" pattern="[789][0-9]{9}" ToolTip="Invalid Mobile Number"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        GST No :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtgstno" runat="server" MaxLength="15" MinLength="15" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
         </td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>

    </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Fax No.:</td>
    <td style="width:20%; text-align: left;" align="left">
        <asp:TextBox ID="txtfaxno" runat="server"></asp:TextBox>
        </td>
    <td style="width:20%" align="right"><asp:Label ID="Label4" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        State Code :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstatecode" runat="server" MaxLength="2" pattern="[0-9]+" MinLength="2" ToolTip="Please Enter Numeric"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Contact Person :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcperson" runat="server" pattern="^[a-z A-Z]*"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Contact Person No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcpersonno" runat="server" MaxLength="10" MinLength="10" pattern="[0-9]+"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Email Address :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemail1" runat="server" type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ToolTip="Invalid Email Id"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Alternate Email Address :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemail2" runat="server" type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ToolTip="Invalid Email Id"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Excise Duty :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtexcise" runat="server" MaxLength="15" MinLength="15" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label6" runat="server" style="color: #FF0000" Text="*"></asp:Label>Permament A/C No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtperacno" runat="server" MaxLength="14" MinLength="11" pattern="[0-9]+([,\.][0-9]+)?" ToolTip="Invalid Account Number"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Bank A/C No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbankacno" runat="server" MaxLength="14" MinLength="11" pattern="[0-9]+([,\.][0-9]+)?" ToolTip="Invalid Account Number"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label7" runat="server" style="color: #FF0000" Text="*"></asp:Label>Name of Bank :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbankname" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Branch Address :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbranch" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label9" runat="server" style="color: #FF0000" Text="*"></asp:Label>Type of A/c :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="txtactype" runat="server" style="width:130px">
            <asp:ListItem>Please Select</asp:ListItem>
            <asp:ListItem Value="Current"></asp:ListItem>
            <asp:ListItem>Saving</asp:ListItem>
        </asp:DropDownList>
        <%--<asp:TextBox ID="txtactype" runat="server"></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label8" runat="server" style="color: #FF0000" Text="*"></asp:Label>IFSC :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtifsc" runat="server" MaxLength="11" MinLength="11" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Opening :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtopening" runat="server" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
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
    <td style="width:20%; text-align: right;" align="left">Search Vendor :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtsearchname" runat="server" CssClass="form-control input-sm m-bot15"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        <asp:Button ID="btn_search" runat="server" BackColor="#66CCFF" OnClick="btn_search_Click" Text="Search" />
         </td>
    <td style="width:20%" align="center">
        <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>          
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="8"
              OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True"
              AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered"
              OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME1" HeaderText="Name" HeaderStyle-CssClass="text-center"/>
                 <asp:BoundField DataField="CITY" HeaderText="City" HeaderStyle-CssClass="text-center"/>
                  <asp:BoundField DataField="STATE" HeaderText="State" />
                  <asp:BoundField DataField="PIN" HeaderText="PIN" />
                  <asp:BoundField DataField="TEL_NO" HeaderText="Contact No." />
                 <asp:BoundField DataField="GST" HeaderText="GST" HeaderStyle-CssClass="text-center"/>
                  <asp:BoundField DataField="OPENING" HeaderText="Opening" />
                  <asp:BoundField DataField="ADDRESS1" HeaderText="Address" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
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
    </div>
          </ContentTemplate></asp:UpdatePanel></div>
</asp:Content>

