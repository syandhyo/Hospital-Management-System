<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/Pharma_MasterPage.master" AutoEventWireup="true" CodeFile="pharmacy_debit_credit_report.aspx.cs" Inherits="PHARMACYSTORE_pharmacy_debit_credit_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        $(function () {
            SetDatePicker1();
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
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker1();
                }
            });
        };

        function SetDatePicker() {
            $("[id$=txt_Fdate]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: 'img/calendar.png'

            });
        }
        function SetDatePicker1() {
            $("[id$=txt_Todate]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: 'img/calendar.png'

            });
        }
    </script>
     
     <script type="text/javascript">

         function Validate() {

             if (document.getElementById("<%=txt_Fdate.ClientID%>").value == "") {
                 alert("Admission From Date Is Required !");
                 document.getElementById("<%=txt_Fdate.ClientID%>").focus();
                 return false;
             }

             if (document.getElementById("<%=txt_Todate.ClientID%>").value == "") {
                 alert("Admission To Date is Required !");
                 document.getElementById("<%=txt_Todate.ClientID%>").focus();
                 return false;
             }
         }
    </script>
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reports <span> / </span> Sale Return Report
                    </div>

                </div>

                <div class="layout-main-content">

        <div class="ui-fluid">
             <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Sale Report</u></b></h1>
				<div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                    <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                       
                        <div class="ui-grid-row">
                            <!----Select from date : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Select From date :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txt_Fdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                            </div>
                             
                        </div>
                        <div class="ui-grid-row">
                            <!----Select to date :----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Select to date :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txt_Todate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                            </div>
                             
                        </div>
                        <div class="ui-grid-row">
                            <!----Search By Patient Name : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:Button ID="btnCredit" runat="server" CssClass="update"  Text="Credit Bill" OnClick="btnCredit_Click"  OnClientClick="return Validate();"/>&nbsp;&nbsp;
                               <asp:Button ID="btnDebitShow" runat="server" CssClass="create"  Text="Debit Bill" OnClick="btnDebitShow_Click" OnClientClick="return Validate();" />
                            </div>
                             <div class="ui-panelgrid-cell ui-grid-col-1">
                                
                            </div>
                        </div>
                        </div>
                    </div>
				 <br/><br/><br/>
                 
                       <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">        
        <asp:Button ID="btn_Download" runat="server" CssClass="cancel"  Text="Download" Visible="false" OnClick="btn_Download_Click"/></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
           
           <%-- gridview--%>
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="grdd_show" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True" AllowSorting="True"   CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns> 
                 <asp:BoundField DataField="ID" HeaderText="Sl No" />
                 <asp:BoundField DataField="RTYPE" HeaderText="Sale Type " />
                <asp:BoundField DataField="PNAME" HeaderText="PATIENT NAME" />               
                <asp:BoundField DataField="INVDATE" HeaderText="INVOICE DATE" /> 
                 <asp:BoundField DataField="TOTALPRICE" HeaderText="TOTAL PRICE" />
                 <asp:BoundField DataField="TOTALDISCAMT" HeaderText="TOTAL DISCAMT" /> 
                 <asp:BoundField DataField="TOTALAMT" HeaderText="TOTAL AMOUNT" />
                 <asp:BoundField DataField="GSTAMT" HeaderText="GST AMOUNT" /> 
                 <asp:BoundField DataField="GT" HeaderText="GRAND TOTAL" />

                  <asp:BoundField DataField="IPNO" HeaderText="IP NO" /> 
                 <asp:BoundField DataField="PAMT" HeaderText="PAID AMT" />
                  <asp:BoundField DataField="BAMT" HeaderText="BALANCE AMOUNT" /> 
                 
              
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
    <td style="width:20%" align="center"></td>
</tr>
</table>
                                
<br/>
<br/>
<br/>
</div>
        </div>
   </ContentTemplate>
        </asp:UpdatePanel>      
</asp:Content>

