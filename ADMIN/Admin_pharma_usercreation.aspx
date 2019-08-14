<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_pharma_usercreation.aspx.cs" Inherits="ADMIN_Admin_pharma_usercreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>


                <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Creat User
                    </div>
    

    </div>

                <div class="layout-main-content">

                    
        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblusid" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="lblfyear" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title"><br/>
				<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                       <!---- Store Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Store Name </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropstore" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            </asp:DropDownList>
                         </div>
                        </div>
                        <!---- Employee Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Employee Name </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="Dropempname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            </asp:DropDownList>
                         </div>
                        </div>
                      <!----User Name :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>User Name </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                              <asp:Label ID="Label6" runat="server" ForeColor="#CC0000" style="margin-left:-29px;"></asp:Label> </label>
                         <asp:TextBox ID="txtusername" runat="server" pattern="^[a-zA-Z0-9_]*" MinLength="4" style="width:168px;" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtusername" ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>
             
                         </div>
                        </div>
                      <!---- Password :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Password </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtpassword" ErrorMessage="*" ForeColor="#CC0000"></asp:RequiredFieldValidator>--%>
        
               
                         </div>
                        </div>
                     <!---- Confirm Password :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Confirm Password </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtcnfrmpassword" runat="server" TextMode="Password" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpassword" ControlToValidate="txtcnfrmpassword" ErrorMessage="Password and confirm password are not same" ForeColor="#CC0000"></asp:CompareValidator>
                         </div>
                        </div>
                      <!---- Set Access Permission :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget">Set Access Permission </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Edit">Edit</asp:ListItem>
                            <asp:ListItem Value="Delete">Delete</asp:ListItem>
                        </asp:CheckBoxList>
               
                         </div>
                        </div>
                     <div class="ui-grid-row" >
                          <!---- Set Access Module :-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-3" hidden="hidden">
                             <label class="ui-outputlabel ui-widget">Set Access Module </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-6" hidden="hidden">
                               <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                                <asp:ListItem Value="ACCOUNTS">ACCOUNTS</asp:ListItem>
                                <asp:ListItem Value="LAB">LAB</asp:ListItem>
                                 <asp:ListItem Value="NURSE">NURSE</asp:ListItem>
                                <asp:ListItem Value="OT">OT</asp:ListItem>
                                <asp:ListItem Value="PHARMACY" Selected="True">PHARMACY</asp:ListItem>
                                <asp:ListItem Value="RECEPTION">RECEPTION</asp:ListItem>
                                <asp:ListItem Value="GENERALSTOCK">GENERALSTOCK</asp:ListItem>
                                <asp:ListItem Value="STOREKEEPER">STOREKEEPER</asp:ListItem>
                                <asp:ListItem Value="ASSET">ASSET</asp:ListItem>
                            </asp:CheckBoxList>
                           </div>
                            
</div>
                      
                         <!---- Status :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Status </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropstatus" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                             <asp:ListItem>ACTIVE</asp:ListItem>
                            <asp:ListItem>INACTIVE</asp:ListItem>
                          </asp:DropDownList>
                         </div>
                        </div>
                     </div>
                    </div>
					<br/>
				 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" style="margin-left:5%;"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
                   &nbsp;&nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" CausesValidation="False" />
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    User Creation
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Width="1060"
                                         CssClass="table table-bordered" DataKeyNames="slno" OnPageIndexChanging="GridView1_PageIndexChanging"
                                         OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" 
                                        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:CommandField>
            </Columns>
          <%--  <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />--%>
                                        <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
            <%--  <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />--%>
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

