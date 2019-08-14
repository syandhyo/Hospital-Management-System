<%@ Page Title="" Language="C#" MasterPageFile="~/OT/OT_MasterPage.master" AutoEventWireup="true" CodeFile="ot_surgery_details.aspx.cs" Inherits="OT_ot_surgery_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Surgery Details		

                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
              <h1 style="color:#0071bc;"><b><u>Surgery Detail Entry</u></b></h1>
                
              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Select Bed NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Bed ID :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropbedno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="True">
                                 </asp:DropDownList>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Datetime :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtinvdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" ></asp:TextBox>
                                </div>

                            </div>
                            
                            <div class="ui-grid-row">
                                <!---- Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Name :</label>
                                       
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server" Visible="true"></asp:Label>
                                </div>
                                <!---- IPDNO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        IPDNO. : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblipdno" runat="server" Visible="true"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  SurgeryType :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;SurgeryType :</label>
                                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropsurgerytype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                        </asp:DropDownList>
                                </div>
                                <!---- Surgeon :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                       Surgeon :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                      <asp:TextBox ID="txtsurgeon" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Anaesthesia :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" style="color: #CC0000" Text="*"></asp:Label>Anaesthesia :</label>
                                     
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtanaesthesia" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="true"></asp:TextBox>
                                </div>
                                <!---- Anaesthesist :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">Anaesthesist :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtanaesthesist" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170" TextMode="MultiLine"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Staff Nurse :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Staff Nurse :</label>
                                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtsn" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <!----  Pharmacist :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Pharmacist :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtpharmacist" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170" TextMode="MultiLine"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Test Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" style="color: #CC0000" Text="*"></asp:Label>Test Name :</label>
                                                        </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Droptestname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                        </asp:DropDownList>
                                </div>
                                  <!----  Medicine :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" style="color: #CC0000" Text="*"></asp:Label>Test Name :</label>
                                                        </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                        </asp:DropDownList>
                                </div>
                                  </div>
                            <div class="ui-grid-row">
                                 <!----  Equipment :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" style="color: #CC0000" Text="*"></asp:Label>Equipment :</label>
                                                        </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Dropequip" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                        </asp:DropDownList>
                                </div>
                             <!----  Surgery Charge :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" style="color: #CC0000" Text="*"></asp:Label>Surgery Charge :</label>
                        
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtscharge" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" 
                             pattern="[0-9]+([,\.][0-9]+)?"  value="0" 
                            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" title="Please Enter Numeric Value" ></asp:TextBox>
                                </div>
                                <!---- ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            </div>
                  </div>
				 <br><br>
                  <hr>
                  <br>
                 <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" />
        &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="false" />
                   &nbsp;&nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             Surgery Details
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"   AllowPaging="True"
             AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
            OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>                
                <asp:BoundField DataField="SurgeryType" HeaderText="SurgeryType" /> 
                <asp:BoundField DataField="Surgeon" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Surgeon&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                <asp:BoundField DataField="Anaesthesia" HeaderText="Anaesthesia" />
                <asp:BoundField DataField="Anaesthesist" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Anaesthesist&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                <asp:BoundField DataField="SNurse" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Staff&nbsp;Nurse&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                <asp:BoundField DataField="Pharmacist" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pharmacist&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />                        
                <asp:CommandField ShowDeleteButton="false" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />
            </Columns>
             <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#EFF3FB" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
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
                    </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

