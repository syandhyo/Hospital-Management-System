<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_createward.aspx.cs" Inherits="ADMIN_admin_createward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblslno" runat="server" Text="Label" Visible="false"></asp:Label>
                      <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>

                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                             <div class="ui-grid-row">
                                <!---- Ward Name ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Department :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropdept" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Style="width: 180px">
                                    </asp:DropDownList>
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" Style="color: #CC0000" Text="*"></asp:Label>type :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="droptype" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Style="width: 180px">
                                        <asp:ListItem Value="Ward">Ward</asp:ListItem>
                                         <asp:ListItem Value="Cabin">Cabin</asp:ListItem>
                                        <asp:ListItem Value="Icu">Icu</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!---- Ward Name ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                                        pattern="^[A-Za-z ]+$"></asp:TextBox>
                                </div>
                                <!---- Prifix  ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" Style="color: #CC0000" Text="*"></asp:Label>Prefix :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtprifix" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-zA-Z0-9]+"></asp:TextBox>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-2" >
                                    
                                </div>
                            </div>

                           

                            <div class="ui-grid-row">
                                <!---- No Of Bed  ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label1" runat="server" Style="color: #CC0000" Text="*"></asp:Label>No Of Bed :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtnoofbed" runat="server" onkeypress="return isNumber(event)" MaxLength="3" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+"
                                        value="0"
                                        onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                
                                
                            </div>
                            
                        </div>
                    </div>
                    <br />

                    <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:5%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>

                    <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" Style="margin-left: 5%;" />
                    &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" OnClick="Button2_Click" Visible="False" />
                    &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                    <br />
                    <br /><br />
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                        
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            Ward Master
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" Width="1060" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" 
                                AllowSorting="True" CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
                                OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DEPARTMENT" HeaderStyle-CssClass="text-center" SortExpression="NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_dept" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NAME" HeaderStyle-CssClass="text-center" SortExpression="NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BED" HeaderStyle-CssClass="text-center" SortExpression="BED">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_bed" runat="server" Text='<%#Eval("BED")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prifix" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_prifix" runat="server" Text='<%#Eval("PRIFIX")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="False"   
                                        HeaderText="ACTION" HeaderStyle-CssClass="text-center" >
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

