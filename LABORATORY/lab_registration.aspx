<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/Lab_MasterPage.master" AutoEventWireup="true" CodeFile="lab_registration.aspx.cs" Inherits="LABORATORY_lab_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $("[id$=txtipid]").autocomplete({
                source: function (request, response) {
                    
                 
                    $.ajax({
                        url: '<%=ResolveUrl("~/LABORATORY/lab_registration.aspx/GetIp") %>',
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
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $("[id$=txtopid]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/LABORATORY/lab_registration.aspx/GetOp") %>',
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                 <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Test Registration
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:TextBox ID="txtbranch" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
         <div class="card card-w-title">
        
                <h1 style="color:#0071bc;"><b><u>Test Registration</u></b></h1>
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                            <div class="ui-grid-row">
                                <!----  Date:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Date : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
                                </div>
                                  <!----  Time:----->
                                <div class="ui-panelgrid-cell ui-grid-col-3">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="lbltime" runat="server" ForeColor="Red" Text="*"></asp:Label>Time : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txttime" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                 <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Patient Type : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4" >
                                    <asp:DropDownList ID="droptype" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="droptype_SelectedIndexChanged">
                                       
                                        <asp:ListItem Value="InPatient">InPatient</asp:ListItem>
                                        <asp:ListItem Value="OutPatient">OutPatient</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-3">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="lblpid" runat="server" ForeColor="Red" Text="*"></asp:Label><asp:Label ID="lblip" runat="server" Text="Patient Id(IP) :"></asp:Label><asp:Label ID="lblop" runat="server" Text="Patient Id(OP) :" Visible="false"></asp:Label> </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtipid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtipid_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:TextBox ID="txtopid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="false" OnTextChanged="txtopid_TextChanged" AutoPostBack="true"></asp:TextBox>
                                     <asp:HiddenField ID="hfCustomerId" runat="server" />
                                </div>
                               

                            </div>
                             <div class="ui-grid-row">
                                  <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Patient Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtpname" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" ></asp:TextBox>
                                </div>
                                  <!---- Sent To Lab :----->
                                <div class="ui-panelgrid-cell ui-grid-col-3">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Address And Demographic :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <%--<textarea id="txtAddr" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></textarea>--%>

                                    <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="172"></asp:TextBox>
                                </div>
                                 </div>
                              <div class="ui-grid-row">
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="lblrefdoc" runat="server" ForeColor="Red" Text="*"></asp:Label>Reference By Doctor : </label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="droprefdoc" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="droprefdoc_SelectedIndexChanged"></asp:DropDownList><asp:TextBox ID="txtrefdoc" runat="server" autocomplete="off" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="false"></asp:TextBox>
                                </div>
                                  <div class="ui-panelgrid-cell ui-grid-col-3">
                                      <asp:CheckBox ID="checkdoc" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnCheckedChanged="CheckBox1_CheckedChanged"/>Doctor Outside
                                      </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                 
                            </div>
                            </div>
                      </div>
                      </div>
              <br/>
                     <hr/>
                    
                        
                         <div id="Div1" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="Div2" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                             <div class="ui-grid-row">
                                <!---- Company:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                   
                                    <label class="ui-outputlabel ui-widget">Select Test Type</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropTestype" runat="server" AutoPostBack="true" Width="180"
                                        OnSelectedIndexChanged="dropTestype_SelectedIndexChanged" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" >
                                    </asp:DropDownList>
                                </div>
                              
                                

                            </div>
                             </div>
                            </div>
                        
			   <br/>
             <div id="divinner2" visible="false" runat="server" >
                         <hr />
        <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:60%" align="center"> 
       <asp:GridView ID="grdtestype" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID" CellPadding="4"  GridLines="None" ForeColor="#333333">
           <AlternatingRowStyle BackColor="White" />
           <Columns>
                <%--<asp:TemplateField Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lbltype" Visible="false" runat="server" Text='<%#Eval("TESTYPE")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>--%>
               <asp:TemplateField HeaderText="TEST&nbsp;NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>' ></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged"/>
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
      
    </td>
    <td style="width:20%" align="right"></td>   
</tr>
</table>   
            <div id="div3"  runat="server" >
                <hr />
                <div id="Div4" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="Div5" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!---- Price :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Price :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:Label ID="lbltotalprice" runat="server" Text="0"></asp:Label>
               
                         </div>
                        </div>
                      <!----Total Discount :-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Total Discount(%) :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdisc" runat="server" OnTextChanged="txtdisc_TextChanged" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="60px" AutoPostBack="true" Text="0"></asp:TextBox>
             
                         </div>
                        </div>
                     <!---- Total Amount:-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Total Amount:</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:Label ID="lbltotalamt" runat="server" style="color: #D20000; font-weight: 700" Text="0" ></asp:Label>
                         </div>
                        </div>
                     </div>
                   </div>
                
             
</div>

</div><br/> 
             	 
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" style="margin-left:5px;" OnClick="btncreate_Click"/>
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click"/>
        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">Test Registration</div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grvtest" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" AllowPaging="True"  PageSize="2"  GridLines="None" ForeColor="#333333">
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
               <asp:TemplateField HeaderText="TIME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbltime" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="PATIENT ID" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblpid" runat="server" Text='<%#Eval("PATIENTYPE")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />

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

