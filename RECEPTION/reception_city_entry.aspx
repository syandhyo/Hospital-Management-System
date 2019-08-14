<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_city_entry.aspx.cs" Inherits="RECEPTION_reception_city_entry" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Out Patient <span> / </span> City Entry
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
            <asp:TextBox ID="txtcityupdate" runat="server"  Visible="false"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server"  Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>City Entry</u></b></h1>
                
              <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!----  City Name:-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-1.5">
                             <label class="ui-outputlabel ui-widget">City Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtcity" runat="server" AutoComplete="off"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                pattern="^[A-Za-z ]+$" MaxLength="40" minlength="3" title="Please Enter City Name"></asp:TextBox>
               
                         </div>
                        </div>
                     </div>
                  </div>
		<br/><br/>
           <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" style="margin-left:5%;"/> 
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" style="margin-left:5%;"/>       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />   
                      
						 <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
							 <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   CITY ENTRY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" 
                                        OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True"
                                         AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" PageSize="10" 
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
                                        CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>                
                                            <asp:BoundField DataField="CITY" HeaderText="CITY" HeaderStyle-CssClass="text-center" SortExpression="CITY">                         
                                            <HeaderStyle CssClass="text-center" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" HeaderStyle-CssClass="text-center">
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
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

