<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="radiology_test_type.aspx.cs" Inherits="RADIOLOGY_radiology_test_type" %>

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
                        <i class="fa fa-home"></i><span>/ </span>Master <span> / </span> Test Type
                    </div>
    
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
             <asp:Label ID="lblsession" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Procedure Entry</u></b></h1>
					
                   <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----Procedure Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Procedure Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                </div>
                              </div>
                            </div>
                       </div>
                 <br>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" style="margin-left:5px;" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                          <br /><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                    PROCEDURE ENTRY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="NAME" HeaderText="NAME" HeaderStyle-CssClass="text-center">
                                        <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="ACTION" HeaderStyle-CssClass="text-center">
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
                                </div><br/>

</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

