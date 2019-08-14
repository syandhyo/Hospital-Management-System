<%@ Page Title="" Language="C#" MasterPageFile="~/RADIOLOGY/radioMasterPage.master" AutoEventWireup="true" CodeFile="Patient_information.aspx.cs" Inherits="RADIOLOGY_Patient_information" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../offline/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="../offline/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../offline/jquery-ui.js" type="text/javascript"></script>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
     <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            $(".add").change(function () {
               
             var selectedText = $('#<%=ddlPatientCategory.ClientID %>').val();
                if (selectedText == 'IPD') {
                    $("#divipd").show();
                  $("#divopd").hide();
                  

              } else if (selectedText == 'OPD') {
                  $("#divipd").hide();
                  $("#divopd").show();
                 
              }
          });
      });

     </script>

      <script type="text/javascript">
          $(function () {
              SearchText();
             // SearchText2();
             
          });
          function SearchText() {
           
              $(".autosuggest").autocomplete({
                 
                  source: function (request, response) {
                      $.ajax({
                          type: "POST",
                          contentType: "application/json; charset=utf-8",
                          url: "Patient_information.aspx/GetAutoCompleteData",
                          data: "{'prefix':'" + $('#ContentPlaceHolder1_txtPatientId').val() + "'}",
                          dataType: "json",
                          success: function (data) {
                              //alert(data);
                              if (data.d.length > 0) {
                                  response($.map(data.d, function (item) {
                                      return {
                                          label: item.split('/')[0],
                                          val: item.split('/')[1],
                                          val2: item.split('/')[2]
                                          //val3: item.split('/')[3],
                                          //val4: item.split('/')[4],
                                          //val5: item.split('/')[5],
                                          //val6: item.split('/')[6],
                                          //val7: item.split('/')[7]

                                      }
                                  }));
                              }
                              else {
                                  response([{ label: 'No Records Found', val: -1, val2: -1, val3: -1, val4: -1, val5: -1, val6: -1, val7: -1}]);
                              }
                          },
                          error: function (result) {
                              alert("Error");
                          }
                      });
                  },
                  select: function (event, ui) {
                      if (ui.item.val == -1) {
                          return false;
                      }
                      //$('#ContentPlaceHolder1_txtPatient').text(ui.item.val);
                      $('#ContentPlaceHolder1_lblPatientName').val(ui.item.val);
                      if (ui.item.val2 == -1) {
                          return false;
                      }
                      $('#ContentPlaceHolder1_txtAddress').text(ui.item.val2);

                }
            });
          }
         
</script>


    <script type="text/javascript">
        $(function () {
           // SearchText();
           SearchText2();

        });
        
        function SearchText2() {

            $(".autosuggest2").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Patient_information.aspx/GetAutoCompleteData2",
                        data: "{'prefix':'" + $('#ContentPlaceHolder1_txtPatientIpd').val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            //alert(data);
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('/')[0],
                                        val: item.split('/')[1],
                                        val2: item.split('/')[2]
                                        //val3: item.split('/')[3],
                                        //val4: item.split('/')[4],
                                        //val5: item.split('/')[5],
                                        //val6: item.split('/')[6],
                                        //val7: item.split('/')[7]

                                    }
                                }));
                            }
                            else {
                                response([{ label: 'No Records Found', val: -1, val2: -1, val3: -1, val4: -1, val5: -1, val6: -1, val7: -1 }]);
                            }
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item.val == -1) {
                        return false;
                    }

                    $('#ContentPlaceHolder1_lblPatientName').val(ui.item.val);
                    alert(ui.item.val);
                    if (ui.item.val2 == -1) {
                        return false;
                    }
                    $('#ContentPlaceHolder1_txtAddress').text(ui.item.val2);

                }
            });
        }
