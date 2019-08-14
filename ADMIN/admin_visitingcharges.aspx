<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_visitingcharges.aspx.cs" Inherits="ADMIN_admin_visitingcharges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
     <script type="text/javascript">
         //On Page Load.
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
             $("[id$=txtdate]").datepicker({
                 dateFormat: 'dd-mm-yy',
                 showOn: 'button',
                 buttonImageOnly: true,
                 dateFormat: 'dd-mm-yy',
                 changeMonth: true,
                 changeYear: true,
                 minDate: '0',

                 yearRange: "c-75:c+10",
                 buttonImage: '../bootstraptemplate/images/calendar.png'

             });
         }
        

    </script>
       <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Consultation/Visiting Charges For Corporate	 <span>
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
             <asp:TextBox ID="txtid" runat="server" CssClass="" Visible="false"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
       
         
            <div class="card card-w-title"><br>
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                     <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                       <!----  Company/Insurance Co-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2.5">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Company/Insurance Co. :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:DropDownList ID="dropcompany" runat="server" style="width:179px;height:28px;border-radius:3px;margin-left:0px;"> 
                        </asp:DropDownList>
                          
                         </div>
                        </div>
                          <!----  Consulation fee for Op-----> 
                         <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2.5">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Consulation fee for Op:</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtop" runat="server" pattern="[0-9]+([,\.][0-9]+)?" value="0" Style="width:170px;border-radius:3px;margin-left:13px;"
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">            
         </asp:TextBox>
                          
                         </div>
                        </div>

                          <!----  Consultation fee for Ip-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2.5">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Consultation fee for Ip:</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtip" runat="server"  pattern="[0-9]+([,\.][0-9]+)?" value="0" Style="width:170px;border-radius:3px;margin-left:14px;"
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
        </asp:TextBox>
                          
                         </div>
                        </div>

                            <!----  Date-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Date :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-3">
                                <asp:TextBox ID="txtdate" runat="server"  Enabled="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="width:170px;margin-left: -130px;"></asp:TextBox>
                          
                         </div>
                        </div>
                               <!----  Doctor Name-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2.5">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Doctor Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:DropDownList ID="dropdoctor" runat="server" style="width:180px;height:28px; margin-left:70px;border-radius:3px;">
         </asp:DropDownList>
                          
                         </div>
                        </div>

                     </div>
                 </div>
     
                <br />
                 <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="Button1_Click" Text="Submit" aria-disabled="false" style="margin-left:8%" />
      <asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" aria-disabled="false" style="margin-left:2%"/>       
        <asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" style="width:80px; background-color:#e71a33; border-color:#b11124;margin-left:2%;" />
                           
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                               <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    Consultation/Visiting Charges For Corporate	
                                </div>
                                <div class="ui-datatable-tablewrapper">

                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="5"  
           OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" AllowSorting="True" OnSorting="GridView1_Sorting" Width="1100"
                                                  OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <Columns> 
                <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
                    <ItemTemplate>
                      <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="COMNYNAME" HeaderText="INSURANCE" SortExpression="COMNYNAME" HeaderStyle-CssClass="text-center">                
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sname" HeaderText="NAME" SortExpression="Sname" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="OP_CHARGES" HeaderText="OPCHARGES" HeaderStyle-CssClass="text-center">                               
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="IP_CHARGES" HeaderText="IPCHARGES" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center">
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
                                </div>
                                </div>
                               
                                <br>
<br>
<br>
<br>
  </div>    
        </div>
            </strong></span>
            </ContentTemplate></asp:UpdatePanel>
</asp:Content>

