<%@ Page Language="C#" AutoEventWireup="true" CodeFile="radio_bill.aspx.cs" Inherits="RADIOLOGY_radio_bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 250px;
            margin-right: 0px;
        }
        .auto-style2 {
            width: 106px;
        }
        .auto-style3 {
            width: 106px;
            height: 48px;
        }
        .auto-style4 {
            height: 48px;
            width: 96px;
        }
        .auto-style5 {
            width: 154px;
            text-align: right;
        }
        .auto-style6 {
            width: 154px;
            text-align: right;
            height: 23px;
        }
        .auto-style7 {
            height: 23px;
        }
        .auto-style8 {
            height: 48px;
            width: 165px;
        }
        .auto-style9 {
            width: 165px;
        }
        .auto-style10 {
            height: 48px;
            width: 61px;
        }
        .auto-style11 {
            width: 61px;
        }
        .auto-style12 {
            width: 96px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
        &nbsp;<table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logo5.png" />
                </td>
                <td class="auto-style8">
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style6">
                                <asp:Label ID="lblOrgName" runat="server" style="text-align: right"></asp:Label>
                            </td>
                            <td class="auto-style7"></td>
                        </tr>
                        <tr>
                            <td class="auto-style5">
                                <asp:Label ID="lblAddress" runat="server" ></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style5">
                                <asp:Label ID="Label5" runat="server" Text="Phone No :"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </td>
                <td class="auto-style10">
                    <asp:Label ID="Label9" runat="server" Text="Date:"></asp:Label>
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Invoice :"></asp:Label>
                </td>
                <td class="auto-style4">
                                <asp:Label ID="lblDate" runat="server" ></asp:Label>
                            </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label7" runat="server" Text="OPD/IPD No :"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="lblIpdopd" runat="server" ></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="Label11" runat="server" Text="Patient Name :"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lblPatientName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style9">
                    <asp:Label ID="Label12" runat="server" Text="Radiology Bill"></asp:Label>
                </td>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style12">&nbsp;</td>
            </tr>
            </table>
        <br />

    <asp:PlaceHolder ID="plBarCode" runat="server" />
        <br />
        <br />

         <asp:GridView ID="GridView1" PageSize="5" class="bgbox1 table table-hover" runat="server" AutoGenerateColumns="False" >
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

                                </asp:GridView>

         <label class="ui-outputlabel ui-widget">Amount in Word :</label>
        <asp:Label ID="lblCurword" runat="server"></asp:Label>
         <label class="ui-outputlabel ui-widget">Total Amount :</label>
     <asp:TextBox ID="txtTotalAmount2" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
         <label class="ui-outputlabel ui-widget">Discount Amount :</label>
     <asp:TextBox ID="lblDiscountamt" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
    <label class="ui-outputlabel ui-widget">Total Amount :</label>
     <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true"></asp:TextBox>
        <label class="ui-outputlabel ui-widget">Prepared By :</label>
     <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="true"></asp:TextBox>


    </div>
    </form>
</body>
</html>
