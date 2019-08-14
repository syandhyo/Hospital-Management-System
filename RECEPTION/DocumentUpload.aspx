<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="DocumentUpload.aspx.cs" Inherits="RECEPTION_DocumentUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>--%>
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
        function ValidateSize(file) {
            var FileSize = file.files[0].size / 1024 / 1024; // in MB
            if (FileSize > 2)
            {
                alert('File size exceeds 2 MB');
                
                // $(file).val(''); //for clearing with Jquery
            } 
            else 
            {

            }
      
        }
    </script>
      
       
       
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
         <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
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
    <td style="width:20%" align="center" class="auto-style1"><strong>Document Upload</strong></td>
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
        <br />
        <div id="div1" runat="server">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By IPDNO :</td>
    <td style="width:30%; text-align: left;" align="right">        
        <asp:TextBox ID="txtspid" runat="server" BackColor="#CCFFFF" pattern="^[A-Za-z0-9]+$" MaxLength="8" minlength="6" title="Please enter The IPD Number EX:IP0001"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshow" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            
    </div>
        <div id="div2" runat="server" >
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Name :</td>
    <td style="width:20%" align="left">        
        <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
    </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="true" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right">IPD No :&nbsp; </td>
    <td style="width:20%" align="left">
       <asp:Label ID="lblip" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" class="auto-style3"></td>
    <td style="width:20%" align="left">        
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>



     <tr>
    <td style="width:20%" align="right">Image Upload :</td>
    <td style="width:20%" align="left">
        <%--<asp:Label ID="lblbed" runat="server" Text=""></asp:Label>--%>
        <asp:FileUpload ID="fluImg" runat="server"  type="file"/>
         </td>
    <td align="right" class="auto-style3">&nbsp; </td>
    <td style="width:20%" align="left">        
           <%--<asp:Label ID="lblbed" runat="server" Text=""></asp:Label>--%>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
   <%--  <tr>
    <td style="width:20%" align="right">Ward Name : </td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblward" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" class="auto-style2">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
            <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>--%>
            </table>    
         
         <table width="100%">
    <%-- <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="lblward1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Charge Type :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropcharge" runat="server" CssClass="form-control input-sm m-bot15" Enabled="true" AutoPostBack="true">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="lblward4" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Description of Charge :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdsc" runat="server" CssClass="form-control input-sm m-bot15" Text=""  Width="152px"></asp:TextBox>
         </td>
    <td style="width:20%" align="center">&nbsp;</td>
</tr>--%>
</table>   
          <%--  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">
        <asp:Label ID="lblward3" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Price :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtprice" runat="server" CssClass="form-control input-sm m-bot15" Text="0" pattern="[0-9]+([,\.][0-9]+)?" Width="152px"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%>
         <table width="100%">
             <%--button--%>
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm"  Text="Submit" OnClick="btnSubmit_Click" />
         &nbsp;<%--<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary btn-sm"  Text="Delete" Visible="False" />--%>&nbsp;
        <%--<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm"  Text="Cancel" />--%>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>
        <table width="100%">
           <%-- gridview--%>
    <%-- <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" >
            <Columns> 
                 <asp:BoundField DataField="BDate" HeaderText="Date" />
                <asp:BoundField DataField="PID" HeaderText="PID" />               
                <asp:BoundField DataField="NAME" HeaderText="Name" /> 
                <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                <asp:BoundField DataField="WARD" HeaderText="Ward Name" />
                <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />
            </Columns>
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>--%>
</table>
        </div>   
             <%-- </ContentTemplate></asp:UpdatePanel>--%>


</asp:Content>

