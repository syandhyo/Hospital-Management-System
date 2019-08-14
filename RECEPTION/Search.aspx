<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="RECEPTION_Search" %>

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
    </td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Staff Search</strong></td>
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
    <td style="width:20%" align="right">By Dr Name :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtname" runat="server" CssClass="form-control input-sm m-bot15" style="width:130px" pattern="[A-Z a-z]+" title="Please Enter Alphabets"></asp:TextBox>
         </td>
    <td style="width:20%" align="left"><asp:Button ID="Button1" runat="server" Text="Search By Name" CssClass="btn btn-primary btn-sm" OnClick="btnName_Click" /></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">By Designation :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdesg" runat="server" CssClass="btn btn-default dropdown-toggle" style="width:130px">
        </asp:DropDownList>
         </td>    
    <td style="width:20%" align="left"><asp:Button ID="Button2" runat="server" Text="Search By Desg" CssClass="btn btn-primary btn-sm" OnClick="btnDesg_Click" /></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">By Department :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdept" runat="server" CssClass="btn btn-default dropdown-toggle">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="left"><asp:Button ID="Button3" runat="server" Text="Search By Dept" CssClass="btn btn-primary btn-sm" OnClick="btnDept_Click" /></td>
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
    <td style="text-align: center;" align="left">
        <%--<asp:GridView ID="GridView1" runat="server">
        </asp:GridView>--%>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" CssClass="table table-bordered" >
            <Columns>
                 <asp:BoundField DataField="Sname" HeaderText="Name" HeaderStyle-CssClass="text-center" /> 
                 <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="Contact" HeaderText="Contact" HeaderStyle-CssClass="text-center"/>                
                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="false" />--%>
            </Columns>
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

