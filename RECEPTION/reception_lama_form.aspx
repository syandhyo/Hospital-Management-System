<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_lama_form.aspx.cs" Inherits="RECEPTION_reception_lama_form" %>

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
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> LAMA Form
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
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>LAMA Form</u></b></h1>
                
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Select Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Select Bed No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropbedno" runat="server" AutoPostBack="True" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         Width="180" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <!---- ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>
		                  <br><br>
		                   <hr>
		   
                         <div class="ui-grid-row">
                                <!----IPD NO:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        IPD NO :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblipno" runat="server"></asp:Label> 
                                </div>
                                <!----  Patient Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Patient Name : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpname" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Parent/Guardian Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Parent/Guardian &nbsp;&nbsp;Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtgname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" MaxLength="40" minLength="3" Title="Please enter The Alphabets"></asp:TextBox>
                                </div>
                                <!---- Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Date :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                        
                                        
                                </div>

                            </div>
                         </div>
                   </div>
                 <br><br>
			   <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" style="margin-left:5%;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
				<br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                          
							 <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                LAMA Form Details
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                        <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}"/>
                                         <asp:BoundField DataField="IPNO" HeaderText="IPDNO" />
                                          <asp:BoundField DataField="PNAME" HeaderText="PATIENTNAME" />
                                        <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>
                                     <asp:TemplateField HeaderText="PRINT">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" >Print</asp:LinkButton>
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

