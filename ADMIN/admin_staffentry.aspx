<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_staffentry.aspx.cs" Inherits="ADMIN_admin_staffentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <script type="text/javascript">
        //On Page Load.
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
        function SetDatePicker() {
            $("[id$=txtdoj]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: '../bootstraptemplate/images/calendar.png'

            });
        }


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
            $("[id$=txtdob]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: '../bootstraptemplate/images/calendar.png'

            });
        }
        

        function AgeValidation() {
            
            var ageOfCustomer = document.getElementById('<%= txtdob.ClientID %>').value;
            
            var date = saildates.split('/')[2];
            var d = new Date();
            var lipday = ((d.getFullYear() - date) / 4);
            var month = d.getMonth() + 1;
            var day = d.getDate();
            var output = day + '/' + month + '/' + d.getFullYear();
            var age = ((daydiff(parseDate(output), parseDate(saildates)) - lipday) / 365);
            if (age > 18.000 || age == 18.000) {
                return true;
            }
            else {
                alert('Your age should be 18 or greater than 18 yrs!!!');
            }
        }
        function parseDate(str) {
            var mdy = str.split('/')
            return new Date(mdy[2], mdy[1] - 1, mdy[0]);
        }
        function daydiff(first, second) {
            return (first - second) / (1000 * 60 * 60 * 24)
        }
    </script>


  <%--<script>   
$(function () {
        $('#<%=GridView1.ClientID %>').footable({
            breakpoints: {
                phone: 480,
                tablet: 1024
            }
        });
});
      </script>--%>

    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/</span>Staff Master<span>/</span> </span>Staff Entry
                    </div>
    <script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">

<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label> 
            <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:Label ID="lblempid" runat="server" Text="Label" Visible="false"></asp:Label>

         <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
             <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                <div class="ui-grid-row">
                    <!----  Name-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Name:</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" 
                             pattern="^[A-Za-z. -]+$" MaxLength="40" MinLength="3" Title="Please Enter Only Alphabets"></asp:TextBox>
                    </div>
                        <!----  Designation-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Designation:</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:DropDownList ID="dropdesg" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="true" OnSelectedIndexChanged="dropdesg_SelectedIndexChanged">
        </asp:DropDownList>
                    </div>

                </div>
                 <div class="ui-grid-row">
                    <!----  Present Address-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Present Address :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txttadd" runat="server"  Width="170" cols="20" rows="3" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" TextMode="MultiLine" pattern="[a-zA-Z0-9]+"></asp:TextBox>
                    </div>
                     
                </div>
                <div class="ui-grid-row">
                    <!----  Present Address-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                       <asp:CheckBox ID="CheckBox1" runat="server" class="ui-selectcheckboxmenu ui-widget ui-state-default ui-corner-all" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <label class="ui-outputlabel ui-widget">Present address as above:</label>  
                    </div>
                   
                </div>

                  <div class="ui-grid-row">
                    <!----  DOJ-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                         <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Permanent Address:</label>
                        
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtpadd" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Width="170" cols="20" rows="3" TextMode="MultiLine" pattern="[a-zA-Z0-9]+" ></asp:TextBox>
                    </div>
                        <!----  Permanent Address-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>DOJ  : </label>	
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtdoj" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  onkeydown="return false;" onpaste ="return false;" AutoComplete="off"></asp:TextBox>
                    
                    </div>

                  </div>
                  <div class="ui-grid-row">
                    <!----  DOB-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>DOB : </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtdob" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;" AutoComplete="off"></asp:TextBox>
                    </div>
                        <!----  Blood Group-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                Blood Group:</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:DropDownList ID="dropblood" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                        <asp:ListItem>Please Select</asp:ListItem>
                        <asp:ListItem>O+</asp:ListItem>
                        <asp:ListItem>O-</asp:ListItem>
                        <asp:ListItem>A+</asp:ListItem>
                        <asp:ListItem>A-</asp:ListItem>
                        <asp:ListItem>B+</asp:ListItem>
                        <asp:ListItem>B-</asp:ListItem>
                        <asp:ListItem>AB+</asp:ListItem>
                        <asp:ListItem>AB-</asp:ListItem>
                    </asp:DropDownList>
                    </div>

                  </div>

                 <div class="ui-grid-row">
                    <!----  Job Type-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Job Type : </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropstaff" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
        </asp:DropDownList>
                    </div>
                        <!----  Sex-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>
                    Sex :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                     <asp:DropDownList ID="dropgen" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                        <asp:ListItem>Please Select</asp:ListItem>
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                    </div>

                  </div>
                   <div class="ui-grid-row">
                    <!----  Experience-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label10" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Experience  : </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtexp" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+" MaxLength="2" AutoComplete="off"></asp:TextBox>
                    </div>
                        <!----  Department-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Department :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                    <asp:DropDownList ID="dropdept" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                     </asp:DropDownList> 
                    </div>

                  </div>
                   <div class="ui-grid-row">
                    <!----  Specialisation-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Specialisation  :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtspl" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false"  AutoComplete="off" pattern="[a-zA-Z -]+" tilte="Please Enter The specialisation"></asp:TextBox>
                    </div>
                        <!----  PF No.-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget" style="margin-left:1%;">PF No. :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                   <asp:TextBox ID="txtpf" runat="server"  class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-zA-Z0-9]+" MaxLength="15" MinLength="10" AutoComplete="off"></asp:TextBox>
                    </div>

                  </div>
                  <div class="ui-grid-row">
                    <!----  ESI No.-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget" style="margin-left:1%;">ESI No.</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtesi" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" role="textbox" aria-disabled="false" aria-readonly="false" pattern="[a-zA-Z0-9-]+" MaxLength="15" MinLength="10" AutoComplete="off" title="Please Enter A Valid ESI No."></asp:TextBox>
                    </div>
                        <!----  Email Id  :-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Email Id  :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                   <asp:TextBox ID="txtemail" runat="server" type="email" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoComplete="off" MaxLength="60" title="Please enter Valid E-Mail"></asp:TextBox>
                    </div>

                  </div>

                 <div class="ui-grid-row">
                    <!----  Photo :-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Photo :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:FileUpload ID="FileUpload1" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="170"/>
                    </div>
                     
                        <!----  Adhar Card No :-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label12" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Adhar Card No. :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                  <asp:TextBox ID="txtadhar" runat="server" Minlength="12"  Maxlength="12" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+" AutoComplete="off"></asp:TextBox>
                    </div>

                  </div>
                   <div class="ui-grid-row">
                    <!----  Reporting To : :-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Reporting To :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:DropDownList ID="dropreporting" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
        </asp:DropDownList>
                    </div>
                        <!---- Emergency Contact-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label13" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Emergency Contact. :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                  <asp:TextBox ID="txtcontact" runat="server" pattern="[0-9]{10,10}" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" minlength="10" AutoComplete="off"></asp:TextBox>
                    </div>

                  </div>
                  <div class="ui-grid-row">
                    <!----  Follow Up Days Limit -----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Follow Up Days Limit:</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtfollowup" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Maxlength="2" Minlength="1" pattern="[0-9]+" Text="0" AutoComplete="off"></asp:TextBox>
                    </div>
                        <!---- Fee  :-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget" style="margin-left:1%;">Fee  :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                  <asp:TextBox ID="txtfee" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Maxlength="6" Minlength="1" AutoComplete="off" pattern="[0-9]+([,\.][0-9]+)?" Text="0" ></asp:TextBox>
                    </div>

                  </div>
                    <hr />
                 <h1><b>Salary Details</b></h1>	
                  <div class="ui-grid-row">
                    <!----  Basic-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label14" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Basic  :</label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtbasic" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" required="" Text="0" AutoComplete="off" MaxLength="6"></asp:TextBox>
                    </div>
                        <!---- HRA  :-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label15" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>HRA   :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                  <asp:TextBox ID="txthra" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" required="" Text="0" MaxLength="5" AutoComplete="off"></asp:TextBox>
                    </div>

                  </div>
                  <div class="ui-grid-row">
                    <!----  Convence-----> 
                     <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label16" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Convence : </label>	
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txtcon" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" required="" Text="0" MaxLength="5" AutoComplete="off"></asp:TextBox>
                    </div>
                        <!---- Medical  :-----> 
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                    <label class="ui-outputlabel ui-widget"><asp:Label ID="Label17" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Medical  :</label>
                           
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                  <asp:TextBox ID="txtmed" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" required="" Text="0" MaxLength="5" AutoComplete="off"></asp:TextBox>
                    </div>

                  </div>
                 <hr />
             </div>
         </div>


