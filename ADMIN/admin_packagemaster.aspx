<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_packagemaster.aspx.cs" Inherits="ADMIN_admin_packagemaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/</span>Laboratory<span>/ </span> </span>Package Entry
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
        </div>
        <div class="card card-w-title"><br/>
        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>

             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                     <div class="ui-grid-row">
                    <!---- Package Name-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Package Name : </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                    </div>
                  
                     </div>

                      <div class="ui-grid-row">
                    <!----  Select Test Type  -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Select Test Type  :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:DropDownList ID="droptesttype" runat="server" AutoPostBack="true" Width="180" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="droptesttype_SelectedIndexChanged" >
        </asp:DropDownList>
                    </div>
                  
                     </div>
                 
                 </div>
             </div>


            <table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td style="width:60%" align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" CellPadding="4" DataKeyNames="ID" GridLines="None" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
           <Columns>

               <asp:TemplateField HeaderText="TEST NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>' value="0.00" 
                     onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table>
            <br />
                <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:5%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
                   <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" Style="margin-left:5%;" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"   Visible="False" />
            &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                         <br><br><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Package Master
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                    
                                              <asp:GridView ID="grdPackage" runat="server" Width="1060" AutoGenerateColumns="False"  DataKeyNames="ID" AllowPaging="True" AllowSorting="True" OnSorting="GridView1_Sorting" EmptyDataText="No Data Is There" 
                                                  CssClass="table table-bordered" OnRowDataBound="grdPackage_RowDataBound" OnRowDeleting="grdPackage_RowDeleting" CellPadding="4" 
                                                  ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="grdPackage_SelectedIndexChanging" OnPageIndexChanging="grdPackage_PageIndexChanging">
                                                  <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SlNo">
                                                        <ItemTemplate>
                                                            <%#Container.DisplayIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:BoundField DataField="ID" HeaderText="Package&nbsp;Id" HeaderStyle-CssClass="text-center">
                                                    <HeaderStyle CssClass="text-center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PACKGNAME" HeaderText="Name" HeaderStyle-CssClass="text-center" SortExpression="PACKGNAME">
                                                    <HeaderStyle CssClass="text-center" />
                                                    </asp:BoundField>
                                                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="false" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
                                                    <HeaderStyle CssClass="text-center" />
                                                    </asp:CommandField>
                                                </Columns>

                                                  <EditRowStyle BackColor="#2461BF" />

                                                  <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                                  <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                                  <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
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

