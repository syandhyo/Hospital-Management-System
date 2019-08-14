<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_report_document.aspx.cs" Inherits="RECEPTION_reception_report_document" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Reception-Report Document</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(
    <asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
    <script type="text/javascript">

        function Validate() {

            if (document.getElementById("<%=txt_Fdate.ClientID%>").value == "") {
                alert("*Please!!Enter The From Date..");
                document.getElementById("<%=txt_Fdate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txt_Todate.ClientID%>").value == "") {
                alert("*Please!!Enter The To Date..");
                document.getElementById("<%=txt_Todate.ClientID%>").focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
            $("[id$=txt_Fdate]").datepicker({
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
        $(function () {
            SetDatePicker1();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker1();
                }
            });
        };
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
                buttonImage: '../images/calendar.png'

            });
        }
    </script>
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
   
    <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
    <div class="route-bar">
        <div class="route-bar-breadcrumb">
            <i class="fa fa-home"></i><span>/ </span>Reports <span>/ </span>Report Document
        </div>

    </div>
    <div class="ui-fluid">
        <strong>
    <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
    </div>
    <div class="card card-w-title">
        <h1 style="color: #0071bc;"><b><u>Download Report</u></b></h1>

 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
     <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">

        <div class="ui-grid-row">
            <div class="ui-panelgrid-cell ui-grid-col-1">
                From Date:
            </div>
            <div class="ui-panelgrid-cell ui-grid-col-4">
                 <asp:TextBox ID="txt_Fdate" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
            </div>
           
        </div>
        <div class="ui-grid-row">
            <div class="ui-panelgrid-cell ui-grid-col-1">
                To Date :
            </div>
            <div class="ui-panelgrid-cell ui-grid-col-4">
                <asp:TextBox ID="txt_Todate" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" ></asp:TextBox>
            </div>
           
        </div>
        <div class="ui-grid-row">
            
            <div class="ui-panelgrid-cell ui-grid-col-4">
                <asp:Button ID="btnShow" runat="server" CssClass="search"  Text="Show" OnClick="btnShow_Click" OnClientClick="return Validate();" style="margin-left:5%;" />
            </div>
          
        </div>
   </div>
     </div>
             	
        <h1>&nbsp;</h1>
        <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
            <div class="ui-datatable-header ui-widget-header ui-corner-top">
                            REPORT DOCUMENT DETAIL
                        </div>
                        <div class="ui-datatable-tablewrapper">
                            <asp:GridView ID="GridView1" runat="server" Width="1090" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True" AllowSorting="True" 
                                PageSize="10"
                                CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
            <Columns> 
                 <%--<asp:BoundField DataField="ID" HeaderText="Sl No" />--%>
                 <asp:BoundField DataField="PATIENTID" HeaderText="PATIENT ID" />
                <asp:BoundField DataField="NAME" HeaderText="NAME" />               
                <asp:BoundField DataField="DATE" HeaderText="DATE" /> 
                 <asp:BoundField DataField="SYSDATE" HeaderText="CURRENT DATE" />
                <asp:TemplateField HeaderText="ACTION">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbn_download" runat="server" OnClick="lbn_download_Click" href='<%#Eval("FILEUPLOAD") %>' >Show</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />--%>
            </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <%--<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />--%>
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                        </div>
            <br />
            <br />
            <br />
        </div>
    </div>
              </strong>
              </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

