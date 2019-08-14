<%@ Page Title="" Language="C#" MasterPageFile="~/NURSE/NurseMaster.master" AutoEventWireup="true" CodeFile="nurse_Advance_Bill.aspx.cs" Inherits="NURSE_nurse_Advance_Bill" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Advance Bill
                    </div>
    
                </div>
    <div class="layout-main-content" >
        <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
        <input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid">
            <strong>
        </div>
        <div class="card card-w-title" style="min-height:450px;">
            <h1 style="color: #0071bc;"><b><u>Advance Bill</u></b></h1>

            <br>
             <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

                            <div id="div2" runat="server">
                            <div class="ui-grid-row">
                                <!----  Patient Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Patient Name :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server" Text="" CssClass="auto-style4" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:Label>
                                </div>

                                <!---- Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Date :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----IPD No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">IPD No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblip" runat="server" Text="" CssClass="auto-style4"></asp:Label>
                                </div>

                                <!---- Total Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget">Total Amount :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lbltotalamt" runat="server" CssClass="auto-style2" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Bed No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblbed" runat="server" Text="" style="color: #000000"></asp:Label>
                                </div>

                                <!---- Advance Paid Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                   <label class="ui-outputlabel ui-widget"> Advance&nbsp;Paid Amount :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpaidamt" runat="server" CssClass="auto-style2" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Payment Mode :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Payment Mode :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="droppayment" runat="server" AutoPostBack="True" 
                                        class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="droppayment_SelectedIndexChanged">
                                       <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Card</asp:ListItem>
                                        <asp:ListItem>DD</asp:ListItem>
                                        <asp:ListItem>Credit</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <!---- Due Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Due Amount :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblremainamt" runat="server" style="font-weight: 700; color: #CC0000; font-size: large" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Card No. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Card No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtcard" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="4" pattern="[0-9]+([,\.][0-9]+)?" Text="0"></asp:TextBox> 
                                </div>

                                <!---- Enter Amount :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                     <label class="ui-outputlabel ui-widget"><asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                             Enter Amount :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtamount" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="0" value="0" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" Type="number" min="0"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Employee :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Employee :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="dropemp" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            </asp:DropDownList>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                </div>

                            </div>

                            </div>
                 </div>
            <br><br>
            <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="btnSubmit_Click" Text="Submit" />
         &nbsp;<asp:Button ID="btnDELETE" runat="server" CssClass="search" OnClick="btnDELETE_Click" Text="Delete" Visible="False"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="btnupdate_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="btncancel_Click" Text="Cancel" />
                 </div>
            <br />
             <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  AllowPaging="True" PageSize="5" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                         <AlternatingRowStyle BackColor="White" />
                                    <Columns> 
                                        <asp:BoundField DataField="id" HeaderText="Invoice" />
                                        <asp:BoundField DataField="PID" HeaderText="OPDNO" />
                                        <asp:BoundField DataField="NAME" HeaderText="Name" />
                                        <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                                        <asp:BoundField DataField="Amount" HeaderText="Advance" />
                                        <asp:BoundField DataField="ADate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
                                        
                                          <asp:TemplateField HeaderText="PRINT">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
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
                                </div><br/>
<br/>
<br/>
<br/>
            
            
           
                
        </div>
    </div>
              </strong>
              </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

