<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="deptstock_material_receive.aspx.cs" Inherits="GENERALSTOCK_deptstock_material_receive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 
      <asp:Timer ID="Timer1" runat="server" Interval="1000" ></asp:Timer>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
   <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Dept Stock <span> / </span>Material Receive
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid">
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
             <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Material Receive</u></b></h1>
                   <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
							MATERIAL RECEIVE
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdDeptgrn" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" OnSelectedIndexChanging="grdDeptgrn_SelectedIndexChanging" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
               
                <asp:BoundField DataField="MRNO" HeaderText="MR&nbsp;NUMBER" />
                <asp:BoundField DataField="MINDATE" HeaderText="DATE" DataFormatString="{0:MM/dd/yyyy}"/> 
                <asp:BoundField DataField="DeptName" HeaderText="DEPARTMENT&nbsp;NAME" />
              
               <%-- <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Select" Enabled="false" Height="35px" ImageUrl='<%# Eval("STATUS") %>' Width="110px" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" HeaderText="ACTION"/>
            </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
              </ContentTemplate>
     <Triggers>
              <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
          </Triggers>
     </asp:UpdatePanel>

</asp:Content>

