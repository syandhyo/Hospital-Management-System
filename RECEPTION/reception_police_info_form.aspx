<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_police_info_form.aspx.cs" Inherits="RECEPTION_reception_police_info_form" %>

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
            $("[id$=txtdeathdate]").datepicker({
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
                        <i class="fa fa-home"></i><span>/ </span> Reception <span> / </span> Police Info Form
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong/>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="TXTID" runat="server" BackColor="#CCFFFF" Visible="false"></asp:TextBox>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Police Information Form</u></b></h1>
                
                 <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


                            <div class="ui-grid-row">
                                <!---- Select Bed No :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                     Select Bed No :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:DropDownList ID="dropbedno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList> 
                                </div>

                                <!----  Date :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">Date :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdate" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" runat="server" Enabled="false"></asp:TextBox>
                                </div>

                            </div>
                            
                           
                            <div class="ui-grid-row">
                                <!---- IPDNo:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        IPDNo :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblipno" runat="server"></asp:Label>
                                </div>
                                <!---- Patient Name :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Patient Name :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblpname" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Age----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Age :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:Label ID="lblage" runat="server"></asp:Label>
                                </div>
                                <!---- Gender :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Gender :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                     <asp:Label ID="lblgender" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Religion----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Religion :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtreligion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>
                                <!----  Nationality :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                 Nationality :</label>
                           
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtnationality" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----Permanenet Address :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label4" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                        Permanent &nbsp;&nbsp;Address:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtpaddress" runat="server" TextMode="MultiLine"
                                         CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="168px"></asp:TextBox>
                                </div>
                                <!----  Cause:----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                         <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                         Cause :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:DropDownList ID="dropcause" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                                    <asp:ListItem>RTA</asp:ListItem>
                                    <asp:ListItem>POISINING</asp:ListItem>
                                    <asp:ListItem>BURN</asp:ListItem>
                                    <asp:ListItem>HANGING</asp:ListItem>
                                    <asp:ListItem>MISCELLANEOUS</asp:ListItem>
                                </asp:DropDownList>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Correspondence Address with Phone No. :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Correspondence Address with Phone No. :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtcaddress" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="168px"></asp:TextBox>
                                </div>
                                <!---- Identification Mark :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Identification Mark : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtidmark" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Admission Date Time :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Admission Date Time:</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                   <asp:Label ID="lbladdate" runat="server" Text=""></asp:Label>
                                </div>
                                <!---- Death Date Time :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Death Date : </label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtdeathdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                                      
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!---- Accompanying Person----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        <asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
                                        Accompanying &nbsp;&nbsp;Person :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtperson" runat="server" pattern="^[A-Za-z -]+$"
                                         CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                                        
                                </div>
                                <!---- Relation with Patient :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Relation with Patient :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtrelationpatient" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                                </div>

                            </div>
                            <div class="ui-grid-row">
                                <!----Case History :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Case History :</label>
                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtcasehis" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*" Width="168px" ></asp:TextBox>                                                                                 
                                </div>
                                <!---- Relation with Patient :----->
                                <div class="ui-panelgrid-cell ui-grid-col-2">
                                    <label class="ui-outputlabel ui-widget">
                                        Cause Of Death :</label>

                                </div>
                                <div class="ui-panelgrid-cell ui-grid-col-4">
                                    <asp:TextBox ID="txtcauseofdeath" runat="server" pattern="^[a-zA-Z0-9_]*" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 

                                </div>

                            </div>
                            </div>
                     </div>
             	<br/><br/>
                  <hr/>
                  <br/>
                 <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" style="margin-left:5%;"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="update" OnClick="Button2_Click" Text="Update" Visible="False" style="margin-left:5%;"/>
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" />
                         <br/><br/><br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             POLICE INFORMATION
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="1060" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Id" />
                                        <asp:BoundField DataField="PID" HeaderText="Patient&nbsp;Id" />
                                        <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                        <asp:BoundField DataField="PNAME" HeaderText="Patient&nbsp;Name" />
                                        <asp:BoundField DataField="BEDNO" HeaderText="BedNo" />
                                        <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
                                    <asp:TemplateField HeaderText="Print">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
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

