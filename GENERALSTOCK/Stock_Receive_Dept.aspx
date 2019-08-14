<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/MasterPage.master" AutoEventWireup="true" CodeFile="Stock_Receive_Dept.aspx.cs" Inherits="GENERALSTOCK_Stock_Receive_Dept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
    <td style="width:60%; font-weight: 700; color: #000000;" align="center"> Stock Receive Department</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <div id="div3" runat="server">
         
           
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
</table>
     <div id="div1" runat="server" style="border: thin solid #000000">
    <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">
        <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        Transfer No. :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lbltransferno"  runat="server" Text="label"></asp:Label>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="lblrecvno" runat="server" ForeColor="Red" Text="*"></asp:Label>Receive No :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtrecvno" runat="server" ReadOnly="true" ></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">Transfer Date :</td>
    <td style="width:20%" align="left">
       <asp:Label ID="lbltransferdate"  runat="server" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="right">Receive Date  : </td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtrecvDate" runat="server" CssClass="formdate"></asp:TextBox>
         </td>
    <td style="width:20%" align="center">  <asp:Label ID="lblGRNNoS" Visible="false" runat="server" Text="Label"></asp:Label></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">From Department :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropfrdept" runat="server" Enabled="false"></asp:DropDownList></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Department To :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droptodept" runat="server" Enabled="false"></asp:DropDownList></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <%--<table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">MR No :</td>
    <td style="width:20%" align="left">
       <asp:Label ID="lblMrno"  runat="server" Text="label"></asp:Label>
         </td>
    <td style="width:20%" align="right"> Department Name :</td>
    <td style="width:20%" align="left">
      <asp:DropDownList ID="ddDeptname" runat="server" Enabled="false">
             <asp:ListItem></asp:ListItem>
         </asp:DropDownList> 
         </td>
    <td style="width:20%" align="center"><asp:HiddenField ID="hdnIssuefrom" runat="server" /></td>
</tr>
</table>--%>
        
          
           <br />
    <table width="100%">
     <tr>
    <td align="center">
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
   
    <td style="width:60%" align="center">
        <asp:GridView ID="grvStockRecv" runat="server" AutoGenerateColumns="False"   AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None"  >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
             
             
                <%--<asp:CommandField ShowDeleteButton="True" />--%>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item&nbsp;Name">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("ITEM_NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_qunty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
             <EditRowStyle BackColor="#7C6F57" />
             <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#E3EAEB" />
             <SelectedRowStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
             <SortedAscendingCellStyle BackColor="#F8FAFA" />
             <SortedAscendingHeaderStyle BackColor="#246B61" />
             <SortedDescendingCellStyle BackColor="#D4DFE1" />
             <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
  <table width="100%">
     <tr>
    <td style="width:20%" align="left">
        <asp:Label ID="lblUpId" runat="server" Visible="false" Text="Label"></asp:Label>
    </td>
    <td style="text-align: center;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Accept" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click" OnClientClick="return Validate();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm"  />
    
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table> 
    </div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>

