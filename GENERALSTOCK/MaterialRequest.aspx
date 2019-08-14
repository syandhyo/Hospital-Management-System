<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialRequest.aspx.cs" Inherits="GENERALSTOCK_MaterialRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function Validate() {

             if (document.getElementById("<%=txtMaterial.ClientID%>").value == "") {
                 alert("Material Field Is Required !");
                 document.getElementById("<%=txtMaterial.ClientID%>").focus();
                 return false;
             }

             if (document.getElementById("<%=txtquantity.ClientID%>").value == "") {
                 alert("Quantity Field Is Required !");
                 document.getElementById("<%=txtquantity.ClientID%>").focus();
                 return false;
             }
             if (document.getElementById("<%=txtUnit.ClientID%>").value == "") {
                 alert("Unit Field Is Required !");
                 document.getElementById("<%=txtUnit.ClientID%>").focus();
                 return false;
             }

         }
    </script>  
    <script type="text/javascript">

        function ValidateCret() {

            if (document.getElementById("<%=txtdate.ClientID%>").value == "") {
                 alert("Date Field Is Required !");
                 document.getElementById("<%=txtdate.ClientID%>").focus();
                 return false;
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
        //Sys.Application.add_load(function () {
        //    $('.formdate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",
        //    });
        //});  
        $(function () {
            SetDatePicker();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();
                }
            });
        };

        function SetDatePicker() {
            $("[id$=txtdate]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                //minDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: '../images/calendar.png'
            });
        }

    </script>
     <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"rel="Stylesheet" type="text/css" />
    
                  <script type="text/javascript">
                      Sys.Application.add_load(function () {
                          $("[id$=txtMaterial]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/GENERALSTOCK/MaterialRequest.aspx/GetCustomers") %>',
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
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
  <%-- ---------------------------------------------------------%>
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
    
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; font-weight: 700; color: #000000;" align="center">Material Request</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    
   
     <div id="div1" runat="server" style="border: thin solid #000000">
    <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">
        <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Department :</td>
    <td style="width:20%" align="left">
         <asp:DropDownList ID="ddDeptment" runat="server" CssClass="btn btn-default dropdown-toggle">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Invoice No. :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtInvoice" runat="server" Enabled="false"  Text=""></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
       </td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
     <div id="div2" runat="server" style="border: thin solid #000000">
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">
         <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Item Name :</td>
    <td style="width:20%" align="left">
      <asp:TextBox ID="txtMaterial" runat="server"  Width="132px" placeholder="Material" pattern="^[a-zA-Z0-9_]*" ToolTip="Enter Material Here" AutoPostBack="true" OnTextChanged="txtMaterial_TextChanged" ></asp:TextBox>
     
         </td>
    <td style="width:20%" align="right">        
         <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtquantity" runat="server" placeholder="Quantity" ToolTip="Enter Quantity Here" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" Width="132px"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>  
  <table width="100%">
     <tr>
    <td style="width:20%" align="right">Unit :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" ToolTip="Enter Unit Here" pattern="^[A-Za-z0-9 -]+$" Width="132px" MaxLength="6"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" OnClientClick="return Validate();" Text="ADD" Width="66px" />
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     
    <br />
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">
          <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False" AllowSorting="True" OnRowDeleting="grdMaterial_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" >
              <AlternatingRowStyle BackColor="White" />
            <Columns>
              <%--  <asp:BoundField DataField="ID" HeaderText="Sl No" />--%>
                <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>--%>
               <%--  <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Sl No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Material&nbsp;Name">
            <ItemTemplate>
                 <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                  <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                 <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                   <asp:TemplateField HeaderText="Unit">
            <ItemTemplate>
                 <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("PUNIT")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                 
               
                <asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>
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
         <br />
          <table width="100%">
     <tr>
    <td style="width:20%" align="left">
        <asp:Label ID="lblUpId" runat="server" Visible="false" Text="Label"></asp:Label>
    </td>
    <td style="text-align: center;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click"  OnClientClick="return ValidateCret();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" />
    
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>


     </div>
   
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
    <td align="center">

        <asp:GridView ID="grdShowAll" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grdShowAll_PageIndexChanging" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"/>
                <asp:BoundField DataField="INVNO" HeaderText="Invoice&nbsp;No." />
                <asp:BoundField DataField="DeptName" HeaderText="Department" />  
                
                <asp:CommandField HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
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
</table>
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
</table>
    </div>
   </ContentTemplate></asp:UpdatePanel>

</asp:Content>

