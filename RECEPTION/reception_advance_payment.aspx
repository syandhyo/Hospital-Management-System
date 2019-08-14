<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_advance_payment.aspx.cs" Inherits="RECEPTION_reception_advance_payment" %>

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
                    $("[id$=txtdate]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> Advance Payment
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
            <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>IPD Advance Payment</u></b></h1>
                
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                            <div id="div1" runat="server">
                            <div class="ui-grid-row">
                                <!----  Search By IPD NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    Search By IPD NO. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtspid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 

                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshow" runat="server" CssClass="search" Text="Show" OnClick="btnshow_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
                            
                            <div class="ui-grid-row">
                                <!---- Company:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        Search By Bed No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtbedno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshowph" runat="server" CssClass="search" Text="Show" OnClick="btnshowph_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   
                                </div>

                            </div>
                                </div>
                            <div id="div2" runat="server">
                            <div class="ui-grid-row">
                                <!---- Patient Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Patient Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server" Text="" ></asp:Label>
                                </div>
                                <!----Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Date :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- IPD No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        IPD No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblip" runat="server" Text="" CssClass="auto-style4"></asp:Label>
                                </div>
                                <!---- Total Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Total Amount :</label>
                           
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:Label ID="lbltotalamt" runat="server" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Bed No : ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Bed No : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblbed" runat="server" Text="" style="color: #000000"></asp:Label>
                                </div>
                                <!---- Advance Paid Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Advance Paid Amount : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpaidamt" runat="server" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Purchase unit----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Payment Mode :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="droppayment" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="droppayment_SelectedIndexChanged">
                                       <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Card</asp:ListItem>
                                        <asp:ListItem>DD</asp:ListItem>
                                        <asp:ListItem>Credit</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <!---- Due Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Due Amount :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblremainamt" runat="server" style="font-weight: 700; color: #CC0000; font-size: large" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Card No. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Card No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtcard" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="4" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox>
                                </div>
                                <!----  Enter Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                         Enter Amount :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtamount" runat="server" value="0" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" Type="number" min="0"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Employee :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Employee :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropemp" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                                        </div>
                                <!---- Expiry date----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                                <br />
                                <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="Button1_Click" Text="Submit" />
         &nbsp;<asp:Button ID="btnDELETE" runat="server" CssClass="search" OnClick="btndelete_Click" Text="Delete" Visible="False"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                                </div>
                            </div>
                   </div>
               
			  
				<br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
							 <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   OPD CONSULTANCY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  AllowPaging="True" AllowSorting="True" 
                                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" 
                                         CssClass="table table-bordered" >
                                        <Columns> 
                                            <asp:BoundField DataField="id" HeaderText="" />
                                            <asp:BoundField DataField="PID" HeaderText="OPDNO" />
                                            <asp:BoundField DataField="NAME" HeaderText="Name" />
                                            <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                                            <asp:BoundField DataField="Amount" HeaderText="Advance Amount" />
                                            <asp:BoundField DataField="ADate" HeaderText="Date" />
                                            <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" 
                                                HeaderText="Action" />
                                              <asp:TemplateField HeaderText="PRINT">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
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
                                </div><br/>	

                              
<br/>
<br/>
<br/>
</div>
        </div>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

