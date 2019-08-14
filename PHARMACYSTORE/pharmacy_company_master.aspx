<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="pharmacy_company_master.aspx.cs" Inherits="PHARMACYSTORE_pharmacy_company_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                       <i class="fa fa-home"></i><span>/</span>Pharmacy Master<span>/</span>Company Master
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
         <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>

            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                  <!----  Search Company Name -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             
                           <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label11" runat="server"></asp:Label>Search Company Name :  </label>	
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtsearchname" runat="server" class="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$"></asp:TextBox>
                         
                         </div>
                         <div class="ui-panelgrid-cell ui-grid-col-2">
                            <asp:Button ID="btn_search" runat="server" OnClick="Btnsearch_click" Text="Search" class="search" style="margin-left:1%; border:none;" />
                        </div>
                       </div>
                     <!----  Company Name -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             
                           <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label1" runat="server"></asp:Label>Company Name : </label>	
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$"></asp:TextBox>
                         </div>
                        </div>
                </div>
              </div>

            <br>
                 
                
            <asp:Button ID="btncreate" runat="server" Text="Create" class="create" style="margin-left:8%;" OnClick="Button1_Click" />
        <asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:11%;" OnClick="Button2_Click" Visible="False" />
                   &nbsp;&nbsp;<%--<button id="Button2" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" class="cancel"  OnClick="Button3_Click" />
                         <br><br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <%--<label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            <select id="j_idt79:j_idt131_reflowDD" name="j_idt79:j_idt131_reflowDD" class="ui-reflow-dropdown ui-state-default" autocomplete="off">
                            	<option value="0_0">Id Ascending</option>
                                <option value="0_1">Id Descending</option>
                                <option value="1_0">Year Ascending</option>
                                <option value="1_1">Year Descending</option>
                                <option value="2_0">Brand Ascending</option>
                                <option value="2_1">Brand Descending</option>
                                <option value="3_0">Color Ascending</option>
                                <option value="3_1">Color Descending</option>
                              </select>--%>
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                               Company Master
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" 
                                        DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting" 
                                        AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" 
                                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
                                        EmptyDataText="No RECORDS" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                        <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                                        <ItemTemplate>
                                          <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            <asp:BoundField DataField="NAME" HeaderText="Company Name" HeaderStyle-CssClass="text-center" SortExpression="NAME">
                                            <HeaderStyle CssClass="text-center" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Action" ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderStyle-CssClass="text-center">
                                            <HeaderStyle CssClass="text-center" />
                                            </asp:CommandField>
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
                                
                                </div><br>
<br>
<br>
<br>
</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

