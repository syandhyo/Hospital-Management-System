<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/MasterPage.master" AutoEventWireup="true" CodeFile="TestwiseConspRado.aspx.cs" Inherits="RADIOLOGY_TestwiseConspRado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
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
                          $("[id$=txtmaterialnm]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/RADIOLOGY/TestwiseConspRado.aspx/GetCustomers") %>',
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
    <td style="width:20%" align="center"> <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
       
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
    
   
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;text-decoration:underline;" align="right" class="auto-style1"><strong>Test Wise Consumption&nbsp;</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
         <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
   <br />
  <div>
  <table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td style="width:70%" align="center">

      <div id="div3" runat="server" style="border: thin solid #000000;">
        <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"><%--<asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>Date :</td>
    <td style="width:20%" align="left">
       <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><%--<asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>Radiology Id  :</td>
    <td style="width:20%" align="left">
       <asp:TextBox ID="txtAutoid" runat="server" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
          <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Radiology Test Name:</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droptesname" runat="server" style="width:130px"></asp:DropDownList>
         </td>
    <td style="width:20%" align="right"><%--<asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>--%></td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
       
           <div id="div1" runat="server" style="border: thin solid #000000;">
              <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;text-decoration: underline;" align="right" class="auto-style1"><strong>Item Details</strong></td>
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
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Material Name :</td>
    <td style="width:20%" align="left">
       <asp:TextBox ID="txtmaterialnm" runat="server" pattern="^[A-Za-z -0-9]+$" MaxLength="50"></asp:TextBox>
        <asp:HiddenField ID="hfCustomerId" runat="server" />
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtquant" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="4"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"></td>
    <td style="width:20%" align="left">
         
         </td>
    <td style="width:20%" align="right"><%--<asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label> --%></td>
    <td style="width:20%" align="left">
        <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click"/>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
              
                <table width="100%">
     <tr>
    <td style="width:10%; text-align: right;" align="right"></td>
    <td style="width:10%" align="left">
       
         </td>
    <td style="width:60%" align="center">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" OnRowDeleting="GridView1_RowDeleting">
           <Columns>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Quantity" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblqty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>    
            <asp:CommandField ShowDeleteButton="True"  HeaderText="ACTION"/>
       <%-- <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" />
            </ItemTemplate>
        </asp:TemplateField>--%>

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
    <td style="width:10%" align="left">
       
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table>
               
                <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"></td>
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
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:40%; text-align: left;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm" Visible="False" OnClick="btnupdate_Click"/>
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning" Visible="False" OnClick="btndelete_Click"  OnClientClick="return confirm('Are you sure you want to delete this item?');" />
        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click"/>
         </td>
    <td style="width:10%" align="left">
        <asp:HiddenField ID="hdndel" runat="server" />
    </td>
    <td style="width:10%" align="center"></td>
</tr>
</table>
          
         </div>
                    
            
            <table width="100%">
     <tr>
    <td style="width:10%" align="right">   
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
           <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
    </td>
    <td style="width:70%" align="center"> 
       <asp:GridView ID="grvtestCons" runat="server" AutoGenerateColumns="False"  BackColor="White" DataKeyNames="ID"  BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" AllowPaging="True"  PageSize="10"  GridLines="Horizontal" OnPageIndexChanging="grvtestCons_PageIndexChanging" OnSelectedIndexChanging="grvtestCons_SelectedIndexChanging" OnRowDataBound="grvtestCons_RowDataBound" OnRowDeleting="grvtestCons_RowDeleting">
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="DATE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="TEST&nbsp;NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblcdnme" runat="server" Text='<%#Eval("RADTESTNAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" />

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
          <%-- <div id="div2" runat="server" style="border: thin solid #000000;width:auto;">

               </div>--%>
      </div>
    </td>
    <td style="width:10%" align="right"></td>
</tr>
</table>
    </div>  
       <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:70%" align="center"> 
       
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%; text-align: left;" align="right">
&nbsp;&nbsp; <asp:GridView ID="grvhidden" runat="server" Visible="False">
        </asp:GridView></td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
           </ContentTemplate>   </asp:UpdatePanel>
</asp:Content>

