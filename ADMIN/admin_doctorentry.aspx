<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeFile="admin_doctorentry.aspx.cs" Inherits="ADMIN_admin_doctorentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span>Common<span> / </span> Doctor Entry
                    </div>
    <ul class="route-bar-menu">
        <li class="search-item">
          <i class="fa fa-search"></i>
          <input type="text" placeholder="Search..." />
        </li>
        <li>
            <a href="#" data-tooltip="Notifications">
                <i class="fa fa-globe"></i>
            </a>
        </li>
        <li>
            <a href="#" data-tooltip="Calendar">
                <i class="fa fa-calendar"></i>
            </a>
        </li>
        <li>
            <a href="#" data-tooltip="Help">
                <i class="fa fa-life-saver"></i>
            </a>
        </li>
    </ul><script id="j_idt77_s" type="text/javascript">$(function () { PrimeFaces.cw("Tooltip", "widget_j_idt77", { id: "j_idt77", showEffect: "fade", hideEffect: "fade", showDelay: 0, globalSelector: ".route-bar-menu a", position: "bottom" }); });</script>
                </div>

                <div class="layout-main-content">
<form id="j_idt79" name="j_idt79" method="post" action="https://www.primefaces.org/california/sample.xhtml" enctype="application/x-www-form-urlencoded">
<input type="hidden" name="j_idt79" value="j_idt79" />

        <div class="ui-fluid"><strong>
        </div>
        <div class="card card-w-title"><br>
          
                    <div id="j_idt79:j_idt81" class="ui-panelgrid ui-widget ui-panelgrid-blank form-group" style="border:0px none; background-color:transparent;">
                 <div id="j_idt79:j_idt81_content" class="ui-panelgrid-content ui-widget-content ui-grid ui-grid-responsive">
                      
                        <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label11" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Doctor Name:</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="txtdoctornm" runat="server" pattern="^[A-Za-z -]+$" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                           </div>
                             <!----  Doctor Type :-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Doctor Type :</label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="txtcity" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox1" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox2" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox3" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox4" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox5" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label7" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox6" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox7" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label9" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox8" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label10" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox9" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label12" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox10" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label13" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox11" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label14" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox12" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                      <div class="ui-grid-row">
                          <!----  Name-----> 
                             <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget"><asp:Label ID="Label15" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Supplier Name :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                               <asp:TextBox ID="TextBox13" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z ]+$" title="Please enter Alphabets"></asp:TextBox>
                           </div>
                             <!----  City-----> 
                            <div class="ui-panelgrid-cell ui-grid-col-2">
                           <label class="ui-outputlabel ui-widget"><asp:Label ID="Label16" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>City : </label>
                           
                          </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <asp:TextBox ID="TextBox14" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" pattern="^[A-Za-z]+$" title="Please enter Alphabets"></asp:TextBox>
                         </div>

                        </div>
                     </div></div>
            <br><br>   
                  <button id="j_idt212:j_idt214" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px; margin-left:25%; background-color:#e71a33; border-color:#b11124;"><span class="ui-button-text ui-c">Create</span></button>
               
         &nbsp;&nbsp;<button id="Button1" name="j_idt212:j_idt214" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button" role="button" aria-disabled="false" style="width:80px"><span class="ui-button-text ui-c">Cancel</span></button>
                            <h1>&nbsp;</h1>
                            <div id="j_idt79:j_idt131" class="ui-datatable ui-widget ui-datatable-reflow">
                            <label id="j_idt79:j_idt131_reflowDD_label" for="j_idt79:j_idt131_reflowDD" class="ui-reflow-label">Sort</label>
                            <select id="j_idt79:j_idt131_reflowDD" name="j_idt79:j_idt131_reflowDD" class="ui-reflow-dropdown ui-state-default" autocomplete="off">
                            	<option value="0_0">Id Ascending</option>
                                <option value="0_1">Id Descending</option>
                                <option value="1_0">Year Ascending</option>
                                <option value="1_1">Year Descending</option>
                                <option value="2_0">Brand Ascending</option>
                                <option value="2_1">Brand Descending</option>
                                <option value="3_0">Color Ascending</option>
                                <option value="3_1">Color Descending</option>
                              </select>
                              
                                <div class="ui-datatable-header ui-widget-header ui-corner-top">
                                     Doctor Details
                                </div>
                                <div class="ui-datatable-tablewrapper">
                                	<table role="grid">
                                    	<thead id="j_idt79:j_idt131_head">
                                        	<tr role="row">
                                            	
                                                <th width="81" class="ui-state-default ui-sortable-column" id="j_idt79:j_idt131:j_idt135" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">SL No</th>
                                             	<th width="115" class="ui-state-default ui-sortable-column" id="j_idt79:j_idt131:j_idt133" scope="col" role="columnheader" aria-label="Id: activate to sort column ascending" tabindex="0" aria-sort="other">
                                                <span class="ui-column-title">Doctor Name</span>
                                                </th>
                                                <th width="81" class="ui-state-default ui-sortable-column" id="Th1" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">Doctor Type</th>
                                             	 <th width="81" class="ui-state-default ui-sortable-column" id="Th2" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">Mobile No</th>
                                             	 <th width="81" class="ui-state-default ui-sortable-column" id="Th3" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">Qualification</th>
                                             	<th width="81" class="ui-state-default ui-sortable-column" id="Th4" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">Specialisation</th>
                                             	<th width="81" class="ui-state-default ui-sortable-column" id="Th5" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">Follow_Up</th>
                                             	<th width="81" class="ui-state-default ui-sortable-column" id="Th6" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">Charges</th>
                                             
                                                <th width="81" class="ui-state-default ui-sortable-column" id="Th7" scope="col" role="columnheader" aria-label="Year: activate to sort column ascending" tabindex="0">ACTION</th>
                                             
                                             </tr>
                                        </thead>
                                        <tbody id="j_idt79:j_idt131_data" class="ui-datatable-data ui-widget-content" tabindex="0">
                                        	<tr data-ri="0" data-rk="0f19037c" class="ui-widget-content ui-datatable-even ui-datatable-selectable" role="row" aria-selected="false">
                                            	
                                                <td role="gridcell">1</td>
                                                <td role="gridcell">Suresh Patra</td>
                                                 <td role="gridcell">Radiologist</td>
                                                 <td role="gridcell">34565434545</td>
                                                 <td role="gridcell">MBBS</td>
                                                  <td role="gridcell">rtfghtrr</td>
                                                 <td role="gridcell">4</td>
                                                  <td role="gridcell">700.00</td>
                                               	 <td role="gridcell"><a onClick="return confirm('Are you sure want to delete this info ?');" href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$GridView1','Delete$0')">Delete</a>
                                                 <a href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$GridView1','Select$0')">   Edit</a>
                                                </td>
                                            </tr>
                                            <tr data-ri="1" data-rk="89ce678a" class="ui-widget-content ui-datatable-odd ui-datatable-selectable" role="row" aria-selected="false">
                                            	<td role="gridcell">2</td>
                                                <td role="gridcell">Suresh Patra</td>
                                                 <td role="gridcell">Radiologist</td>
                                                 <td role="gridcell">34565434545</td>
                                                 <td role="gridcell">MBBS</td>
                                                  <td role="gridcell">rtfghtrr</td>
                                                 <td role="gridcell">4</td>
                                                  <td role="gridcell">700.00</td>
                                               	 <td role="gridcell"><a onClick="return confirm('Are you sure want to delete this info ?');" href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$GridView1','Delete$0')">Delete</a>
                                                 <a href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$GridView1','Select$0')">   Edit</a>
                                                </td>
                                            </tr>
                                           
                                         </tbody>
                                      </table>
                                </div><br>
<br>
<br>
<br>
<input type="hidden" id="j_idt79:j_idt131_selection" name="j_idt79:j_idt131_selection" autocomplete="off" value=""></div>
        </div>
            </div>
</asp:Content>


