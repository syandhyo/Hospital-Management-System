<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Barcode2.aspx.cs" Inherits="ADMIN_Barcode2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Enter Barcode Code:<br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Generate" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Show" />
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" />--%>
            <div class="route-bar">
                <div class="route-bar-breadcrumb">
                    <i class="fa fa-home"></i><span>/ </span>Ward Master <span>/ </span>Create Ward
                </div>
               
                <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
            </div>

            <div class="layout-main-content">


                <div class="ui-fluid">
                    <strong>
                </div>
                <div class="card card-w-title">
                    <br>
                   

                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                             <div class="ui-grid-row">
                               
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Style="color: #CC0000" Text="*"></asp:Label>X-MARGIN :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtXmargin" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Font Type :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="drpFontType" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Style="width: 180px">
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>



                            <div class="ui-grid-row">
                                
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label1" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Y-MARGIN :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtYmargin" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Font Size :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="drpFontsize" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Style="width: 180px">
                                       <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                         <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                         <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                         <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                         <asp:ListItem Value="14">14</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>

                             <div class="ui-grid-row">
                               
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Width :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtWidth" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Font Color :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="drpFontcolor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Style="width: 180px">
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="ui-grid-row">
                               
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label7" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Height :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtHeight" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label8" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Line Gap :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="drpLineGap" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Style="width: 180px">
                                       <asp:ListItem Value="1">1</asp:ListItem>
                                         <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                         <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                         <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                         <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                         <asp:ListItem Value="14">14</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                            

                           

                           
                        </div>
                    </div>
                    <br />

                  

                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="create"  Style="margin-left: 5%;" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnGeneratebarcode" runat="server" Text="Generate Barcode" CssClass="create"  Style="margin-left: 5%;" OnClick="btnGeneratebarcode_Click"/>
                    &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" />
                    &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                    <br />
                    <br /><br />
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                        
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Ward Master
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            

                            <asp:GridView ID="gvBarcode" Width="98%" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered dt-responsive nowrap"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvBarcode_SelectedIndexChanged" >
                                <Columns>
                                    <asp:TemplateField HeaderText="SLNO">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:Label runat="server" ID="lblId" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" HorizontalAlign="left" />
                                        <ItemStyle Width="10%" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="xmargin" HeaderText="X - margin" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ymargin" HeaderText="y margin" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="width" HeaderText="width" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Height" HeaderText="Height" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fonttype" HeaderText="font type" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fontsize" HeaderText="font size" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fontcolor" HeaderText="fontcolor" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="linegap" HeaderText="line gap" 
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:BoundField>
                                    
                                    <asp:CommandField HeaderText="Edit" ShowSelectButton="True" SelectText="Edit" ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                    </asp:CommandField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#337ab7" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" Font-Size="15px" ForeColor="White" />
                                <PagerStyle ForeColor="White" HorizontalAlign="left" BackColor="#2461BF" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>


                        </div>
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

