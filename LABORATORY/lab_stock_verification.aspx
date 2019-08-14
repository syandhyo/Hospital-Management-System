<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/Lab_MasterPage.master" AutoEventWireup="true" CodeFile="lab_stock_verification.aspx.cs" Inherits="LABORATORY_lab_stock_verification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                 maxDate: '0',

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
             $("[id$=txtexpiry]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span>Lab Stock Verification
                    </div>
    
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
             <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title"><br>
        
                  <br>
                
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Date:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Date : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
                                </div>

                                <!----  Item----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Item :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:DropDownList ID="dropitem" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" width="180" OnSelectedIndexChanged="dropitem_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Batch No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Batch No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtbatchno" runat="server" pattern="^[a-zA-Z0-9_]*" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="14" minlength="12" autocomplete="off"></asp:TextBox> 
                                </div>

                                <!----  Expiry Date:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Expiry Date:</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtexpiry" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Packing Type :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Packing Type :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtpacktype" runat="server" pattern="^[a-zA-Z0-9_]*" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="100" autocomplete="off"></asp:TextBox>
                                </div>

                                <!---- No Of Packing :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;No Of Packing :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtnopacking" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" autocomplete="off" Title="Please!!Enter The Numeric Value.."></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Closing Stock :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Closing Stock :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtclostock" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"  Title="Please!!Enter The Numeric Value.."></asp:TextBox>
      
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Physical Stock :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtphystck" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" autocomplete="off"  Title="Please!!Enter The Numeric Value.."></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Reason :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Reason :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtreson" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="172"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                     </div>
				 <br/>
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" style="margin-left:5px;"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             Lab Stock Verification 
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	 <asp:GridView ID="grvlabst" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True"  GridLines="None" OnSelectedIndexChanging="grvlabst_SelectedIndexChanging" OnRowDataBound="grvlabst_RowDataBound" OnRowDeleting="grvlabst_RowDeleting" OnPageIndexChanging="grvlabst_PageIndexChanging" ForeColor="#333333">
                                         <AlternatingRowStyle BackColor="White" />
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="DATE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="ITEM&nbsp;NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblcdnme" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="VALID&nbsp;DAY" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblbatch" runat="server" Text='<%#Eval("BATCHNO")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
         <asp:TemplateField HeaderText="PACKAGE&nbsp;TYPE" Visible="true" >
            <ItemTemplate>
                 <asp:Label ID="lblPacktp" runat="server" Text='<%#Eval("PACKTYPE")%>'></asp:Label>
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

