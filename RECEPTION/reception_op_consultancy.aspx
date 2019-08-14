<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_op_consultancy.aspx.cs" Inherits="RECEPTION_reception_op_consultancy" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Out Patient <span> / </span> OP Consultancy
                    </div>
   <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>OPD Consultancy</u></b></h1>
                
               <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                            <div id="div1" runat="server">
                                <div class="ui-grid-row">
                                <!----  Search By OPD NO:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>Search By OPD NO :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtspid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         pattern="[a-zA-Z0-9_]+" MaxLength="6" minLength="6" title="Please Enter A Valid OPD Number"></asp:TextBox>
                                </div>

                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:Button ID="btnshow" runat="server" CssClass="search" Text="Show" Width="70px" OnClick="btnshow_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="LBLFOLLOWUP" runat="server" Text="0" Visible="false"></asp:Label>
                                    <asp:Label ID="LBLLASTDATE" runat="server" Text="0" Visible="false"></asp:Label>
                                </div>

                            </div>
                            
                            
                            
                            <div class="ui-grid-row">
                                <!---- Search By Phone Number:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>Search By Phone &nbsp;&nbsp;Number :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtsmobile" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                         onkeypress="return onlyNos(event);" MaxLength="10" MinLength="10"></asp:TextBox>
                                </div>
                                <!----  Button----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    
                                        <asp:Button ID="btnshowph" runat="server" class="search" Text="Show" Width="70px" OnClick="btnshowph_Click" />

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    
                                </div>

                            </div>
                                </div>
                            <div id="div2" runat="server">
                                 <div class="ui-grid-row">
                                <!---- OP No. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        OP No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtopno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False"></asp:TextBox>
                                </div>
                                <!----  Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Date :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                </div>

                            </div>
                                 <div class="ui-grid-row">
                                <!---- Doctor :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>Doctor :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropdoctor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180"
                                          AutoPostBack="true" OnSelectedIndexChanged="dropdoctor_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                </div>
                                <!---- Patient Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Patient Name :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblname" runat="server" Text=""></asp:Label> 
                                </div>

                            </div>
                                 <div class="ui-grid-row">
                                <!---- Fee :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Fee :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtfee" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170px" Text="0" pattern="[0-9]+([,\.][0-9]+)?" Enabled="false"></asp:TextBox>
                                </div>
                                <!----  Item Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"/> Folllow Up

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
                                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
                                     <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
                                    <asp:Label ID="LBLSLNO" runat="server" Text="Label" Visible="false"></asp:Label> 
                                </div>

                            </div>
                                </div>
                            </div>
                   </div>
               
			  
				<br>
            <asp:Button ID="btnSubmit" runat="server" CssClass="create" OnClick="Button1_Click" Text="Submit" style="margin-left:5%;" />
         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="create" OnClick="btndelete_Click" Text="Delete" Visible="False" style="margin-left:5%;"/>       
         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
            <br /><br /><br /><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
							 <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                   OPD CONSULTANCY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060"  DataKeyNames="id" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>                
                <asp:BoundField DataField="OPNo" HeaderText="OPD No." /> 
                   <asp:BoundField DataField="PNAME" HeaderText="Patient&nbsp;Name" SortExpression="PNAME"/>
                <asp:BoundField DataField="Sname" HeaderText="Doctor" HeaderStyle-CssClass="text-center" SortExpression="Sname">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="Fee" HeaderText="Fee" />
                <asp:BoundField DataField="CDate" HeaderText="Consultancy&nbsp;Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="CDate"/>                     
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="Edit" HeaderText="Action" />
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
                                </div>

                              

</div>
        </div>
        </div>
              </strong>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

