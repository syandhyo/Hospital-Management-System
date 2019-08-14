<%@ Page Title="" Language="C#" MasterPageFile="~/OT/OT_MasterPage.master" AutoEventWireup="true" CodeFile="ot_patient_search.aspx.cs" Inherits="OT_ot_patient_search" %>

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
                        <i class="fa fa-home"></i><span>/ </span> OT <span> / </span> Patient Search
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title"><br>
        
                          <h1 style="color:#0071bc;"><b><center>PATIENT SEARCH</center></b></h1>
					
                  <hr/>
                    <div class="ui-panelgrid-cell ui-grid-col-6">
                <h1><b><span style="padding-left:10%;color:#0071bc;"><u>OPD Search</u></span></b></h1>
                    </div>
              <div class="ui-panelgrid-cell ui-grid-col-6">
                      <h1><b><span style="color:#0071bc;"><u>IPD Search</u></span></b></h1>
                        </div>
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Search By OPD No.:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label13" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Search By OPD No. : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtspid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    &nbsp;<%--<asp:Button ID="btnshow" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" />--%></div>
                                 <div class="ui-panelgrid-cell ui-grid-col-1">
                                     <asp:Button ID="btnshow" runat="server" CssClass="search" Text="Show" OnClick="btnshow_Click" />
                                  <%--<button id="Button3" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; background-color:#0071bc; border:none;"><span class="ui-button-text ui-c">Search</span></button>--%>
                                  </div>

                                <!----  Search By IPD NO.:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    Search By IPD NO.:

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtip" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-zA-Z0-9]+"></asp:TextBox>
                             &nbsp;<%--<asp:Button ID="btnshowip" runat="server" CssClass="search" Text="Show" Width="70px" OnClick="btnshowip_Click" />--%></div>
                                 <div class="ui-panelgrid-cell ui-grid-col-1">
                                  <asp:Button ID="btnshowip" runat="server" CssClass="search" Text="Show" OnClick="btnshowip_Click" />
                                  </div>

                            </div>
                            
                            <!-- -->
                            
                            <div class="ui-grid-row">
                                <!---- Search By Phone No:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Search By Phone No:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtsmobile" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10"  pattern="[789][0-9]{9}" ></asp:TextBox>
                                     &nbsp;
                                    
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-1">
                                  <asp:Button ID="btnshowph" runat="server" CssClass="search" Text="Show" OnClick="btnshowph_Click" />
                                  </div>


                                <!---- Search By Bed Number:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Search By Bed No: </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <%--<asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z0-9 -]+$" title="Please enter Alpha Or Numeric"></asp:TextBox>--%>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="180" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
        </asp:DropDownList>
      
                                </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-1">
                                  <asp:Button ID="btnshowbed" runat="server" CssClass="search" Text="Show" OnClick="btnshowbed_Click" />
                                  </div>

                            </div>

                          <div class="ui-grid-row" style="margin-top:2%;font-size:12px;">

                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" Width="50%" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="OPNO" HeaderStyle-CssClass="text-center">
<HeaderStyle CssClass="text-center"></HeaderStyle>
                 </asp:BoundField>
                <asp:BoundField DataField="PNAME" HeaderText="NAME" HeaderStyle-CssClass="text-center">
<HeaderStyle CssClass="text-center"></HeaderStyle>
                 </asp:BoundField>
                <asp:BoundField DataField="TELPHNO" HeaderText="PHONE" HeaderStyle-CssClass="text-center">                
<HeaderStyle CssClass="text-center"></HeaderStyle>
                 </asp:BoundField>
                <asp:BoundField DataField="MOBNO" HeaderText="ECONTACT" HeaderStyle-CssClass="text-center">
<HeaderStyle CssClass="text-center"></HeaderStyle>
                 </asp:BoundField>
                <%--<asp:BoundField DataField="DISEASE" HeaderText="DISEASE" />--%>
                <asp:BoundField DataField="DATETIME" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center">
<HeaderStyle CssClass="text-center"></HeaderStyle>
                 </asp:BoundField>
                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
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
                              <div class="ui-panelgrid-cell ui-grid-col-1" style="margin-left:25%; width:40%;">

                                  <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="VN"   AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                            <Columns>                               
                                <asp:BoundField DataField="VN" HeaderText="IPNO" />
                                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                                <asp:BoundField DataField="PNAME" HeaderText="NAME" />
                                <%--<asp:BoundField DataField="PHONE" HeaderText="PHONE" />
                                <asp:BoundField DataField="ECONTACT" HeaderText="ECONTACT" />--%>
                                <asp:BoundField DataField="DISEASE" HeaderText="DISEASE" />
                                <asp:BoundField DataField="DATE" HeaderText="ADMISSION DATE" DataFormatString="{0:dd/MM/yyyy}"/>
                                 
                                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
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

                  <hr/>
                  <br/>
            <br />
                
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                          
               </div>
        </div>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

