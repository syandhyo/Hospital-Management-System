<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Lamaform.aspx.cs" Inherits="RECEPTION_Lamaform" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
  <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
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
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">LAMA FORM</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"><asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Select Bed No :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:DropDownList ID="dropbedno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged">
        </asp:DropDownList>
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
</table>
              <table width="100%">
     <tr>
    <td style="width:20%" align="right">IPD NO:</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblipno" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="right">Patient Name :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblpname" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="center">
        <asp:Label ID="txtid" runat="server" Text="Label" Visible="False"></asp:Label>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">        
        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Parent/Guardian Name :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtgname" runat="server" style="Width:150px" pattern="^[A-Za-z ]+$" MaxLength="40" minLength="3" Title="Please enter The Alphabets"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Date :&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:10%" align="left"></td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Create" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
              <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    
    <td style="width:60%" align="center">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" PageSize="10"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                 <asp:BoundField DataField="IPNO" HeaderText="IPDNO" />
                  <asp:BoundField DataField="PNAME" HeaderText="PATIENTNAME" />
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>
             <asp:TemplateField HeaderText="PRINT">
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
</table>
              <table width="100%">
     <tr>
    <td align="center">
        
    </td>
</tr>
</table>
    </ContentTemplate></asp:UpdatePanel></div>
</asp:Content>

