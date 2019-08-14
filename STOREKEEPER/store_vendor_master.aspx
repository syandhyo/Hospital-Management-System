<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_vendor_master.aspx.cs" Inherits="STOREKEEPER_store_vendor_master" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> Vendor Master
                    </div>
                </div>

                <div class="layout-main-content">


        <div class="ui-fluid"><strong>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblfyear" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lblAuto" Visible="false" runat="server" Text=""></asp:Label>
        </div>
        <div class="card card-w-title">
        
                  <h1 style="color:#0071bc;"><b><u>Vendor Master</u></b></h1>
    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
        <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
              <div class="ui-grid-row">
                    <!----VendorId : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        VendorId :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtvendorid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  Enabled="false"></asp:TextBox>
                    </div>
                  <!----Date----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Date :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>        
                    </div>
                </div>
             <div class="ui-grid-row">
                    <!----Search Vendor : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Search Vendor :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtsearchname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Button ID="btn_search" runat="server" CssClass="search" OnClick="btn_search_Click" Text="Search" />
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                               
                    </div>
                </div>
          
            <hr />
            
            <div class="ui-grid-row">
                    <!----Company Name Part I : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label10" runat="server" style="color: #FF0000" Text="*"></asp:Label>Company Name Part I :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtcname1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$"></asp:TextBox>
                    </div>
                <!-----Address Line I :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget"> <asp:Label ID="Label11" runat="server" style="color: #FF0000" Text="*"></asp:Label>Address Line I :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtadd1" runat="server" TextMode="MultiLine" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*" Width="170"></asp:TextBox>       
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Company Name Part II : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <label class="ui-outputlabel ui-widget">Company Name Part II :</label>
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtcname2" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$"></asp:TextBox>
                    </div>
                <!-----Address Line II :------>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">Address Line II :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtadd2" runat="server" TextMode="MultiLine"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*" Width="170"></asp:TextBox>        
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----City :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label5" runat="server" style="color: #FF0000" Text="*"></asp:Label>City :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtcity" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$"></asp:TextBox >
                    </div>
                <!------PIN Code------->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">PIN Code :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtpin" runat="server" MaxLength="6" MinLength="6" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>       
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----State : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label2" runat="server" style="color: #FF0000" Text="*"></asp:Label>State :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtstate" runat="server" pattern="^[A-Za-z -]+$" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                    </div>
                <!-------Tel-No.:------>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                            Tel-No.:
                        </label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                          <asp:TextBox ID="txttelno" runat="server"  MaxLength="12" MinLength="7" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>     
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Mobile No : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label3" runat="server" style="color: #FF0000" Text="*"></asp:Label>Mobile No :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtmobno" runat="server"  MaxLength="10" MinLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[789][0-9]{9}" ToolTip="Invalid Mobile Number"></asp:TextBox>
                    </div>
                <!-----GST No :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        GST No :</label>
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtgstno" runat="server" MaxLength="15" MinLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>       
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Fax No.: ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Fax No.:
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtfaxno" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </div>
                <!-----State Code :--->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="Label4" runat="server" style="color: #FF0000" Text="*"></asp:Label>
        State Code :
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtstatecode" runat="server" MaxLength="2" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+" MinLength="2" ToolTip="Please Enter Numeric"></asp:TextBox>   
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Contact Person : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Contact Person :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtcperson" runat="server"  CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-z A-Z]*"></asp:TextBox>
                    </div>
                <!-----Contact Person No. :------>
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                       <label class="ui-outputlabel ui-widget">
                        Contact Person No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtcpersonno" runat="server" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MinLength="10" pattern="[0-9]+"></asp:TextBox>        
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Email Address : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Email Address :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtemail1" runat="server" type="email" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ToolTip="Invalid Email Id"></asp:TextBox>
                    </div>
                <!-----Alternate Email Address :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Alternate Email Address :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtemail2" runat="server" type="email" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ToolTip="Invalid Email Id"></asp:TextBox>      
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Excise Duty : ----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Excise Duty :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtexcise" runat="server" MaxLength="15" MinLength="15" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
                    </div>
                <!------Permament A/C No. :---->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <asp:Label ID="Label7" runat="server" style="color: #FF0000" Text="*"></asp:Label>Permament A/C No. :
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                       <asp:TextBox ID="txtperacno" runat="server" MaxLength="14" MinLength="11" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" ToolTip="Invalid Account Number"></asp:TextBox>        
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Bank A/C No. :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Bank A/C No. :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtbankacno" runat="server" MaxLength="14" MinLength="11" pattern="[0-9]+([,\.][0-9]+)?" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ToolTip="Invalid Account Number"></asp:TextBox>
                    </div>
                <!----Name of Bank :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        <asp:Label ID="Label8" runat="server" style="color: #FF0000" Text="*"></asp:Label>Name of Bank :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtbankname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>      
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----Branch Address :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Branch Address :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtbranch" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z -]+$"></asp:TextBox>
                    </div>
                <!----Type of A/c :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                       <asp:Label ID="Label9" runat="server" style="color: #FF0000" Text="*"></asp:Label>Type of A/c :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:DropDownList ID="txtactype" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180 ">
                            <asp:ListItem>Please Select</asp:ListItem>
                            <asp:ListItem Value="Current"></asp:ListItem>
                            <asp:ListItem>Saving</asp:ListItem>
                        </asp:DropDownList>        
                    </div>
                </div>
            <div class="ui-grid-row">
                    <!----IFSC :----->
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget"><asp:Label ID="lblEditgrd" Visible="false" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="Label12" runat="server" style="color: #FF0000" Text="*"></asp:Label>IFSC :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                        <asp:TextBox ID="txtifsc" runat="server" MaxLength="11" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" MinLength="11" pattern="^[a-zA-Z0-9_]*"></asp:TextBox>
                    </div>
                <!----Opening :----->
                        <div class="ui-panelgrid-cell ui-grid-col-2">
                        <label class="ui-outputlabel ui-widget">
                        Opening :
                        </label> 
                    </div>
                    <div class="ui-panelgrid-cell ui-grid-col-4">
                         <asp:TextBox ID="txtopening" runat="server" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>      
                    </div>
                </div>
            </div>

        </div>
                  <br>
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                         <br><br>
						 <hr>
						 <br>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             VENDOR MASTER
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" Width="1060" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="8"
              OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True"
              AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-bordered"
              OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME1" HeaderText="Name" HeaderStyle-CssClass="text-center"/>
                 <asp:BoundField DataField="CITY" HeaderText="City" HeaderStyle-CssClass="text-center"/>
                  <asp:BoundField DataField="STATE" HeaderText="State" />
                  <asp:BoundField DataField="PIN" HeaderText="PIN" />
                  <asp:BoundField DataField="MOB_NO" HeaderText="Contact No." />
                 <asp:BoundField DataField="GST" HeaderText="GST" HeaderStyle-CssClass="text-center"/>
                  <asp:BoundField DataField="OPENING" HeaderText="Opening" />
                  <asp:BoundField DataField="ADDRESS1" HeaderText="Address" />
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action"/>
            </Columns>
              <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
              <FooterStyle BackColor="#0071bc" ForeColor="#003399" />
              <HeaderStyle BackColor="#0071bc" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#0071bc" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
                                </div>

</div>
        </div>
    </div>
             </strong>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

