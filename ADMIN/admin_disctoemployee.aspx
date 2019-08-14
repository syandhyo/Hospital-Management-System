<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_disctoemployee.aspx.cs" Inherits="ADMIN_admin_disctoemployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Discount<span>/ </span>Discount To Employee
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
				<asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
          

              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                   <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                       <!---- Card Type -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                          
                               <asp:Label ID="Label7" class="ui-outputlabel ui-widget" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                              Card Type:</label>	
               
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          
                             <asp:TextBox ID="txtcardname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                         </div>
                        </div>
                        <!---- Disc% In Pharmacy  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                         Disc% In Pharmacy :</label>
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtdiscpharmacy" runat="server" value="0.00" 
                onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="3" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                           
                              </div>
                        </div>

                        <!---- Disc% In Lab  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                               <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                          Disc% In Lab :</label>
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtdisclab" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0.00" 
                    onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="3"></asp:TextBox>
                 
                              </div>
                        </div>

                        <!---- Disc% In Room Charge  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                               <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                           Disc% In Room &nbsp;&nbsp;Charge :</label>	
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtroomcharge" runat="server" value="0.00" 
                     onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="3" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                 
                              </div>
                        </div>
                    <!---- Disc% In Radiology  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                               <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                     Disc% In Radiology :</label>	
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtdiscradilogy" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="3"></asp:TextBox>
                 
                              </div>
                        </div>
                         <!---- Disc% In OT  -----> 
                        <div class="ui-grid-row">
                              <div class="ui-panelgrid-cell ui-grid-col-2">
                               <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                           Disc% In OT :</label>	
                              </div>
                              <div class="ui-panelgrid-cell ui-grid-col-4">
                              <asp:TextBox ID="txtdiscot" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="3"></asp:TextBox>
                 
                              </div>
                        </div>

                </div>
            </div>


            <br>
                <asp:Button ID="btncreate" runat="server" Text="Create" class="create" style="margin-left:15%;margin-top: 10px;" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" class="update" style="margin-left:15%;margin-top: 10px;" OnClick="Button2_Click" Visible="False" />
                 
                 
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                  Card Entry List
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                    <asp:GridView ID="grvdoctor" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True"  PageSize="5" OnSorting="grvdoctor_Sorting" Width="1060"
                                        EmptyDataText="No Data Is There" GridLines="None" OnSelectedIndexChanging="grvdoctor_SelectedIndexChanging"
                                         OnRowDataBound="grvdoctor_RowDataBound" OnRowDeleting="grvdoctor_RowDeleting" OnPageIndexChanging="grvdoctor_PageIndexChanging" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
                                   <Columns>
           
                                         <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                                    <ItemTemplate>
                                      <%#Container.DisplayIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="CARD&nbsp;NAME" Visible="true" SortExpression="CARDNAME" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("CARDNAME")%>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ROOM&nbsp;RENT" Visible="true" SortExpression="ROOMRENT">
                                    <ItemTemplate>
                                         <asp:Label ID="lbldtype" runat="server" Text='<%#Eval("ROOMRENT")%>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="PHARMACY" Visible="true" SortExpression="PHARMACY">
                                    <ItemTemplate>
                                         <asp:Label ID="lblmob" runat="server" Text='<%#Eval("PHARMACY")%>'></asp:Label>
                                      <%--  <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("")%>' ></asp:TextBox>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Laboratory" Visible="true" SortExpression="LAB">
                                    <ItemTemplate>
                                         <asp:Label ID="lbllab" runat="server" Text='<%#Eval("LAB")%>'></asp:Label>
                                        <%--<asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("")%>' ></asp:TextBox>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="RADIOLOGY" Visible="true" SortExpression="RADIOLOGY">
                                    <ItemTemplate>
                                         <asp:Label ID="lblradiology" runat="server" Text='<%#Eval("RADIOLOGY")%>'></asp:Label>
                                       <%-- <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("")%>' ></asp:TextBox>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OT" Visible="true" SortExpression="OT">
                                    <ItemTemplate>
                                         <asp:Label ID="lblot" runat="server" Text='<%#Eval("OT")%>'></asp:Label>
                                       <%-- <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("")%>' ></asp:TextBox>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />

                            </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True"/>

                                    <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White"/>
                                    <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView> 
                                	
                                </div><br>

</div>
</div>
        </div>
              </strong>
              </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

