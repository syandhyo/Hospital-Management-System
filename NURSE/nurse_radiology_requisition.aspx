<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_radiology_requisition.aspx.cs" Inherits="NURSE_nurse_radiology_requisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <script type="text/javascript">
                Sys.Application.add_load(function () {
                    $("[id$=txtipdno]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("~/NURSE/nurse_radiology_requisition.aspx/GetCustomers") %>',
                                      data: "{ 'prefix': '" + request.term + "'}",
                                      dataType: "json",
                                      type: "POST",
                                      contentType: "application/json; charset=utf-8",
                                      success: function (data) {
                                          response($.map(data.d, function (item) {
                                              return {
                                                  label: item.split('/ \s*/')[0]
                                              }
                                          }))
                                      },
                                      error: function (response) {
                                          alert(response.responseText);
                                      },
                                      failure: function (response) {
                                          alert(response.responseText);
                                      }
                                  });
                              },
                              select: function (e, i) {
                                  $("[id$=hfCustomerId]").val(i.item.val);
                              },
                              minLength: 1
                          });
                      });
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                       <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Radiology Requisition
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
            <asp:Label ID="LBLUHID" runat="server"  Visible="false"></asp:Label>
            <asp:Label ID="lblbed" runat="server" Visible="false"></asp:Label>
             <asp:Label ID="LBPAIDAMT" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
				 <h1 style="color:#203a5a;"><b><center>RADIOLOGY REQUISATION</center></b>
                     <h1></h1>
                     <hr/>
                     <h1 style="color:#0071bc;"><b><u>IPD Search</u></b></h1>
                     <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                         <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                             <!---- OPD----->
                             <div class="ui-grid-row">
                                 <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">
                                     <asp:Label ID="Label11" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>IPD No :</label>
                                    
                                 </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtipdno" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtipdno_TextChanged" pattern="^[a-zA-Z0-9 ]*"></asp:TextBox>
                                     <asp:HiddenField ID="hfCustomerId" runat="server" />
                                 </div>
                             </div>
                             <!----  NAME----->
                             <div class="ui-grid-row">
                                 <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">&nbsp;NAME :</label>
                                     
                                 </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="6" MinLength="6" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                 </div>
                             </div>
                         </div>
                     </div>
                     <br/>
                     <br/>
                     <hr/>
                     <div id="div6" runat="server" style="border: thin solid #000000" visible="false">
                         <div id="Div7" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                             <div id="Div8" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                 <!---- Corporate----->
                                 <div class="ui-grid-row">
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Corporate :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:DropDownList ID="dropCorprt" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                                 <!---- Emp Id----->
                                 <div class="ui-grid-row">
                                     <div class="ui-panelgrid-cell ui-grid-col-2">
                                         <label class="ui-outputlabel ui-widget">
                                         Emp Id :</label>
                                     </div>
                                     <div class="ui-panelgrid-cell ui-grid-col-4">
                                         <asp:TextBox ID="txtEmpid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                     <br/>
                     <div id="Div1" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                         <div id="Div2" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                             <div class="ui-grid-row">
                                 <!----  Search Item :----->
                                 <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <asp:CheckBox ID="chkPackage" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnCheckedChanged="chkPackage_CheckedChanged" ValidationGroup="a" />
                                     <label class="ui-outputlabel ui-widget">
                                     Select Package</label>
                                 </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:DropDownList ID="dropPackage" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="dropPackage_SelectedIndexChanged" Visible="false" Width="180">
                                     </asp:DropDownList>
                                 </div>
                                 <!----  Button----->
                                 <div class="ui-panelgrid-cell ui-grid-col-2">
                                 </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                 </div>
                             </div>
                             <!-- -->
                             <div class="ui-grid-row">
                                 <!---- Company:----->
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:CheckBox ID="chkTestType" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnCheckedChanged="chkTestType_CheckedChanged" ValidationGroup="a" />
                                     <label class="ui-outputlabel ui-widget">
                                     Select Test Type</label>
                                 </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:DropDownList ID="dropTestype" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="dropTestype_SelectedIndexChanged" Visible="false" Width="180">
                                     </asp:DropDownList>
                                 </div>
                                 <!----  Item Name :----->
                                 <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">
                                     </label>
                                 </div>
                                 <div class="ui-panelgrid-cell ui-grid-col-4">
                                 </div>
                             </div>
                         </div>
                     </div>
                     <div id="divinner2" runat="server" visible="false">
                         <hr />
                         <table width="100%">
                             <tr>
                                 <td align="right" style="width:10%">
                                     <asp:HiddenField ID="hdntype" runat="server" />
                                 </td>
                                 <td align="center" style="width:60%">
                                     <asp:GridView ID="grdtestype" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IDD" ForeColor="#333333" GridLines="None" Width="1060px">
                                         <AlternatingRowStyle BackColor="White" />
                                         <Columns>
                                             <asp:TemplateField HeaderText="INVESTIGATION" Visible="true">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PRICE" Visible="true">
                                                 <ItemTemplate>
                                                     <%--<asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                                                     <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Select">
                                                 <ItemTemplate>
                                                     <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" />
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
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
                                 </td>
                                 <td align="right" style="width:20%"></td>
                             </tr>
                         </table>
                         <div id="div3" runat="server">
                             <div id="Div4" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                                 <div id="Div5" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                                     <!---- Price :----->
                                     <div class="ui-grid-row">
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             Price :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
                                         </div>
                                     </div>
                                     <!----Total Discount :----->
                                     <div class="ui-grid-row">
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             Total Discount :</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:TextBox ID="txtdisc" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtdisc_TextChanged" Text="0" Width="60px"></asp:TextBox>
                                         </div>
                                     </div>
                                     <!---- Total Amount:----->
                                     <div class="ui-grid-row">
                                         <div class="ui-panelgrid-cell ui-grid-col-2">
                                             <label class="ui-outputlabel ui-widget">
                                             Total Amount:</label>
                                         </div>
                                         <div class="ui-panelgrid-cell ui-grid-col-4">
                                             <asp:Label ID="lbltotalamt" runat="server" style="color: #D20000; font-weight: 700" Text="0"></asp:Label>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                     <br/>
                     <br/>
                     <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="btncreate_Click" style="margin-left:5px;" Text="Create" />
                     <asp:Button ID="btnupdate" runat="server" CssClass="update" style="margin-left:5%;" Text="Update" Visible="False" />
                     &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                     <h1>&nbsp;</h1>
                     <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                         <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             RADIOLOGY REQUISITION
                         </div>
                         <div class="ui-datatable-tablewrapper">
                             <asp:GridView ID="grdpackge" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grdpackge_PageIndexChanging" OnSelectedIndexChanging="grdpackge_SelectedIndexChanging" Width="1060">
                                 <AlternatingRowStyle BackColor="White" />
                                 <Columns>
                                     <%-- <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" />  
            </ItemTemplate>
        </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                         <ItemTemplate>
                                             <%#Container.DisplayIndex+1 %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="ID" HeaderText="RadiologyNo" />
                                     <asp:TemplateField HeaderText="PATIENT&nbsp;NAME">
                                         <ItemTemplate>
                                             <asp:Label ID="lblPaname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="TOTAL&nbsp;AMOUNT" Visible="true">
                                         <ItemTemplate>
                                             <asp:Label ID="lblinv" runat="server" Text='<%#Eval("TOTAMT")%>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DATE" Visible="true">
                                         <ItemTemplate>
                                             <asp:Label ID="lblprice" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                                             <%--  <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>' ></asp:TextBox>--%>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="PRINT">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                 <EditRowStyle BackColor="#2461BF" />
                                 <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                 <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                 <RowStyle BackColor="#EFF3FB" />
                                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="white" />
                                 <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                 <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                 <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                 <SortedDescendingHeaderStyle BackColor="#4870BE" />
                             </asp:GridView>
                         </div>
                       
                     </div>
                     <h1></h1>
                  
                     <h1></h1>
                     <h1></h1>
                     </h1>
        </div>
                    </strong>
                    </label>
                    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

