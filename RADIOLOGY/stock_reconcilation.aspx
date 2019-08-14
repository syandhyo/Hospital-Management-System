<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/MasterPage.master" AutoEventWireup="true" CodeFile="stock_reconcilation.aspx.cs" Inherits="RADIOLOGY_stock_reconcilation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
            font-size:16px;
        }
        .auto-style2 {
            background-color: #CCCCCC;
            font-size:14px
        }
        
    </style>
    <script type="text/javascript">
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
        }
 </script>
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
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
   <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Stock Reconcilation</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <div style="border-style: solid; border-width: thin"><br />
    <table width="100%">
        <caption>
            <br />
            <tr>
                <td align="right" style="width:20%"></td>
                <td align="left" style="width:20%; text-align: right;"></td>
                <td align="right" class="auto-style2" style="width:20%; text-align: left;">Date :&nbsp;<asp:Label ID="lbldate" runat="server"></asp:Label>
                </td>
                <td align="right" style="width:20%"></td>
                <td align="right" style="width:20%"></td>
            </tr>
        </caption>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:20%; text-align: left;" align="right" class="auto-style2">Department:&nbsp;<asp:DropDownList ID="Ddldept" runat="server" AutoPostBack="false" ></asp:DropDownList>
      
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
        <%--<asp:TextBox ID="Txt_pre" runat="server" Visible="false"></asp:TextBox>--%>
        <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
         </td>
</tr>
</table>
        <br />
        </div>
    <div align="center" style="border-style: solid; border-width: thin; "><br />
       
        <table width="100%">
     <tr>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%; text-align: center;" align="right"> </td>
    <td style="width:20%; text-align:center;" class="auto-style2">Item Name:&nbsp;<asp:DropDownList ID="Ddlitem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddlitem_SelectedIndexChanged"></asp:DropDownList></td>
    <td style="width:20%; text-align: center;" align="left"></td>
    <td style="width:20%; text-align: center;" align="left"></td>
</tr>
</table>
             <table width="100%">
     <tr>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%; text-align: center;" class="auto-style2">Quantity:&nbsp;<asp:TextBox ID="txtquant" runat="server" OnTextChanged="txtquant_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" AutoPostBack="True" Width="68px"></asp:TextBox><asp:Label ID="lblquant" runat="server" ForeColor="Red" Text=""></asp:Label>
        
    </td>
    <td style="width:20%; text-align: center;" align="left"></td>
    <td style="width:20%; text-align: center;" align="left"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%; text-align: center;" align="right"></td>
    <td style="width:20%; text-align: center;" class="auto-style2">Reason:&nbsp;<asp:TextBox ID="txt_des" runat="server"></asp:TextBox></td>
    <td style="width:20%; text-align: center;" align="left"></td>
    <td style="width:20%; text-align: center;" align="left"></td>
</tr>
</table>
       
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right">
        
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
       

    </div>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:40%;text-align: center;" align="left">
         <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" OnClick="btnsubmit_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm" Visible="False" OnClick="btnupdate_Click"/>
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" Visible="false"/>
        &nbsp;<asp:Button ID="btncancel1" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel1_Click"/>
        &nbsp;<asp:Button ID="btndelete" runat="server" Text="delete" CssClass="btn btn-primary btn-sm" OnClick="btndelete_Click" OnClientClick="return DeleteItem()" Visible="false"/>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
        <caption>
            <br />
            <tr>
                <td align="right" style="width:20%"></td>
                <td align="left" style="width:20%"></td>
                <td align="center" style="width:20%">
                    
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="text-center" HeaderStyle-ForeColor="Maroon" HeaderText="ID" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE" HeaderStyle-CssClass="text-center" DataFormatString="{0:yyyy/MM/dd}" HeaderStyle-ForeColor="Maroon" HeaderText="DATE" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM" HeaderStyle-CssClass="text-center" HeaderStyle-ForeColor="Maroon" HeaderText="ITEM" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPTN" HeaderStyle-CssClass="text-center" HeaderStyle-ForeColor="Maroon" HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QUANTITY" HeaderStyle-CssClass="text-center" HeaderStyle-ForeColor="Maroon" HeaderText="QUANTITY" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField HeaderStyle-CssClass="text-center" HeaderText="Action" HeaderStyle-ForeColor="Maroon" ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="true" >
                                <HeaderStyle CssClass="text-center" />
                                </asp:CommandField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    
                </td>
                <td align="left" style="width:20%"></td>
                <td align="center" style="width:20%"></td>
            </tr>
        </caption>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
</div>
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>

