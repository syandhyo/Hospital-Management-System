<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_outsidelaboratory.aspx.cs" Inherits="ADMIN_admin_outsidelaboratory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Common <span> / </span> Outside Laboratory Entry
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lblSesion" runat="server" Text="Label" Visible="false"></asp:Label> 
           <asp:Label ID="lblAuto" runat="server" Text="Label" Visible="false"></asp:Label> 
        <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title"><br/>
            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                       
                        <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtname" runat="server" pattern="[a-zA-Z ]*$" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                           </div>
                             <!----  Address-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-1">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000" ></asp:Label>Address : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtadress" runat="server" TextMode="MultiLine" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"
                                 class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" width="168px"></asp:TextBox>
                         </div>

                        </div>

                      <div class="ui-grid-row">
                          <!----  Phone-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Phone No. :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtphoneno" runat="server" onkeypress="return onlyNos(event);" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="10"></asp:TextBox>
              
                           </div>
                             <!----  City -----> 
                            <div class="ui-panelgrid-cell ui-grid-col-1">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtcity" runat="server" pattern="^[A-Za-z ]+$" title="Please enter Alphabets" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                         </div>

                        </div>

                      <div class="ui-grid-row">
                          <!----  State -----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>State : </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:TextBox ID="txtstate" runat="server"  pattern="[a-zA-Z ]*$" title="Please enter Alphabets" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>   
              
                           </div>
                          </div>

                 </div>
            
            <br/><br/>   
                  <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" style="margin-left:5%;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" OnClick="btnupdate_Click" Visible="False" />
         &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                              
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                     Outside Laboratory Details
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvoutlab" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True" AllowSorting="True" Width="1060" PageSize="5" OnSorting="grvoutlab_Sorting"  GridLines="None" OnSelectedIndexChanging="grvoutlab_SelectedIndexChanging" OnRowDataBound="grvoutlab_RowDataBound" OnRowDeleting="grvoutlab_RowDeleting" OnPageIndexChanging="grvoutlab_PageIndexChanging" ForeColor="#333333" >
                                        <AlternatingRowStyle BackColor="White" />
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="Name" Visible="true" SortExpression="NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="Phone&nbsp;No" Visible="true" SortExpression="PHONENO" >
            <ItemTemplate>
                 <asp:Label ID="lbldtype" runat="server" Text='<%#Eval("PHONENO")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="State" Visible="true" SortExpression="STATE" >
            <ItemTemplate>
                 <asp:Label ID="lblmob" runat="server" Text='<%#Eval("STATE")%>'></asp:Label>
              <%--  <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("")%>' ></asp:TextBox>--%>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="City" Visible="true" SortExpression="CITY" >
            <ItemTemplate>
                 <asp:Label ID="lblqulif" runat="server" Text='<%#Eval("CITY")%>'></asp:Label>
                <%--<asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("")%>' ></asp:TextBox>--%>
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

