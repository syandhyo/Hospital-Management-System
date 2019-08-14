<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/MasterPage1.master" AutoEventWireup="true" CodeFile="Indent_pharmacy.aspx.cs" Inherits="LABORATORY_Indent_pharmacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
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
     <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"rel="Stylesheet" type="text/css" />
    
                  <script type="text/javascript">
                      Sys.Application.add_load(function () {
                          $("[id$=txtname]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({                                     
                                      url: '<%=ResolveUrl("~/LABORATORY/Indent_pharmacy.aspx/GetCustomers") %>',
                                      data: "{ 'prefix': '" + request.term + "'}",
                                      dataType: "json",
                                      type: "POST",
                                      contentType: "application/json; charset=utf-8",
                                      success: function (data) {
                                          response($.map(data.d, function (item) {
                                              return {
                                                  label: item.split('/ \s*/')[0]
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
   
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="" Visible="false"> </asp:Label>
       <asp:Label ID="lblEditgrd" Visible="false" runat="server" Text=""></asp:Label>
    </td>
</tr>
</table>
   
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; font-weight: 700; color: #000000;" align="center">Indent To Pharmacy</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    
   
     <div id="div1" runat="server" style="border: thin solid #000000">
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server" CssClass="" Visible="false"></asp:TextBox>
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>        
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Date of Indent : </td>
    <td style="width:20%" align="left">        
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="False"></asp:TextBox>
    </td>
    <td style="width:20%" align="right">Indent No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtindentno" runat="server" Text="" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right">&nbsp;:<asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        &nbsp;From Dept :&nbsp; </td>
    <td style="width:20%" align="left">
        
         <asp:DropDownList ID="dropdept" runat="server" style="width:130px">
         </asp:DropDownList>
                </td>
    <td align="right" class="auto-style3">IPNO. :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtipno" runat="server" Text="" pattern="^[a-zA-Z0-9_]*" MaxLength="8" autocomplete="off"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
     <tr>
    <td style="width:20%" align="right">Enter By :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtenterby" runat="server" Text="" pattern="^[A-Za-z -]+$" MaxLength="100" MinLength="3" autocomplete="off"></asp:TextBox>
         </td>
    <td align="right" class="auto-style3">&nbsp; </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Authorised By :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtauthorised" runat="server" Text="" pattern="^[A-Za-z -]+$" MaxLength="100" MinLength="3" autocomplete="off"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
     <div id="div2" runat="server" style="border: thin solid #000000">
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">
         Item Name :</td>
    <td style="width:20%" align="left">
      <asp:TextBox ID="txtname" runat="server"  Width="132px" placeholder="Item" ToolTip="Enter Item Here" pattern="^[a-zA-Z]*" MaxLength="100" MinLength="3" autocomplete="off"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        
      Unit :</td>
    <td style="width:20%" align="left">
         <%--<asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" ToolTip="Enter Unit Here" Width="132px"></asp:TextBox>--%>
       <asp:DropDownList ID="txtUnit" runat="server" CssClass="" Enabled="False">
              <asp:ListItem>PCS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>  
  <table width="100%">
     <tr>
    <td style="width:20%" align="right">Quantity :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtqty" runat="server" placeholder="Quantity" ToolTip="Enter Quantity Here" Width="132px" AutoPostBack="true" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="4" ></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       <asp:Button ID="btnAdd" runat="server"  OnClientClick="return Validate();" Text="ADD" OnClick="btnAdd_Click"  Width="66px" />
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
  
    <br />
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">
          <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grdMaterial_RowDeleting" >
              <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Sl No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Item&nbsp;Name">
            <ItemTemplate>
                 <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                  <asp:TemplateField HeaderText="Unit">
            <ItemTemplate>
                 <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                   <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                 <asp:Label ID="lbl_Qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>
            </Columns>
              <FooterStyle BackColor="#b70000" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#E3EAEB" />
            <EditRowStyle BackColor="#7C6F57" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#b70000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
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
         <br />
          <table width="100%">
     <tr>
    <td style="width:20%" align="left">
        <asp:Label ID="lblUpId" runat="server" Visible="false" Text="Label"></asp:Label>
    </td>
    <td style="text-align: center;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm"  OnClick="btncreate_Click" OnClientClick="return ValidateCret();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" Visible="False" OnClick="btnupdate_Click" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm"  OnClick="btncancel_Click"/>
    
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">
      </td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="center">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="INDENT_NO" PageSize="10"  OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" >
            <Columns> 
                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yy}" HeaderStyle-CssClass="text-center"/>                                
                <asp:BoundField DataField="INDENT_NO" HeaderText="INDENT_NO" HeaderStyle-CssClass="text-center"/>  
                <asp:BoundField DataField="IPNO" HeaderText="IPNO" HeaderStyle-CssClass="text-center"/>                                          
                <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" HeaderStyle-CssClass="text-center"/>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center"/>
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
   </ContentTemplate></asp:UpdatePanel>
</asp:Content>

