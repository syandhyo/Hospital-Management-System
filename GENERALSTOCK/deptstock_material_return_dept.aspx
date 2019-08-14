<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_material_return_dept.aspx.cs" Inherits="GENERALSTOCK_deptstock_material_return_dept" %>

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
                       $("[id$=txtmrndate]").datepicker({
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
             
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span>Material Return Dept
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
              <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#203a5a;"><b><center>MATERIAL RETURN TO DEPT</center></b>
                         <h1></h1>
                         <hr>
                         <h1 style="color:#0071bc;"><b><u>Return Details</u></b></h1>
                         <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                 <div class="ui-grid-row">
                                     <!----MRN No : ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         MRN NO :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtmrnno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                     </div>
                                     <!---- MRN Date :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         MRN Date :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtmrndate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!----Return From Dept :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Return From Dept :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropreturnto" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                                     </div>
                                     <!----Received BY :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Received BY :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtrecivedby" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="50" MinLength="3" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                     </div>
                                 </div>
                                 <br>
                                 <br>
                                 <hr>
                                 <h1 style="color:#0071bc;"><b><u>Item Info</u></b></h1>
                                 <div class="ui-grid-row">
                                     <!----Select Item :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Item Name :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="txtitemname" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="txtitemname_SelectedIndexChanged">
                                         </asp:DropDownList>
                                     </div>
                                     <!----Unit :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Unit :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtunit" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false" placeholder="Unit" ToolTip="Enter Unit Here"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="ui-grid-row">
                                     <!---- Quantity : ----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                         Quantity :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtqty" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="6" pattern="[0-9]+([,\.][0-9]+)?" placeholder="Quantity" ToolTip="Enter Quantity Here"></asp:TextBox>
                                     </div>
                                     <!---- Button :----->
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <asp:Button ID="btnadd" runat="server" CssClass="search" OnClick="btnadd_Click" Text="ADD" />
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                     </div>
                                 </div>
                                 </hr>
                                 </br>
                                 </br>
                             </div>
                         </div>
                         <asp:GridView ID="grvStudentDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" ShowFooter="True" style="text-align: right">
                             <Columns>
                                 <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
                                 <asp:TemplateField HeaderText="Slno">
                                     <ItemTemplate>
                                         <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                                         <asp:Label ID="lbl_slno" runat="server"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ITENNAME">
                                     <ItemTemplate>
                                         <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                                         <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="QTY">
                                     <ItemTemplate>
                                         <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                                         <asp:Label ID="lbl_qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="UNIT">
                                     <ItemTemplate>
                                         <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                                         <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
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
                         <br>
                         <br>
                         <hr>
                         <br>
                         <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" />
                         <asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />
                         <asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
                         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br>
                         <br>
                         <br>
                         <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                             
                         </div>
                         <div class="ui-datatable-tablewrapper">
                             <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView2_PageIndexChanging" OnSelectedIndexChanging="GridView2_SelectedIndexChanging">
                                 <AlternatingRowStyle BackColor="White" />
                                 <Columns>
                                     <asp:BoundField DataField="DATE" HeaderText="DATE" />
                                     <asp:BoundField DataField="INVOICE" HeaderText="INVOICE" />
                                     <asp:BoundField DataField="PARTY" HeaderText="PARTY&nbsp;NAME" />
                                     <asp:CommandField HeaderText="ACTION" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
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
                             
                             <br></br>
                             </br>
                             </br>
                         </div>
                         <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
                             <AlternatingRowStyle BackColor="White" />
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
                         <br>
                         </br>
                         
                         </hr>
                     </h1>
        </div>
    </div>
               </strong>
              </ContentTemplate>
              </asp:UpdatePanel>
</asp:Content>

