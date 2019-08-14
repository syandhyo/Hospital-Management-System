<%@ Page Title="" Language="C#" MasterPageFile="~/OT/OT_MasterPage.master" AutoEventWireup="true" CodeFile="ot_indent_to_pharmacy.aspx.cs" Inherits="OT_ot_indent_to_pharmacy" %>

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
                         <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Indent To Pharmacy
                    </div>
    
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server"  Visible="false"> </asp:Label>
            <asp:Label ID="lblinsurance" runat="server"  Visible="false"></asp:Label>
             <asp:Label ID="lblEditgrd" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Indent To Pharmacy</u></b></h1>
                
                  
             		  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                             
                        <div class="ui-grid-row">
                            <!----Date of Indent :-----> 
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Date of Indent :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>

                          </div>
                        <!---- Indent No. :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">&nbsp;Indent No. :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtindentno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                        </div>
                       </div>
                            
                        <div class="ui-grid-row">
                              <!----From Dept :-----> 
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>From Dept :</label>
                   	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            </asp:DropDownList>
                          </div>
                        <!---IPNO. :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>IPNO. :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtipno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-zA-Z0-9]+"></asp:TextBox> 
                        </div>
                       </div>
                             
                        <div class="ui-grid-row">
                            <!----Enter By :-----> 
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Enter By :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtenterby" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Z a-z]+$"></asp:TextBox>

                          </div>
                             <!----Authorised By :-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Authorised By :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtauthorised" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                              pattern="^[A-Z a-z]+$"></asp:TextBox>

                          </div>
                        
                       </div>
                            <br />
                            <hr />
                            <br />
                          
                        <div class="ui-grid-row">
                             <!----Item Name :-----> 
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Item Name :</label>       
                           
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" placeholder="Item" ToolTip="Enter Item Here" pattern="^[A-Z a-z -]+$"></asp:TextBox>

                          </div>
                        <!---- Unit :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">&nbsp;Unit :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:DropDownList ID="txtUnit" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" style="border-radius:4px;height:27px;" Enabled="False">
                                  <asp:ListItem>PCS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                       </div>
                             
                        <div class="ui-grid-row">
                            <!----Quantity :-----> 
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Quantity :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtqty" runat="server" placeholder="Quantity" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Quantity Here" AutoPostBack="true" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>

                          </div>
                       <!---- Unit :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                          &nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="search"  OnClientClick="return Validate();" Text="ADD" OnClick="btnAdd_Click" />
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           
                        </div>
                       </div>
                      
                            </div>
                           </div>
            <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False" AllowSorting="True"  CssClass="table table-bordered" OnRowDeleting="grdMaterial_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" >
              <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Sl No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Item&nbsp;Name">
            <ItemTemplate>
                 <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                  <asp:TemplateField HeaderText="Unit">
            <ItemTemplate>
                 <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
                   <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                 <asp:Label ID="lbl_Qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
               
                <asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>
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
                 <br><br>
                  <hr>
                  <br>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create"  OnClick="btncreate_Click" OnClientClick="return ValidateCret();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel"  OnClick="btncancel_Click"/>
            <br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Indent To Pharmacy
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="INDENT_NO" EmptyDataText="No Record found." OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
            <Columns> 
                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yy}" HeaderStyle-CssClass="text-center">                                
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="INDENT_NO" HeaderText="INDENT_NO" HeaderStyle-CssClass="text-center">  
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="IPNO" HeaderText="IPNO" HeaderStyle-CssClass="text-center">                                          
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="DeptName" HeaderText="DEPARTMENT" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center">
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
                    </strong>
              </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

