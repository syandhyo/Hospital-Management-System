<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Police_info_form.aspx.cs" Inherits="RECEPTION_Police_info_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>
    (<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
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
            $("[id$=txtdeathdate]").datepicker({
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
    </script>--%>

   <div>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <div>
   <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" class="auto-style1"><strong>Police Information Form</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Select Bed No :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropbedno" runat="server" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <%--<asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>--%>
        Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <%--<asp:Label ID="Label10" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>--%>
        IPDNo:</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblipno" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="right">Patient Name :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblpname" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Age :</td>
    <td style="width:30%" align="left">
        <asp:Label ID="lblage" runat="server"></asp:Label>
         &nbsp;</td>
    <td style="width:10%" align="right">Gender :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblgender" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">        
        Religion :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtreligion" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label9" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Nationality :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtnationality" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Permanenet Address :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpaddress" runat="server" TextMode="MultiLine" Width="199px"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Cause :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropcause" runat="server">
            <asp:ListItem>RTA</asp:ListItem>
            <asp:ListItem>POISINING</asp:ListItem>
            <asp:ListItem>BURN</asp:ListItem>
            <asp:ListItem>HANGING</asp:ListItem>
            <asp:ListItem>MISCELLANEOUS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center">
        <asp:Label ID="TXTID" runat="server" Text="Label" Visible="False"></asp:Label>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Correspondence Address with Phone No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcaddress" runat="server" TextMode="MultiLine" Width="199px"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Identification Mark :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtidmark" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Admission Date Time :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lbladdate" runat="server" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="right">Death Date Time :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdeathdate" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Accompanying Person :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtperson" runat="server" Width="192px" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Relation with Patient :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtrelationpatient" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Case History :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcasehis" runat="server" TextMode="MultiLine" Width="199px" pattern="^[a-zA-Z0-9_]*" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Cause of Death :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcauseofdeath" runat="server" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
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
</table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="center">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Create" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
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
    <td align="cenetr">

        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Id" />
                <asp:BoundField DataField="PID" HeaderText="Patient&nbsp;Id" />
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                <asp:BoundField DataField="PNAME" HeaderText="Patient&nbsp;Name" />
                <asp:BoundField DataField="BEDNO" HeaderText="BedNo" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
            <asp:TemplateField HeaderText="Print">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>

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
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
              </div></ContentTemplate></asp:UpdatePanel></div>
</asp:Content>

