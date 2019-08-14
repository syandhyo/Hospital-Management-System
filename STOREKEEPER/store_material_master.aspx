<%@ Page Title="" Language="C#" MasterPageFile="~/STOREKEEPER/StoreMasterPage.master" AutoEventWireup="true" CodeFile="store_material_master.aspx.cs" Inherits="STOREKEEPER_store_material_master" %>

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
                        <i class="fa fa-home"></i><span>/ </span> Transaction <span> / </span> <B>Material Master</B>
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
        </div>
        <div class="card card-w-title">
        
                
                  
            <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border: 0px none; background-color: transparent;">
                    <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                        <div class="ui-grid-row">
                            <!----Material ID : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="Label4" runat="server" Text="*"  ForeColor="#CC0000"></asp:Label>Material Id :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtmatid" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                            </div>
                              <!-------Date------>
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Date :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtdate" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                         <div class="ui-grid-row">
                            <!----Search Item : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Search Item :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtsearchname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </div>
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <asp:Button ID="btn_search" runat="server" CssClass="search" OnClick="Btnsearch_click" Text="Search" />
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               
                            </div>
                        </div>
                        <br />
                        <hr />
                        <br />
                       
                        <div class="ui-grid-row">
                            <!----Name : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Name :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtname" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$"></asp:TextBox>
                            </div>
                          
                        </div>
                        <div class="ui-grid-row">
                            <!----Type : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Type :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:DropDownList ID="droptype" runat="server" OnSelectedIndexChanged="droptype_SelectedIndexChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true" Width="180">
                                     <asp:ListItem>Please Select</asp:ListItem>
                                     <asp:ListItem>Consumable</asp:ListItem>
                                     <asp:ListItem>Assets</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Qty :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtqty" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="[0-9]+([,\.][0-9]+)?" alue="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" Text="0"></asp:TextBox>
                            </div>
                        </div>
                        <div runat="server" id="tag" visible="false">
                        <div class="ui-grid-row">
                            <!----Tag : ----->
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Tag :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txttag" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </div>
                            
                        </div>
                            </div>
                        <div class="ui-grid-row">
                           
                           <!-----Price :----->
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Price :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txtprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?"  value="0.00" 
            onclick="if(this.value=='0.00'){this.value=''}" onblur="if(this.value==''){this.value='0.00'}" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                            </div>
                            <!----GST% :----->
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                GST% :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtgst" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" alue="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" Text="0"></asp:TextBox>
                            </div>
                        </div>
                        <!-----HSN Code :----->
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                <asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>HSN Code :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                                <asp:TextBox ID="txthsncode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"  MaxLength="8" MinLength="6" pattern="^[a-zA-Z0-9_]*"  value="0" 
            onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
                            </div>
                            <!----Unit :----->
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                                <label class="ui-outputlabel ui-widget">
                                Unit :
                                </label> 
                            </div>
                            <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtunit" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Text="PCS" pattern="^[A-Za-z -]+$"></asp:TextBox>
                            </div>
                        </div>
                        </div>
                </div>
           
                 
                 <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="create" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="update"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="Button3_Click" />
                         <br/><br/>
						 <hr/>
						 <br/>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                             MATERIAL MASTER
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ID" Width="1090" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="Name" HeaderStyle-CssClass="text-center"/>
                 <asp:BoundField DataField="PRICE" HeaderText="Price" HeaderStyle-CssClass="text-center"/>
                  <asp:BoundField DataField="QTY" HeaderText="Qty" HeaderStyle-CssClass="text-center"/>
                <asp:CommandField ShowDeleteButton="FALSE" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>
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
                                </div><br/>
<br/>
<br/>
<br/>
</div>
        </div>
    </div>

            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

