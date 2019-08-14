<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/MasterPage.master" AutoEventWireup="true" CodeFile="REQUIS_REPORT.aspx.cs" Inherits="RADIOLOGY_REQUIS_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <%--  <Triggers>
           <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                        
 </Triggers>--%>
          <ContentTemplate>
               <asp:Timer ID="Timer1" runat="server" Interval="2000" OnTick="Timer1_Tick"></asp:Timer>
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
                          $("[id$=txtpono]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/Purchase.aspx/GetCustomers") %>',
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
    <td style="width:20%" align="center"></td>
</tr>
</table>



       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td align="left" style="text-decoration:underline;"><strong>Radiology Requisition</strong></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
         </td>
</tr>
</table>
        <div style="border: thin solid #000000">
     
          <%--  <div style="border-style: none solid solid solid; border-width: thin; border-color: #000000;">--%>
          <%--  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Item Info</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td align="center">
        <asp:GridView ID="grvrequsion" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true" CellPadding="4" 
            DataKeyNames="ID" ShowFooter="true" style="text-align: right" BackColor="White" BorderColor="#3366CC" BorderStyle="None"
             BorderWidth="1px" OnSelectedIndexChanging="grvrequsion_SelectedIndexChanging" OnPageIndexChanging="grvrequsion_PageIndexChanging">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                <asp:TemplateField HeaderText="SlNO">
                    <ItemTemplate>
                       <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <%--<asp:TemplateField HeaderText="SELECT">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" Checked='<%#Eval("Isselected")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:BoundField DataField="ID" HeaderText="REQ&nbsp;ID" />
                 <asp:BoundField DataField="OPDNO" HeaderText="OPD&nbsp;NO" />
                 <asp:BoundField DataField="NAME" HeaderText="NAME" />
                 <asp:BoundField DataField="DATE" HeaderText="DATE" />
                
               <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Print" HeaderText="ACTION" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
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
</table>
             </div>
               <%-- </div>--%>
            <div>
           <%-- <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" Text="Create" />
        <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm"  Text="Update" Visible="False" />
        <asp:Button ID="btndelete" runat="server" CssClass="btn btn-warning btn-sm"  Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm"  Text="Cancel" />
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>--%>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td  align="center" class="auto-style1">
    
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>
       
    </div></ContentTemplate></asp:UpdatePanel>
</asp:Content>