</script>
          <%--<ContentTemplate>--%>
               <div class="route-bar">
                    <div class="route-bar-breadcrumb">
                        <i class="fa fa-home"></i><span>/ </span> Transction <span> / </span> Patient information
                    </div>
   
                </div>
            <%------------------%>
              <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Patient Category :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="Div4">
                         <%-- <asp:TextBox ID="TextBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all autosuggest"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlPatientCategory" runat="server" Class="add">
                                    <asp:ListItem Value="OPD" Selected ="True">OPD</asp:ListItem>
                                    <asp:ListItem Value="IPD" >IPD</asp:ListItem>
                                    
                                </asp:DropDownList>
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">&nbsp;Bar Code :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           
                           <%-- <asp:Label ID="Label1" runat="server"></asp:Label>--%>
                             <asp:TextBox ID="txtBarcode" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </div>
               </div>

            <%------------------%>
    <br />

                <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Patient Id :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="dvPassport1">
                                <div id="divopd">
                          <asp:TextBox ID="txtPatientId" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all autosuggest"></asp:TextBox>
                                    </div>
                           <div id ="divipd" style="display:none">
                          <asp:TextBox ID="txtPatientIpd" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all autosuggest2"></asp:TextBox>
                             </div>
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">&nbsp;Patient Name :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="lblPatientName" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                           <%-- <asp:Label ID="lblPatientName" runat="server"></asp:Label>--%>
                        </div>
               </div>
    
              <br />
               <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                             <label class="ui-outputlabel ui-widget">&nbsp;Address :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="Div1">
                          <asp:TextBox ID="txtAddress" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Height="42px" TextMode="MultiLine" AutoPostBack="True" OnTextChanged="txtAddress_TextChanged"></asp:TextBox>
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">&nbsp;Doctor Name :</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            <%--<asp:TextBox ID="txtPatientName" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all autosuggest" Height="42px"></asp:TextBox>--%>
                            <asp:DropDownList ID="dropdoctorname" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180">
                            </asp:DropDownList>
                        </div>
               </div>
          <br />
               <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                             <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="Div2">
                                
                          <asp:TextBox ID="txtDoctorName" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Visible="false"></asp:TextBox>
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">&nbsp;</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                            
                        </div>
                   </div>
               <%-- Dynamic Table--%>
              <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                              <label class="ui-outputlabel ui-widget">&nbsp;Radiology Test :</label>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="Div3">
                             <asp:DropDownList ID="drpRadiologyTest" runat="server" class="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="180" AutoPostBack="True" OnSelectedIndexChanged="drpRadiologyTest_SelectedIndexChanged">
                            </asp:DropDownList>   
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                          <label class="ui-outputlabel ui-widget">Detail Note : &nbsp;</label>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <asp:TextBox ID="txtDetailNote" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                             <asp:Label ID="lblRate" runat="server"></asp:Label>
                        </div>
                   </div>

                     <%--  ----------------%>
                  <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                             <%-- <label class="ui-outputlabel ui-widget">&nbsp;Radiology Test :</label>--%>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="Div5">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="create" style="margin-left:5px;" OnClick="btnAdd_Click" />
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                         <%-- <label class="ui-outputlabel ui-widget">Detail Note : &nbsp;</label>--%>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                             <asp:Label ID="Label1" runat="server"></asp:Label>--%>
                        </div>
                   </div>
                    <%--  ----------------%>

                        <asp:GridView ID="GridView1" PageSize="5" class="bgbox1 table table-hover" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Name">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtTestName" Text='<%#Bind("TestName")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Note">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNote" Text='<%#Bind("Note")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtPrice" Text='<%#Bind("Price")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                              <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ItemStyle-Font-Size="12px" ButtonType="Button"
                                                    ItemStyle-HorizontalAlign="Center" />
                                              </Columns>
                                </asp:GridView>
    <label class="ui-outputlabel ui-widget">Price :</label>
     <asp:TextBox ID="txtTotAmount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all "></asp:TextBox>
    <label class="ui-outputlabel ui-widget">Discount (%) :</label>
     <asp:TextBox ID="txtDiscount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtDiscount_TextChanged" AutoPostBack ="True"></asp:TextBox>
    <label class="ui-outputlabel ui-widget">Discount Amount :</label>
     <asp:TextBox ID="lblDiscountamt" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" OnTextChanged="txtDiscount_TextChanged" AutoPostBack ="True"></asp:TextBox>
    <label class="ui-outputlabel ui-widget">Total Amount :</label>
     <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack ="True" Enabled="true"></asp:TextBox>
   
     <div class="ui-grid-row">
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                             <%-- <label class="ui-outputlabel ui-widget">&nbsp;Radiology Test :</label>--%>	
                           </div>
                          <div class="ui-panelgrid-cell ui-grid-col-4">
                            <div id="Div6">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="create" style="margin-left:5px;" OnClick="btnSubmit_Click" />
                          </div>
                          </div>
                    <div class="ui-panelgrid-cell ui-grid-col-2">
                         <%-- <label class="ui-outputlabel ui-widget">Detail Note : &nbsp;</label>--%>
                        </div>
                        <div class="ui-panelgrid-cell ui-grid-col-4">
                           <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox> 
                             <asp:Label ID="Label1" runat="server"></asp:Label>--%>
                            
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="create" style="margin-left:5px;" OnClick="btnCancel_Click" />
                            <asp:PlaceHolder ID="plBarCode" runat="server" />
                        </div>
                   </div>
    <br />
    <div class="box-body table-responsive" runat="server" id="lblgriddata">
       <asp:GridView ID="GridView2" PageSize="10" CssClass="table table-hover table-striped table-bordered" DataKeyNames="Patient_Id" runat="server" AutoGenerateColumns="False" >
                                            <Columns>
                                                 <asp:BoundField DataField="Patient_Id" HeaderText="Patient Id"></asp:BoundField>
                                                 <asp:BoundField DataField="NAME" HeaderText="Patient Name"></asp:BoundField>
                                                 <asp:BoundField DataField="Sname" HeaderText="Doctor Name"></asp:BoundField>
                                                 <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                 <asp:LinkButton ID="lbGenerateBill"  CausesValidation="false" CommandName="Select" EnableTheming="False" Text="Generate Invoice"  runat="server" CssClass="btn btn-info" OnClick="lbGenerateBill_Click"></asp:LinkButton>
                                               <%-- <asp:HiddenField  runat="server" ID="hdfagentid" Value='<%#Eval("workorderid")%>' />
                                                    <asp:HiddenField  runat="server" ID="hdfcustcompid" Value='<%#Eval("customercompanyid")%>' />--%>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                              </asp:GridView>
        </div>
                
<%--                    </ContentTemplate>
     </asp:UpdatePanel>--%>

</asp:Content>

