<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_dept_material_master.aspx.cs" Inherits="GENERALSTOCK_deptstock_dept_material_master" %>

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
                     $("[id$=txtName]").autocomplete({
                         source: function (request, response) {
                             $.ajax({
                                 url: '<%=ResolveUrl("~/GENERALSTOCK/deptstock_dept_material_master.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span> Dept Material Master
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid">
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
             <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Department Material Master</u></b></h1>
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
                                <!---- Item Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Item Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtName" runat="server" OnTextChanged="txtName_TextChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TabIndex="2" AutoPostBack="true" pattern="^[A-Za-z -]+$"></asp:TextBox>
        <asp:HiddenField ID="hfCustomerId" runat="server" />
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!---- Quantity :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtQuantity" runat="server" MaxLength="4" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" TabIndex="3" ToolTip="Please Enter Number"></asp:TextBox>
                                </div>
                                <!----Unit :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Unit :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtUnit" runat="server" pattern="^[A-Za-z -]+$" MaxLength="6" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TabIndex="4" ToolTip="Please Enter Character"></asp:TextBox>
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!----  Type :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label> Type :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="DDType" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" TabIndex="5">
                                        <asp:ListItem>Select Type</asp:ListItem>
                                         <asp:ListItem>CONSUMABLE</asp:ListItem>
                                         <asp:ListItem>ASSETS</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <!-------Date------->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TabIndex="6" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                  </div>
                 <br><br>
			
				<hr>
					<br>
                  <asp:Button ID="btncreate" runat="server" Text="Create"  CssClass="create" OnClick="btncreate_Click"   />
                &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" />
                         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                         <br><br><br>
						
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                           DEPT. MATERIAL MASTER
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False"   AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grdMaterial_PageIndexChanging" OnSelectedIndexChanging="grdMaterial_SelectedIndexChanging"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None"  >
                                        <AlternatingRowStyle BackColor="White" />
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
                     <HeaderStyle CssClass="text-center" />
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                 <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>
                     <HeaderStyle CssClass="text-center" />
        </asp:TemplateField>
               
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true"  HeaderText="Action"/>
                <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION"/>--%>
               <%--  <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>--%>
            </Columns>
             <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
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
            </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

