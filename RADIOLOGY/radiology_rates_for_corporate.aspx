<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="radiology_rates_for_corporate.aspx.cs" Inherits="RADIOLOGY_radiology_rates_for_corporate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
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
                       $("[id$=Txtdate]").datepicker({
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
    </script>
               <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Master <span> / </span> Rates For Corporate
                    </div>
    
                </div>

                <div class="layout-main-content">

        <div class="ui-fluid"><strong>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
             <asp:Label ID="lblsession" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
        
                <h1 style="color:#0071bc;"><b><u>Radiology Rate For Corporates</u></b></h1>
                
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Corporates :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Corporates :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Ddlincrnce" AutoPostBack="false" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Category :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Category :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Ddlcata" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="Ddlcata_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="ui-grid-row">
                                <!---- Apply From :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;&nbsp;Apply From :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="Txtdate" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" Enabled="false" autocomplete="off" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>
                            </div>
                            
                            </div>
                    </div>
                 <br>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="INVESTIGATIONS" Visible="true" >
                                    <ItemTemplate>
                                         <asp:Label ID="lblname" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRICE" Visible="true" >
                                    <ItemTemplate>
                                         <asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CORPORATE PRICE" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtprice" runat="server" Text="0.00" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
                                    </ItemTemplate>
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
            <asp:GridView ID="GridView3" runat="server" DataKeyNames="CID" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="INVESTIGATIONS" Visible="true" >
                                    <ItemTemplate>
                                         <asp:Label ID="lblnam" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRICE" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtpric" runat="server" Text='<%#Eval("PRICE")%>' pattern="[0-9]+([,\.][0-9]+)?" ></asp:TextBox>
                                    </ItemTemplate>
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
        
                  <hr>
                  <br>
                 <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="create" OnClick="btnsubmit_Click" style="margin-left:5px;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
        &nbsp;<asp:Button ID="btndelete" runat="server" Text="delete" CssClass="create" OnClientClick="return DeleteItem()" Visible="false" onclick="btndelete_Click"/>
                         <br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              INSURANCE HISTORY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	 <asp:GridView ID="GridView2" runat="server" Width="1060" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" AllowPaging="True" AllowSorting="True">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="ID&nbsp;&nbsp;" datafield="ID" HeaderStyle-ForeColor="white" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center">
                                <HeaderStyle CssClass="text-center" ForeColor="white" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="INSURANCE&nbsp;&nbsp;&nbsp;" datafield="CORPORATE" HeaderStyle-ForeColor="white" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center">
                                <HeaderStyle CssClass="text-center" ForeColor="white" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="CATEGORY&nbsp;&nbsp;&nbsp;" datafield="CATAGORY" HeaderStyle-ForeColor="white" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center">
                                <HeaderStyle CssClass="text-center" ForeColor="white" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="DATE&nbsp;&nbsp;&nbsp;" datafield="DATE" HeaderStyle-ForeColor="white" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle CssClass="text-center" ForeColor="white" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="APPLY FROM" datafield="APPLY_DATE" HeaderStyle-ForeColor="white" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle CssClass="text-center" ForeColor="white" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField HeaderStyle-CssClass="text-center" HeaderText="ACTION" HeaderStyle-ForeColor="white" ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="true" >
                                <HeaderStyle CssClass="text-center" ForeColor="white" />
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
                                </div>

</div>
        </div>
    </div>
               </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

