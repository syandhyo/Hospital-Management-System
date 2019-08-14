<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="SearchPatientData.aspx.cs" Inherits="RECEPTION_SearchPatientData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type = "text/javascript">

        function SetTarget() {

            document.forms[0].target = "_blank";

        }
        </script>

    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
        }
    </style>
    <script type="text/javascript">
        function openpopup() {
            window.open("PatientBill.aspx")
        }
    </script>
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
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",

            });
            $('.todate').datepicker({
                dateFormat: 'yy-mm-dd',
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
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
    </td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong> Patient details</strong></td>
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
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right">
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By PATIENT NAME :</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtname" runat="server" BackColor="#CCFFFF" ></asp:TextBox>
         &nbsp;<asp:Button ID="btnSearch" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnSearch_Click"  />
         </td>
    <td style="width:10%" align="left"> &nbsp;<asp:Button ID="Button1" runat="server" BackColor="#008B8B" Text="Show ALL" Width="70px" OnClick="Button1_Click" /></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" ></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>

        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="grSearchPatient" runat="server" AutoGenerateColumns="False"  pagesize="10" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" >
            <Columns>                               
                  <asp:BoundField DataField="OPDNO" HeaderText="OPD&nbsp;NO" />
                <asp:BoundField DataField="IPDNO" HeaderText="IPD&nbsp;NO" />
              <asp:BoundField DataField="NAME" HeaderText="NAME" /> 
                <asp:BoundField DataField="PHONE" HeaderText="PHONE" />
                <asp:BoundField DataField="PACKAGE" HeaderText="PACKAGE" />

                <asp:BoundField DataField="DATEOFADMISSION" HeaderText="DATE&nbsp;OF&nbsp;ADMISSION" />
                <asp:BoundField DataField="DATEOFDISCHARGE" HeaderText="DATE&nbsp;OF&nbsp;DISCHARGE" />
               <asp:TemplateField HeaderText="SELECT">
            <ItemTemplate >
                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openpopup();" OnClick="LinkButton1_Click">VIEW&nbsp;FINAL&nbsp;BILL</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField> 

                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
            </Columns>
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
      
       
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
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
    <td style="width:20%" align="center">
        &nbsp;</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         
</div>
        </ContentTemplate></asp:UpdatePanel>
</asp:Content>

