<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="Radio_RequistnResult.aspx.cs" Inherits="RADIOLOGY_Radio_RequistnResult" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transction <span> / </span> Requisation Result
                    </div>
   
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="Txt_minus" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtdate" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
            <asp:Label ID="lblSesion" runat="server" Text="Label" Visible="false"></asp:Label> 
        </div>
        <div class="card card-w-title">
        
                <h1 style="color:#0071bc;"><b><u>Requisation Result</u></b></h1>
                   <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Select Package :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Select Package :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="droppackge" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"  OnSelectedIndexChanged="droppackge_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <!----Requisition  No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">&nbsp;Requisition  No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:TextBox ID="txtreqno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                                </div>
                                </div>
                            <div class="ui-grid-row">
                                <!----Select Consult Radiology:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>Select Consult &nbsp;&nbsp;Radiology:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="dropconsltRadio"  runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"   AutoPostBack="true"  ></asp:DropDownList>
                                </div>
                                <!----Select Radiographer:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>Select Radiographer:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="dropradiogrph"  runat="server"  AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" ></asp:DropDownList>
                                </div>
                                </div>
                            <br><br>
				                <hr>
                                <br />
				
             	 <div class="ui-grid-row">
                    <!---- TECHNIQUE----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>Technique :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="172" MaxLength="350"></asp:TextBox>
                    </div>
                    
                  </div>
                <div class="ui-grid-row">
                    <!---FINDING----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">&nbsp;Finding :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtfinding" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="172" MaxLength="350"></asp:TextBox>
                    </div>
                    
                    </div>
                <div class="ui-grid-row">
                    <!---- Name  :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">&nbsp;Impression :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtimpresion" runat="server" TextMode="MultiLine" Width="172" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="350"></asp:TextBox>
                    </div>
                    
                 </div>
                
                </div>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" vissible="false" BackColor="White" BorderColor="#336666" Visible="true" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
           <Columns>

                   <asp:BoundField DataField="NAME" HeaderText="INVESTIGATION" />
                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
        
            <%--<asp:CommandField ShowDeleteButton="True"  HeaderText="Action"/>--%>
    </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>

                  <hr>
                  <br>
                  <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" style="margin-left:5px;" OnClick="btncreate_Click"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"   Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btncancel_Click"/>
            <br><br><br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                               <br>
<br>
<br>
<br>
</div>
        </div>
    </div>
               </strong>
              </ContentTemplate>
     <triggers>
    <asp:asyncpostbacktrigger controlid="droppackge" eventname="SelectedIndexChanged" />   
    </triggers>
     </asp:UpdatePanel>
</asp:Content>

