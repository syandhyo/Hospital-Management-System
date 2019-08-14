<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_infant_entry.aspx.cs" Inherits="NURSE_nurse_infant_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                      $("[id$=txtdob]").datepicker({
                          dateFormat: 'dd-mm-yy HH:MM',
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
    </script>  
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                       <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Infant Entry
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
				<h1 style="color:#0071bc;"><b><u>New Born Entry</u></b></h1>
				
		        <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----Mother ID-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Mother ID :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:DropDownList ID="dropmotherid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="dropmname_SelectedIndexChanged" AutoPostBack="true">
                                 </asp:DropDownList>
                         </div>
                        </div>
                      <!----Mother Name :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Mother Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:Label ID="lblmname" runat="server" Text="" Visible="true"> </asp:Label>
             
                         </div>
                        </div>
                      <!---- Infant DOB :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Infant DOB :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtdob" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                         </div>
                        </div>
                      <!---- Gender :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Gender :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:DropDownList ID="dropgen" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                <asp:ListItem>Please Select</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList>
                         </div>
                        </div>
                      <!---- Weight (in KG):-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Weight (in KG):</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtweight" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" step="0.001" 
                                pattern="^\d+(?:\.\d{1,3})?$" ToolTip="Enter numeric value"></asp:TextBox>
             
                         </div>
                        </div>
                      <!----  Father Name :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Father Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtfname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
               
                         </div>
                        </div>
                     <!----  Assign Bed (Optional) :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Assign Bed &nbsp;(Optional):</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
               
                         </div>
                        </div>
                     <div id="div1" runat="server">
                         <!---- Select Ward :-----> 
                         <hr />
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Select Ward :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropward" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="dropward_SelectedIndexChanged">
                            </asp:DropDownList>
                         </div>
                        </div>
                         <!---- Select Bed :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Select Bed :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:DropDownList ID="dropbedno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                         </asp:DropDownList>
                         </div>
                        </div>
                         <hr />
                         </div>
                     </div>
                    </div>
                  <br/><br/>
                 <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" Style="margin-left:5px;" /> 
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />     
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   NEW BORN ENTRY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID"  
              OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True"
               AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
               OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Mother_id" HeaderText="Mother_id" />
                <asp:BoundField DataField="MName" HeaderText="Mother_Name" />
                <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:dd-MM-yyyy}"/>
                 <asp:BoundField DataField="Gender" HeaderText="Gender" />                 
                <asp:BoundField DataField="Weight" HeaderText="Weight" />
                <asp:BoundField DataField="FatherName" HeaderText="FatherName" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action"/>
            </Columns>
                 <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#2461BF" />
                  <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
                  <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                  <RowStyle BackColor="#EFF3FB" />
                  <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="white" />
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

