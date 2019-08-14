<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="OPConsultancy.aspx.cs" Inherits="RECEPTION_OPConsultancy" %>

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
        <asp:Label ID="LBLSLNO" runat="server" Text="Label" Visible="false"></asp:Label>    
    </td>
</tr>
</table>        
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>OPD CONSULTANCY</strong></td>
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
        <div id="div1" runat="server">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By OPD NO:</td>
    <td style="width:30%; text-align: left;" align="right">        
        <asp:TextBox ID="txtspid" runat="server" BackColor="#CCFFFF" pattern="[a-zA-Z0-9_]+" MaxLength="6" minLength="6" title="Please Enter A Valid OPD Number"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshow" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By Phone Number:</td>
    <td style="width:30%; text-align: left;" align="right">        
        <asp:TextBox ID="txtsmobile" runat="server" BackColor="#CCFFFF" onkeypress="return onlyNos(event);" MaxLength="10" MinLength="10"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshowph" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowph_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    </div>
        <div id="div2" runat="server">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">OP No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtopno" runat="server" CssClass="form-control input-sm m-bot15 formdate" Enabled="False"></asp:TextBox>
        <%--<asp:DropDownList ID="dropopno" runat="server" CssClass="form-control input-sm m-bot15"  AutoPostBack="True">
        </asp:DropDownList>--%>
    </td>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" style="color: #CC0000" Text="*"></asp:Label>Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control input-sm m-bot15 formdate" ></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        <asp:Label ID="LBLFOLLOWUP" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBLLASTDATE" runat="server" Text="0" Visible="false"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Doctor :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdoctor" runat="server" CssClass="form-control input-sm m-bot15"  AutoPostBack="true" OnSelectedIndexChanged="dropdoctor_SelectedIndexChanged" style="width:150px">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Patient Name :</td>
    <td style="width:20%" align="left"><asp:Label ID="lblname" runat="server" Text=""></asp:Label>        
        </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Fee :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfee" runat="server" CssClass="form-control input-sm m-bot15" Width="152px" Text="0" pattern="[0-9]+([,\.][0-9]+)?" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:CheckBox ID="CheckBox1" runat="server" ForeColor="#FF3300" Text="Folllow Up" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"/>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>    
         <table width="100%">
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
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Submit" />
         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btndelete_Click" Text="Delete" Visible="False" />       
         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" pagesize="10" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" >
            <Columns>                
                <asp:BoundField DataField="OPNo" HeaderText="OPD No." /> 
                   <asp:BoundField DataField="Name" HeaderText="Patient&nbsp;Name" />
                <asp:BoundField DataField="Sname" HeaderText="Doctor" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="Fee" HeaderText="Fee" />
                <asp:BoundField DataField="CDate" HeaderText="Consultancy&nbsp;Date" />                     
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="Edit" HeaderText="Action" />
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

