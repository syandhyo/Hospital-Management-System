<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_suppliermaster.aspx.cs" Inherits="ADMIN_admin_suppliermaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/</span>Pharmacy Master<span>/</span>Supplier Master
                    </div>
  <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
        </div>
        <div class="card card-w-title"><br/>
        <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="lblfyear" runat="server" Visible="false"></asp:Label>

              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                       
                        <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                               </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                                </div>
                              <!----Select Category-----> 
                           <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget"><asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Category : </label>	
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="Dropcategory" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" runat="server"  AutoPostBack="false">
                              <asp:ListItem Value="Select">Select</asp:ListItem>
                              <asp:ListItem Value="Regular">Regular</asp:ListItem>
                              <asp:ListItem Value="One-time supplier">One-time supplier</asp:ListItem>
                              <asp:ListItem Value="Material Supplier">Material Supplier</asp:ListItem>
                              <asp:ListItem Value="Master Supplier">Master Supplier</asp:ListItem>
                           </asp:DropDownList>
                             </div>                            
                        </div>
                     <div class="ui-grid-row">
                         <!----Pharmaceutical License number----->
                           <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>License number :</label>
                          </div>
                         <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtlicno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="15" MinLength="15"></asp:TextBox>
                         </div>                       
                    <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>                         
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtcity" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>
                     </div>

                      <div class="ui-grid-row">
                          <!----  State-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>State : </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtstate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
              
                           </div>
                             <!----  PIN Code -----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>PIN Code : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtpin" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="6" pattern="[0-9]+([,\.][0-9]+)?" title="Please enter Numeric Values"></asp:TextBox>
                         </div>

                        </div>

                      <div class="ui-grid-row">
                          <!----  Contact No -----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Contact No : </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:TextBox ID="txtcontactno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MinLength="10" MaxLength="10" pattern="[789][0-9]{9}" title="Please enter Numeric Values"></asp:TextBox>
              
                           </div>
                             <!----  GST No  -----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>GST No :</label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtgstno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[^[A-Z0-9]{15}$]+" MaxLength="15" MinLength="15"></asp:TextBox>
                         </div>

                        </div>

                     <div class="ui-grid-row">
                         
                             <!----  Address  -----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Address :</label>
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Width="170" cols="20" rows="3" maxlength="2147483647" class="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                         </div>
                          <!----  State Code -----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>State Code :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:TextBox ID="txtstatecode" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="2" MinLength="2" pattern="[0-9]+"></asp:TextBox>
              
                           </div>
                        </div>
                     
                    </div>
                </div> 

            <asp:TextBox ID="txtopening" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Visible="False"></asp:TextBox>
         <br>
                 <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:11%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
            <asp:Button ID="btncreate" runat="server" Text="Create" class="create" style="margin-left:13.5%;" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:11%;"  OnClick="Button2_Click" Visible="False" />
                   &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" class="cancel"  OnClick="Button3_Click" />
                         <br><br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Supplier Master
                                </div>
                                <div class="ui-datatable-tablewrapper">
  <%--                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" Width="1060"
                                        OnRowDataBound="GridView1_RowDataBound" 
                                        OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" PageSize="5" AllowSorting="True" 
                                        OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" 
                                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                             <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME"/>
                                             <asp:BoundField DataField="CITY" HeaderText="CITY" />
                                              <asp:BoundField DataField="STATE" HeaderText="STATE" />
                                              <asp:BoundField DataField="PIN" HeaderText="PIN" />
                                              <%--<asp:BoundField DataField="CONTACT" HeaderText="CONTACT_NO" />--%>
                                            <%--  <asp:BoundField DataField="GST" HeaderText="GST" SortExpression="GST"/>
                                              
                                              <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                                              <asp:BoundField DataField="SUPCATEGORY" HeaderText="Category" />
                                              <asp:BoundField DataField="LICENCE" HeaderText="Licence Number" />

                                            <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>
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
                                    </asp:GridView>--%>
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060px" OnRowDataBound="GridView1_RowDataBound"
                                 DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting"
                                AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" OnSorting="GridView1_Sorting" PageSize="5" 
                                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"
                                  CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NAME" HeaderText="Name" HeaderStyle-CssClass="text-center" SortExpression="NAME">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CITY" HeaderText="CITY" HeaderStyle-CssClass="text-center" SortExpression="CITY">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="STATE" HeaderText="STATE" HeaderStyle-CssClass="text-center" SortExpression="STATE">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="PIN" HeaderText="PIN" HeaderStyle-CssClass="text-center" SortExpression="PIN">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="CONTACT" HeaderText="CONTACT" HeaderStyle-CssClass="text-center" SortExpression="CONTACT">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GST" HeaderText="GST" HeaderStyle-CssClass="text-center" SortExpression="GST">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" HeaderStyle-CssClass="text-center" SortExpression="ADDRESS">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SUPCATEGORY" HeaderText="SUPCATEGORY" HeaderStyle-CssClass="text-center" SortExpression="SUPCATEGORY">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="LICENCE" HeaderText="LICENCE" HeaderStyle-CssClass="text-center" SortExpression="LICENCE">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:CommandField>
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                               
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                                </div><br>
<br>
<br>
<br>
</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

