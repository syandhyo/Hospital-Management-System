<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="AddFreestock.aspx.cs" Inherits="PHARMACYSTORE_USER_AddFreestock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {


            $('.formdate').datepicker({
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",

            });
            $('.todate').datepicker({
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",
            });
        });
    </script>
     <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtname]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/PHARMACYSTORE/USER/AddFreestock.aspx/GetCustomers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #009F50;
        }
        .auto-style2 {
            text-decoration: underline;
            color: #000000;
        }
        .auto-style3 {
            color: #000000;
        }
        .auto-style4 {
            color: #0970C4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="background-color: #D8D8D8">
       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
         </td>
</tr>
</table><table width="100%">
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
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Add Free Stock</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:TextBox ID="txtcstatecode" runat="server" Visible="False">21</asp:TextBox>
         </td>
</tr>
</table>
        
        
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
         
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <div style="border: 2px solid #000000">
           
             <table width="100%" style="border: 1px solid #000000">
     <tr>
    <td style="width:20%" align="right" class="auto-style2"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style3"><strong>Item Info</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtcomp" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="True" OnTextChanged="txtname_TextChanged" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtinvdate" runat="server" CssClass="formdate"></asp:TextBox>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    
    <td style="width:20%; text-align: right;" align="left">Name :</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:TextBox ID="txtname" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="True" OnTextChanged="txtname_TextChanged"></asp:TextBox>
    </td>
    <td style="width:20%; text-align: right;" align="left">Hsn No :</td>
    <td style="width:20%; text-align: left;" align="center">
          <asp:TextBox ID="txthsncode" runat="server" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
         <td style="width:20%" align="right"></td>
</tr>
</table> <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">Batchno :</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:TextBox ID="txtbatchno" runat="server" CssClass="form-control input-sm m-bot15" Text="0" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: right;" align="left">Category :</td>
    <td style="width:20%; text-align: left;" align="center">
          <asp:DropDownList ID="dropcate" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False">
        </asp:DropDownList>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table>
         <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">&nbsp;Unit :</td>
    <td style="width:20%; text-align: left;" align="right">
          <asp:DropDownList ID="droppurchaseunit" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False">
              <asp:ListItem>PCS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: right;" align="left">Expiry Date :</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:TextBox ID="txtexpirydate" runat="server" CssClass="formdate"></asp:TextBox>
         </td>
          <td style="width:20%" align="right"></td>
</tr>
</table>
         
         <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">&nbsp; Qty :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtopening" runat="server" Text="0" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" AutoPostBack="True" OnTextChanged="txtopening_TextChanged"></asp:TextBox>
    </td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="center">
        &nbsp;</td>
          <td style="width:20%" align="right"></td>
</tr>
</table><table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
         &nbsp;</td>
    <td style="width:20%; text-align: center;" align="left">
               <asp:ImageButton ID="ImageButton1" runat="server" BorderColor="Black" Height="32px" ImageUrl="~/PHARMACYSTORE/USER/img/Add.png" Width="43px" OnClick="ImageButton1_Click" />
         </td>
    <td style="width:20%; text-align: left;" align="center">
         &nbsp;</td>
          <td style="width:20%" align="right"></td>
</tr>
</table><table width="100%">
     <tr>
  
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
         &nbsp;</td>
    
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="center">
         &nbsp;</td>
           <td style="width:20%" align="right"></td>
</tr>
</table>
         <table width="100%">
     <tr>
   
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right"> 
         &nbsp;</td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="center">
         &nbsp;</td>
          <td style="width:20%" align="right"></td>
</tr>
</table>
         <table width="100%">
     <tr>
  
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
         &nbsp;</td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="center">
         &nbsp;</td>
           <td style="width:20%; text-align: left;" align="right">
               &nbsp;</td>
</tr>
</table>
         <table width="100%">
     <tr>
   
    <td align="center">

        <asp:GridView ID="grvStudentDetails" runat="server" 
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" 
                GridLines="None"  style="text-align: right" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" >
    <Columns>
       <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
         <asp:TemplateField HeaderText="Sno">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_slno" runat="server" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="COMPANY">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_lblcompany" runat="server" Text='<%#Eval("COMPANY")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="NAME">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="HSNCODE">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_hsn" runat="server" Text='<%#Eval("HSNCODE")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="BATCHNO">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_batchno" runat="server" Text='<%#Eval("BATCHNO")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField  HeaderText="CATEGORY">
            <ItemTemplate>
                <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                 <asp:Label ID="lbl_category" runat="server" Text='<%#Eval("CATEGORY")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="EXP DATE">
            <ItemTemplate>
                <asp:Label ID="lbl_exp" runat="server" Text='<%#Eval("EXP")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="UNIT">
            <ItemTemplate>
                <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                 <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
         <asp:TemplateField HeaderText="QTY">
            <ItemTemplate>
                <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
        
           
        <asp:TemplateField>
           
            <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
                
                <%--<asp:Button ID="ButtonAdd" runat="server" 
                        Text="Add New Row" OnClick="ButtonAdd_Click" BackColor="SteelBlue" ForeColor="White"/>--%>
                 
            </FooterTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#E3EAEB" />
    <EditRowStyle BackColor="#7C6F57" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>
    </td>
   
       
          
</tr>
</table>


        </div>
        
        
        
       <table width="100%">
     <tr>
    <td style="width:30%" align="right">
&nbsp;&nbsp;&nbsp;</td>
    <td style="width:10%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left">&nbsp;</td>
    <td style="width:20%; text-align: right;" align="center">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left">&nbsp;</td>
    <td style="width:20%; text-align: right;" align="center">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    
    <td style="width:70%" align="center">

         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-sm"  OnClick="Btndelete_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />

    </td>
    <td style="width:10%" align="left">&nbsp;</td>
    <td style="width:20%; text-align: right;" align="center">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left">&nbsp;</td>
    <td style="width:20%; text-align: right;" align="center">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="right"></td>
    <td style="width:10%" align="left" class="auto-style3">&nbsp;</td>
    <td style="width:20%; text-align: right;" align="center">
        &nbsp;</td>
</tr>
</table>
        <div style="border: 1px solid #000000">
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; font-weight: 700;" align="center" class="auto-style4">Invoice History</td>
    <td style="width:20%" align="right"></td>
    
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                 <asp:BoundField DataField="ID" HeaderText="ID" />
                 
                <asp:CommandField  ShowSelectButton="true" />
            </Columns>
              <FooterStyle BackColor="White" ForeColor="#000066" />
              <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
              <RowStyle ForeColor="#000066" />
              <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F1F1F1" />
              <SortedAscendingHeaderStyle BackColor="#007DBB" />
              <SortedDescendingCellStyle BackColor="#CAC9C9" />
              <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right">
        <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
  
        </div>
</asp:Content>

