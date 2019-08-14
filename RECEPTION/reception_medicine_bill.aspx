<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_medicine_bill.aspx.cs" Inherits="RECEPTION_reception_medicine_bill" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="layout-main-content">

                <div class="ui-fluid">
                    <strong>
                        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
                            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                             <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
                </div>
                <div class="card card-w-title">

                    <h1 style="color: #0071bc;"><b><u>Detail Medicine SALE/REFUND Bill</u></b></h1>
                <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-2">Date :

                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                         </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:Label ID="lblmid" runat="server"></asp:Label>
                         </div>
                   </div>
				<br>
                    <br>
                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            * Enter Bed No 	
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox1" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-6">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" class="search" />
                            
                        </div>
                    </div>
                    
                    <hr>

                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label style="margin-left: 0.5%;">IPD NO	</label>
                        </div>
                        <div class="ui-grid-col-4">
                            
                            <asp:Label ID="lblipno" runat="server" >&nbsp;</asp:Label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                             <label>Name </label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                            
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="ui-grid-row" style="margin-top:10px;">
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <label style="margin-left: 0.5%;">Total Amount 	</label>
                            </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            
        <asp:Label ID="lbltotalamt" runat="server">&nbsp;</asp:Label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                    <label >Paid /Balance/ Refundable </label>
                            </div>
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                            
                            <asp:Label ID="lblbalance" runat="server">&nbsp;</asp:Label>
                        </div>
                    <br>

                           
                         <asp:Button ID="Button2" runat="server"  class="search" style="width:auto;" OnClick="Button2_Click" Text="Print Detail Bill" />
                   </div>
                           
                    
                    <br>
                    <hr>
                    <br>
                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                    * Enter Amount :
                            </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtamount" runat="server" pattern="[0-9]+([,\.][0-9]+)?" value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </div>
                   
                    </div>
          
                    <br>
                    <br />

                       
                        <asp:Button ID="btnsave" runat="server" class="create"  OnClick="Btnsave_Click" Text="Save" style="margin-left:5%;" />
                         <asp:Button ID="btndelete" runat="server" CssClass="update" OnClick="btndelete_Click"  Text="Delete"  Visible="false" style="margin-left:5%;"/>
                         <asp:Button ID="btrcancel" runat="server"  OnClick="btncancel_Click" class="cancel" Text="Cancel"  Visible="true" style="margin-left:1%;"/>

            </div>
                    
                 </div>
                        
                    <br>
                    <br>
                    <br>
                    <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                        <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            MEDICINE BILL DETAIL
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" width="1060" DataKeyNames="ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                 <asp:BoundField DataField="NAME" HeaderText="NAME" />
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                  <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" HeaderText="ACTION" />
            </Columns>
               <%-- <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />--%>
                                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#0071bc" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
               <%-- <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />--%>
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                        </div>
                      
                    </div>
                </div>  
            </div>
            </strong>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

