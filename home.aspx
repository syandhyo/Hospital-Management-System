<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Home</title>
    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 300px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
    
 <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="css/w3.css"/>
    <link rel="stylesheet" href="ism/css/my-slider.css"/>
<script src="ism/js/ism-2.2.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            background-color: #66CCFF;
            color: #FFFFFF;
            font-size: x-large;
        }
        .auto-style2 {
            width: 74px;
            height: 57px;
        }
    </style>

 

    

</head>
<body>
    <form id="form1" runat="server">
      
        <table width="100%" style="height: 40px">
     <tr>
    <td align="right" style=" background-color: #007acc; font-family: Castellar;" class="auto-style1"><strong>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></strong></td>
    <td style="width:20%; background-color: #007acc;" align="center">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/logout.png" OnClick="ImageButton1_Click"  />
         </td>
</tr>
</table>
        <div style="width:100%">
    
        <table width="100%">
            <tr>
               
                <td align="center" style="width:80%">
                    <%--<asp:Image ID="Image1" runat="server" Height="268px" Width="517px" ImageUrl="~/images/walle.jpg" />--%>
                   <div class='container'>

<div class="ism-slider" data-transition_type="zoom" data-play_type="loop" data-interval="3000" data-buttons="false" id="my-slider">
  <ol>
    <li>
      <img src="ism/image/slides/_u/1513923455895_917089.jpg">
    
    </li>
    <li>
      <img src="ism/image/slides/_u/1513923457098_995500.jpeg">
     
    </li>
    <li>
      <img src="ism/image/slides/_u/1513923455901_262769.jpg">
     
    </li>
    <li>
      <img src="ism/image/slides/_u/1513923455869_603353.jpg">
     
    </li>
  </ol>
</div></div>
                </td>
                <td align="center" style="width:20%">
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%">&nbsp;</td>
                            <td align="left" style="width:20%; text-align: center;">
                                <img src="gimg/hospicare.png"/></td>
                            <td align="right" style="width:20%">&nbsp;</td>
                            <td align="left" style="width:20%">&nbsp;</td>
                            <td align="center" style="width:20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width:20%">&nbsp;</td>
                            <td align="left" style="width:20%">
                                &nbsp;</td>
                            <td align="right" style="width:20%">&nbsp;</td>
                            <td align="left" style="width:20%">&nbsp;</td>
                            <td align="center" style="width:20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                <asp:LinkButton ID="LinkButtonADMIN" runat="server" class="w3-button w3-border w3-hover-pink w3-blue" Visible="false" OnClick="LinkButtonADMIN_Click" width="300">ADMIN</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                 <asp:LinkButton ID="LinkButtonACCOUNTS" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonACCOUNTS_Click" width="300">ACCOUNTS</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                 <asp:LinkButton ID="LinkButtonLAB" runat="server" Visible="false"  class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonLAB_Click" width="300">LAB</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                      <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                            <asp:LinkButton ID="LinkButtonRADIO" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonRADIOLOGY_Click" width="300">RADIOLOGY</asp:LinkButton></td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                <asp:LinkButton ID="LinkButtonNURSE" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonNURSE_Click" width="300">NURSE</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                <asp:LinkButton ID="LinkButtonOT" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonOT_Click" width="300">OT</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                 <asp:LinkButton ID="LinkButtonPHARMACY" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonPHARMACY_Click" width="300">PHARMACY</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                <asp:LinkButton ID="LinkButtonRECEPTION" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonRECEPTION_Click" width="300">RECEPTION</asp:LinkButton>
                            </td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                                <asp:LinkButton ID="LinkButtonSTOCK" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonSTOCK_Click" width="300">DEPARTMENTSTOCK</asp:LinkButton></td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                            <asp:LinkButton ID="LinkButtonSTOREKEEPER" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonSTOREKEEPER_Click" width="300">STORE</asp:LinkButton></td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                 <table width="100%">
                        <tr>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%">
                            <asp:LinkButton ID="LinkButtonASSET" runat="server" Visible="false" class="w3-button w3-border w3-hover-pink w3-blue" OnClick="LinkButtonASSET_Click" width="300">ASSET</asp:LinkButton></td>
                            <td align="right" style="width:20%"></td>
                            <td align="left" style="width:20%"></td>
                            <td align="center" style="width:20%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <table width="100%" style="height: 41px; background-color: whitesmoke">
     <tr>
    <td align="center">Powered By : <a href="http://www.zemusitech.com/" target="_blank"><img src="images/zemusi.png"></a></td>
 
</tr>
</table>
    </div>
        <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="66.gif" alt="" />
</div>

          
    </form>
</body>
</html>
