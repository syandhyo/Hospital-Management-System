<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Medicine_Bill.aspx.cs" Inherits="RECEPTION_Medicine_Bill" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <td style="width:15%" align="left"></td>
    <td style="width:25%; text-align: left; font-weight: 700; text-decoration: underline;" align="right">Detail Medicine SALE/REFUND Bill</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Date :&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Label ID="lbldate" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblmid" runat="server"></asp:Label>
         </td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Enter Bed No :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
         &nbsp;<asp:Button ID="Button1" runat="server" BackColor="#66CCFF" OnClick="Button1_Click" Text="Show" />
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
       
    </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
        IPD NO :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblipno" runat="server"></asp:Label>
         </td>
    <td style="width:20%; text-align: right;" align="right">
        Name :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblname" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Total Amount :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lbltotalamt" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="right">Paid / Balance /Refundable :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblbalance" runat="server"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Button ID="Button2" runat="server" BackColor="#66CCFF" OnClick="Button2_Click" Text="Print Detail Bill" />
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Enter Amount :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtamount" runat="server" pattern="[0-9]+([,\.][0-9]+)?" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Button ID="btnsave" runat="server" BackColor="#66CCFF" OnClick="Btnsave_Click" Text="Save" Width="63px" />
         <asp:Button ID="btndelete" runat="server" BackColor="#FFCC66" OnClick="btndelete_Click" Text="Delete" Width="63px" Visible="false" />
         <asp:Button ID="btrcancel" runat="server" BackColor="#ff0000" OnClick="btncancel_Click" Text="Cancel" Width="63px" Visible="true" />
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                 <asp:BoundField DataField="NAME" HeaderText="NAME" />
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                  <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" HeaderText="ACTION" />
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
        
       
    </div></ContentTemplate></asp:UpdatePanel>
</asp:Content>

