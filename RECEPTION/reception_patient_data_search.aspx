<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_patient_data_search.aspx.cs" Inherits="RECEPTION_reception_patient_data_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                  function openpopup() {
                      window.open("Recep_PatientBill.aspx")
                  }
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Patient Data Search
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
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Search By Patient Name</u></b></h1>
					
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Search Item :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2.5">
                                    <label class="ui-outputlabel ui-widget"> Search By Patient Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-1">
                                   
                                    <asp:Button ID="btnSearch" runat="server" class="search" Text="Show" Width="70px" OnClick="btnSearch_Click"  />
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Button ID="Button1" runat="server" class="search" Text="Show All" Width="70px" OnClick="Button1_Click" /></td>
                                </div>

                            </div>
                           </div>
                      </div>
                   
					
				<h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                Patient Data
                            </div>
                        <div class="ui-datatable-tablewrapper">
                           <asp:GridView ID="grSearchPatient" runat="server" AutoGenerateColumns="False" Width="1060" AllowPaging="True" AllowSorting="True"  
                               CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Is There">
                               <AlternatingRowStyle BackColor="White" />
            <Columns>                               
                  <asp:BoundField DataField="OPDNO" HeaderText="OPD&nbsp;NO" />
                <asp:BoundField DataField="IPDNO" HeaderText="IPD&nbsp;NO" />
              <asp:BoundField DataField="NAME" HeaderText="NAME" /> 
                <asp:BoundField DataField="PHONE" HeaderText="PHONE" />

                <asp:BoundField DataField="DATEOFADMISSION" HeaderText="ADMISSION DATE" />
                <asp:BoundField DataField="DATEOFDISCHARGE" HeaderText="DISCHARGE DATE" />
               <asp:TemplateField HeaderText="SELECT">
            <ItemTemplate >
                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openpopup();" OnClick="LinkButton1_Click">VIEW&nbsp;FINAL&nbsp;BILL</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField> 

                <%--<asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="False" />--%>
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
       <br />
                                <br />
                                <br />
                                <br />
                                 <br />
                                <br />

        </div>
    </div>
</div>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

