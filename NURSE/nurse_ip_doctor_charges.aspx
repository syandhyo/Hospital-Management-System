<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_ip_doctor_charges.aspx.cs" Inherits="NURSE_nurse_ip_doctor_charges" %>

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
                 maxDate: '0',

                 yearRange: "c-75:c+10",
                 buttonImage: '../images/calendar.png'

             });
         }
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> IP Doctor Charges
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
        
              <h1 style="color:#0071bc;"><b><u>	Corporate Charges For Doctors</u></b></h1>
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  IPNO:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">IPNO :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtip" runat="server" pattern="[a-zA-Z0-9]+" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter valid IPNumber">

                                                                                  </asp:TextBox>
                                </div>

                                <!--------->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"></label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  BED :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                         BED :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:DropDownList ID="ddlbed" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" runat="server">
                        </asp:DropDownList>
                                </div>
                                <!---- Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshow" runat="server" CssClass="search" OnClick="btnshow_Click" Text="Show" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----DATE :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        DATE :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="Txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                                <!---- ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            <br />
                            <div id="div1" runat="server">
                            <br />
                            <hr />
                            <br />
                            
                            <div class="ui-grid-row">
                                <!---- IPNO----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        IPNO :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblip" runat="server"></asp:Label>
                                </div>
                                <!----  BED :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">
                                        BED :</label>
                           
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblbed" runat="server"></asp:Label>
                                    <asp:Label ID="lblcorpo" runat="server" Visible="false"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- name----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server"></asp:Label>
                                </div>
                                

                            </div>
                             <div class="ui-grid-row">
                                <!---- Doctor.----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Doctor :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="ddldoctor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddldoctor_SelectedIndexChanged">
                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!---- Price----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Price :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblprice" runat="server"></asp:Label>
                                </div>
                            </div>
                          </div>
                          </div>
                     </div>
             		 <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="btnSubmit_Click" Text="Save" />
                        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />
                        &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="btndelete_Click" OnClientClick="return  DeleteItem()" 
                            Text="Delete" Visible="False" />
                        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                 <br/><br/>
                  <hr/>
                  <br/><br/>
                
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              IP DOCTOR CHARGES
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView2" runat="server" Width="1060" EmptyDataText="No Record Is There" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView2_PageIndexChanging" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="text-center" HeaderText="ID" ItemStyle-HorizontalAlign="center" Visible="false" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IPNO" HeaderStyle-CssClass="text-center" HeaderText="IPNO" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bed" HeaderStyle-CssClass="text-center" HeaderText="BED" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Doctor" HeaderStyle-CssClass="text-center" HeaderText="DOCTOR" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="price" HeaderStyle-CssClass="text-center" HeaderText="PRICE" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="date" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" HeaderText="DATE" ItemStyle-HorizontalAlign="center" >
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
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="White" />
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

