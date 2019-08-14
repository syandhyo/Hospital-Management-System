<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_roomrateentry.aspx.cs" Inherits="ADMIN_admin_roomrateentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Common <span> / </span> Room Rate Entry 
                    </div>
    <ul class="route-bar-menu">
        <li class="search-item">
          <i class="fa fa-search"></i>
          <input type="text" placeholder="Search..." />
        </li>
        <li>
            <a href="#" data-tooltip="Notifications">
                <i class="fa fa-globe"></i>
            </a>
        </li>
        <li>
            <a href="#" data-tooltip="Calendar">
                <i class="fa fa-calendar"></i>
            </a>
        </li>
        <li>
            <a href="#" data-tooltip="Help">
                <i class="fa fa-life-saver"></i>
            </a>
        </li>
    </ul><script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
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
                      
                        <!----Ward  Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Ward Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropWard" runat="server" style="width:130px">           
                            </asp:DropDownList>
               
                         </div>
                        </div>
                      <!----  Price -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Price :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtprice" runat="server" value="0.00" pattern="[0-9]+([,\.][0-9]+)?" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
             
                         </div>
                        </div>
                  <br/><br/>
             
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="btnupdate_Click" Visible="False" />
                   &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            <select id="j_idt79:j_idt131_reflowDD" name="j_idt79:j_idt131_reflowDD" class="ui-reflow-dropdown ui-state-default" autocomplete="off">
                            	<option value="0_0">Id Ascending</option>
                                <option value="0_1">Id Descending</option>
                                <option value="1_0">Year Ascending</option>
                                <option value="1_1">Year Descending</option>
                                <option value="2_0">Brand Ascending</option>
                                <option value="2_1">Brand Descending</option>
                                <option value="3_0">Color Ascending</option>
                                <option value="3_1">Color Descending</option>
                              </select>
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   Room Rate Entry 
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvrooment" runat="server" AutoGenerateColumns="False"  BackColor="White" DataKeyNames="ID"  BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" AllowPaging="True"  PageSize="10"  GridLines="Horizontal" OnSelectedIndexChanging="grvrooment_SelectedIndexChanging" OnRowDataBound="grvrooment_RowDataBound" OnRowDeleting="grvrooment_RowDeleting" OnPageIndexChanging="grvrooment_PageIndexChanging">
           <Columns>           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="WARD&nbsp;NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("WARDID")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE&nbsp;" Visible="true" >
            <ItemTemplate>
                 <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />

    </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
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

