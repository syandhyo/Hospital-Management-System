<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Corpo_Services.aspx.cs" Inherits="ADMIN_Admin_Corpo_Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        maxDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }
            </script>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Ambulance <span>/ </span>Ambulance Entry
                </div>
               
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>

            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong />
                </div>
                <div class="card card-w-title">
                    <br>
                    <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                            <!----  Ambulance No. : ----->
                            <div class="ui-grid-row">
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Corporate Name.: </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropcorpo" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Style="width: 180px;" ></asp:DropDownList>

                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Service Name.: </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtsername" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" size="20" ></asp:TextBox>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Apply From: </label>
                                    </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtdate" runat="server" onkeydown="return false;" onpaste="return false;" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>

                                </div>
                                </div>
                            <div class="ui-grid-row">
                                 <!----  Service Type. : ----->
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Service Type : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="droptype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="droptype_SelectedIndexChanged" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Ward">Ward</asp:ListItem>
                                        <asp:ListItem Value="Cabin">Cabin</asp:ListItem>
                                        <asp:ListItem Value="Labrotory">Labrotory</asp:ListItem>
                                        <asp:ListItem Value="Radiology">Radiology</asp:ListItem>
                                        <asp:ListItem Value="Ambulance">Ambulance</asp:ListItem>
                                        <asp:ListItem Value="OT">OT</asp:ListItem>

                        </asp:DropDownList>

                                </div>
                               
                            </div>
                            <!-- Vehicle Name : -->
                              <hr />

                                <div class="ui-grid-row">
                                    <!----  Item Name  ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Procedure : </label>
                                    </div>
                                      <div class="ui-panelgrid-cell ui-grid-col-4">
                                       <asp:DropDownList ID="dropproc" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Style="width: 180px;" OnSelectedIndexChanged="dropproc_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                   
                                     <!---- Unit  ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Rate : </label>
                                    </div>
                                   <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtprice" runat="server" ToolTip="Enter Item Here" MaxLength="50" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                <div class="ui-grid-row">
                                    <!---- Quantity  ----->
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <label class="ui-outputlabel ui-widget">
                                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Discount Rate : </label>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-4">
                                        <asp:TextBox ID="txtdiscount" runat="server" ToolTip="Enter Quantity Here" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </div>
                                    <div class="ui-panelgrid-cell ui-grid-col-2">
                                        <asp:Button ID="btnAdd" runat="server" OnClientClick="return Validate();" CssClass="add" Text="ADD" OnClick="btnAdd_Click" />
                                    </div>

                                </div>

                        </div>
                    </div>

                    <br />
                        <table width="100%">
                            <tr>
                                <td style="width: 20%" align="right"></td>
                                <td style="width: 60%" align="center">
                                    <asp:GridView ID="grdMaterial" runat="server" Width="1060" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grdMaterial_RowDeleting" OnRowDataBound="grdMaterial_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item&nbsp;Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_type" runat="server" Text='<%#Eval("Type")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                            <asp:CommandField ShowDeleteButton="True" HeaderText="Action" HeaderStyle-HorizontalAlign="Left"/>
                                        </Columns>
                                        <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                                <td style="width: 20%" align="center"></td>
                            </tr>
                        </table>
                        <hr />
                        <br />

                 

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="create" OnClick="btnSubmit_Click" style="margin-left:5%;"/>
                    &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />
                    &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                    <br>
                    <br>
                    <div id="Div1" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="Div2" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive" >
                                 <div runat="server" id="show" visible="false">
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Total Price :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
                             </div>
                         </div>
                          <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 GST Amount:</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lblgstamt" runat="server" Text="0"></asp:Label>
                             </div>
                         </div>
                         <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                 <label class="ui-outputlabel ui-widget">
                                 Total Amount :</label>
                             </div>
                             <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:Label ID="lbltotalamt" runat="server" Text="0"></asp:Label>
                             </div>
                         </div>
                         </div>
                         
                        </div>
                    </div>
                      <br>
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        

                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Services
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" Width="1060"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True"
                                AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSorting="GridView1_Sorting" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="CNAME" HeaderText="Corporate Name" SortExpression="CNAME" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NAME" HeaderText="Service Name" SortExpression="NAME" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DATE" HeaderText="Apply From" SortExpression="Apply From" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PRICE" HeaderText="Price" SortExpression="Price" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                   <%-- <asp:CommandField ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="false" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:CommandField>--%>
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />

                    </div>
                </div>

            </div>
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

