<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="PayrollEntry.aspx.cs" Inherits="ACCOUNTS_PayrollEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    

    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
        }
        .auto-style2 {
            text-decoration: underline;
            color: #333333;
        }
        .auto-style3 {
            color: #000000;
        }
        </style>
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
    <td style="width:20%" align="center" class="auto-style1"><strong>Payroll Entry</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server"  Visible="false"></asp:TextBox></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Empid :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropname" runat="server" OnSelectedIndexChanged="dropname_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtdate" runat="server" CssClass="formdate"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Name :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblname" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Month :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropmonth" runat="server">
            <asp:ListItem>--Select--</asp:ListItem>
            <asp:ListItem>Jan</asp:ListItem>
            <asp:ListItem>Feb</asp:ListItem>
            <asp:ListItem>Mar</asp:ListItem>
            <asp:ListItem>Apr</asp:ListItem>
            <asp:ListItem>May</asp:ListItem>
            <asp:ListItem>Jun</asp:ListItem>
            <asp:ListItem>Jul</asp:ListItem>
            <asp:ListItem>Aug</asp:ListItem>
            <asp:ListItem>Sep</asp:ListItem>
            <asp:ListItem>Oct</asp:ListItem>
            <asp:ListItem>Nov</asp:ListItem>
            <asp:ListItem>Dec</asp:ListItem>
        </asp:DropDownList>
        
        <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Year :<asp:DropDownList ID="dropyear" runat="server">
            <asp:ListItem>--Select--</asp:ListItem>
            <asp:ListItem>2017</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
            <asp:ListItem>2021</asp:ListItem>
            <asp:ListItem>2022</asp:ListItem>
            <asp:ListItem>2023</asp:ListItem>
            <asp:ListItem>2024</asp:ListItem>
            <asp:ListItem>2025</asp:ListItem>
            <asp:ListItem>2026</asp:ListItem>
            <asp:ListItem>2027</asp:ListItem>
            <asp:ListItem>2028</asp:ListItem>
            <asp:ListItem>2029</asp:ListItem>
            <asp:ListItem>2030</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <div style="border: thin solid #000000">
              <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style2"><strong>Attendance</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Days In Month :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdm" runat="server">
            <asp:ListItem>28</asp:ListItem>
            <asp:ListItem>29</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>31</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label8" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Days Paid :</td>
    <td style="width:20%; text-align: left;" align="left">
        <asp:TextBox ID="txtDays" runat="server" Text="" pattern="[0-9]+" MaxLength="2"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label9" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Absent Days :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtad" runat="server" MaxLength="2" pattern="[0-9]+" Text=""></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
        <div style="border: thin solid #000000">
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left" class="auto-style3"><strong>Earning</strong></td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left" class="auto-style3">
        <strong>Deduction</strong></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Basic :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtbasic" runat="server" Enabled="false"></asp:TextBox></td>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>PF :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtpf" runat="server" Text="0"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>     
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">HRA :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txthra" runat="server" Enabled="false"></asp:TextBox></td>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>PT :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtpt" runat="server" Text="0"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Conveyance :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtcon" runat="server" Enabled="false"></asp:TextBox></td>
    <td style="width:20%" align="right"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>TDS :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txttds" runat="server" Text="0"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Medical :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmed" runat="server" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label10" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Other Deduction :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtodeduc" runat="server" Text="0"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right"><strong>Total Deduction</strong> :</td>
    <td style="width:20%" align="left"><asp:Label ID="lbldeduction" runat="server" Text="" style="font-weight: 700"></asp:Label></td></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"><strong>Total Earning</strong> :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lbltamt" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
    <td style="width:20%" align="right">
        <strong>Net Pay</strong> :</td>
    <td style="width:20%" align="left"><asp:Label ID="lblcal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
        
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:Button ID="btncal" runat="server" OnClick="btncal_Click" Text="Calculate" BackColor="#CCFFCC" /></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>         
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        
        &nbsp;<asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Submit" /> 
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />  
         
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">          
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10" DataKeyNames="id"  
               AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnRowDeleting="gvDetails_RowDeleting" 
              OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
              OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                 <asp:BoundField DataField="Name" HeaderText="Name" />
                 <asp:BoundField DataField="Basic" HeaderText="Basic" />
                <asp:BoundField DataField="HRA" HeaderText="HRA" />
                <asp:BoundField DataField="Con" HeaderText="Con" />
                <asp:BoundField DataField="Med" HeaderText="Med" />
                <asp:BoundField DataField="PF" HeaderText="PF" />
                <asp:BoundField DataField="PT" HeaderText="PT" />
                <asp:BoundField DataField="TDS" HeaderText="TDS" />
                <asp:BoundField DataField="TotalSal" HeaderText="Net Pay" />
                <asp:BoundField DataField="Month" HeaderText="Month" />
                <asp:BoundField DataField="Year" HeaderText="Year" />
                <asp:BoundField DataField="DateStamp" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
            </Columns>
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
              </ContentTemplate></asp:UpdatePanel>
</asp:Content>

