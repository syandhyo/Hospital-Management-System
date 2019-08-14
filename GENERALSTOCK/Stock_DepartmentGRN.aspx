<%@ Page Title="" Language="C#" MasterPageFile="~/GENERALSTOCK/StockMaster.master" AutoEventWireup="true" CodeFile="Stock_DepartmentGRN.aspx.cs" Inherits="GENERALSTOCK_Stock_DepartmentGRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function Validate() {

             if (document.getElementById("<%=txtgrnDate.ClientID%>").value == "") {
                 alert("Date Field Is Required !");
                 document.getElementById("<%=txtgrnDate.ClientID%>").focus();
                 return false;
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
              <script type = "text/javascript">

                  function SetTarget() {

                      document.forms[0].target = "_blank";

                  }
        </script>
              <script type="text/javascript">

                  $(function () {
                      SetDatePicker1();
                  });

                  //On UpdatePanel Refresh.
                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                  if (prm != null) {
                      prm.add_endRequest(function (sender, e) {
                          if (sender._postBackSettings.panelsToUpdate != null) {
                              SetDatePicker1();
                          }
                      });
                  };

                  function SetDatePicker1() {
                      $("[id$=txtgrnDate]").datepicker({
                          dateFormat: 'dd-mm-yy',
                          showOn: 'button',
                          buttonImageOnly: true,
                          dateFormat: 'dd-mm-yy',
                          changeMonth: true,
                          changeYear: true,
                          //  maxDate: '0',

                          yearRange: "c-75:c+10",
                          buttonImage: '../images/calendar.png'

                      });
                  }
    </script>
            
                              <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Good Receive Note
                    </div>

                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
               <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
             <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblGRNNoS" Visible="false" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Good Receive Note</u></b></h1>
				
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                            <div class="ui-grid-row">
                                <!----Issue No. : ----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                    Issue No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblminno"  runat="server" Text="label"></asp:Label>
                                </div>
                                <!---- GRN No. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>GRN No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                 <asp:TextBox ID="txtgrnno" runat="server" ReadOnly="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="ui-grid-row">
                                <!----Issue Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                    Issue Date :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblmindate"  runat="server" Text="label"></asp:Label>
                                </div>
                               <!----Received Person :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                    GRN Date  :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtgrnDate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>
                            </div>
                        
				 <div class="ui-grid-row">
                        <!----Request No. :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label class="ui-outputlabel ui-widget">
                             Request No. :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:Label ID="lblMrno"  runat="server" Text="label"></asp:Label>
                        </div>
                        <!----Unit :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Department Name :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:DropDownList ID="ddDeptname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" Enabled="false">
             <asp:ListItem></asp:ListItem>
         </asp:DropDownList> 
                            <asp:HiddenField ID="hdnIssuefrom" runat="server" />
                        </div>
                    </div>
                    
                 </div>
            </div>
            <asp:GridView ID="grvDeptGrnItm" runat="server" AutoGenerateColumns="False"   AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None"  >
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                <%-- <asp:BoundField DataField="SL" HeaderText="SNo" />--%>
             
             
                <%--<asp:CommandField ShowDeleteButton="True" />--%>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MR&nbsp;ITEM&nbsp;NAME">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QUANTITY">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_qunty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UNIT">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txt_desc" runat="server"  TextMode="MultiLine" Text='<%#Eval("DESC")%>'></asp:TextBox>--%>
                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             
             
            </Columns>
             <EditRowStyle BackColor="#2461BF" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#EFF3FB" />
             <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
            <br />
            <hr />
            <br />
              <asp:Button ID="btncreate" runat="server" Text="Accept" CssClass="create" OnClick="btncreate_Click" OnClientClick="return Validate();"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel"  />
            <br />
                          
        </div>
    </div>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

