<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_Receive_Dept.aspx.cs" Inherits="GENERALSTOCK_deptstock_Receive_Dept" %>

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
              <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span>Stock Receive Department
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#203a5a;"><b><center>	STOCK RECEIVE DEPARTMENT</center></b></h1>
					 <hr>
                  
				<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                    <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                        <div class="ui-grid-row">
                            <!----Transfer No. :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Transfer No. :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:Label ID="lbltransferno"  runat="server" Text="label"></asp:Label>
                            </div>
                            <!---- Receive No :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="lblrecvno" runat="server" ForeColor="Red" Text="*"></asp:Label>Receive No :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtrecvno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="ui-grid-row">
                            <!----Transfer Date :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Transfer Date :</label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:Label ID="lbltransferdate"  runat="server" Text="" ></asp:Label>
                            </div>
                            <!----Receive Date  : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Receive Date  : </label>
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtrecvDate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </div>
                        </div>
				 
				<div class="ui-grid-row">
                    <!----From Department :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        From Department :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="dropfrdept" runat="server" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:DropDownList>
                    </div>
                    <!----Department To :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       Department To :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="droptodept" runat="server" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
                    </div>
                </div>
                
                </div>
            </div>
            <asp:GridView ID="grvStockRecv" runat="server" AutoGenerateColumns="False"   AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None"  >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
             
             
                <%--<asp:CommandField ShowDeleteButton="True" />--%>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item&nbsp;Name">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("ITEM_NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_qunty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
             <EditRowStyle BackColor="#7C6F57" />
             <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#E3EAEB" />
             <SelectedRowStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
             <SortedAscendingCellStyle BackColor="#F8FAFA" />
             <SortedAscendingHeaderStyle BackColor="#246B61" />
             <SortedDescendingCellStyle BackColor="#D4DFE1" />
             <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
               <br><br>
				<hr>
					<br>
                  <asp:Button ID="btncreate" runat="server" Text="Accept" CssClass="create" OnClick="btncreate_Click" OnClientClick="return Validate();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel"  />
                         <br><br><br>
						
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                              
<br>
<br>
<br>
</div>
        </div>
    </div>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

