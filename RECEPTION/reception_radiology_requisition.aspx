<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_radiology_requisition.aspx.cs" Inherits="RECEPTION_reception_radiology_requisition" %>

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
                       <i class="fa fa-home"></i><span>/ </span> Out Patient <span> / </span> Radiology Requisition
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
        </div>
        <div class="card card-w-title">
				 <h1 style="color:#203a5a;"><b><center>RADIOLOGY BILL ENTRY</center></b>
                     <h1></h1>
                     <hr/>
                     <h1 style="color:#0071bc;"><b><u>OPD Search</u></b></h1>
                     
                     <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!---- OPD-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>OPD No :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtopdno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                               OnTextChanged="txtopdno_TextChanged" AutoPostBack="true" pattern="[a-zA-Z0-9_]+" MaxLength="6" minLength="6" title="Please Enter A Valid OPD Number" > </asp:TextBox>
               
                         </div>
                        </div>
                      <!----  NAME-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">NAME :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$" MaxLength="6" MinLength="6"></asp:TextBox>
             
                         </div>
                        </div>
                     </div>
                   </div>
                     <br/>
                     <br/>
                     <hr/>
                     <div id="div6" visible="false" runat="server" style="border: thin solid #000000">
                         <div id="Div7" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                         <div id="Div8" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <!---- Corporate-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Corporate :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:DropDownList ID="dropCorprt" runat="server" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                Width="180"  >
                            </asp:DropDownList>
                         </div>
                        </div>
                      <!---- Emp Id-----> 
                        <div class="ui-grid-row">
                          <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">Emp Id :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtEmpid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
             
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
                                    <asp:CheckBox ID="chkPackage" runat="server" OnCheckedChanged="chkPackage_CheckedChanged"
                                         class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ValidationGroup="a"  AutoPostBack="true"/>  
                                    <label class="ui-outputlabel ui-widget">Select Package</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropPackage" Visible="false" runat="server"
                                         OnSelectedIndexChanged="dropPackage_SelectedIndexChanged" AutoPostBack="true" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" >
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
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:CheckBox ID="chkTestType" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         AutoPostBack="true" ValidationGroup="a"  OnCheckedChanged="chkTestType_CheckedChanged"/> 
                                    <label class="ui-outputlabel ui-widget">Select Test Type</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropTestype" Visible="false" runat="server" AutoPostBack="true" Width="180"
                                        OnSelectedIndexChanged="dropTestype_SelectedIndexChanged" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" >
                                    </asp:DropDownList>
                                </div>
                                <!----  Item Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                       

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                            </div>
                          </div>
                     <div id="divinner2" visible="false" runat="server" >
                         <hr />
        <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:60%" align="center"> 
       <asp:GridView ID="grdtestype" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="IDD" CellPadding="4" ForeColor="#333333" GridLines="None">
           <AlternatingRowStyle BackColor="White" />
           <Columns>

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
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true"  OnCheckedChanged="chkRow_CheckedChanged" />
            </ItemTemplate>
        </asp:TemplateField>

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
      
    </td>
    <td style="width:20%" align="right"></td>   
</tr>
</table>   
            <div id="div3"  runat="server" >
             
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
                             <label class="ui-outputlabel ui-widget">Total Discount :</label>	
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

</div>
                     <br/>
                     <br/>
                     <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="btncreate_Click" style="margin-left:5%;"/>
        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"   Visible="False" style="margin-left:5%;" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
                     <h1>&nbsp;</h1>
                     <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                         
                         <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             RADIOLOGY REQUISITION
                         </div>
                         <div class="ui-datatable-tablewrapper">
                             <asp:GridView ID="grdpackge" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID" CellPadding="4" GridLines="None" OnSelectedIndexChanging="grdpackge_SelectedIndexChanging" OnPageIndexChanging="grdpackge_PageIndexChanging" AllowSorting="True" AllowPaging="True" ForeColor="#333333">
                                 <AlternatingRowStyle BackColor="White" />
           <Columns>
               
       <%-- <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" />  
            </ItemTemplate>
        </asp:TemplateField>--%>

                  <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="RadiologyNo" />
          
               <asp:TemplateField HeaderText="PATIENT&nbsp;NAME" >
            <ItemTemplate>
                <asp:Label ID="lblPaname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="TOTAL&nbsp;AMOUNT" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("TOTAMT")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="DATE" Visible="true" >
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
                         <br/>
                         <br/>
                         <br/>
                         <br/>
                     </div>
                     
                     <h1></h1>
                     
                     <h1></h1>
                     
                     </h1>
        </div>
                    </strong>
                    </label>
                    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

