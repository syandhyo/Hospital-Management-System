<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/Lab_MasterPage.master" AutoEventWireup="true" CodeFile="Lab_samplecollection.aspx.cs" Inherits="LABORATORY_Lab_samplecollection" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Test Registration
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
         <div class="card card-w-title">
        
                <h1 style="color:#0071bc;"><b><u>Test Registration</u></b></h1>
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                            <div class="ui-grid-row">
                                <!----  Date:----->
                                <div class="ui-panelgrid-cell ui-grid-col-3">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Date : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
                                </div>
                                  <!----  Time:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Time : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txttime" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                
                                <div class="ui-panelgrid-cell ui-grid-col-3">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="lbltid" runat="server" ForeColor="Red" Text="*"></asp:Label>Transaction Id : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txttid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Test Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="true"></asp:DropDownList>
                                     <%--<asp:TextBox ID="txttest" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" ></asp:TextBox>--%>
                                </div>

                            </div>
                      </div>

			   <br/><br/> 
             	 
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" style="margin-left:5px;" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False"/>
        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel"/>
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Outside Lab Entry 
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvlabst" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True"  PageSize="2"  GridLines="None" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
               
         <asp:TemplateField HeaderText="Transaction Id" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbltid" runat="server" Text='<%#Eval("PATIENTYPE")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                 
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />

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

