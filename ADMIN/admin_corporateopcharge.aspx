<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_corporateopcharge.aspx.cs" Inherits="ADMIN_admin_corporateopcharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Common <span> / </span> Corporate OP Charges  
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>

        </div>
        <div class="card card-w-title"><br/>
        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----Corporate  Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Corporate Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropCorport" Width="180" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" >           
                            </asp:DropDownList>
               
                         </div>
                        </div>
                      <!----  Procedure Name -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Procedure Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="dropproc" runat="server" Width="180" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">           
                         </asp:DropDownList>
             
                         </div>
                        </div>
                     <!----  Price -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Price :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?" value="0.00" 
             onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
             
                         </div>
                        </div>
                     </div>
                   <br><br>
             
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" style="margin-left:5%;"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="btnupdate_Click" Visible="False" />
                   &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   Corporate OP Charge 
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvrooment" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True" Width="1060" PageSize="5" OnSorting="grvrooment_Sorting"  GridLines="None" OnSelectedIndexChanging="grvrooment_SelectedIndexChanging" OnRowDataBound="grvrooment_RowDataBound" OnRowDeleting="grvrooment_RowDeleting" OnPageIndexChanging="grvrooment_PageIndexChanging" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="CORPRATE" Visible="true" SortExpression="CORPORATE" >
            <ItemTemplate>
                <asp:Label ID="lblCNAME" runat="server" Text='<%#Eval("CORPORATE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="PROCED&nbsp;NAME" Visible="true" SortExpression="PROCEDNAME" >
            <ItemTemplate>
                <asp:Label ID="lblpname" runat="server" Text='<%#Eval("PROCEDNAME")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                 <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" />

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
                    </strong>
                    </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

