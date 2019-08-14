<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_dutyroster.aspx.cs" Inherits="ADMIN_admin_dutyroster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">

                $(function () {
                    SetDatePicker();
                });



                //On UpdatePanel Refresh.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker();
                        }
                    });
                };

                function SetDatePicker() {
                    $("[id$=txtfdate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }

                $(function () {
                    SetDatePicker1();
                });
                //On UpdatePanel Refresh.
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            SetDatePicker1();
                        }
                    });
                };

                function SetDatePicker1() {
                    $("[id$=txttdate]").datepicker({
                        dateFormat: 'dd-mm-yy',
                        showOn: 'button',
                        buttonImageOnly: true,
                        dateFormat: 'dd-mm-yy',
                        changeMonth: true,
                        changeYear: true,
                        minDate: '0',

                        yearRange: "c-75:c+10",
                        buttonImage: '../images/calendar.png'

                    });
                }


    </script>

    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Duty Roster
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Label ID="lblfyear" runat="server" Visible="false"></asp:Label>

            <asp:RadioButtonList ID="rbHDR" runat="server" Visible="false">
            <asp:ListItem Text = "Yes" Value = "Yes" Selected = "True" ></asp:ListItem>
            <asp:ListItem Text = "No" Value = "No"></asp:ListItem>
        </asp:RadioButtonList>
        </div>
        <div class="card card-w-title"><br/>
          
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                       
                        <div class="ui-grid-row">
                          <!---- From Date-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>From Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtfdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                           </div>
                             <!----  To Date-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-1">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>To Date : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txttdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                         </div>

                        </div>
                      <!---- Shift -----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Shift :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropshift" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                             <asp:ListItem>Please Select</asp:ListItem>
                            <asp:ListItem>Morning</asp:ListItem>
                            <asp:ListItem>Day</asp:ListItem>
                            <asp:ListItem>Night</asp:ListItem>
                            <asp:ListItem>General</asp:ListItem>
                        </asp:DropDownList>
               
                         </div>
                        </div>
                      <!---- Staff-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Staff :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropstaff" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                      </asp:DropDownList>
                         </div>
                        </div>
                      <div class="ui-grid-row">
                          <!---- Ward-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Ward :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:DropDownList ID="dropward" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                </asp:DropDownList>
                           </div>
                             <!---- Templete-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><a href="DutyRoster (1).xlsx">Template Download</a></label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            
                         </div>

                        </div>
                     <div class="ui-grid-row">
                          <!----  Upload-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"></label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:FileUpload ID="flu_Image" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170" />
                           </div>
                             <!---- Button-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Button ID="btnUpload" runat="server" CssClass="add"  Text="Upload" OnClick="btnUpload_Click" /> 
                            <asp:Button ID="btn_update" runat="server" CssClass="add"  Text="Update " Visible="false" OnClick="btn_update_Click" /></label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            
                         </div>

                        </div>
                     </div>
                      </div>
                     <!-- -->
         
         <br/><br/>
                  
                  <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" style="margin-left:5%;"/> 
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />       
                  &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" /> 
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                              
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    <asp:GridView ID="grd_temp" runat="server"  AutoGenerateColumns="False"  Visible="False" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>     
                
                   <asp:TemplateField  >
            <ItemTemplate>
          
                <asp:Label ID="lblStaffId" runat="server" Text='<%#Eval("StaffId")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>

                   <asp:TemplateField  >
            <ItemTemplate>
                <asp:Label ID="lblWardName" runat="server" Text='<%#Eval("WardName")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>

                   <asp:TemplateField>
            <ItemTemplate>
               <asp:Label ID="lblShift" runat="server" Text='<%#Eval("Shift")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                   <asp:TemplateField >
            <ItemTemplate>
                <asp:Label ID="lblFDate" runat="server" Text='<%#Eval("FDate")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                   <asp:TemplateField  >
            <ItemTemplate>
               <asp:Label ID="lblTDate" runat="server" Text='<%#Eval("TDate")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>

            </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                     Duty Roster
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" Width="1060"
                                    OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" 
                                    OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" 
                                        CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>  
                                           <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>   
                                         <asp:BoundField DataField="Sname" HeaderText="Staff" HeaderStyle-CssClass="text-center" SortExpression="Sname"> 
                                           <HeaderStyle CssClass="text-center" />
                                           </asp:BoundField>
                                         <asp:BoundField DataField="NAME" HeaderText="WardName" HeaderStyle-CssClass="text-center" SortExpression="NAME">
                                           <HeaderStyle CssClass="text-center" />
                                           </asp:BoundField>
                                         <asp:BoundField DataField="Shift" HeaderText="Shift" />             
                                         <asp:BoundField DataField="FDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"/>  
                                         <asp:BoundField DataField="TDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"/> 
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" 
                                            HeaderText="Action" HeaderStyle-CssClass="text-center">
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

