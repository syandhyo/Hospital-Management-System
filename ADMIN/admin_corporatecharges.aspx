<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_corporatecharges.aspx.cs" Inherits="ADMIN_admin_corporatecharges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
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

              <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
               <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>

      <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Corporate <span>/ </span>Corporate Charges 
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----  Corporate Name :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Corporate Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="ddlcorporate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcorporate_SelectedIndexChanged" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="184" style="margin-left:-30px;">
                        </asp:DropDownList>
                         </div>
                        </div>
                      <!----  Apply From: -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Apply From: </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="Txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;" AutoComplete="off" style="margin-left:-30px;"> </asp:TextBox>
                         </div>
                        </div>
                     </div>
                </div>
          
              <%--&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>Corporate Name :
					<select style="width:18%;height:28px; margin-left:1.6%;">
						<option value="">----Please Select----</option>
					  <option value="Corporate1">Corporate1</option>
					  <option value="Corporate2">Corporate2</option>
					  <option value="Corporate3">Corporate3</option>
					 
					</select>
            <asp:DropDownList ID="ddlcorporate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcorporate_SelectedIndexChanged" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                        </asp:DropDownList>
					<br><br>
		
                   &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>Apply From: 
                 
                  <span id="popup" class="ui-calendar">--%>
                   <%-- <input id="popup_input" name="popup_input" type="text" class="ui-inputfield ui-widget ui-state-default ui-corner-all" aria-labelledby="j_idt109" style="margin-left:4.5%;width:17.2%;" />
                      <asp:TextBox ID="Txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;" AutoComplete="off"> </asp:TextBox>
                  </span>--%>
                  <script id="popup_s" type="text/javascript">$(function () {
    PrimeFaces.cw("Calendar", "widget_popup", {
        id: "popup", popup: true, locale: "en_US", dateFormat: "m\/d\/y"
    }
                 );
}
                                                               );
                  </script>
              
            <%-------------------------------%>
    	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="ID" CellPadding="4" Width="1090" 
            ForeColor="#333333" GridLines="None" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CATEGORY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcata" runat="server" Text='<%#Eval("catagory")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CHARGE&nbsp;NAME" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcharge" runat="server" Text='<%#Eval("Charge")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="PRICE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprice" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CORPORATE&nbsp;PRICE" Visible="true">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtprice" runat="server" onblur="if(this.value==''){this.value='0.00'}" onclick="if(this.value=='0.00'){this.value=''}" value="0.00">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
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
            <br>  
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" Width="1060" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CATEGORY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcata1" runat="server" Text='<%#Eval("catagory")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CHARGE&nbsp;NAME" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcharge1" runat="server" Text='<%#Eval("Charge")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="CORPORATE&nbsp;PRICE" Visible="true">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtprice1" runat="server" Text='<%#Eval("Price")%>' onblur="if(this.value==''){this.value='0.00'}" onclick="if(this.value=='0.00'){this.value=''}" value="0.00"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
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
          
                  <%-- <button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px;margin-left:8%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Submit</span>
                  </button>--%>
            <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="btnSubmit_Click" Text="Submit" style="margin-left:5%;" />
                        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />
             &nbsp;
            <asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="btndelete_Click" OnClientClick="return DeleteItem()" Text="Delete" Visible="False" />
                        &nbsp;<asp:Button ID="btncancel" runat="server"  OnClick="btncancel_Click" Text="Cancel" CssClass="cancel" />
                 <%-- <button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span>
                  </button>--%>
            
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                         
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    Corporate Charges
                                </div>
                                <div class="ui-datatable-tablewrapper">

                                	<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" OnSorting="GridView2_Sorting" AutoGenerateColumns="False" CellPadding="4" Width="1060" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView2_PageIndexChanging" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" pagesize="5" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="text-center" HeaderText="ID" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CName" HeaderStyle-CssClass="text-center" HeaderText="CORPORATE NAME" ItemStyle-HorizontalAlign="center" SortExpression="CName" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" HeaderText="DATE" ItemStyle-HorizontalAlign="center" SortExpression="Date" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ApplyDate" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" HeaderText="APPLY DATE" ItemStyle-HorizontalAlign="center" SortExpression="ApplyDate" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField HeaderStyle-CssClass="text-center" HeaderText="ACTION" ItemStyle-HorizontalAlign="center" ShowDeleteButton="false" ShowSelectButton="true" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
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
                         


</div>
        </div>
            </div>
                     </strong>
                     </ContentTemplate></asp:UpdatePanel>
</asp:Content>

