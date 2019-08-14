<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_gate_pass.aspx.cs" Inherits="STOREKEEPER_store_gate_pass" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Gate Pass
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Gate Pass</u></b></h1>
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----Vendor :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Gate Pass Type&nbsp; :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:DropDownList ID="dropward" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"> 
                            <asp:ListItem>please Select </asp:ListItem>           
                            <asp:ListItem>Inward</asp:ListItem>
                            <asp:ListItem>Outward</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!----Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                    </div>
                </div>  
                  <div class="ui-grid-row">
                    <!----Bill No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Bill No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtbillno" runat="server" Text="" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*" MaxLength="10" MinLength="6"></asp:TextBox>
                    </div>
                    <!----Security Name :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Security Name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:TextBox ID="txtname" runat="server" pattern="^[A-Za-z ]+$" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="40" MinLength="3"></asp:TextBox>
                    </div>
                </div>  
                 </div>
                     </div>
                
                   <br><br>
                 <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="Button1_Click" Text="Submit" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br><br>
						 <hr>
						 <br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              GATE PASS
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                         <AlternatingRowStyle BackColor="White" />
            <Columns> 
                <asp:BoundField DataField="TYPE" HeaderText="TYPE" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="BILL_NO" HeaderText="BILL NO" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="SECURITY_NAME" HeaderText="NAME" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center">                                  
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:CommandField>
            </Columns>
              <SelectedRowStyle BackColor="#0071bc" ForeColor="#333333" />
                                         <EditRowStyle BackColor="#2461BF" />
              <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
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
    </div>
                              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

