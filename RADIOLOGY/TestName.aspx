<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/MasterPage.master" AutoEventWireup="true" CodeFile="TestName.aspx.cs" Inherits="RADIOLOGY_TestName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"> 
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
<div>
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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Test Name</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <div style="border-style: solid; border-width: thin">
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Category : </td>
    <td style="width:40%; text-align: left;" align="right">
        <asp:DropDownList ID="Dropcategory" runat="server" CssClass="btn btn-default dropdown-toggle" AutoPostBack="false">
        </asp:DropDownList>
    </td>
    
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:20%; text-align: left;" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
         </td>
</tr>
</table>
        
        </div>
    <div align="center" style="border-style: solid; border-width: thin; ">
        <table width="100%">
     <tr>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%; text-align: center;" align="right"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Investigation Desired </td>
    <td style="width:20%; text-align: center;" align="right">Price </td>
    <td style="width:20%; text-align: center;" align="left"></td>
    <td style="width:20%; text-align: center;" align="left"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="right">
        <asp:TextBox ID="txtinv" runat="server" CssClass="form-control input-sm m-bot15" pattern="[a-z A-Z_]+"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:TextBox ID="txtprice" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="6"  pattern="[0-9]+([,\.][0-9]+)?" Text="0" ></asp:TextBox>

         </td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center">
       
         </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right">
        <asp:Button ID="btnadd" runat="server" Text="Add" Width="80px" OnClick="btnadd_Click" />
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <asp:GridView ID="grvStudentDetails" runat="server" 
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" 
                GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" >
    <Columns>
       <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
         <asp:TemplateField HeaderText="Sno">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_slno" runat="server" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Investigation Desired">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Price">
            <ItemTemplate>
                <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField>
            <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
                
                <%--<asp:Button ID="ButtonAdd" runat="server" 
                        Text="Add New Row" OnClick="ButtonAdd_Click" BackColor="SteelBlue" ForeColor="White"/>--%>
                 
            </FooterTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#E3EAEB" />
    <EditRowStyle BackColor="#7C6F57" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>
    </div>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />
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
    <td align="center">
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" DataKeyNames="slno" 
              OnRowDeleting="GridView2_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" 
              OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="10">
            <Columns>
                <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>
            </Columns>
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="left">
        <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
</div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>

