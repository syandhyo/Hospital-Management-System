<%@ Page Title="" Language="C#" MasterPageFile="~/ASSET/MasterPage.master" AutoEventWireup="true" CodeFile="Indent.aspx.cs" Inherits="ASSET_Indent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript">

            function Validate() {

               if (document.getElementById("<%=txtMaterial.ClientID%>").value == "") {
                 alert("Material Field Is Required !");
                 document.getElementById("<%=txtMaterial.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtqty.ClientID%>").value == "") {
                 alert("Quantity Field Is Required !");
                 document.getElementById("<%=txtqty.ClientID%>").focus();
                return false;
           }
            if (document.getElementById("<%=txtUnit.ClientID%>").value == "") {
                 alert("Unit Field Is Required !");
                 document.getElementById("<%=txtUnit.ClientID%>").focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">

        function ValidateCret() {

           if (document.getElementById("<%=txtdate.ClientID%>").value == "") {
               alert("Date Field Is Required !");
               document.getElementById("<%=txtdate.ClientID%>").focus();
               return false;
            }
        }
    </script>
     <script type="text/javascript">

         function SetTarget() {

             document.forms[0].target = "_blank";

         }
            </script>
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
                      $("[id$=txtdate]").datepicker({
                          dateFormat: 'dd-mm-yy',
                          showOn: 'button',
                          buttonImageOnly: true,
                          dateFormat: 'dd-mm-yy',
                          changeMonth: true,
                          changeYear: true,
                          maxDate: '0',

                          yearRange: "c-75:c+10",
                          buttonImage: '../images/calendar.png'

                      });
                  }
                </script>
             <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
            <script type="text/javascript">
                Sys.Application.add_load(function () {
                    $("[id$=txtMaterial]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("~/ASSET/Indent.aspx/GetCustomers") %>',
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Indent
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
            <asp:Label ID="lblgst" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblsgst" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblcgst" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblhsncode" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Indent</u></b></h1>
       <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
             <div class="ui-grid-row">
                    <!----Date :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Date :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label> IndentNumber :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtrfqid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false" Text=""></asp:TextBox>     
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Vendor :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>From Department :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="dropvendor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" TabIndex="1">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                    </div>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>To Department :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="ddltodept" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" TabIndex="2">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                    </div>
                </div>
             <div class="ui-grid-row">
                    <!----Vendor :----->
                 
                     
                </div>
					<br><br>
					    <hr>
				  <br>
             <div class="ui-grid-row">
                    <!----Item Name : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>Item Name :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" placeholder="Item" pattern="^[A-Za-z -]+$" ToolTip="Enter Item Here" AutoPostBack="true" OnTextChanged="txtMaterial_TextChanged" TabIndex="2"></asp:TextBox>
                    </div>
                 <!----Unit :---->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                         <label class="ui-outputlabel ui-widget">
                        Unit :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                      <asp:TextBox ID="txtUnit" runat="server" placeholder="Unit" ToolTip="Enter Unit Here" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*" TabIndex="3"></asp:TextBox>         
                    </div>
                     <!----Quantity : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Quantity :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtqty" runat="server" placeholder="Quantity" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Enter Quantity Here" AutoPostBack="true" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" TabIndex="4"></asp:TextBox>
                    </div>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Button ID="btnAdd" runat="server" OnClientClick="return Validate();" CssClass="search" Text="ADD" OnClick="btnAdd_Click" TabIndex="5"/>
                    </div>
                </div>
           <%-- <div class="ui-grid-row">
                
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                               
                    </div>
                </div>--%>
            </div>
           </div>
            <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="False" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grdMaterial_RowDeleting" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl No" ItemStyle-HorizontalAlign ="Center">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item&nbsp;Name" ItemStyle-HorizontalAlign ="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign ="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Unit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign ="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Qty" runat="server" Text='<%#Eval("QTY")%>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                 
              
                        <asp:CommandField ShowDeleteButton="True" HeaderText="Action" />
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
				<br><br><br>
             
                  <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" OnClientClick="return ValidateCret();" TabIndex="6"/>
                        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update" Visible="False" OnClick="btnupdate_Click" />
                        &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="create" Visible="False" OnClick="btndelete_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click" />
                         <br><br>
						 <hr>
						 <br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <asp:Label ID="lblEditgrd" Visible="false" runat="server" Text="Label"></asp:Label>
                                <asp:TextBox ID="txtContrtno" runat="server" Visible="false"></asp:TextBox>
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            REQUEST FOR QUOTATION
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="grdrfq" runat="server" AllowPaging="True" Width="1060" AllowSorting="True" DataKeyNames="INDH_NUM" AutoGenerateColumns="False" CellPadding="4" OnPageIndexChanging="grdrfq_PageIndexChanging"
                                OnSelectedIndexChanging="grdrfq_SelectedIndexChanging" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="DATE" HeaderText="DATE" />
                                    <asp:BoundField DataField="INDH_NUM" HeaderText="NUMBER" />
                                    <asp:BoundField DataField="DEPT_From" HeaderText="FROM DEPT" />                                    
                                     <asp:BoundField DataField="DEPT_TO" HeaderText="TO DEPT" />  
                                    <asp:CommandField HeaderText="ACTION" SelectText="&nbsp;&nbsp;&nbsp;Edit" ShowSelectButton="true" />
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

