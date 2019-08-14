<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_percentagemaster.aspx.cs" Inherits="ADMIN_admin_percentagemaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Percentage Entry 
                    </div>
  <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="lblfyear" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title"><br/>
        		<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----  Type-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Type :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="drptype" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" 
                              AutoPostBack="true"  OnSelectedIndexChanged="drptype_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                <asp:ListItem Value="1">Staff</asp:ListItem>
                                <asp:ListItem Value="2">Broker</asp:ListItem>

                            </asp:DropDownList>
                         </div>
                        </div>
                      <!----Select Recipint-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Recipient :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="droprecipient" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
             
                         </div>
                        </div>
                      <!---% In Pharmacy :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>% In Pharmacy :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdiscpharmacy" runat="server"  pattern="[0-9]+([,\.][0-9]+)?" MaxLength="11" 
        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" ></asp:TextBox>
               
                         </div>
                        </div>
                     <!---% In Lab :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>% In Lab :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdisclab" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  pattern="[0-9]+([,\.][0-9]+)?" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" ></asp:TextBox>
               
                         </div>
                        </div>
                     <!---% In Room Charge:-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>% In Room Charge :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtroomcharge" runat="server"   
        pattern="[0-9]+([,\.][0-9]+)?" MaxLength="2" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
        value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
               
                         </div>
                        </div>
                     <!---% In Radiology Charge :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>% In Radiology Charge :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtradiology" runat="server" pattern="[0-9]+([,\.][0-9]+)?"
         MaxLength="2" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
               
                         </div>
                        </div>
                     <!---% In Others :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>% In Others :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdiscothers" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" value="0.00" pattern="[0-9]+([,\.][0-9]+)?"
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
               
                         </div>
                        </div>
                     </div>
                    </div>
                
                    <br/>
                  <asp:Button ID="btncreate" runat="server" Text="Create" Class="create" OnClick="Button1_Click" style="margin-left:5%;" />
            &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
                  &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Percentage Master
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                    
                                              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" Width="1060"
                                                    OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" AllowSorting="True" 
                                                    OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                   CssClass="table table-bordered" OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                         <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                                                    <ItemTemplate>
                                                      <%#Container.DisplayIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                        <asp:BoundField Datafield="Etype" HeaderText="Type" SortExpression="Etype" HeaderStyle-CssClass="text-center" >
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                        <asp:BoundField DataField="Name" HeaderText="Recipient" HeaderStyle-CssClass="text-center" SortExpression="Name">
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                        <asp:BoundField DataField="PHARMACY" HeaderText="% In Pharmacy" HeaderStyle-CssClass="text-center">  
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                         <asp:BoundField DataField="LAB" HeaderText="% In LAB" HeaderStyle-CssClass="text-center"> 
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                         <asp:BoundField DataField="ROOMRENT" HeaderText="% In RoomRent" HeaderStyle-CssClass="text-center"> 
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                         <asp:BoundField DataField="OTHERS" HeaderText="% In Other" HeaderStyle-CssClass="text-center"> 
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                         <asp:BoundField DataField="RADIO" HeaderText="% In Radio" HeaderStyle-CssClass="text-center">                       
                                                         <HeaderStyle CssClass="text-center" />
                                                         </asp:BoundField>
                                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit"  
                                                             HeaderText="Action" HeaderStyle-CssClass="text-center">
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

