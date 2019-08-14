<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Ambulance.aspx.cs" Inherits="RECEPTION_Ambulance" %>

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
    <td style="width:20%" align="center" class="auto-style1"><strong>Ambulance Service</strong></td>
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
        <div style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtAdate" runat="server"  CssClass="form-control input-sm m-bot15" Enabled="False"></asp:TextBox></td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>Driver :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdriver" runat="server" CssClass="btn btn-default dropdown-toggle" style="width:150px">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Ambulance No.:</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropambulanceno" runat="server" CssClass="btn btn-default dropdown-toggle" style="width:150px">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>From Destination:</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfrom" runat="server" CssClass="form-control input-sm m-bot15" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Patient Name :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpatient" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Z a-z -]+$" MaxLength="30" MinLength="3" title="Please Enter Alphabets Only"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>To Destination :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtto" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Z a-z -]+$" MaxLength="30" MinLength="3" title="Please Enter Alphabets Only"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Attendent Name :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtattend" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Za-z ]+$" MaxLength="40" minlength="3" title="Please Enter Attendent Name"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>Approx Km :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtkm" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="3" pattern="[0-9]+([,\.][0-9]+)?" title="Please Enter numeric value" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Contact No :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcont" runat="server" pattern="[789][0-9]{9}" MaxLength="10" minlength="10" title="Please Enter Phone number" CssClass="form-control input-sm m-bot15"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Fee :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfee" runat="server" CssClass="form-control input-sm m-bot15" maxLength="5" pattern="[0-9]+([,\.][0-9]+)?" title="Please Enter numeric value" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
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
    <td align="center">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table></div>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" PageSize="10" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" >
            <Columns> 
                 <asp:BoundField DataField="id" HeaderText="" />    
                 <asp:BoundField DataField="ADate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                 <asp:BoundField DataField="PatientName" HeaderText="Patient&nbsp;Name" /> 
                 <asp:BoundField DataField="AttendentName" HeaderText="Attendent" />
                 <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />             
                 <asp:BoundField DataField="From" HeaderText="From" />  
                 <asp:BoundField DataField="To" HeaderText="To" />
                 <asp:BoundField DataField="ApproxKm" HeaderText="Km" />
                 <asp:BoundField DataField="Fee" HeaderText="Fee" />
                 <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
                <asp:TemplateField HeaderText="Print">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
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

