<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/Lab_MasterPage.master" AutoEventWireup="true" CodeFile="lab_Optestresult.aspx.cs" Inherits="LABORATORY_lab_Optestresult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type = "text/javascript">

        function SetTarget() {

            document.forms[0].target = "_blank";

        }
        </script>
    <script type="text/javascript">
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> 	OP test result
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
        <div class="card card-w-title"><br/>
        <h1 style="color:#0071bc;"><b><u>Outside Lab Entry</u></b></h1>
                  <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  OPD Number :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">OPD Number :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtopdno" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                </div>

                                <!---- Requisition  No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Requisition  No :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtreqno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>

                                <!---- Datetime :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Datetime :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:TextBox ID="txtinvdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>

                            </div>
                          </div>
                      </div>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" GridLines="None" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
           <Columns>
                 <asp:TemplateField HeaderText="" Visible="false" >
            <ItemTemplate>
               <%#Container.DisplayIndex+1 %>

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

               <asp:TemplateField HeaderText="RANGE" >
            <ItemTemplate>
                <asp:TextBox ID="txtref" runat="server" Text='<%#Eval("RANGE")%>'></asp:TextBox>
            </ItemTemplate>
       </asp:TemplateField>
                   <asp:TemplateField HeaderText="UNIT" >
            <ItemTemplate>
                <asp:TextBox ID="txtunit" runat="server" Text='<%#Eval("UNIT")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>

        <asp:TemplateField HeaderText="TESTVALUE" >
            <ItemTemplate>
                <asp:TextBox ID="txt_value" runat="server" Text="0" TextMode="MultiLine"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
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
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" GridLines="None" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
           <Columns>
                <asp:TemplateField HeaderText="" Visible="false" >
            <ItemTemplate>
                <%#Container.DisplayIndex+1 %>

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
          
               <asp:TemplateField HeaderText="RANGE" >
            <ItemTemplate>
                <asp:TextBox ID="txtref" runat="server" Text='<%#Eval("RANGE")%>'></asp:TextBox>
            </ItemTemplate>
       </asp:TemplateField>
                   <asp:TemplateField HeaderText="UNIT" >
            <ItemTemplate>
                <asp:TextBox ID="txtunit" runat="server" Text='<%#Eval("UNIT")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="TESTVALUE" >
            <ItemTemplate>
                <asp:TextBox ID="txt_value" runat="server" Text='0'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
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

      <asp:GridView ID="Gvwidaltest" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="INV" ForeColor="#333333" GridLines="None">
           <AlternatingRowStyle BackColor="White" />
           <Columns>
         <asp:TemplateField HeaderText="WIDAL&nbsp;TEST&nbsp;INVESTIGATIONS" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinvw" runat="server" Text='<%#Eval("INVES")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
          <asp:TemplateField HeaderText="1:140" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice0" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="txt140" runat="server" Text='<%#Eval("A140")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="1:160" >
            <ItemTemplate>
                <asp:TextBox ID="txt160" runat="server" Text='<%#Eval("A160")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
                   <asp:TemplateField HeaderText="1:180" >
            <ItemTemplate>
                <asp:TextBox ID="txt180" runat="server" Text='<%#Eval("A180")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
                <asp:TemplateField HeaderText="1:320" >
            <ItemTemplate>
                <asp:TextBox ID="txt320" runat="server" Text='<%#Eval("A320")%>'></asp:TextBox>
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
            <br/><br/> 
             	 
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click"  />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"   Visible="False" OnClick="btnupdate_Click" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="create"  OnClientClick="return DeleteItem()"  Visible="False" OnClick="btndelete_Click" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"  />
                         <br/><br/><br/>
            
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             	Test results
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                 <asp:BoundField DataField="ID" HeaderText="ID" />
                                                 <asp:BoundField DataField="DATE" HeaderText="DATE" dataformatstring="{0:dd-Mm-yyyy  HH:mm}"/>
                                                <asp:BoundField DataField="PID" HeaderText="PID" />
                                                  <asp:BoundField DataField="NAME" HeaderText="NAME" />
                
                                                <%--<asp:CommandField  ShowSelectButton="true" ShowDeleteButton="false" HeaderText="ACTION"/>--%>
                                              <%--<asp:TemplateField HeaderText="PRINT">
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="SetTarget();" OnClick="LinkButton2_Click">Print</asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>--%>
                                            </Columns>
                                             <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                              <EditRowStyle BackColor="#2461BF" />
                                              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
    <//div>
        </strong>
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

