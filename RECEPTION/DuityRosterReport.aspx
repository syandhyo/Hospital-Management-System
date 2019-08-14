<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="DuityRosterReport.aspx.cs" Inherits="RECEPTION_DuityRosterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
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
            $("[id$=txtfdate]").datepicker({
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
            $("[id$=txttdate]").datepicker({
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
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Duty Roster Report</strong></td>
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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>From Date</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        To Date</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table> 
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;<td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="left">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Show" />
        </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </table>   
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        &nbsp;&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">        
        <asp:Button ID="Button1" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" Visible="False"/></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        <asp:GridView ID="GridView1" runat="server"   DataKeyNames="id"  AllowPaging="True" AllowSorting="True"  pagesize="10" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" AutoGenerateColumns="False">
            <Columns>     
                 <asp:BoundField DataField="Sname" HeaderText="Staff" HeaderStyle-CssClass="text-center"/> 
                 <asp:BoundField DataField="WardName" HeaderText="Ward" HeaderStyle-CssClass="text-center"/>
                 <asp:BoundField DataField="Shift" HeaderText="Shift" />             
                 <asp:BoundField DataField="FDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"/>  
                 <asp:BoundField DataField="TDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"/> 
                <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>--%>
            </Columns>
               <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
              </ContentTemplate>

 </asp:UpdatePanel>
</asp:Content>

