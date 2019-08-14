<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_Branch_Master.aspx.cs" Inherits="ADMIN_admin_Branch_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Branch Master
                </div>
                  
            </div>

            <div class="layout-main-content">
                
                <div class="ui-fluid" ><strong/>
                    <input type="hidden" name="j_idt79" value="j_idt79" />
                <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" ForeColor="#0066FF" Style="font-weight: 700" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    
                             <div class="card card-w-title"><br/>
            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                        



                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Branch Name </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" pattern="^[A-Za-z -]+$"></asp:TextBox>
                         </div>
                        </div>
                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Branch User Code </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtbranchusercode" runat="server" MaxLength="3" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" ></asp:TextBox>
                         </div>
                        </div>
                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="Label1" class="ui-outputlabel ui-widget" for="email"><asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Phone 	</label>
                                            
                                            
                                            
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtphone" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="11" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                         </div>
                        </div>
                     
                                            
                                            
                                             
                                           
                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="Label2" class="ui-outputlabel ui-widget" for="email"><asp:Label ID="Label71" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> GST IN 	</label>
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtgst" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-zA-Z0-9]+"></asp:TextBox>
                                            
                         </div>
                        </div>
                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="Label3" class="ui-outputlabel ui-widget" for="email"><asp:Label ID="Label10" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> State Code </label>
                                            
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtstatecode" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="2" CssClass="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                           
                         </div>
                        </div>
                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="j_idt114" class="ui-outputlabel ui-widget" for="message"><asp:Label ID="Label61" runat="server" Text="*" ForeColor="#CC0000"></asp:Label> Address 	</label>
                                            
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtaddress" runat="server" CssClass="ui-inputfield ui-inputtextarea ui-widget ui-state-default ui-corner-all" 
                                                 Enabled="true" TextMode="MultiLine" Height="65px" width="170px"></asp:TextBox>
                         </div>
                        </div>

                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="Label5" class="ui-outputlabel ui-widget" for="email"> REGNO 	</label>
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtregdnumber" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                                Enabled="true" ></asp:TextBox>
                         </div>
                        </div>

                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="Label6" class="ui-outputlabel ui-widget" for="email"> Email 	</label>
                                            
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtmailid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                      type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" Enabled="true" ></asp:TextBox>
                         </div>
                        </div>
                     <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label id="Label7" class="ui-outputlabel ui-widget" for="email"> Mobile No 	</label>
                                           
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtmobilenumber" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="11" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                                Enabled="true" ></asp:TextBox>
                         </div>
                        </div>
                    
                     <br />
                     <div class="ui-grid-row">
                        
                          <div class="ui-panelgrid-cell ui-grid-col-6">
                              <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="create" Visible="false" OnClick="btnupdate_Click" style="margin-left:5%;" />
                                           <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" />
                              &nbsp;&nbsp;
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"  /></div>
                          </div>
                          <h1>&nbsp;</h1>
                            <div id="Div2" class="ui-datatable ui-widget ui-datatable-reflow">
                         
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Branch Master
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                    
                                              <asp:GridView ID="grdvwbranch" runat="server" AutoGenerateColumns="False" DataKeyNames="BRANCH_ID" OnRowDataBound="grdvwbranch_RowDataBound" Width="1090"
                                                    OnRowDeleting="grdvwbranch_RowDeleting" AllowPaging="True" AllowSorting="True" 
                                                    OnSelectedIndexChanging="grdvwbranch_SelectedIndexChanging" OnPageIndexChanging="grdvwbranch_PageIndexChanging"
                                                   CssClass="table table-bordered" OnSorting="grdvwbranch_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                   
                                                  <AlternatingRowStyle BackColor="White" />
                                                   
                                                  <Columns> 
                                                      <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch Name" SortExpression="BRANCH_NAME"  HeaderStyle-CssClass="text-center">                                
                                                      <HeaderStyle CssClass="text-center" />
                                                      </asp:BoundField>
                <asp:BoundField DataField="BRANCH_USER_CODE" HeaderText="Branch UserCode" SortExpression="BRANCH_USER_CODE" HeaderStyle-CssClass="text-center" >                                            
                                                      <HeaderStyle CssClass="text-center" />
                                                      </asp:BoundField>
                <asp:BoundField DataField="BRANCH_PHONE" HeaderText="Phone No." HeaderStyle-CssClass="text-center" >
                                                      <HeaderStyle CssClass="text-center" />
                                                      </asp:BoundField>
                <asp:BoundField DataField="BRANCH_ADRRES" HeaderText="Address" HeaderStyle-CssClass="text-center" >                                            
                                                      <HeaderStyle CssClass="text-center" />
                                                      </asp:BoundField>
                <asp:BoundField DataField="BRANCH_REGNO" HeaderText="Regd. No."  HeaderStyle-CssClass="text-center">                                
                                                      <HeaderStyle CssClass="text-center" />
                                                      </asp:BoundField>
                <%--<asp:BoundField DataField="BRANCH_EMAIL" HeaderText="Email-Id" HeaderStyle-CssClass="text-center"   ItemStyle-wrap="true" />--%>                                            
                
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center">
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
                               

</div>
                         </div>
                        </div>

                       
                                 
                                  
                                    </div>
                                </div>
                        
              
            </strong>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

