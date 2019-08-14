<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/Lab_MasterPage.master" AutoEventWireup="true" CodeFile="lab_stock_reconciliation1.aspx.cs" Inherits="LABORATORY_lab_stock_reconciliation1" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Stock Reconciliation
                    </div>
    
                </div>

                <div class="layout-main-content">

        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblevent" runat="server" Text="Label" Visible="false"> </asp:Label>
             <asp:Label ID="lblupdate" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblupdate1" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title"><br/>
        
                  <br/>
             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Date:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Date : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lbldate" runat="server"></asp:Label>
                                </div>

                                
                            </div>
                            <div class="ui-grid-row">
                                <!----  Department:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Department:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Ddldept" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="false" ></asp:DropDownList>
                                </div>

                            </div>
                            <br />
                            <hr />
                            <br />
                            <div class="ui-grid-row">
                                <!---- Item Name:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Item Name:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Ddlitem" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="Ddlitem_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!----  Quantity:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Quantity:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtquant" runat="server" OnTextChanged="txtquant_TextChanged"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" AutoPostBack="True" autocomplete="off" Title="Please!!Enter The Numeric Value.." value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}"></asp:TextBox>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Label ID="lblquant" runat="server" ForeColor="Red" Text=""></asp:Label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Reason :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Reason :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txt_des" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" autocomplete="off" pattern="^[a-zA-Z0-9_]*" Title="Please!!Enter The AlphaNumeric Value.."></asp:TextBox>

                                </div>

                                

                            </div>
                        </div>
                     </div>
              <br/><br/>
                  <hr/>
                  <br/>
                 <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="create" OnClick="btnsubmit_Click"  style="margin-left:5px;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
         &nbsp;<%--<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" Visible="false"/>--%>
        &nbsp;<asp:Button ID="btncancel1" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel1_Click"/>
        &nbsp;<asp:Button ID="btndelete" runat="server" Text="delete" CssClass="create" OnClick="btndelete_Click" OnClientClick="return DeleteItem()" Visible="false"/>
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Stock Reconciliation
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderStyle-CssClass="text-center" HeaderText="ID" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE" HeaderStyle-CssClass="text-center" HeaderText="DATE" ItemStyle-HorizontalAlign="center" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM" HeaderStyle-CssClass="text-center" HeaderText="ITEM" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPTN" HeaderStyle-CssClass="text-center" HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QUANTITY" HeaderStyle-CssClass="text-center" HeaderText="QUANTITY" ItemStyle-HorizontalAlign="center" >
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:CommandField HeaderStyle-CssClass="text-center" HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp;Select" ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="true" >
                                <HeaderStyle CssClass="text-center" />
                                </asp:CommandField>
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

