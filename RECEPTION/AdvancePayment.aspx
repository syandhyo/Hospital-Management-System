<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="AdvancePayment.aspx.cs" Inherits="RECEPTION_AdvancePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script type = "text/javascript">

        function SetTarget() {

            document.forms[0].target = "_blank";

        }
        </script>
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
        }
        .auto-style2 {
            width: 20%;
            color: #000000;
            font-weight: 700;
        }
        .auto-style4 {
            color: #000000;
        }
        .auto-style5 {
            width: 18%;
            color: #000000;
        }
        .auto-style6 {
            width: 20%;
            color: #000000;
        }
    </style>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
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
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
         <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
    </tr>
</table>
          
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>   
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>     
    </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>IPD Advance Payment</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <div id="div1" runat="server">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By IPD NO. :</td>
    <td style="width:30%; text-align: left;" align="right">        
        <asp:TextBox ID="txtspid" runat="server" BackColor="#CCFFFF"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshow" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshow_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Search By Bed No. :</td>
    <td style="width:30%; text-align: left;" align="right">        
        <asp:TextBox ID="txtbedno" runat="server" BackColor="#CCFFFF"></asp:TextBox>
         &nbsp;<asp:Button ID="btnshowph" runat="server" BackColor="#66CCFF" Text="Show" Width="70px" OnClick="btnshowph_Click" />
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    </div>
        <div id="div2" runat="server">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right" class="auto-style4">Patient Name :</td>
    <td style="width:20%" align="left">        
        <asp:Label ID="lblname" runat="server" Text="" CssClass="auto-style4"></asp:Label>
    </td>
    <td style="width:20%" align="right" class="auto-style4">Date :</td>
    <td style="width:25%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="False"></asp:TextBox>
         </td>
    <td style="width:15%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right" class="auto-style4">IPD No :&nbsp; </td>
    <td style="width:20%" align="left">
       <asp:Label ID="lblip" runat="server" Text="" CssClass="auto-style4"></asp:Label>
         </td>
    <td align="right" class="auto-style5">
        Total Amount :</td>
    <td style="width:20%" align="left">        
        
         <asp:Label ID="lbltotalamt" runat="server" CssClass="auto-style2" Text=""></asp:Label>
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>



     <tr>
    <td style="width:20%" align="right" class="auto-style4">Bed No : </td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblbed" runat="server" Text="" style="color: #000000"></asp:Label>
         </td>
    <td align="right" class="auto-style5">Advance&nbsp;Paid Amount : </td>
    <td style="width:20%" align="left">        
        <asp:Label ID="lblpaidamt" runat="server" CssClass="auto-style2" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right" class="auto-style4">Payment Mode :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droppayment" runat="server" AutoPostBack="True" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="droppayment_SelectedIndexChanged">
           <asp:ListItem>Cash</asp:ListItem>
            <asp:ListItem>Card</asp:ListItem>
            <asp:ListItem>DD</asp:ListItem>
            <asp:ListItem>Credit</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td align="right" class="auto-style6">Due Amount :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblremainamt" runat="server" style="font-weight: 700; color: #CC0000; font-size: large" Text=""></asp:Label>
         </td>
            <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
       </table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">Card No. :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcard" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="10" MinLength="4" pattern="[0-9]+([,\.][0-9]+)?" Text="0" Width="102px"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
        Enter Amount :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtamount" runat="server" value="0" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}" Type="number" min="0"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Employee :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropemp" runat="server" CssClass="btn btn-default dropdown-toggle">
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>    
         
         
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Submit" />
         &nbsp;<asp:Button ID="btnDELETE" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btndelete_Click" Text="Delete" Visible="False"/>
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" >
            <Columns> 
                <asp:BoundField DataField="id" HeaderText="" />
                <asp:BoundField DataField="PID" HeaderText="OPDNO" />
                <asp:BoundField DataField="NAME" HeaderText="Name" />
                <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                <asp:BoundField DataField="Amount" HeaderText="Advance Amount" />
                <asp:BoundField DataField="ADate" HeaderText="Date" />
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />
                  <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
            </Columns>
             <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div> 
              </ContentTemplate></asp:UpdatePanel>  
</asp:Content>

