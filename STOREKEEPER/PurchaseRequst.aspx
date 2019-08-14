<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseRequst.aspx.cs" Inherits="STOREKEEPER_PurchaseRequst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function Validate() {

             if (document.getElementById("<%=txtdate.ClientID%>").value == "") {
                 alert("Field Is Mandatory !");
                 document.getElementById("<%=txtdate.ClientID%>").focus();
                 return false;
             }

             if (document.getElementById("<%=txtQuantity.ClientID%>").value == "") {
                 alert("Field Is Mandatory !");
                 document.getElementById("<%=txtQuantity.ClientID%>").focus();
                 return false;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
                <script type = "text/javascript">

                    function SetTarget() {

                        document.forms[0].target = "_blank";

                    }
        </script>
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
    <td style="width:60%; text-decoration: underline; font-weight: 700; color: #000000;" align="center">Purchase Requisition</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <div id="div3" runat="server" style="border: thin solid #000000">
         
           
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
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Department :</td>
    <td style="width:20%" align="left">
         <asp:DropDownList ID="ddDeptment" runat="server" CssClass="btn btn-default dropdown-toggle">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Material :</td>
    <td style="width:20%" align="left">
         <asp:DropDownList ID="ddItem" runat="server" CssClass="btn btn-default dropdown-toggle">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        Quantity :</td>
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
    <td style="width:20%" align="right">
        <asp:Label ID="lblUpId" runat="server" Visible="false" Text="Label"></asp:Label>
    </td>
    <td style="text-align: center;" align="left">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" Text="Create" OnClick="btncreate_Click" OnClientClick="return Validate();" />
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
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" PageSize="10" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" >
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Sl No" />
                 <asp:BoundField DataField="DeptName" HeaderText="Department&nbsp;Name" />
                  <asp:BoundField DataField="NAME" HeaderText="Material&nbsp;Name" />
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yy}"/>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>
               <%--  <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>--%>
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
</table>
    </div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>