<%--                   </div> 
                     <script id="j_idt116_s" type="text/javascript">$(function () {
    PrimeFaces.cw("SelectManyCheckbox", "widget_j_idt116", {
        id: "j_idt116"
    }
                 );
}
                                                                );
                </script>--%>
                    
           
          
         <br/>
       
                  <%--<button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:25%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>--%>
            
            <asp:Button ID="btncreate" runat="server" class="create" 
                style="margin-left:25%;"
                 OnClick="Button1_Click" Text="Create"
                 OnClientClick="AgeValidation();"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" class="update" style="margin-left:25%;" OnClick="Button2_Click" Text="Update" Visible="False" OnClientClick="AgeValidation();" />
        &nbsp;<asp:Button ID="btndelete" runat="server" class="delete"  style=" margin-left:25%;" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
               
         &nbsp;&nbsp;<%--<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>--%><asp:Button ID="btncancel" runat="server" class="cancel" OnClick="Button3_Click" Text="Cancel" CausesValidation="false" />
                           <br><br><br />
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                           
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                     STAFF ENTRY
                                </div>
                                <div class="ui-datatable-tablewrapper">
                               
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvDetails_RowDeleting" Width="1060"
                                                 OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                                                 OnPageIndexChanging="GridView1_PageIndexChanging" 
                                                OnSorting="GridView1_Sorting"
                                                 CssClass="table table-bordered " CellPadding="4" ForeColor="#333333" GridLines="None" >
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns> 
                                                    <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true">
                                                  <ItemTemplate>
                                                   <%#Container.DisplayIndex+1 %>
                                                  </ItemTemplate>
                                                  </asp:TemplateField>               
                                                    <asp:BoundField DataField="Sname" SortExpression="Sname" HeaderText="Name" /> 
                                                    <asp:BoundField DataField="TempAddress" SortExpression="TempAddress" HeaderText="Temparary Address" />
                                                    <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email Id" HeaderStyle-CssClass="text-center">                
                                                    <HeaderStyle CssClass="text-center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Contact" SortExpression="Contact" HeaderText="Emergency Contact" />  
                                                    <asp:BoundField DataField="Bloodgroup" SortExpression="Bloodgroup" HeaderText="Blood Group" />      
                                                    <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status" />                
                                                    <asp:CommandField ShowDeleteButton="true" DeleteText="Deactivate" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />
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
                                      
                                	
                                </div><br/>
<br/>
<br />
<br />
</div>
        </div>
    </div>
              </strong>
              </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btncreate" />
<asp:PostBackTrigger ControlID="btnupdate"></asp:PostBackTrigger>
            </Triggers>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnupdate" />
            </Triggers>


     </asp:UpdatePanel>
</asp:Content>

