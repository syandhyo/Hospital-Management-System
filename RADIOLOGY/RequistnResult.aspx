<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/MasterPage.master" AutoEventWireup="true" CodeFile="RequistnResult.aspx.cs" Inherits="RADIOLOGY_RequistnResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
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
        <asp:Label ID="lblSesion" runat="server" Text="Label" Visible="false"></asp:Label> 
        <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
    </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;text-decoration: underline;" align="right" class="auto-style1"><strong>Requisition Result</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <div id="div3" runat="server" style="border: thin solid #000000">
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
   
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"> Select Package :- </td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:DropDownList ID="droppackge" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="droppackge_SelectedIndexChanged"></asp:DropDownList>
    <%--    <asp:DropDownList ID="dropPackg" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
         </td>
    <td style="width:30%" align="left">Requisition  No :-   <asp:TextBox ID="txtreqno" runat="server" Enabled="false"></asp:TextBox></td>
    <td style="width:10%" align="right">
     </td>
</tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Select Consult Radiology:- </td>
    <td style="width:20%; text-align: left;" align="right">
   <asp:DropDownList ID="dropconsltRadio"  runat="server"   AutoPostBack="true"  ></asp:DropDownList>
         </td>
    <td style="width:30%" align="left"> Select Radiographer:- 
        <asp:DropDownList ID="dropradiogrph"  runat="server"  AutoPostBack="true"  ></asp:DropDownList></td>
    <td style="width:10%" align="center"></td>
</tr>
</table>
          <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Select Package:- </td>
    <td style="width:20%; text-align: left;" align="right">
   <asp:DropDownList ID="dropPackg" runat="server" AutoPostBack="true"></asp:DropDownList>
         </td>
    <td style="width:30%" align="left"> 
      </td>
    <td style="width:10%" align="center"></td>
</tr>
</table>--%>
          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"> </td>
    <td style="width:20%; text-align: left;" align="right">
 
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">  <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox></td>
    <td style="width:20%;text-align: left;text-decoration: underline;" align="right">TECHNIQUE</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">
     
    </td>
    <td style="width:60%" align="center"> 
       <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" Width="990px" Height="150px" MaxLength="350"></asp:TextBox>
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table> 
         <%-------------------------%>
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">  <asp:TextBox ID="TextBox1" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox></td>
    <td style="width:20%;text-align: left;text-decoration: underline;" align="right">FINDING</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">
     
    </td>
    <td style="width:60%" align="center"> 
       <asp:TextBox ID="txtfinding" runat="server" TextMode="MultiLine" Width="990px" Height="150px" MaxLength="350"></asp:TextBox>
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table> 
         <%-------------------------------%>
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">  <asp:TextBox ID="TextBox3" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox></td>
    <td style="width:20%;text-align: left;text-decoration: underline;" align="right">IMPRESSION</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">
     
    </td>
    <td style="width:60%" align="center"> 
       <asp:TextBox ID="txtimpresion" runat="server" TextMode="MultiLine" Width="990px" Height="150px" MaxLength="350"></asp:TextBox>
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table> 
</div>

    <div id="divMain" runat="server" style="border: thin solid #000000">

          <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
      </td>
    <td style="width:40%; text-align: left;" align="right">       
        
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:10%" align="center"></td>
</tr>
</table>
<%--</div>--%>

        <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:70%" align="center"> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" vissible="false" BackColor="White" BorderColor="#336666" Visible="true" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
           <Columns>

                   <asp:BoundField DataField="NAME" HeaderText="INVESTIGATION" />
                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
        
            <%--<asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>--%>
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

    <td style="width:20%" align="right"></td>
   
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"   Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click"/>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>    
           
</div>       
     </div>

   </ContentTemplate>
     <triggers>
    <asp:asyncpostbacktrigger controlid="droppackge" eventname="SelectedIndexChanged" />   
    </triggers>
 </asp:UpdatePanel>
   
</asp:Content>

