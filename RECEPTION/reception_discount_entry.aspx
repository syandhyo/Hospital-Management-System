<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_discount_entry.aspx.cs" Inherits="RECEPTION_reception_discount_entry" %>

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
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> Discount Entry
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Discount Entry</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!---- DATE-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:Label ID="lbldate" runat="server"></asp:Label>
               
                         </div>
                        </div>
                      <!---------> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <asp:Label ID="lblmid" runat="server"></asp:Label>
                              <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
             
                         </div>
                        </div>
                     <!----Select Type :----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            Select Type :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="droptype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            <asp:ListItem>Medicine</asp:ListItem>
                            <asp:ListItem>Nonmedicine</asp:ListItem>
                        </asp:DropDownList>
               
                         </div>
                        </div>
                      <!----  NAME-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"></label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                         </div>
                        </div>
                     <!----Enter Bed No :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            Enter Bed No :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="TextBox1" runat="server"  autocomplete="off" pattern="[a-zA-Z0-9\s]*$" MaxLength="5" minLength="3" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
               
                         </div>
                       
                      <!---- BOTTON-----> 
                        
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <asp:Button ID="Button1" runat="server" CssClass="search" OnClick="Button1_Click" Text="Show" />
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                         </div>
                        </div>
                     <br />
                     <hr />
                     <br />
                     <!---- IPD No.-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    IPD NO :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:Label ID="lblipno" runat="server"></asp:Label>
                         </div>
                        </div>
                      <!----NAME -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">NAME :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:Label ID="lblname" runat="server"></asp:Label>
                         </div>
                        </div>
                     <!---- Total Amount-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Total Amount :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:Label ID="lbltotalamt" runat="server"></asp:Label>
                         </div>
                        </div>
                      <!----  Paid / Refundable :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             	<label class="ui-outputlabel ui-widget">Paid / Refundable :</label>
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:Label ID="lblbalance" runat="server"></asp:Label>
                         </div>
                        </div>
                     <!----Check Box /Discount For Dependancy -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-6">
                             Discount For Dependancy :
                               <asp:CheckBox ID="chkRelation" runat="server" OnCheckedChanged="chkRelation_CheckedChanged" AutoPostBack="true"/>
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         
                         </div>
                        </div>
                      <!---- -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                         </div>
                        </div>
                     <br />
                     <hr />
                     <br />
                     <!---- Enter Authority :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        Enter Authority :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                              
                         </div>
                        </div>
                      <!---- -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"></label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                       
                         </div>
                        </div>
                     <!---- Enter Amount-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            Enter Amount :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtamount" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                               pattern="[0-9]+([,\.][0-9]+)?" value="0" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" autocomplete="off"></asp:TextBox>
               
                         </div>
                        </div>
                      <!---- E-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"></label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                         </div>
                        </div>
                     </div>
                    </div>

                     <br />
                    <asp:Button ID="btnsave" runat="server" CssClass="create" OnClick="Btnsave_Click" Text="Save" style="margin-left:5%;" />
                     <asp:Button ID="btndelete" runat="server" CssClass="update" OnClick="btndelete_Click" Text="Delete"  Visible="false" style="margin-left:5%;"/>
                     <asp:Button ID="btrcancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel"  Visible="true" style="margin-left:1%;"/>
				            <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                          
							 <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                DISCOUNT ENTRY DETAILS
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" width="1060" DataKeyNames="ID" 
                                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging"   AllowPaging="True" AllowSorting="True"  
                                        CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                         <asp:BoundField DataField="NAME" HeaderText="NAME" />
                                        <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                                          <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                                        <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowSelectButton="true" HeaderText="ACTION"/>
                                    </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
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

