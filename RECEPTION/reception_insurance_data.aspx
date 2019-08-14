<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_insurance_data.aspx.cs" Inherits="RECEPTION_reception_insurance_data" %>

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
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> Insurance Data
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
            <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Insurance data</u></b></h1>
					
                
				   <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Select HEAD :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Select HEAD :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropinsurance" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        Width="180" OnSelectedIndexChanged="dropinsurance_SelectedIndexChanged" AutoPostBack="true">
                                     </asp:DropDownList> 
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="Button1" runat="server" Text="Show All" CssClass="search" OnClick="Button1_Click" />

                                </div>
                                

                            </div>
                            </div>
                       </div>
                  
				<h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
							 <div class="ui-datatable-tablewrapper">
                                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" width="1060" AllowPaging="True"  AllowSorting="True"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                     <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="WARD" HeaderText="WARD" />                
                                    <asp:BoundField DataField="BEDNO" HeaderText="BED_NO" />
                                     <asp:BoundField DataField="PNAME" HeaderText=" PATIENT_NAME" />    
                                    <asp:BoundField DataField="VN" HeaderText="IP_NO" />
                                    <asp:BoundField DataField="INSURANCENAME" HeaderText="INSURANCE_NAME" />
                                    
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
                                 
<br/>
<br/>
<br/>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
</div>
        </div>
    </div>
    </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

