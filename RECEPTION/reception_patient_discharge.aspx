<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_patient_discharge.aspx.cs" Inherits="RECEPTION_reception_patient_discharge" %>

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
    <div class="route-bar">

                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Patient Discharge
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
            <asp:Label ID="lblins" runat="server" Text="" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="" Visible="false"> </asp:Label>

        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Patient Discharge</u></b></h1>
                
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!----  Select Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                Select Bed No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropbedno" runat="server" AutoPostBack="true" 
                                        class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged">
                                </asp:DropDownList>
                                </div>

                                <!---- Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>Date :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                                </div>

                            </div>
                            
                            <div class="ui-grid-row">
                                <!---- IPD NO. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                        IPD NO. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpid" runat="server" CssClass="auto-style3" Text=""></asp:Label>
                                </div>
                                <!----  Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label3" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Name : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:Label ID="lblpname" runat="server" CssClass="auto-style3" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Ward----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                     Ward :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblward" runat="server" CssClass="auto-style3" Text=""></asp:Label>
                                </div>
                                <!---- Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                        Bed No :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblbedno" runat="server" CssClass="auto-style3" Text=""></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Condition on Discharge.----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Condition on &nbsp;&nbsp;Discharge :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropstatus" runat="server" Width="180" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Alive</asp:ListItem>
                                    <asp:ListItem>Dead</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                <!----  Location :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Patient Discharge :</label>
                           
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropdischarge" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Discharge</asp:ListItem>
                                    <asp:ListItem>Abscond</asp:ListItem>
                                     <asp:ListItem>UnBlock</asp:ListItem>
                                </asp:DropDownList>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Category----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Consultant :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTdoctor" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                </div>
                                <!----  Diagnosis :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Diagnosis :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTdiagnosis" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="168px"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Operative Procedure:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Operative Procedure :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTclinicalsummary" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>
                                <!---- Investigation :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Investigation : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <td style="width:20%" align="center"> <asp:TextBox ID="TXTinvestigation" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="168px"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----  Treatment :t----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Treatment :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTtraetment" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>
                                <!---- Operative Findings :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Operative Findings : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTdiscussion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Advice :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Advice :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTadvice" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>
                                <!---- Past History :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Past History :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTpast" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----Chief Complains :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Chief Complains :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTcomplains" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>
                                <!----Follow Up :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Follow Up :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="TXTRemarks" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" Width="168px"></asp:TextBox>
                                </div>

                            </div>
                            </div>
                     </div>
             	<br/><br/>
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Discharge" style="margin-left:5%;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             DISCHARGE HISTORY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="1060" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="" />
                             <asp:BoundField DataField="DISCHARGE" HeaderText="DISCHARGE" />
                            <asp:BoundField DataField="DATE" HeaderText="DATE" />
                            <asp:BoundField DataField="PID" HeaderText="PID" />
                            <asp:BoundField DataField="NAME" HeaderText="NAME" />
                            <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                            <asp:CommandField HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" />
                            <asp:TemplateField HeaderText="PRINT">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#0071bc" ForeColor="White" HorizontalAlign="Center" />
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
    </div>
            </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

