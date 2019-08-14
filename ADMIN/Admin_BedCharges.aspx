<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_BedCharges.aspx.cs" Inherits="ADMIN_Admin_BedCharges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                 
                  //function SetDatePicker() {
                  //    $("[id$=Txtdate]").datepicker({
                  //        dateFormat: 'dd-mm-yy',
                  //        showOn: 'button',
                  //        buttonImageOnly: true,
                  //        dateFormat: 'dd-mm-yy',
                  //        changeMonth: true,
                  //        changeYear: true,
                  //        minDate: '0',

                  //        yearRange: "c-75:c+10",
                  //        buttonImage: '../images/calendar.png'

                  //    });
                  //}
                  function isNumber(evt) {
                      evt = (evt) ? evt : window.event;
                      var charCode = (evt.which) ? evt.which : evt.keyCode;
                      if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                          return false;
                      }
                      return true;
                  }
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Ward Master <span> / </span>Bed Charges
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
        	<div class="col-md-12">
                <div class="col-md-6">
               <asp:Label ID="id" runat="server" Text="Label" Visible="false"></asp:Label>
                     <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                     <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>

             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                    <div class="ui-grid-row">
                         <!----  Select Department -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Department :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" TabIndex="0" OnSelectedIndexChanged="dropdept_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget">Apply from :</label>
                            </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="Txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste="return false;" AutoComplete="off" TabIndex="1"> </asp:TextBox>
                    </div>
                    </div>
                     
                 <div class="ui-grid-row">
                      <!----  Select Ward -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Ward :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropward" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="True">
                        </asp:DropDownList>
                    </div>
                      <!----  Price -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label><label class="ui-outputlabel ui-widget">Price :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtprice"  runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="5" value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" onkeypress="return isNumber(event)" TabIndex="3"></asp:TextBox>
                    </div>
                 </div>
                 
                 </div>
                    </div>
                </div>
             <br />
                        <%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:9%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
                        <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" OnClientClick="return ValidateCret();" Style="margin-left: 5%;" tabindex="4"/>
                        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click" />
                        &nbsp;&nbsp;<%--<button id="Button2" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                        <br />

            <hr />
            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Daily Test Wise Consuption
                            </div>
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="gridwardprice" runat="server" AutoGenerateColumns="False" Width="1060" EmptyDataText="No Record Is There" OnSorting="gridwardprice_Sorting" DataKeyNames="SERIAL_NO" OnRowDataBound="gridwardprice_RowDataBound" OnRowDeleting="gridwardprice_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="gridwardprice_SelectedIndexChanging" OnPageIndexChanging="gridwardprice_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                            <ItemTemplate>
                                                <%#Container.DisplayIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="NAME" HeaderText="Ward Name" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DATE" HeaderText="Apply From" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRICE" HeaderText="Rate" HeaderStyle-CssClass="text-center" SortExpression="PROCE" >
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center" >
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
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
            </div>

                    </div>
            </ContentTemplate>
                        </asp:UpdatePanel>
</asp:Content>

