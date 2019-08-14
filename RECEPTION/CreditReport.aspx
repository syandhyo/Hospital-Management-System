<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="CreditReport.aspx.cs" Inherits="RECEPTION_CreditReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
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
    <td style="width:20%" align="center" class="auto-style1"><strong>Credit Report</strong></td>
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
         <td style="width:20%" align="center"></td>
    <td style="width:20%" align="right">Employee :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropemp" runat="server"></asp:DropDownList></td>
    <td style="width:20%" align="left">
        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
        <asp:Button ID="Button1" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" Visible="False"/>
    </td>
    <td style="width:00%" align="left"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="center"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">        
        </td>
    <td style="width:20%" align="left"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <%--<asp:BoundField DataField="ID" HeaderText="ID" />--%>
                <asp:BoundField DataField="IPD" HeaderText="IPD" />
                 <asp:BoundField DataField="EMPID" HeaderText="EMPID" />
                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:MM/dd/yy}"/>
                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                <asp:BoundField DataField="TOTALAMT" HeaderText="TOTAL&nbsp;AMT" />
                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowSelectButton="true" HeaderText="ACTION"/>--%>
            </Columns>
            <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>
       </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
</asp:Content>

