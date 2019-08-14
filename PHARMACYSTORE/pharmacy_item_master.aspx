<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="pharmacy_item_master.aspx.cs" Inherits="PHARMACYSTORE_pharmacy_item_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        
        .GridPager a
        {
            display:block;
            height:15px;
            width:15px;
            background-color:#3AC0F2;
            color:#fff;
            font-weight:bold;
            border:1px solid #3AC0F2;
            text-align:center;
            text-decoration:none;
        }
         .GridPager span
        {
            display:block;
            height:15px;
            width:15px;
            background-color:#fff;
            color:#3AC0F2;
            font-weight:bold;
            border:1px solid #3AC0F2;
            text-align:center;
            text-decoration:none;
            padding-left: 4px;     
            padding-right: 4px;   
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                //On Page Load.
                $(function () {
                    SetDatePicker();
                });

                $(function () {
                    SetDatePicker1();
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
                    $("[id$=txtexpirydate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }


                //On UpdatePanel Refresh.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker1();
                        }
                    });
                };
                function SetDatePicker1() {
                    $("[id$=txtmfgdate0]").datepicker({
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
                    <i class="fa fa-home"></i><span>/</span>Pharmacy Master<span>/</span>Item Master
                </div>
                
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>

            <div class="layout-main-content">


                <div class="ui-fluid">
                    <strong />
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                     <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
                </div>
                <div class="card card-w-title">
                    <br />
                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Search Item :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Search Item :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtsearchname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btn_search" runat="server" OnClick="Btnsearch_click" CssClass="search" Text="Search" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
                            <hr />
                            <!-- -->
                            <h1 style="margin-left: 1%;"><b>Item Master</b></h1>
                            <div class="ui-grid-row">
                                <!---- Company:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Company:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropcompany" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    </asp:DropDownList>
                                </div>
                                <!----  Item Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Item Name : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z0-9 -]+$" title="Please enter Alpha Or Numeric"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Hsn Code----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Hsn Code:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txthsncode" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                <!---- Tablet Per Strip(for tablets only) :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Tablet Per Strip<br />
                                        (for tablets only) :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txttabletperstrip" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="3"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Batch No.----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Batch No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtbatchno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                <!----  Location :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <strong><u>Location :</u></strong> :
                           
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Category----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Category</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropcate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True"
                                        OnSelectedIndexChanged="dropcate_SelectedIndexChanged" Style="width: 180px">
                                    </asp:DropDownList>
                                </div>
                                <!----  1.Shelf :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>1.Shelf : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtself" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="2"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Purchase unit----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label10" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Purchase Unit :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="droppurchaseunit" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true" Width="180" OnSelectedIndexChanged="droppurchaseunit_SelectedIndexChanged">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                        <asp:ListItem>PCS</asp:ListItem>
                                        <asp:ListItem>Ltr</asp:ListItem>
                                        <asp:ListItem>ml</asp:ListItem>
                                        <asp:ListItem>Kg</asp:ListItem>
                                        <asp:ListItem>gram</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <!----  2.Rack----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label12" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>2.Rack : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtrack" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="5"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Sale Unit----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label13" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Sale Unit</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropsaleunit" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        Width="180" Enabled="false">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem>PCS</asp:ListItem>
                                        <asp:ListItem>Ltr</asp:ListItem>
                                        <asp:ListItem>ml</asp:ListItem>
                                        <asp:ListItem>Kg</asp:ListItem>
                                        <asp:ListItem>gram</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <!---- Reorder point----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label14" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Reorder Point : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtreorder" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="5" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                        value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Purchase price----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label15" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Purchase Price :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtpprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                <!---- Expiry date----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label16" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Expiry Date : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtexpirydate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- MRP(With out tax) :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label17" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>MRP(With out tax) :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtsprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="5"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                <!----Manufacturing Date----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label18" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Manufacturing Date : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtmfgdate0" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  GST----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label19" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>GST :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTGST" runat="server" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="5" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                                <!---- Opening Qty :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label20" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Opening Qty : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtopening" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="3" pattern="[0-9]+([,\.][0-9]+)?"
                                        value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>

                    <br />
                    <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" Style="margin-left: 5%;" />
                    &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" OnClick="Button2_Click" Visible="False" />
                    &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        
                        
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Item Master
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060px"
                                OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting"
                                AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" OnSorting="GridView1_Sorting"
                                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NAME" HeaderText="Name" HeaderStyle-CssClass="text-center" SortExpression="NAME">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CATEGORY" HeaderText="Category" HeaderStyle-CssClass="text-center" SortExpression="CATEGORY">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTY" HeaderText="Qty" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center" >
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:CommandField>
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

