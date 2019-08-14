<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/MasterPage1.master" AutoEventWireup="true" CodeFile="LabIndent_View.aspx.cs" Inherits="LABORATORY_LabIndent_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
       <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
    </td>
</tr>
</table>
   <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Pathelogy Test Result</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;Bed No :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblbedno" runat="server" style="color: #0066FF"></asp:Label>
         </td>
    <td style="width:20%" align="right">Datetime :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtinvdate" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Name :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblpname" runat="server" style="color: #0066FF"></asp:Label>
         </td>
    <td style="width:20%" align="right">Ward :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblward" runat="server" style="color: #0066FF; font-weight: 700"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Indenet Type :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblindetype" runat="server" style="color: #990033; font-weight: 700" Text="IP INDENT"></asp:Label>
         </td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="background-color: #3399FF;" align="right"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td align="center">
        <asp:GridView ID="GridView1"  runat="server" DataKeyNames="ID" AutoGenerateColumns="False"  CellPadding="4" GridLines="Horizontal" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
    <Columns>
         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="TESTVALUE" >
            <ItemTemplate>
                <asp:TextBox ID="txt_value" runat="server" Text="0" Type="number"></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
    </Columns>

            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />

</asp:GridView>

        <asp:GridView ID="GridView3"  runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  CellPadding="4" GridLines="Horizontal" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
    <Columns>
         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv0" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
          <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblprice0" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="TESTVALUE" >
            <ItemTemplate>
                <asp:TextBox ID="txt_value0" runat="server" Text='<%#Eval("VALUE")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
    </Columns>

            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />

</asp:GridView>

    </td>
    <td style="width:10%" align="center"></td>
</tr>
</table><%--<table width="100%">
  
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">

         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-sm"  OnClick="Btndelete_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />

    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">

         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-sm"  OnClick="Btndelete_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />

         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="background-color: #3399FF;" align="right">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Test History</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                <asp:BoundField DataField="PID" HeaderText="PID" />
                <asp:CommandField  ShowSelectButton="true" ShowDeleteButton="false"/>
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
</div>
   </ContentTemplate></asp:UpdatePanel>

</asp:Content>

