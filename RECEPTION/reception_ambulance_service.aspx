<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_ambulance_service.aspx.cs" Inherits="RECEPTION_reception_ambulance_service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
     
    <div class="route-bar">
        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Ambulance Service
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Ambulance Service</u></b></h1>
                
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Date:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtAdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                </div>

                                <!---- Driver :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Driver :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropdriver" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    </asp:DropDownList>
                                </div>

                            </div>
                           
                            <!-- -->
                            
                            <div class="ui-grid-row">
                                <!---- Ambulance NO:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Ambulance No.:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropambulanceno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" width="180">
                                    </asp:DropDownList>
                                </div>
                                <!----  From Destination:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server"  AutoComplete="off" Text="*" ForeColor="#CC0000"></asp:Label>From Destination : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtfrom" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Patient Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Patient Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtpatient" runat="server" AutoComplete="off"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Z a-z -]+$" MaxLength="30" MinLength="3" title="Please Enter Alphabets Only"></asp:TextBox>
                                </div>
                                <!---- To Destination :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" Text="*" AutoComplete="off" ForeColor="#CC0000"></asp:Label>To Destination :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtto" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Z a-z -]+$" MaxLength="30" MinLength="3" title="Please Enter Alphabets Only"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Attendent Name .----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Attendent Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtattend" runat="server" AutoComplete="off"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" MaxLength="40" minlength="3" title="Please Enter Attendent Name"></asp:TextBox>
                                </div>
                                <!----  Approx Km :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <strong>Approx Km :</strong> :
                           
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtkm" runat="server" AutoComplete="off" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="3" pattern="[0-9]+([,\.][0-9]+)?" title="Please Enter numeric value" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Contact No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Contact No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtcont" runat="server" AutoComplete="off" pattern="[789][0-9]{9}" MaxLength="10" minlength="10" title="Please Enter Phone number" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                                <!----  Fee :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Fee : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtfee" runat="server" AutoComplete="off"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" maxLength="5" pattern="[0-9]+([,\.][0-9]+)?" title="Please Enter numeric value" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>

                            </div>
                            <//div>
                     </div>
             	<br/><br/>
                  <hr/>
                  <br/>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="create" OnClick="Button1_Click" style="margin-left:5%;" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" style="margin-left:5%;"/>       
        
                   &nbsp;&nbsp; <asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              AMBULANCE SERVICE
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" Width="1060" 
                                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" OnSorting="GridView1_Sorting"
                                        AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" 
                                        CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns> 
                                         <asp:BoundField DataField="id" HeaderText="" visible="false"/>    
                                         <asp:BoundField DataField="ADate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                         <asp:BoundField DataField="PatientName" HeaderText="Patient&nbsp;Name" SortExpression="PatientName" /> 
                                         <asp:BoundField DataField="AttendentName" HeaderText="Attendent" />
                                         <asp:BoundField DataField="ContactNo" HeaderText="Contact No"  SortExpression="ContactNo" />             
                                         <asp:BoundField DataField="From" HeaderText="From" />  
                                         <asp:BoundField DataField="To" HeaderText="To" />
                                         <asp:BoundField DataField="ApproxKm" HeaderText="Km" />
                                         <asp:BoundField DataField="Fee" HeaderText="Fee" />
                                         <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
                                        <asp:TemplateField HeaderText="Print">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
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
                                <br/>
                        </div>

        </div>
                      </div>
                    
                    </strong>
                    
                    </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

