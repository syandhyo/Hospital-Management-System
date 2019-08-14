<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_radiologyentry.aspx.cs" Inherits="ADMIN_admin_radiologyentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Radiology Master <span> / </span> Radiology Package Entry
                    </div>
   
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title"><br/>
        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                     <!---- Radiology Name :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Radiology Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
               
                         </div>
                        </div>
                      <!----  Select Test Type :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Test Type :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="droptesttype" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" width="180" OnSelectedIndexChanged="droptesttype_SelectedIndexChanged" >
        </asp:DropDownList>
             
                         </div>
                        </div>
                 </div>
        </div>
                  <br />
            <table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td style="width:40%" align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="1060" DataKeyNames="slno" GridLines="None" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
           <Columns>

               <%--<asp:TemplateField HeaderText="TEST ID" >
            <ItemTemplate>
                <asp:Label ID="lblid" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>--%>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>' value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" ></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server"/>
            </ItemTemplate>
        </asp:TemplateField>

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

      
    </td>

<td style="width:20%" align="right"></td>
    <td style="width:30%" align="right"></td>
   
</tr>
</table>
     
                 
              <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create"  OnClick="btncreate_Click" style="margin-left:5%;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"   Visible="False" OnClick="btnupdate_Click" />
                 
                   &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                 Radiology Package Entry
                                </div>
                                <div class="ui-datatable-tablewrapper">
                               <asp:GridView ID="grdradiology" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID"  AllowPaging="True" 
                                   AllowSorting="True"  CssClass="table table-bordered" CellPadding="4"
                             ForeColor="#333333" GridLines="None" OnRowDataBound="grdradiology_RowDataBound" EmptyDataText="No Record Found" OnSelectedIndexChanging="grdradiology_SelectedIndexChanging"
                                 OnSorting="grdradiology_Sorting" OnRowDeleting="grdradiology_RowDeleting" OnPageIndexChanging="grdradiology_PageIndexChanging">
                                  <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SlNo"  HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                     <%--<asp:BoundField DataField="ID" HeaderText="Radiology Id" HeaderStyle-CssClass="text-center"/>--%>
                                    <asp:BoundField DataField="RADIOLOGYNAME" HeaderText="Radiology Name" SortExpression="RADIOLOGYNAME" HeaderStyle-CssClass="text-center">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="false" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
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
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

