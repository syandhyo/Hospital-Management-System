<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/MasterPage.master" AutoEventWireup="true" CodeFile="DeptMaterialMST.aspx.cs" Inherits="GENERALSTOCK_DeptMaterialMST" %>

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
                          $("[id$=txtName]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/GENERALSTOCK/DeptMaterialMST.aspx/GetCustomers") %>',
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
    <td style="width:60%; font-weight: 700;" align="center">Department Material Master</td>
   
    <td style="width:20%" align="center"></td>
</tr>
</table>
  <%--  <div id="div3" runat="server" style="border: thin solid #000000">
         
           
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>--%>    

    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%; font-weight: 700;" align="center">Department Material Master</td>
   
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
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Department :</td>
    <td style="width:20%" align="left">
         <asp:DropDownList ID="ddDeptment" runat="server" CssClass="" style="width:130px" TabIndex="1">
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Item Name :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtName" runat="server" OnTextChanged="txtName_TextChanged" TabIndex="2" AutoPostBack="true" pattern="^[A-Za-z -]+$"></asp:TextBox>
        <asp:HiddenField ID="hfCustomerId" runat="server" />
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtQuantity" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" TabIndex="3" ToolTip="Please Enter Number"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>Unit : </td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtUnit" runat="server" pattern="^[A-Za-z -]+$" MaxLength="6" TabIndex="4" ToolTip="Please Enter Character"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%; text-align: right;" align="right"><asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label> Type :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="DDType" runat="server" style="width:130px" TabIndex="5">
            <asp:ListItem>Select Type</asp:ListItem>
             <asp:ListItem>CONSUMABLE</asp:ListItem>
             <asp:ListItem>ASSETS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :
       </td>
    <td style="width:20%" align="left">
       <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" TabIndex="6" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
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
         <asp:Button ID="btncreate" runat="server" Text="Create"  CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click"   />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" />
    
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
   <%--  <div id="div2" runat="server" style="border: thin solid #000000">
       --%>
  
     
    <br />
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False"   AllowPaging="True" PageSize="10" AllowSorting="True" OnPageIndexChanging="grdMaterial_PageIndexChanging" OnSelectedIndexChanging="grdMaterial_SelectedIndexChanging"  CssClass="table table-bordered"  >
            <Columns>
              <%--  <asp:BoundField DataField="ID" HeaderText="Sl No" />--%>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Dept&nbsp;Name">
            <ItemTemplate>
                 <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>

                 <asp:TemplateField HeaderText="Material&nbsp;Name">
            <ItemTemplate>
                 <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                  <asp:TemplateField HeaderText="Qty">
            <ItemTemplate>
                 <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Eval("QUANTITY")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                   <asp:TemplateField HeaderText="Unit">
            <ItemTemplate>
                 <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                 <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("TYPE")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                 <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
               
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true"  HeaderText="Action"/>
                <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>--%>
               <%--  <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>--%>
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
        


    <%-- </div>--%>
   
    
    </div>
   </ContentTemplate></asp:UpdatePanel>

</asp:Content>

