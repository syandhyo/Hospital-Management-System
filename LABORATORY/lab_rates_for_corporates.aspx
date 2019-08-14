<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/Lab_MasterPage.master" AutoEventWireup="true" CodeFile="lab_rates_for_corporates.aspx.cs" Inherits="LABORATORY_lab_rates_for_corporates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                  $(function () {
                      SetDatePicker();
                  });

                  //On UpdatePanel Refresh.
                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                  if (prm != null) {
                      prm.add_endRequest(function (sender, e) {
                          if (sender._postBackSettings.panelsToUpdate != null) {
                              SetDatePicker();
                          }
                      });
                  };
                  function SetDatePicker() {
                      $("[id$=Txtdate]").datepicker({
                          dateFormat: 'dd-mm-yy',
                          showOn: 'button',
                          buttonImageOnly: true,
                          dateFormat: 'dd-mm-yy',
                          changeMonth: true,
                          changeYear: true,
                          minDate: '0',

                          yearRange: "c-75:c+10",
                          buttonImage: '../images/calendar.png'

                      });
                  }
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Rates For Corporates
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
        
                <h1 style="color:#0071bc;"><b><u>Pathology Rates For Corporates</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Corporates :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Corporates :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="Ddlincrnce" AutoPostBack="false" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"></asp:DropDownList>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Test Type :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Test Type :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="ddltesttype" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="ddltesttype_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="ui-grid-row">
                                <!---- Apply From :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Apply From :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="Txtdate" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" Enabled="false" autocomplete="off" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>
                            </div>
                            
                            </div>
                    </div>

      
            <br />
 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" GridLines="None" ForeColor="#333333">
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:TemplateField HeaderText="TEST ID" Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lblid" runat="server" Text='<%#Eval("slno")%>' ></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

               <asp:TemplateField HeaderText="TEST NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>  
        <asp:TemplateField HeaderText="RANGE&nbsp;&nbsp;" >
            <ItemTemplate>
                <asp:Label ID="lblrange" runat="server" Text='<%#Eval("RANGE")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>
             <asp:TemplateField HeaderText="UNIT&nbsp;&nbsp;" >
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>
               <asp:TemplateField HeaderText="PRICE&nbsp;&nbsp;" >
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>
          <asp:TemplateField HeaderText="CORPORATE PRICE" Visible="true" >
            <ItemTemplate>
                <asp:TextBox ID="txtprice" runat="server" Text='0.00' pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
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
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" GridLines="None" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:TemplateField HeaderText="TEST ID" Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lblid1" runat="server" Text='<%#Eval("ID")%>' ></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>
         <asp:TemplateField HeaderText="TEST NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname1" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv1" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>  
        <asp:TemplateField HeaderText="RANGE&nbsp;&nbsp;" >
            <ItemTemplate>
                <asp:Label ID="lblrange1" runat="server" Text='<%#Eval("REF")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>
             <asp:TemplateField HeaderText="UNIT&nbsp;&nbsp;" >
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

          <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <asp:TextBox ID="txtprice1" runat="server" Text='<%#Eval("PRICE")%>' pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
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
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" style="margin-left:5px;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" OnClick="btnupdate_Click"  Visible="False" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="create" OnClick="btndelete_Click" OnClientClick="return DeleteItem()" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                              Corporate History
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView2" runat="server" Width="1060" AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView2_PageIndexChanging" CssClass="table table-bordered" CellPadding="4" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="CORPORATE" HeaderText="CORPORATES" />
                  <asp:BoundField DataField="CATAGORY" HeaderText="TYPE OF TEST" />
                <asp:BoundField DataField="APPLY_DATE" HeaderText="APPLY FROM" DataFormatString="{0:dd-MM-yyyy}" />
              
                <asp:CommandField  ShowSelectButton="true" ShowDeleteButton="false" HeaderText="ACTION"/>
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
</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

