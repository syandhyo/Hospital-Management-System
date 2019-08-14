<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="pharmacy_sale_bill.aspx.cs" Inherits="PHARMACYSTORE_pharmacy_sale_bill" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <script type="text/javascript">
                    $(function () {
                        $("[id$=txtname]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/PHARMACYSTORE/pharmacy_sale_bill.aspx/GetCustomers") %>',
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
            <script type="text/javascript">

                function ValidtSw() {

                    if (document.getElementById("<%=txtinvoice.ClientID%>").value == "") {
                 alert("Invoice Field Is Required !");
                 document.getElementById("<%=txtinvoice.ClientID%>").focus();
                 return false;
             }
         }
         function ValidSerch() {
             if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                     alert("Name Field is Required !");
                     document.getElementById("<%=txtname.ClientID%>").focus();
                 return false;
             }
         }
    </script>
             <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Expiry Report
                    </div>
            </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Sale Bill</u></b></h1>
				<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                    <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                        <div class="ui-grid-row">
                            <!---- Invoice : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Invoice :
                                </label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtinvoice" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-1">
                                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="Show" CssClass="search" OnClientClick="return ValidtSw();"/>
                            </div>
                        </div>
                        <div class="ui-grid-row">
                            <!----Search By Patient Name : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Search By Patient Name :
                                </label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtname" runat="server" AutoPostBack="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </div>
                             <div class="ui-panelgrid-cell ui-grid-col-1">
                                <asp:HiddenField ID="hfCustomerId" runat="server" /> &nbsp;<asp:Button ID="Btnsearch" CssClass="search" runat="server" OnClick="Btnsearch_Click" Text="Search" OnClientClick="return ValidSerch();"/>
                            </div>
                        </div>
                        
                    </div>
				
		
                    <table width="100%" >
                        <tr>
                            <td style="width:20%" align="left"> <asp:Button ID="Button3" runat="server" OnClick="Button2_Click" Text="Print" Visible="false" /></td>
                        </tr>
                    </table>
                           <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="ID"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="NAME" />
                 <asp:BoundField DataField="DATE" HeaderText="DATE" />
                  <asp:BoundField DataField="ID" HeaderText="INVOICE_NO" />
                <asp:CommandField ShowDeleteButton="False" HeaderText="ACTION" ShowEditButton="False" ShowSelectButton="true" />
            </Columns>
        </asp:GridView>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
          <table width="100%" >
        <tr>
            <td style="padding-left:140px;"> <asp:Button ID="btnPrint" runat="server" Visible="false" BackColor="Yellow" Text="Print" OnClick="btnPrint_Click"  /></td>
        </tr>
    </table>
    <table width="100%" runat="server" id="tab1">
     <tr>
    <td style="text-align: center;" align="center">
       
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
       
         </td>
</tr>
</table>
            <table id="Table1" width="100%" runat="server" visible="false">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Select Patient Name :</td>
    <td style="width:15%; text-align: left;" align="right">
        <asp:DropDownList ID="droppartyname" runat="server">
        </asp:DropDownList>
&nbsp;<asp:Button ID="Btnpart" runat="server" OnClick="Butparty_Click" Text="Show" />
         </td>
    <td style="width:25%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>

        </div>
                    </div>
           </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

