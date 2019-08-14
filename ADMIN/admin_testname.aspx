<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_testname.aspx.cs" Inherits="ADMIN_admin_testname" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Laboratory <span> / </span> Test Name
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>

             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                   <div class="ui-grid-row">
                    <!----   *Select Category : -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Category : </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="Dropcategory" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" runat="server"  AutoPostBack="false">
                        </asp:DropDownList>
                    </div>
                  
                    </div>

                      <div class="ui-grid-row">
                    <!----  Test Name : -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Test Name :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtname" runat="server" AutoPostBack="false" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                  
                    </div>
                     <hr />
                  <div class="ui-grid-row">
                    <!----  Investigation Desired : -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Investigation Desired: </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtinv" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false"></asp:TextBox>
                    </div>
                        <!---- Unit-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-1">
                    <label class="ui-outputlabel ui-widget"> Unit :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <%--<asp:TextBox ID="txtunit" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>--%>
                        <asp:DropDownList ID="dropunit" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
                    </div>

                  </div>
                     <div class="ui-grid-row">
                    <!----  Reference Ranges  -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Reference Max: </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtref" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                    </div>
                        <!---- Unit-----> 
                         <div class="ui-panelgrid-cell ui-grid-col-1">
                             <label class="ui-outputlabel ui-widget"> Reference Min :</label>
                             </div>
                         <div class="ui-panelgrid-cell ui-grid-col-4">
                             <asp:TextBox ID="txtrefmin" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                             </div>
                   <%-- <div class="ui-panelgrid-cell ui-grid-col-1">
                    <label class="ui-outputlabel ui-widget"> Price :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txtprice" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  MaxLength="6"  pattern="[0-9]+([,\.][0-9]+)?" 
          value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox> 
                         
                    </div>--%>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                         <asp:Button ID="btnadd" runat="server" Text="Add" Width="80px" OnClick="btnadd_Click"  CssClass="add"/> 
                    </div>

                  </div>

                 
                 </div>
             </div>

 
             <asp:GridView ID="grvStudentDetails" runat="server" 
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" 
                GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" >
    <Columns>
       <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
         <asp:TemplateField HeaderText="Sno">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_slno" runat="server" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Investigation Desired">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Reference Max">
            <ItemTemplate>
                <asp:Label ID="lbl_reference" runat="server" Text='<%#Eval("REF")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
       
           <asp:TemplateField HeaderText="Reference Min">
            <ItemTemplate>
                <asp:Label ID="lbl_refmin" runat="server" Text='<%#Eval("REFMIN")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Unit">
            <ItemTemplate>
                <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Unitid" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_unitid" runat="server" Text='<%#Eval("UNITID")%>'></asp:Label>
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
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
                 <br />
                  <hr>
                  <br/>
            <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" Style="margin-left:5%;"/>
         &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
                 <%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:5%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
                   &nbsp;&nbsp;<%--<button id="Button2" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                         <br><br><br />
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
                                Test Name
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="1060" OnRowDataBound="GridView2_RowDataBound" 
                                  DataKeyNames="ID" OnRowDeleting="GridView2_RowDeleting" AllowPaging="True" AllowSorting="True" BackColor="White" BorderColor="#d6d7d9" 
                                   CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnSorting="GridView2_Sorting" EmptyDataText="No Record Is There"
                                   OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" SortExpression="CATEGORY" />
                                    <asp:BoundField DataField="NAME" HeaderText="TEST NAME" SortExpression="NAME" />
                                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="FALSE" SelectText="&nbsp;&nbsp;&nbsp; Edit" DeleteText="Hide" HeaderText="ACTION"/>
                                </Columns>
                                  <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                  <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                  <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                  <RowStyle BackColor="white" ForeColor="#003399" />
                                  <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Navy" />
                                  <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                  <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                  <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                  <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>
                                </div><br/>
                                <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
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

