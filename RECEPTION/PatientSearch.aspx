<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="PatientSearch.aspx.cs" Inherits="RECEPTION_PatientSearch" %>

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
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Patient Search</strong></td>
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
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
        <div id="div1" runat="server" style="border: thin solid #000000">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right">OPD Search</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By OPD NO.:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtspid" runat="server" BackColor="#CCFFFF" pattern="^[A-Za-z0-9]+$" MaxLength="8" minlength="6" title="Please enter The OPD Number"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshow" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By Phone Number:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtsmobile" runat="server" BackColor="#CCFFFF" pattern="[6789][0-9]{9}" MaxLength="10" minlength="10" title="Please Enter The Numeric Values"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshowph" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowph_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" pagesize="10" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="OPNO" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="PNAME" HeaderText="NAME" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="TELPHNO" HeaderText="PHONE" HeaderStyle-CssClass="text-center"/>                
                <asp:BoundField DataField="MOBNO" HeaderText="ECONTACT" HeaderStyle-CssClass="text-center"/>
                <%--<asp:BoundField DataField="DISEASE" HeaderText="DISEASE" />--%>
                <asp:BoundField DataField="DATETIME" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center"/>
                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
            </Columns>
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>


         <div id="div2" runat="server" style="border: thin solid #000000">

             <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right">IPD Search</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By PATIENT NAME :</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtname" runat="server" BackColor="#CCFFFF"  pattern="^[A-Za-z ]+$" MaxLength="40" minlength="3" title="Please Enter The Character"></asp:TextBox>
         &nbsp;<asp:Button ID="Button1" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowname_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By IPD NO.:</td>
    <td style="width:30%; text-align: left;" align="right">
        <asp:TextBox ID="txtip" runat="server" BackColor="#CCFFFF" pattern="^[A-Za-z0-9]+$" MaxLength="8" minlength="6" title="Please Enter The IPD Number"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshowip" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowip_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By Bed Number:</td>
    <td style="width:30%; text-align: left;" align="right">
         &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" style="width:130px">
        </asp:DropDownList>
        <asp:Button ID="btnshowbed" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowbed_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:40%" align="left">
         &nbsp;<asp:Button ID="Button2" runat="server" BackColor="#F8C471" Text="Show All" Width="70px" OnClick="btnshowall_Click" /></td>
         </td>
    <td style="width:10%" align="right">
    <td style="width:10%" align="center"></td>
</tr>
</table>
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" pagesize="10" DataKeyNames="VN" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="GridView2_PageIndexChanging" OnSelectedIndexChanging="GridView2_SelectedIndexChanging">
            <Columns>                               
                  <asp:BoundField DataField="VN" HeaderText="IPNO" />
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
              <asp:BoundField DataField="PNAME" HeaderText="NAME" />
                <asp:BoundField DataField="PHONE" HeaderText="PHONE" />
                <asp:BoundField DataField="ECONTACT" HeaderText="ECONTACT" />
                <asp:BoundField DataField="DISEASE" HeaderText="DISEASE" />
                <asp:BoundField DataField="DATE" HeaderText="ADMISSION DATE" DataFormatString="{0:dd/MM/yyyy}"/>
                <asp:CommandField  ShowSelectButton="true" ShowDeleteButton="false" HeaderText="ACTION"/>
                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
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
        </div>
       
        </div>
              </ContentTemplate></asp:UpdatePanel>
</asp:Content>

