<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Patient_registration.aspx.cs" Inherits="RECEPTION_Patient_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
   <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        Sys.Application.add_load(function () {


            $('.formdate').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",

            });
            $('.todate').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",
            });
        });
    </script>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; text-decoration: underline; font-weight: 700; color: #000000;" align="center">Patient Registration</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <div id="div3" runat="server" style="border: thin solid #000000">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right">OPD Search</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By OPD NO.:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtop" runat="server" BackColor="#CCFFFF" ></asp:TextBox>
         &nbsp;<asp:Button ID="Button1" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By Phone Number:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtmob" runat="server" BackColor="#CCFFFF" ></asp:TextBox>
         &nbsp;<asp:Button ID="Button2" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowph_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
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
    <td style="width:20%; text-align: right;" align="right">OPD NO :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblpid" runat="server" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="right">UHID :</td>
    <td style="width:30%" align="left">
        <asp:Label ID="lbluhid" runat="server" Text="UHID" ForeColor="Red"></asp:Label>
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="center">Patient Type :</td>
    <td style="width:20%; text-align: left;" align="center">
  <asp:DropDownList ID="lbloutpatient" runat="server" CssClass="">
            <asp:ListItem>Outpatient</asp:ListItem>
        </asp:DropDownList></td>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Name :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtname" runat="server" CssClass="" Enabled="TRUE" Text="" pattern="[a-zA-Z ]*$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Age :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtage" runat="server" AutoPostBack="false" CssClass="" MaxLength="3" pattern="[0-9]{1-3}" Text=""></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">Gender :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droprtype" runat="server" CssClass="">
            <asp:ListItem>Female</asp:ListItem>
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Transgender</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        Phone :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtphone" runat="server" CssClass="" MaxLength="10" MinLength="10"  pattern="[0-9]{10}" Text=""></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        Emergency Contact :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtecontact" runat="server" CssClass="" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Email :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemail" runat="server" CssClass="" type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">Reffered By :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpref" runat="server" CssClass=""></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Permanent Address :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="Txtperad" runat="server" TextMode="MultiLine" CssClass=""></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Disease :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdiease" runat="server" CssClass=""   Text="" Enabled="true"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: left;" align="right">
        &nbsp;</td>
    <td style="width:25%" align="left">
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Same address as above" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="True" />
         </td>
    <td style="width:15%" align="right">City :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropcity" runat="server" CssClass="">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Present Address :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttempad" runat="server" TextMode="MultiLine" CssClass=""></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">
        <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Registration fee :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtregfee" runat="server" CssClass="" MaxLength="10"  pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
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
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Create" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />
        &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
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
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" PageSize="10" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="OPDNO" />
                 <asp:BoundField DataField="NAME" HeaderText="PATIENT&nbsp;NAME" />
                  <asp:BoundField DataField="PHONE" HeaderText="PHONE" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>
                 <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <a  ><img height="20px" width="20px"   src="~/gimg/print-icon.png" />
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();"></asp:LinkButton>
             </a> </ItemTemplate>
          </asp:TemplateField>
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
    </div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>


