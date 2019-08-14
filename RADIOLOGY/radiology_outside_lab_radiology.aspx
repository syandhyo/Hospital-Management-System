<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="radiology_outside_lab_radiology.aspx.cs" Inherits="RADIOLOGY_radiology_outside_lab_radiology" %>

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
                           minDate: '0',

                           yearRange: "c-75:c+10",
                           buttonImage: '../images/calendar.png'

                       });
                   }
    </script> 
               <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Master <span> / </span> Outside Lab Radiology
                    </div>
   
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title">
        
                <h1 style="color:#0071bc;"><b><u>Outside Lab Radiology</u></b></h1>
                   <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>
                                <!----Sent Note No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Sent Note No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtsenoteno" runat="server" pattern="^[a-zA-Z0-9_]*" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="8"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Sent To Lab :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Sent To Lab :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtsentolab" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>
                                <!---- Sample Form :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Sample Form :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtsamplfm" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Lab Req No  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;&nbsp;Lab Req No  :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtlabreqno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  pattern="^[a-zA-Z0-9_]*"></asp:TextBox> 
                                </div>
                                
                                </div>
                            <br><br>
				                <hr>
                              
                            <h1 style="color:#0071bc;"><b><u>Patient Details</u></b></h1>
                            <div class="ui-grid-row">
                                <!---- Patient Type :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Patient Type :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="droppatype" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="true">
                                        <asp:ListItem>Select Type</asp:ListItem>
                                        <asp:ListItem>INPATIENT</asp:ListItem>
                                        <asp:ListItem>OUTPATIENT</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                               
                                </div>
                            <div class="ui-grid-row">
                                 <!----OPD No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;OPD No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtopdno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  ></asp:TextBox>
                                </div>
                                <!----Requisition At  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Requisition At  :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtreqat" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtFname" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>
                                <!----Age :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Age :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtage" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+" MaxLength="2"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Gender :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Gender :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="dropgender" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="true">
                                        <asp:ListItem>Female</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Transgender</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <!----Contact No:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Contact No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtcontactno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]{10,10}" MaxLength="10" MinLength="10"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Present Address :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Present Address :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtadress" runat="server" TextMode="MultiLine" Width="172" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
                                </div>
                                <!----Treating Dr. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Treating Dr. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                  <asp:TextBox ID="txttreatingdr" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox> 
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!----Permanent Address :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Permanent Address :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtaddresPermnt" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="172" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
                                </div>
                                <!----Adv. Paid :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Adv. Paid :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtAdvpaid" runat="server"  pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Sent By :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Sent By :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtsentby" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>
                                <!----Total Amt :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Total Amt :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txttotamt" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                </div>
                        </div>
                    </div>
            
            <br />
                  <hr>
                  <br>
                  <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"   Visible="False" OnClick="btnupdate_Click" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
            <br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              OUTSIDE LAB RADIOLOGY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	 <asp:GridView ID="grvlabst" runat="server" Width="1060" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True"  GridLines="None" OnSelectedIndexChanging="grvlabst_SelectedIndexChanging" OnRowDataBound="grvlabst_RowDataBound" OnRowDeleting="grvlabst_RowDeleting" OnPageIndexChanging="grvlabst_PageIndexChanging" ForeColor="#333333">
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
         <asp:TemplateField HeaderText="PATIENT&nbsp;TYPE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblcdnme" runat="server" Text='<%#Eval("PATIENTYPE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="OPD&nbsp;NO" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblbatch" runat="server" Text='<%#Eval("OPDNO")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
         <asp:TemplateField HeaderText="NAME" Visible="true" >
            <ItemTemplate>
                 <asp:Label ID="lblPacktp" runat="server" Text='<%#Eval("FIRSTNAME")%>'></asp:Label>
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
                                </div><br>
<br>
<br>
<br>
</div>
        </div>
    </div>
               </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

