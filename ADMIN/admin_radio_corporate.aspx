<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_radio_corporate.aspx.cs" Inherits="ADMIN_admin_radio_corporate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function disableEnterKey(e) {
            var key;


            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13)
                return false;
            else
                return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Ward Master <span>/ </span>Bed Charges for Coprporate
                </div>
               
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>

            <div class="layout-main-content">


                <div class="ui-fluid">
                    <strong />
                </div>
                <div class="card card-w-title">
                    <br />
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>

                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                <div class="ui-grid-row">
                                      <!---- Test Name ----->
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="lbl_test" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Select Test Name : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="ddltestname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="ddltestname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>         
                            <!---- Apply From----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget">Apply from :</label>
                            </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="Txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;" AutoComplete="off" TabIndex="1"> </asp:TextBox>
                    </div>
                    </div>
                            <div class="ui-grid-row">
                                <!---- Select Corporate----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="lbl_corpo" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Select Corporate : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropcorp" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            

                            <div class="ui-grid-row">
                                <!----Price----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="lbl_price" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Price  :  </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtprice" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                      onkeydown="return false;" onpaste="return false;" AutoComplete="off" ></asp:TextBox>
                                </div>
                                 
                                <!----Price----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="lbl_corpprice" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Corporate Price  :  </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtcopro" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                        value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>

                            
                            </div>
                             
                        </div>
                    </div>
                    <br />
                    <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" Style="margin-left: 5%;" OnClick="btncreate_Click" />
                   <%-- &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" />--%>
                    <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:5%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
                   &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" />
                    <br />
                    <br /><br />
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                        
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Rates for Corporate
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RADC_ID" Width="1060"
                                AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" 
                                CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >

                                <AlternatingRowStyle BackColor="White" />

                                <Columns>
                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Name" SortExpression="Name" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_test" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Corporate Name" SortExpression="NAME" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_corpo" runat="server" Text='<%#Eval("CNAME")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>        
                                   
                                    <asp:TemplateField HeaderText="Corporate Price" SortExpression="Price" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_corpprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Date" SortExpression="PRICE" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Eval("DATE")%>' DataFormatString="{0:dd-MM-yyyy}"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>--%>
                                    <%--<asp:CommandField ShowDeleteButton="true" ShowEditButton="False" HeaderText="ACTION" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:CommandField>--%>
                                </Columns>
                                <%--<SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />--%>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <%--<SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />--%>
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
         <%--   </strong>--%>
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

