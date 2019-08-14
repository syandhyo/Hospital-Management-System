<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/MasterPage.master" AutoEventWireup="true" CodeFile="Vendor_Contract.aspx.cs" Inherits="STOREKEEPER_Vendor_Contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function Validate() {
           
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
                maxDate: '0',

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
                          $("[id$=txtQuotaion]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/STOREKEEPER/Vendor_Contract.aspx/GetCustomers") %>',
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
   
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; text-decoration: underline; font-weight: 700; color: #000000;" align="center">Vendor Contract</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
    
   
     <div id="div1" runat="server" style="border: thin solid #000000">
    <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right">
         <asp:TextBox ID="txtContrtno" runat="server" Visible="false"></asp:TextBox>
        <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>

        
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Contract Id :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtInvoice" runat="server" Enabled="false"  Text=""></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"> Vendor :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="ddVendor" runat="server" >
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
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
    <td style="width:20%" align="right"></td>
    <td style="width:60%; text-decoration: underline; font-weight: 700; color: #000000;" align="center">Quotation Details</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">
       
         </td>
    <td style="width:20%" align="left">
      <%--<asp:TextBox ID="txtMaterial" runat="server"  Width="132px" placeholder="Material" ToolTip="Enter Material Here" OnTextChanged="txtMaterial_TextChanged" AutoPostBack="true" ></asp:TextBox>--%>
       
         </td>
    <td style="width:20%" align="right">
        
    </td>
    <td style="width:20%" align="left">
        <%-- <asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" ToolTip="Enter Unit Here" Width="132px"></asp:TextBox>--%>
       
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>  
  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       <%-- <asp:TextBox ID="txtquantity" runat="server" placeholder="Quantity" ToolTip="Enter Quantity Here" Width="132px" AutoPostBack="true" OnTextChanged="txtquantity_TextChanged"></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <%--<asp:TextBox ID="txtPrice" runat="server" placeholder="Price" ToolTip="Enter Price Here" Width="132px" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged"></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       <%-- <asp:TextBox ID="txtTotamt" runat="server" placeholder="Total Amount" ToolTip="Enter Total Amount Here" Width="132px"></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
     
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:10%" align="left"></td>
    <td style="width:30%" align="center">Quotation No :
<asp:TextBox ID="txtQuotaion" runat="server"  Text="" ></asp:TextBox>
         <asp:HiddenField ID="hfCustomerId" runat="server" />
           <asp:button id="btnadd" runat="server"  onclientclick="return validate();" text="View" OnClick="btnadd_Click"  width="66px" />
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
  
         <div style="border: thin solid #000000">
                <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>Item Info</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
             <br />
               <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="grdVendor" runat="server" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
              <AlternatingRowStyle BackColor="White" />
            <Columns>
             
               <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                      <%#Container.DisplayIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkQuot" runat="server" Checked='<%# Eval("CHKSEL") %>' OnCheckedChanged="chkQuot_CheckedChanged"  AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HSNCODE">
                    <ItemTemplate>
                          <asp:Label ID="lbl_hsncode" runat="server" Text='<%#Eval("HSNCODE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="UNIT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_spec" runat="server"  TextMode="MultiLine" Text='<%#Eval("SPEC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>                      
                        <asp:TextBox  ID="txt_qty" runat="server" Width="50" AutoPostBack="true" Text='<%#Eval("QUANTITY")%>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="PRICE">
                    <ItemTemplate> 
                       <%-- <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                        <asp:TextBox ID="txt_price" runat="server" Width="50" Text='<%#Eval("PRICE")%>' AutoPostBack="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>           
              
            
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                      
                       <%-- <asp:TextBox ID="txt_Amt" runat="server"  Enabled="false" Width="50" Text="0" ></asp:TextBox>--%>
                          <asp:Label ID="lbl_Amt" runat="server" Text='<%#Eval("AMOUNT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST">
                    <ItemTemplate>
                      <%--  <asp:TextBox ID="txt_cgst" runat="server"  Width="50" Text="0" AutoPostBack="true"></asp:TextBox>--%>
                         <asp:Label ID="lbl_cgst" runat="server" Text='<%#Eval("CGST")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST">
                    <ItemTemplate>
                      <%--  <asp:TextBox ID="txt_sgst" Enabled="false" runat="server"  Width="50" Text="0" AutoPostBack="true" ></asp:TextBox>--%>
                         <asp:Label ID="lbl_sgst" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST">
                    <ItemTemplate>
                       <%-- <asp:TextBox ID="txt_igst" runat="server" Enabled="false" Width="50" Text="0" AutoPostBack="true"></asp:TextBox>--%>
                         <asp:Label ID="lbl_igst" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTAMT">
                    <ItemTemplate>
                      <%--  <asp:TextBox ID="txt_gstamt" runat="server"  Width="50" Text="0" Enabled="false" ></asp:TextBox>--%>
                         <asp:Label ID="lbl_gstamt" runat="server" Text='<%#Eval("GSTAMT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMT">
                    <ItemTemplate>
                     <%--   <asp:TextBox ID="txt_TotAmt" runat="server"  Width="50" Text="0" Enabled="false"></asp:TextBox>--%>
                         <asp:Label ID="lbl_totamt" runat="server" Text='<%#Eval("TOTAMT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                 
               
                <%--<asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>--%>
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
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Total Price :-</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
         </td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="center">
        &nbsp;</td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Total Gst Amount :-</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
         </td>
    <td style="width:20%; text-align: right;" align="left">Grand Total :-</td>
    <td style="width:20%; text-align: left;" align="center">
        <asp:Label ID="lblgrandtotal" runat="server" style="color: #D20000; font-weight: 700" Text="0"></asp:Label>
         </td>
</tr>
</table>
             </div>

          <table width="100%">
     <tr>
    <td style="width:20%" align="left">
        <asp:Label ID="lblUpId" runat="server" Visible="false" Text="Label"></asp:Label>
    </td>
    <td style="text-align: center;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click" OnClientClick="return ValidateCret();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" Visible="False" OnClick="btnupdate_Click"/>
        <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning" Visible="False"   OnClick="btndelete_Click"  OnClientClick="return confirm('Are you sure you want to delete this item?');"/>
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" />
    
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <br />
          <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="lblEditgrd" runat="server" Text="" Visible="false"></asp:Label>
      </td>
    <td style="width:60%" align="center">
         <asp:GridView ID="grdVendContrct" runat="server" AllowPaging="True" AllowSorting="True" PageSize="10" DataKeyNames="CONTRACTID" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanging="grdVendContrct_SelectedIndexChanging" OnPageIndexChanging="grdVendContrct_PageIndexChanging">
            <Columns>
                 <asp:BoundField DataField="DATE" HeaderText="DATE" />
                  <asp:BoundField DataField="CONTRACTID" HeaderText="CONTRACT&nbsp;NO" />
                <asp:BoundField DataField="NAME1" HeaderText="VENDOR&nbsp;NAME" />
                <asp:BoundField DataField="QUTIONNO" HeaderText="QUOTATION&nbsp;NO" />                
               
                
                <asp:CommandField HeaderText="ACTION" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
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
</table>
    </div>
   </ContentTemplate></asp:UpdatePanel>


</asp:Content>

