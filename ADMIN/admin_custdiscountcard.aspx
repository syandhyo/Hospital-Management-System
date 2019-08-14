<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_custdiscountcard.aspx.cs" Inherits="ADMIN_admin_custdiscountcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body .ui-widget-header {
    
}
    </style>
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
                      $("[id$=txtdate]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Common <span> / </span> Customer Discount Card 
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title"><br/>
        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                     <div class="ui-grid-row">
                          <!---- Card Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtcname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoComplete="off"></asp:TextBox>
                           </div>
                             <!----  Date-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Date : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" AutoComplete="off"></asp:TextBox>
                         </div>

                        </div>

                      <div class="ui-grid-row">
                          <!---- Card Fee-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Card Fee : </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtcardfee" runat="server" value="0.00" pattern="[0-9]+([,\.][0-9]+)?"
              onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoComplete="off"></asp:TextBox>
              
                           </div>
                             <!----  Valid -----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Valid Days: </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtvalid" runat="server" pattern="[0-9]+([,\.][0-9]+)?" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoComplete="off"></asp:TextBox>
                         </div>

                        </div>

                      <div class="ui-grid-row">
                          <!----Active  -----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Active : </label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:CheckBox ID="chkactive" runat="server" />
              
                           </div>
                     </div>
            </div>
           
             <br/><br/>
                  <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create"  OnClick="btncreate_Click" style="margin-left:5%;"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
               
         &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                         <br/><br/><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                              
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                     Customer Discount Card 
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvrooment" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" Width="1060" EmptyDataText="No Record Found" CellPadding="4" AllowPaging="True"  PageSize="5" AllowSorting="True" OnSorting="grvrooment_Sorting" GridLines="None" OnSelectedIndexChanging="grvrooment_SelectedIndexChanging" OnRowDataBound="grvrooment_RowDataBound" OnRowDeleting="grvrooment_RowDeleting" OnPageIndexChanging="grvrooment_PageIndexChanging" ForeColor="#333333">
                                        <AlternatingRowStyle BackColor="White" />
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="CARD&nbsp;NO" Visible="true" SortExpression="CDNAME" >
            <ItemTemplate>
                <asp:Label ID="lblcdnme" runat="server" Text='<%#Eval("CDNAME")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="VALID&nbsp;DAY" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblvalid" runat="server" Text='<%#Eval("VALID")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="DATE" Visible="true" SortExpression="DATE" >
            <ItemTemplate>
                <asp:Label ID="lbldate" runat="server"  Text='<%#Eval("DATE","{0:MM/dd/yyyy}")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="CARD&nbsp;FEE" Visible="true" SortExpression="CDFEE" >
            <ItemTemplate>
                 <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("CDFEE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" />

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

