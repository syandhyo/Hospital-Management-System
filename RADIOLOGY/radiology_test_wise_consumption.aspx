<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="radiology_test_wise_consumption.aspx.cs" Inherits="RADIOLOGY_radiology_test_wise_consumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function onlyNos(e, t) {
             try {
                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 return true;
             }
             catch (err) {
                 alert(err.Description);
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
 <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Master <span> / </span> Test Wise Consumption 
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
            <asp:HiddenField ID="hdndel" runat="server" />
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Test Wise Consumption </u></b></h1>
               
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>
                                <!---- Radiology Id  :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Radiology Id  :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtAutoid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>
                                
                            </div>
                             <div class="ui-grid-row">
                                <!---- Radiology Test Name:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Radiology Test &nbsp;Name:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="droptesname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" width="180"></asp:DropDownList>
                                </div>
                             </div>
                             <br />
                            <hr />
                            <br />
                            <h1 style="color:#0071bc;"><b><u>Item Details</u></b></h1>
                            <br />
                             <div class="ui-grid-row">
                                <!---- Material Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Material Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtmaterialnm" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -0-9]+$" MaxLength="50"></asp:TextBox>
                                </div>
                              </div>
                            <div class="ui-grid-row">
                                <!---- Quantity ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtquant" runat="server" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="4"></asp:TextBox>
                                </div>
                                  <!---- Button:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnadd" runat="server" CssClass="search" Text="Add" OnClick="btnadd_Click"/>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   
                                </div>
                           </div>
                        </div>
                   </div>
				<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" OnRowDeleting="GridView1_RowDeleting" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
           <Columns>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Quantity" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblqty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>    
            <asp:CommandField ShowDeleteButton="True"  HeaderText="ACTION"/>
       <%-- <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" />
            </ItemTemplate>
        </asp:TemplateField>--%>

    </Columns>
                <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                  <br/>
                <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" style="margin-left:5px;"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="create" Visible="False" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <div class="ui-datatable-tablewrapper">
                                <asp:GridView ID="grvtestCons" runat="server" Width="1060" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True"  GridLines="None" OnPageIndexChanging="grvtestCons_PageIndexChanging" OnSelectedIndexChanging="grvtestCons_SelectedIndexChanging" OnRowDataBound="grvtestCons_RowDataBound" OnRowDeleting="grvtestCons_RowDeleting" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" />
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="DATE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="TEST&nbsp;NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblcdnme" runat="server" Text='<%#Eval("RADTESTNAME")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="ACTION" />

    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
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
        <asp:GridView ID="grvhidden" runat="server" Visible="False" Width="1060" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
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
              </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

