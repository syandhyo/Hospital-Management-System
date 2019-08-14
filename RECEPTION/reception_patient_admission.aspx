<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="reception_patient_admission.aspx.cs" Inherits="RECEPTION_reception_patient_admission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
        }
    </style>
    <script type = "text/javascript">

        function SetTarget() {

            document.forms[0].target = "_blank";

        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <script type="text/javascript">
                  //Sys.Application.add_load(function () {


                  //    $('.formdate').datepicker({
                  //        dateFormat: 'dd-mm-yy',
                  //        changeMonth: true,
                  //        changeYear: true,
                  //        minDate: '-75Y',
                  //        yearRange: "c-75:c+10",

                  //    });
                  //    $('.todate').datepicker({
                  //        dateFormat: 'dd-mm-yy',
                  //        changeMonth: true,
                  //        changeYear: true,
                  //        minDate: '-75Y',
                  //        yearRange: "c-75:c+10",
                  //    });
                  //});
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
                        <i class="fa fa-home"></i><span>/ </span> In Patient <span> / </span> Patient Admission
                    </div>
    
                </div>
              <div class="layout-main-content">

        <div class="ui-fluid"><strong>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
        </div>
        <div class="card card-w-title">
					 <h1 style="color:#0071bc;"><b><u>Patient Admission</u></b></h1>
    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
            <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">


            <div id="div1" runat="server">
					<div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                            <label class="ui-outputlabel ui-widget">*Search By OPD NO. :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                            <asp:TextBox ID="txtspid" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                            <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="search" OnClick="btnshow_Click" />
                        </div>
					</div>

                    <div class="ui-grid-row">
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                           <label class="ui-outputlabel ui-widget">* Search By Phone NO. :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-3">
                             <asp:TextBox ID="txtsmobile" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-6">
                            <asp:Button ID="btnshowph" runat="server" Text="Show" CssClass="search" OnClick="btnshowph_Click" />
                        </div>
					</div>


            <div class="ui-grid-row">
                <div class="ui-panelgrid-cell ui-grid-col-3">
                   <label class="ui-outputlabel ui-widget"> * Search By UHID :</label>
                </div>
                <div class="ui-panelgrid-cell ui-grid-col-3">
                    
                <asp:TextBox ID="TXTUHID" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                 </div>
                <div class="ui-panelgrid-cell ui-grid-col-6">
                    
         <asp:Button ID="BTNUHID" runat="server" CssClass="search" Text="Show"  OnClick="btnUHID_Click" />
         
                </div>
            </div>
            </div>
            <div id="div2" runat="server" >
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"> Pid :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:Label ID="lblpid" runat="server" Text="" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="168px"></asp:Label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> UHID :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                        <asp:Label ID="LBLUHID" runat="server" Text="" Width="168px"></asp:Label>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Patient Type :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                        <asp:Label ID="lbloutpatient" runat="server" Text="INPATIENT" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="168px"></asp:Label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Family Head Name :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
        <asp:TextBox ID="txtfalmilyhead" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"> &nbsp;</asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"> Name :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" Text="" pattern="[a-zA-Z ]*$"></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Age :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtage" runat="server" AutoPostBack="false" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="3"  pattern="[0-9]{1-3}" Text="" Enabled="true"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Gender :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="droprtype" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true" Width="178px">
                            <asp:ListItem>Select Gender</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Transgender</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Phone :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtphone" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10"  pattern="[0-9]+([,\.][0-9]+)?" Text="" Enabled="False" Width="168px"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Emergency Contact :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtecontact" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0" Enabled="False" Width="168px"></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Admission Date :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdate" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Refered By :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                    <asp:DropDownList ID="dropreferedby" runat="server" AutoPostBack="True" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="TRUE" Width="178px">
                    </asp:DropDownList>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Disease :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdiease" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"   Enabled="true"></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Permanent Address :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="Txtperad" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" Width="168px"></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Ward :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="dropward" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="True" OnSelectedIndexChanged="dropward_SelectedIndexChanged" Width="178px">
        </asp:DropDownList>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Present Address :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txttempad" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" Width="168px"></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Bed No. :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
  <asp:DropDownList ID="dropbedno" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px" >
        </asp:DropDownList>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Insurance/Corporate :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
        <asp:DropDownList ID="dropinsurance" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="TRUE" AutoPostBack="True" 
            OnSelectedIndexChanged="dropinsurance_SelectedIndexChanged" Width="178px">
          <%--    <asp:ListItem>--SELECT--</asp:ListItem>
            <asp:ListItem>CORPORATE</asp:ListItem>  
            <asp:ListItem>INSURANCE</asp:ListItem>  --%>
        </asp:DropDownList>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"> Select Package :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="droppackage" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="178px">
        </asp:DropDownList>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Insurance Name :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4" >
                        &nbsp;
                        <asp:TextBox ID="txtinsname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="margin-left:-7px;" ></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                      <label class="ui-outputlabel ui-widget">  Admission fee :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
        <asp:TextBox ID="txtregfee" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="5"  pattern="[0-9]+([,\.][0-9]+)?" Value="0" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"
></asp:TextBox>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Insurance Number :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        &nbsp;
                        <asp:TextBox ID="txtinsnumber" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" style="margin-left:-7px;" ></asp:TextBox>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> Payment Mode :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
                    <asp:DropDownList ID="droppayment" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnSelectedIndexChanged="droppayment_SelectedIndexChanged" AutoPostBack="True" Width="178px">
                        <asp:ListItem>Cash</asp:ListItem>
                         <asp:ListItem>Card</asp:ListItem>           
                        <asp:ListItem>DD</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </div>
                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        
                        <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
                         <label class="ui-outputlabel ui-widget">Card/DD No. :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        
        <asp:TextBox ID="txtcard" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MaxLength="10" MinLength="4" pattern="[0-9]+([,\.][0-9]+)?" Text="0" ></asp:TextBox>
                    </div>
                    
                </div>

                <br />

                <div class="ui-grid-row">
                    
                    <div class="ui-panelgrid-cell ui-grid-col-6">
                        
                        <asp:Button ID="btncreate" runat="server" CssClass="create" OnClick="Button1_Click" Text="Create" style="margin-left:5%;"/>
                        <asp:Button ID="btnupdate" runat="server" CssClass="search" OnClick="Button2_Click" Text="Update" Visible="False" style="margin-left:5%;"/>
                        <asp:Button ID="btncancel" runat="server" CssClass="cancel" OnClick="Button3_Click" Text="Cancel" style="margin-left:1%;"/>
                      </div>
                   

                </div>


                </div>
		</div>
           </div>			
				<h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
							   <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                  PATIENT ADMISSION
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1060" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="IPD&nbsp;No."/>
                 <asp:BoundField DataField="NAME" HeaderText="NAME" HeaderStyle-CssClass="text-center">
                  <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                  <asp:BoundField DataField="PHONE" HeaderText="PHONE" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" HeaderStyle-CssClass="text-center">
                <HeaderStyle CssClass="text-center" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true"  SelectText="&nbsp;&nbsp;&nbsp;&nbsp;Edit"    HeaderText="ACTION" HeaderStyle-CssClass="text-center">
                   <HeaderStyle CssClass="text-center" />
                </asp:CommandField>
                   <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
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
              </label>
              </strong>
              </label>
              </strong>
              </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

