<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/MasterPage.master" AutoEventWireup="true" CodeFile="RFQ.aspx.cs" Inherits="GENERALSTOCK_RFQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
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
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; text-decoration: underline; font-weight: 700; color: #000000;" align="center">Request For Quotation</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <%--<div id="div3" runat="server" style="border: thin solid #000000">--%>
         
           
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
    <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"></td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="center"></td>
    <td style="width:20%; text-align: left;" align="center">
 </td>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">
        <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Vendor Name :</td>
    <td style="width:20%" align="left">
         <asp:DropDownList ID="dropvendor" runat="server" CssClass="btn btn-default dropdown-toggle">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtdate" runat="server" CssClass="formdate"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">Material Name :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="ddItem" runat="server" CssClass="btn btn-default dropdown-toggle">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtQuantity" runat="server"  Text=""></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
       </td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"></td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
      </td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
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
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" Text="Create" OnClick="btncreate_Click" />
      &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" Text="Update" Visible="False" OnClick="btnupdate_Click" />
        &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm"  Text="Cancel" OnClick="btncancel_Click" />
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
          <%--<asp:GridView ID="GridView1" runat="server" Visible="false" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" PageSize="10" AllowSorting="True"  CssClass="table table-bordered" >
            <Columns>
                <asp:BoundField DataField="VENDORID" HeaderText="VendorName" />
                 <asp:BoundField DataField="MID" HeaderText="Material&nbsp;Name" />
                  <asp:BoundField DataField="QTY" HeaderText="Qty" />
                <asp:BoundField DataField="DATE" HeaderText="Date" />
             <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action"/>                          
          
            </Columns>
             <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>--%>
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
                  </div>
          </ContentTemplate></asp:UpdatePanel></div>
</asp:Content>

