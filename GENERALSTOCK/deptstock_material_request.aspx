<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_material_request.aspx.cs" Inherits="GENERALSTOCK_deptstock_material_request" %>

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
     <script type = "text/javascript">

         function SetTarget() {

             document.forms[0].target = "_blank";

         }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
             <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
            <script type="text/javascript">
                Sys.Application.add_load(function () {
                    $("[id$=txtMaterial]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("~/GENERALSTOCK/deptstock_material_request.aspx/GetCustomers") %>',
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
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span>Material Request
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
             <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Material Request</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                            <div class="ui-grid-row">
                                <!---- Department : ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Department :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="ddDeptment" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" TabIndex="1">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <!---- Invoice No. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Invoice No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtInvoice" runat="server" Enabled="false"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!---- Date : ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                    Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>
                              
                            </div>
                        <br><hr /><br>
				 <div class="ui-grid-row">
                        <!---- Item Name : ----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget">
                             <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Item Name :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtMaterial" runat="server" placeholder="Material" pattern="^[a-zA-Z0-9_]*" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Material Here" AutoPostBack="true" OnTextChanged="txtMaterial_TextChanged" ></asp:TextBox>
                        </div>
                        <!----Quantity :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtquantity" runat="server" placeholder="Quantity" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Quantity Here" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
                        </div>
                    </div>
                    <div class="ui-grid-row">
                        <!---- Unit : ----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget">
                            Unit :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Unit Here" pattern="^[A-Za-z0-9 -]+$" MaxLength="6"></asp:TextBox>
                        </div>
                        <!---- Button :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" OnClientClick="return Validate();" Text="ADD" CssClass="search" />
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            
                        </div>
                    </div>
                 </div>
            </div>
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
              <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
             <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
              <SortedAscendingCellStyle BackColor="#F5F7FB" />
              <SortedAscendingHeaderStyle BackColor="#6D95E1" />
              <SortedDescendingCellStyle BackColor="#E9EBEF" />
              <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
				<hr>
					<br>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click"  OnClientClick="return ValidateCret();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                         <br><br><br>
						
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
							MATERIAL REQUEST
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdShowAll" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grdShowAll_PageIndexChanging" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"/>
                <asp:BoundField DataField="INVNO" HeaderText="Invoice&nbsp;No." />
                <asp:BoundField DataField="DeptName" HeaderText="Department" />  
                
                <asp:CommandField HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
            </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                                </div><br/>
<br/>
<br/>
<br/>
</div>
        </div>
    </div>
            </strong>
            </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

