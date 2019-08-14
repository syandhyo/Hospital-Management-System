<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="LabBillEntry.aspx.cs" Inherits="RECEPTION_LabBillEntry" %>

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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
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
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Lab Bill Entry</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">
        <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1">&nbsp;</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <div id="div3" runat="server" style="border: thin solid #000000">
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
    <td style="width:20%" align="right"> <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox></td>
    <td style="width:20%; text-align: right;" align="left">OPD No :</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:TextBox ID="txtopdno" runat="server" BackColor="#CCFFFF" OnTextChanged="txtopdno_TextChanged" AutoPostBack="true" pattern="[a-zA-Z0-9_]+" MaxLength="6" minLength="6" title="Please Enter A Valid OPD Number" > </asp:TextBox>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">NAME</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:TextBox ID="txtname" runat="server" BackColor="#CCFFFF" pattern="^[A-Za-z -]+$" MaxLength="6" MinLength="6"></asp:TextBox>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
          <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:CheckBox ID="chkCorporate" runat="server" OnCheckedChanged="chkCorporate_CheckedChanged" AutoPostBack="true"/></td>
    <td style="width:20%; text-align: left;" align="right">
        Select  Corporate
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%>
</div>
    <div id="div1" visible="false" runat="server" style="border: thin solid #000000">
          
   
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Corporate</td>
    <td style="width:20%; text-align: left;" align="right">
           <asp:DropDownList ID="dropCorprt" runat="server" AutoPostBack="true" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="dropCorprt_SelectedIndexChanged" >
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Emp Id</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:TextBox ID="txtEmpid" runat="server" BackColor="#CCFFFF" ></asp:TextBox>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
</div>


    <div id="divMain" runat="server" style="border: thin solid #000000">
           
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:CheckBox ID="chkPackage" runat="server" OnCheckedChanged="chkPackage_CheckedChanged" ValidationGroup="a"  AutoPostBack="true"/></td>
    <td style="width:40%; text-align: left;" align="right">
        Select Package &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dropPackage" Visible="false" runat="server" OnSelectedIndexChanged="dropPackage_SelectedIndexChanged" AutoPostBack="true" CssClass="btn btn-default dropdown-toggle" >
        </asp:DropDownList>
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:10%" align="center"></td>
</tr>
</table>

<%--</div>--%>


    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"> <asp:CheckBox ID="chkTestType" runat="server"  AutoPostBack="true" ValidationGroup="a"  OnCheckedChanged="chkTestType_CheckedChanged"/></td>
    <td style="width:40%; text-align: left;" align="right">
         Select Test Type&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dropTestype" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropTestype_SelectedIndexChanged" CssClass="btn btn-default dropdown-toggle" >
        </asp:DropDownList> 
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:10%" align="center"></td>
</tr>
</table>
       

 <div id="divinner1" visible="false" runat="server" style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td style="width:60%" align="center">       

      
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table>   
   
</div>

        <div id="divinner2" visible="false" runat="server" style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:60%" align="center"> 
       <asp:GridView ID="grdtestype" runat="server" AutoGenerateColumns="False"  BackColor="White" DataKeyNames="ID"  BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal">
           <Columns>
                <%--<asp:TemplateField Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lbltype" Visible="false" runat="server" Text='<%#Eval("TESTYPE")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>--%>
               <asp:TemplateField HeaderText="TEST&nbsp;NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>' ></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged"/>
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
    <td style="width:20%" align="right"></td>   
</tr>
</table>   
            <div id="div2"  runat="server" style="border: thin solid #000000">
              <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
        
         </td>
    <td style="width:20%; text-align: right;" align="left">Price :-</td>
    <td style="width:20%; text-align: left;" align="center">
       <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
        </td>
</tr>
</table>
   <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"> </td>
    <td style="width:20%; text-align: left;" align="right">
        
         </td>
    <td style="width:20%; text-align: right;" align="left">Total Discount :-</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:TextBox ID="txtdisc" runat="server" OnTextChanged="txtdisc_TextChanged" Width="60px" AutoPostBack="true" Text="0"></asp:TextBox>
        </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
        
         </td>
    <td style="width:20%; text-align: right;" align="left">Total Amount:-</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:Label ID="lbltotalamt" runat="server" style="color: #D20000; font-weight: 700" Text="0" ></asp:Label>
         </td>
</tr>
</table>
</div>

</div>     
   
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
        <br />
        <table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td style="width:60%" align="center">
        <asp:GridView ID="grdlabill" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnPageIndexChanging="grdlabill_PageIndexChanging" PageSize="10" AllowSorting="True" AllowPaging="true">
           <Columns>
               
                 <asp:TemplateField HeaderText="LAB&nbsp;ID" >
            <ItemTemplate>
                <asp:Label ID="lblnamId" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>
               <asp:TemplateField HeaderText="OPD&nbsp;NO" >
            <ItemTemplate>
                <asp:Label ID="lblopdno" runat="server" Text='<%#Eval("OPDNO")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="OPD&nbsp;NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="DATE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
               <%-- <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("DATE")%>' ></asp:TextBox>--%>
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

    <td style="width:20%" align="right"></td>
   
</tr>
</table>

</div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>

