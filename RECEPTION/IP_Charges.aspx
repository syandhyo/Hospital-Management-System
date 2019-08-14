<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="IP_Charges.aspx.cs" Inherits="RECEPTION_IP_Charges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #000000;
            font-size:17px;
        }
        .auto-style2 {
            background-color: #CCCCCC;
            font-size:14px
        }
        .auto-style3{
            text-decoration: underline;
            /*background-color: #e899ef;*/
            font-size:15px
        }
        
    </style>
    <script type="text/javascript">
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
        }
        
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
        
    </script>
    <script type="text/javascript">
        function openpopup() {
            window.open("provisional_billip.aspx")
        }
        function openpopup1() {
            window.open("Advance_payment_ip.aspx")
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
   

         <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
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
            $("[id$=]").datepicker({
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
    <td style="width:20%" align="right"><asp:Label ID="UHID" runat="server" Text="0" Visible="false"></asp:Label></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"><asp:TextBox ID="txtcorpo" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox> </td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
        
        

              <div style="border-style: solid; border-width: thin"><br />
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Patient Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"</td>
    <td style="width:20%" align="right">Name :        
        <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
    </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="false"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Age :&nbsp; 
       <asp:Label ID="lblage" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" style="width:20%" >IPNO.:</td>
    <td style="width:20%" align="left">  <asp:Label ID="lblip" runat="server" Text=""></asp:Label>      
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>



     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Address :
        <asp:Label ID="lbladd" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" style="width:20%">Ward Name.<asp:Label ID="lblward" runat="server" Text=""></asp:Label></td>
    <td style="width:20%" align="left">        
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Gender : 
        <asp:Label ID="lblgender" runat="server" Text=""></asp:Label></td>
    
    <td style="width:20%" align="right">Bed No.<asp:Label ID="lblbed" runat="server" Text=""></asp:Label></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><asp:Label ID="lblcorpid" runat="server" Text="" Visible="false"></asp:Label></td>
</tr>
             
            </table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Admission Date: <asp:Label ID="lbladdate" runat="server" Text=""></asp:Label></td>
    <td style="width:20%" align="right">Corporate:<asp:Label ID="lblcorp" runat="server" Text=""></asp:Label></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Total Amount:<asp:Label ID="lbltoamt" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%;" align="right" ></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Paid Amount:<asp:Label ID="lblpaid" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%;" align="right" ></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">Due/Refundable Amount:<asp:Label ID="lbldue" runat="server" Text="Label"></asp:Label></td>
    <td style="width:20%;" align="right" ></td>
    <td style="width:20%" align="left">
        <asp:Button ID="btnprov" runat="server" CssClass="active" Text="Provisional Bill" OnClick="btnprov_Click" OnClientClick=" openpopup();"/>&nbsp;&nbsp;&nbsp;<asp:Button ID="advncepay" runat="server" CssClass="active" Text="Advance Pay" OnClick="advncepay_Click" OnClientClick="openpopup1();"/></td></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                  </div>
<div style="border-style: solid; border-width: thin"><br />
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style3"><strong>Charges</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><br /><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center" >Charge Category :&nbsp;<asp:DropDownList ID="ddlcate" runat="server" OnSelectedIndexChanged="ddlcate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                          
                   <table width="100%">
     <tr>
    <td style="width:15%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="center">
        <asp:GridView ID="Grvcharge" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <Columns>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" Checked="false"/>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CHARGE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblcharge" runat="server" Text='<%# Eval("Charge") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
         <asp:TemplateField HeaderText="QTY" ItemStyle-Width="150">
            <ItemTemplate>
                 <asp:TextBox ID="txtqty" runat="server" Text='0' MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" AutoPostBack="true" OnTextChanged="txtqty_TextChanged" Enabled="false"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRICE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblprice" runat="server" Text='<%# Eval("PRICE") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="TOTAL" ItemStyle-Width="150">
            <ItemTemplate>
                 <asp:Label ID="lbltotal" runat="server" Text='0.00'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:15%" align="center">
        <asp:Label ID="lbltot" runat="server" Text='0.00' Visible="false"></asp:Label></td>
</tr>
</table>
                                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">
        <div id="upcahrge" visible="false" >
<asp:GridView ID="Grvchargeupdate" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow1" runat="server" checked="true" AutoPostBack="true" OnCheckedChanged="chkRow1_CheckedChanged"/>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CHARGE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblcharge1" runat="server" Text='<%# Eval("DSR") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="QTY" ItemStyle-Width="150">
            <ItemTemplate>
                 <asp:TextBox ID="txtqty1" runat="server" Text='<%# Eval("quantity") %>' MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" AutoPostBack="true" OnTextChanged="txtqty1_TextChanged"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblprice1" runat="server" Text='<%# Eval("initial_price") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
       <asp:TemplateField HeaderText="TOTAL" ItemStyle-Width="150">
            <ItemTemplate>
                 <asp:Label ID="lbltotal1" runat="server" Text='<%# Eval("PRICE") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>
           </div>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                 </div>

         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" Text="Submit" />
         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnupdate_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btndelete_Click" Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" Text="Cancel" />

         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                   <div style="border-style: solid; border-width: thin"><br />
                  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"  AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns> 
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false"/>
                 <asp:BoundField DataField="BDate" HeaderText="Date" />
                <asp:BoundField DataField="PID" HeaderText="PID" />               
                <asp:BoundField DataField="NAME" HeaderText="Name" /> 
                <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                <asp:BoundField DataField="WARD" HeaderText="Ward Name" />
                <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
                       </div>
              </ContentTemplate></asp:UpdatePanel>
</asp:Content>

