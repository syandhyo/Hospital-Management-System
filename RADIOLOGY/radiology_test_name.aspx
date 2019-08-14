<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="radiology_test_name.aspx.cs" Inherits="RADIOLOGY_radiology_test_name" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Master <span> / </span> Test Name
                    </div>
    
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
             <asp:Label ID="lblsession" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Test Name</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Select Category :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Category :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Dropcategory" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="false">
                                     </asp:DropDownList>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!---- Investigation Desired :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Investigation &nbsp;&nbsp;Desired:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtinv" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z A-Z_]+"></asp:TextBox>
                                </div>
                               
                            </div>
                            <div class="ui-grid-row">
                                 <!---- Price  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Price :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtprice" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="6"  pattern="[0-9]+([,\.][0-9]+)?" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                                </div>
                                 <!---- Price  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="search" OnClick="btnadd_Click" />
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
            <asp:GridView ID="grvStudentDetails" runat="server" 
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" 
                GridLines="None" OnRowDataBound="GVOnRowDataBound" OnRowDeleting="OnRowDeleting" >
    <Columns>
       <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
         <asp:TemplateField HeaderText="Sno">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_slno" runat="server" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Investigation Desired">
            <ItemTemplate>
               <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Price">
            <ItemTemplate>
                <asp:Label ID="lbl_price" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
                 <%--<asp:TextBox  ID="txt_qty" runat="server" Width="50" OnTextChanged="txt_qty_TextChanged"  CssClass="GridTextBox" AutoPostBack="true" Text='<%#Eval("QTY")%>'></asp:TextBox>--%>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField>
            <FooterStyle HorizontalAlign="Right" />
            <FooterTemplate>
                
                <%--<asp:Button ID="ButtonAdd" runat="server" 
                        Text="Add New Row" OnClick="ButtonAdd_Click" BackColor="SteelBlue" ForeColor="White"/>--%>
                 
            </FooterTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>

           <asp:GridView ID="GridView1" runat="server" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None">
               <AlternatingRowStyle BackColor="White" />
               <EditRowStyle BackColor="#2461BF" />
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
               <SortedDescendingCellStyle BackColor="#E9EBEF" />
               <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
             	
                 <br/>
				   <hr/>
				
                  <br/>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" style="margin-left:5px;" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                TEST NAME
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" DataKeyNames="ID" 
              OnRowDeleting="GridView2_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" 
              OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION"/>
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

