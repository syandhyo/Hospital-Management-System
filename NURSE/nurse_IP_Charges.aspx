<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_IP_Charges.aspx.cs" Inherits="NURSE_nurse_IP_Charges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
        }

        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }

    </script>
    <script type="text/javascript">
        function openpopup() {
            window.open("nurse_IP_Provisional_bill.aspx")
        }
        function openpopup1() {
            window.open("nurse_Advance_Bill.aspx")
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
                    $("[id$=]").datepicker({
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
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/</span> Reception <span>/ </span>Patient Search
                </div>
               
            </div>

            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong />
                </div>
                <div class="card card-w-title">
                    <h1 style="color: #0071bc;"><b><u>
                        <center>IP Charges</center>
                    </u></b>
                        <h1></h1>
                        <br />
                        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                            <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <div class="ui-grid-row">
                                    <!----  Name :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Name :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                    </div>
                                    <!----  Date :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Date :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!---- Age :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Age :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblage" runat="server" Text=""></asp:Label>
                                    </div>
                                    <!---- IPNO.:----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            IPNO.:</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblip" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!----  Address :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Address :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lbladd" runat="server" Text=""></asp:Label>
                                    </div>
                                    <!---- Ward Name.----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Ward Name.</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblward" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!----  Gender : ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Gender :
                                        </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblgender" runat="server" Text=""></asp:Label>
                                    </div>
                                    <!----  Bed No.----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Bed No.:
                                        </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblbed" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!----  Admission Date:----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Admission Date:</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lbladdate" runat="server" Text=""></asp:Label>
                                    </div>
                                    <!----  Corporate----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Corporate:</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblcorp" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!---- Total Amount:----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Total Amount:</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lbltoamt" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <!--------->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                        </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="UHID" runat="server" Text="0" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtcorpo" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!---- Paid Amount:----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Paid Amount:</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblpaid" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <!---- ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                        </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lblcorpid" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!---- Due/Refundable Amount :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Due/Refundable Amount :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Label ID="lbldue" runat="server" Text="Label"></asp:Label>
                                    </div>
                                    <!----  Button----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <asp:Button ID="btnprov" runat="server" CssClass="search" OnClick="btnprov_Click" OnClientClick=" openpopup();" Text="Pro.Bill" />
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:Button ID="advncepay" runat="server" CssClass="search" OnClick="advncepay_Click" OnClientClick="openpopup1();" Text="Adv.Pay" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br>
                        <hr />
                        <br />
                        <h1 style="color: #0071bc;"><b><u>Charges</u></b></h1>
                        <br />
                        <div id="Div1" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                            <div id="Div2" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <div class="ui-grid-row">
                                    <!---- Charge Category :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            Charge Category :</label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:DropDownList ID="ddlcate" runat="server" AutoPostBack="true" Width="180" 
                                            class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="ddlcate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <!----  Date :----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                        </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 60%">
                                    <asp:GridView ID="Grvcharge" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" Checked="false"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CHARGE" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcharge" runat="server" Text='<%# Eval("Charge") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="QTY" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                     <asp:TextBox ID="txtqty" runat="server" Text='0' MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" AutoPostBack="true" OnTextChanged="txtqty_TextChanged" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PRICE" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text="0.00"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                                <td align="left" style="width: 20%"></td>
                                <td align="left" style="width: 20%">
                                    <asp:Label ID="lbltot" runat="server" Text="0.00" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 60%">
                                    <div id="upcahrge" visible="false">
                                        <asp:GridView ID="Grvchargeupdate" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow1" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkRow1_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CHARGE" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcharge1" runat="server" Text='<%# Eval("DSR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTY" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtqty1" runat="server" AutoPostBack="true" MaxLength="4" OnTextChanged="txtqty1_TextChanged" pattern="[0-9]+([,\.][0-9]+)?" Text='<%# Eval("quantity") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PRICE" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprice1" runat="server" Text='<%# Eval("initial_price") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TOTAL" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotal1" runat="server" Text='<%# Eval("PRICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                </td>
                                <td align="center" style="width: 20%"></td>
                                <td align="right" style="width: 20%"></td>
                            </tr>
                        </table>
                        <br />
                        <hr />
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="btnSubmit_Click" Text="Submit" />
                        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />
                        &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="search" OnClick="btndelete_Click" Text="Delete" Visible="False" />
                        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                        <br />
                        <br />
                        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <label id="j_idt79:j_idt131_reflowDD_label" class="ui-reflow-label" for="j_idt79:j_idt131_reflowDD">
                                Sort</label>
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                CHARGE HISTORY
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                        <asp:BoundField DataField="BDate" HeaderText="Date" />
                                        <asp:BoundField DataField="PID" HeaderText="PID" />
                                        <asp:BoundField DataField="NAME" HeaderText="Name" />
                                        <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                                        <asp:BoundField DataField="WARD" HeaderText="Ward Name" />
                                        <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                                        <%--<asp:CommandField HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" />--%>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </div>

                        </div>

                        <h1></h1>

                    </h1>
                </div>
            </div>
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

