<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="Supplier_Master.aspx.cs" Inherits="ADMIN_PHARMACYSTORE_Supplier_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <div>

   <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left" class="auto-style1"><strong>Supplier Master</strong></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        Supplier Name :</td>
    <td style="width:20%; text-align: right; margin-left: 120px;" align="left">
        <asp:TextBox ID="txtname" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Za-z ]+$" ToolTip="Please Enter Character" MaxLength="30"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: right;" align="right">
        City :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcity" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">State :</td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:TextBox ID="txtstate" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        Pin :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpin" runat="server" CssClass="form-control input-sm m-bot15" onkeypress="return onlyNos(event);"  MaxLength="6" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" style="color: #FF0000" Text="*"></asp:Label>Contact No :</td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:TextBox ID="txtcontactno" runat="server" CssClass="form-control input-sm m-bot15" onkeypress="return onlyNos(event);"  MaxLength="10" MinLength="10" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        GST No :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtgstno" runat="server" CssClass="form-control input-sm m-bot15" pattern="[A-Z0-9]+" MaxLength="15" MinLength="15"></asp:TextBox>
         </td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>

    </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Address :</td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control input-sm m-bot15" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label4" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        State Code :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstatecode" runat="server" CssClass="form-control input-sm m-bot15" onkeypress="return onlyNos(event);"  MaxLength="2" MinLength="2" pattern="[0-9]+"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtopening" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
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
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="NAME" />
                 <asp:BoundField DataField="CITY" HeaderText="CITY" />
                  <asp:BoundField DataField="STATE" HeaderText="STATE" />
                  <asp:BoundField DataField="PIN" HeaderText="PIN" />
                  <asp:BoundField DataField="CONTACT" HeaderText="CONTACT_NO" />
                 <asp:BoundField DataField="GST" HeaderText="GST" />
                  <asp:BoundField DataField="OPENING" HeaderText="OPENING" />
                  <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit&nbsp;" HeaderText="ACTION"/>
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

