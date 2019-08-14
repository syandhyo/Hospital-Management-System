<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Radio_Bill2.aspx.cs" Inherits="RADIOLOGY_Radio_Bill2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <section class="content-header">
        <h1>Equipment Rental Report
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Admin</a></li>
            <li class="active">Equipment Rental Report</li>
        </ol>
    </section>

    <section class="content">
        <div class="box">
            <asp:ScriptManager runat="server" ID="sp"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfCrncVal" runat="server" />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblError" Visible="false" ForeColor="Black" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div runat="server" id="dvReport">

                        <div class="container" style="width: 100% !important;">
                            <div>
                                <table border="1" id="tblinv" runat="server" width="100%" style="background-color: white; margin-top: 200px; margin-bottom: 50px;" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <%--<tr>
                <th style="float:left;border:none;" >
                    <img src="img/INM_logo.jpg" style="height:75px;width:75px;" />

                </th>

			  <th colspan="7" style="height:50px;border:none;" >
                 <p style="float:right;font-size:16px">IN MACHINERIES & EQUIPMENTS PRIVATE LIMITED
                   <br />   <span style="font-size:11px;">ISO:9001:2015 Certified Company</span>
                     <br /><span style="font-size:11px;">GSTIN:20AACCI1568A1Z9 CORPORATE IDENTITY NUMBER : U71100OR2009PTC011208 </span> 
                 </p>
                  
			  </th>
			</tr>--%>
                                        <%--<tr>
				<td colspan="8" align="right"><b>** This is a System generated Invoice. ** </b></td>
			</tr>--%>
                                        <tr>
                                            <td colspan="7" align="center"><b>Tax Invoice </b></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblOrgName" runat="server" style="text-align: right"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="Label9" runat="server" Text="Date:"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="Label10" runat="server" Text="Invoice :"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:PlaceHolder ID="plBarCode" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Phone No :"></asp:Label>
                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <th>
                                                &nbsp;</th>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 25%; max-width: 15%;">
                                                <asp:Label ID="Label7" runat="server" Text="OPD/IPD No :"></asp:Label>
                                            </td>
                                            <td style="width: 25%; max-width: 35%;">
                                                <asp:Label ID="lblIpdopd" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 25%; max-width: 15%;">
                                                <asp:Label ID="Label11" runat="server" Text="Patient Name :"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 25%; max-width: 35%;">
                                                <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="Radiology Bill"></asp:Label>
                                            </td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>

                                        <%-- <tr>
                                                <th>Sr No.</th>
                                                <th>SAC No.</th>
                                                <th id="thServiceDesc" runat="server">Description Of Service</th>
                                                <th id="theqptype" runat="server">Equipment Type</th>
                                                <th>Total Qty</th>
                                                <th>UOM</th>
                                                <th>Unit rate</th>
                                                <th>Value</th>
                                            </tr>--%>

                                        <%--<tr>
                                                <td>1</td>
                                                <td>
                                                    <asp:TextBox BorderStyle="None" runat="server" ID="lblsacno"></asp:TextBox></td>
                                                <td id="tdServiceDesc" runat="server">
                                                    <asp:Label runat="server" ID="lblsodescription"></asp:Label></td>
                                                <td id="tdeqptype" runat="server">
                                                    <asp:Label runat="server" ID="lblequipname"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbltotalhr"></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lbluom"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblCrncType" runat="server" Text=""></asp:Label>
                                                    <asp:TextBox BorderStyle="None" AutoPostBack="true" OnTextChanged="lblunitrate_TextChanged" runat="server" ID="lblunitrate"> </asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCrncType1" runat="server" Text=""></asp:Label>
                                                    <asp:Label runat="server" ID="lblvalue" Style="float: right"></asp:Label></td>
                                            </tr>--%>

                                        <tr>
                                            <td colspan="7">
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False">
                                                  <Columns>
                                                <asp:TemplateField HeaderText="Serial No">
                                                <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Name">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtTestName" Text='<%#Bind("TEST")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                              <%--<asp:TemplateField HeaderText="Note">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNote" Text='<%#Bind("Note")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Price">
                                                <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtPrice" Text='<%#Bind("PRICE")%>' ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                              </Columns>
                                                    <HeaderStyle Font-Size="15px" />
                                                </asp:GridView>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label class="ui-outputlabel ui-widget">
                                                Amount in Word :</label></td>
                                            <td>
                                                <asp:Label ID="lblCurword" runat="server"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <label class="ui-outputlabel ui-widget">
                                                Total Amount :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtTotalAmount2" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trCGST">
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <label class="ui-outputlabel ui-widget">
                                                Discount Amount :</label></td>
                                            <td>
                                                <asp:TextBox ID="lblDiscountamt" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trIGST">
                                            <td class="auto-style3">
                                                <label class="ui-outputlabel ui-widget">
                                                Prepared By :</label></td>
                                            <td class="auto-style3">
                                                <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td class="auto-style3"></td>
                                            <td class="auto-style3"></td>
                                            <td class="auto-style3">
                                                <label class="ui-outputlabel ui-widget">
                                                Total Amount :</label></td>
                                            <td class="auto-style3">
                                                <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td class="auto-style3"></td>
                                            <td class="auto-style3"></td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td class="auto-style3"></td>
                                            <td class="auto-style3"></td>
                                            <td class="auto-style3">
                                                <label class="ui-outputlabel ui-widget">
                                                Authorised Signatory :</label></td>
                                            <td class="auto-style3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;</td>
                                        </tr>
                                        <%--  <tr>
				<td colspan="8">(<asp:Label runat="server" ID="Label1"></asp:Label>)</td>
			</tr>
            <tr>
				<td colspan="8">(<asp:Label runat="server" ID="Label2"></asp:Label>)</td>
			</tr>--%>
                                        <tr>
                                            <td colspan="7">
                                                <table class="table3" border="0" width="100%">
                                                    <tr style="vertical-align: top;">
                                                        <td style="vertical-align: top;" width="65%">
                                                            <br />
                                                        </td>
                                                        <td width="35%">
                                                            <h6 style="text-align: left">&nbsp;</h6>
                                                        </td>
                                                    </tr>

                                                </table>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                
                            </div>
                        </div>

                        <div class="footer" style="display: none;">
                            <div class="container">
                                <p>Regd. Office:Plot No.430,Keonjhar Colony,Tulasipur,Kanika Chhak,cuttack,Pin-753008(Odisha)</p>
                                <p>Branch Office:Plot No.454, Bijayachandrapur, Tarinigada, Paradip, Jagatsinghpur, Pin-754142(Odisha) Ph.06722-228003,Mob:7682839852, 7682839864</p>
                                <p>Email: inmachineries@yahoo.com</p>
                            </div>
                        </div>
                    </div>
                    <div class="container">

                        <p class="print text-center">
                            <asp:Button runat="server" ID="btnback" Text="Back" Style="background: none; color: black; border: medium; font-weight: bold; font-size: 18px;" />
                            &nbsp;&nbsp;&nbsp;   
                                <asp:Button ID="btnSave" Style="background: none; color: black; border: medium; font-weight: bold; font-size: 18px;" runat="server" Text="Save" />
                            &nbsp;
                          <asp:Button ID="btnprint" Visible="false" Style="background: none; color: black; border: medium; font-weight: bold; font-size: 18px;" runat="server" Text="Print" OnClientClick="CallPrint()" />

                        </p>

                    </div>
                    </div>
                     <script type="text/javascript">

                         //function printDiv(printableArea) {
                         //    var printContents = document.getElementById(printableArea).innerHTML;
                         //    var originalContents = document.body.innerHTML;
                         //    document.body.innerHTML = printContents;
                         //    window.print();
                         //    document.body.innerHTML = originalContents;
                         //}

                         //function Print() {
                         //    var dvReport = document.getElementById("dvReport");
                         //    var frame1 = dvReport.getElementsByTagName("iframe")[0];
                         //    if (navigator.appName.indexOf("Internet Explorer") != -1) {
                         //        alert('1');
                         //        frame1.name = frame1.id;
                         //        window.frames[frame1.id].focus();
                         //        window.frames[frame1.id].print();
                         //    }
                         //    else {

                         //        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                         //        alert(frameDoc);
                         //        frameDoc.print();

                         //    }
                         //}
                         function CallPrint() {
                             var prtContent = document.getElementById('<%= dvReport.ClientID %>');
                    var WinPrint = window.open('', '', 'letf=100,top=100,width=600,height=600');
                    WinPrint.document.write(prtContent.innerHTML);
                    WinPrint.document.close();
                    WinPrint.focus();
                    WinPrint.print();
                    //WinPrint.close();
                }
                function Close() {
                    window.close();
                }
    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
    </div>
    </form>
</body>
</html>
