<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="GatePass.aspx.cs" Inherits="STOREKEEPER_GatePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Gate Pass</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">

    <div style="border: thin solid #000000"> 
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
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Gate Pass Type&nbsp; :</td>
    <td style="width:20%" align="left">        
        <asp:DropDownList ID="dropward" runat="server"> 
            <asp:ListItem>please Select </asp:ListItem>           
            <asp:ListItem>Inward</asp:ListItem>
            <asp:ListItem>Outward</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Bill No. :</td>
    <td style="width:20%" align="left">
        
         <asp:TextBox ID="txtbillno" runat="server" Text="" pattern="^[a-zA-Z0-9_]*" MaxLength="10" MinLength="6"></asp:TextBox>
        
                </td>
    <td align="right" class="auto-style3"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Security Name :</td>
    <td style="width:20%" align="left">        
        
         <asp:TextBox ID="txtname" runat="server" pattern="^[A-Za-z ]+$" MaxLength="40" MinLength="3"></asp:TextBox>
                </td>
    <td style="width:20%" align="center"></td>
</tr>
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td align="right" class="auto-style3">&nbsp; </td>
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
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
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
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="10"  OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" >
            <Columns> 
                <asp:BoundField DataField="TYPE" HeaderText="TYPE" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="BILL_NO" HeaderText="BILL NO" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="SECURITY_NAME" HeaderText="NAME" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:MM-dd-yyyy}" HeaderStyle-CssClass="text-center"/>                                  
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center"/>
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
        </td>
    <td style="width:20%" align="right"></td>
    </tr>
</table>
        </div>  
              </ContentTemplate></asp:UpdatePanel> 
</asp:Content>

