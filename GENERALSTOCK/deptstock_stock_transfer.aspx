<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_stock_transfer.aspx.cs" Inherits="GENERALSTOCK_deptstock_stock_transfer" %>

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
                    SetDatePicker1();
                });

                   
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker1();
                        }
                    });
                };

                function SetDatePicker1() {
                    $("[id$=txtdate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        //  maxDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }
    </script>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
        <script type="text/javascript">
            Sys.Application.add_load(function () {
                $("[id$=txtitemname]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/GENERALSTOCK/deptstock_stock_transfer.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span>Stock Transfer
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#203a5a;"><b><center>	STOCK TRANSFER</center></b></h1>
					 <hr>
                  <h1 style="color:#0071bc;"><b><u>Return Details</u></b></h1>
				<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                    <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                        <div class="ui-grid-row">
                            <!----Transfer NO :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Transfer NO :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txttransferno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                            </div>
                            <!---- Date :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Date :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="ui-grid-row">
                            <!----From Dept :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>From Dept :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:DropDownList ID="dropfrdept" runat="server" AutoPostBack="true"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="dropfrdept_SelectedIndexChanged" Width="180"></asp:DropDownList>
                            </div>
                            <!----Transfer To :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Transfer To :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:DropDownList ID="droptransfer" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
                            </div>
                        </div>
				 <br><br>
				 <hr>
				  <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
			
				<div class="ui-grid-row">
                    <!----Select Item :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Item Name :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtitemname" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  OnTextChanged="txtitemname_TextChanged" pattern="^[a-zA-Z0-9-_]*" ></asp:TextBox>
        <asp:HiddenField ID="hfCustomerId" runat="server" />
                    </div>
                    <!----Quantity :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtqty" runat="server" AutoPostBack="True"  MaxLength="5" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtopening_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <!---- Unit : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        Unit :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtunit" runat="server" AutoPostBack="True" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtopening_TextChanged" pattern="^[A-Za-z -]+$" ></asp:TextBox>
                    </div>
                    <!---- Button :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <asp:Button ID="Button3" runat="server" OnClick="Button1_Click" Text="Add" CssClass="search"/>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    </div>
                </div>
                </div>
            </div>
            <asp:GridView ID="grvStTrans" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="grvStTrans_RowDataBound" OnRowDeleted="grvStTrans_RowDeleted" GridLines="None"  ShowFooter="True" style="text-align: right">
            <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                <asp:TemplateField HeaderText="Sno">
                    <ItemTemplate>
                        <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                        <%#Container.DataItemIndex+1 %>
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
               <br><br>
				<hr>
					<br>
                  <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Btncreate_Click" Text="Create" />
        <asp:Button ID="btnupdate" runat="server" CssClass="update" Text="Update" Visible="False" />
        <asp:Button ID="btndelete" runat="server" CssClass="create" Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                         <br><br><br>
						
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                              
<br>
<br>
<br>
</div>
        </div>
    </div>
              <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

