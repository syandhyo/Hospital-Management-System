<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Copy of Ipcharge.aspx.cs" Inherits="RECEPTION_Ipcharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #upcahrge {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
        Sys.Application.add_load(function () {


            $('.formdate').datepicker({
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",

            });
            $('.todate').datepicker({
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",
            });
        });
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
    <td style="width:20%" align="center" class="auto-style1"><strong>INPATIENT CHARGES</strong></td>
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
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="UHID" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
    <td style="width:20%" align="left"></td>
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
        Search By IPD NO :</td>
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
        <div id="div2" runat="server" >
           
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Name :</td>
    <td style="width:20%" align="left">        
        <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
    </td>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control input-sm m-bot15 formdate" Enabled="true"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right">IPD No :&nbsp; </td>
    <td style="width:20%" align="left">
       <asp:Label ID="lblip" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" class="auto-style3"></td>
    <td style="width:20%" align="left">        
        
         </td>
    <td style="width:20%" align="center"></td>
</tr>



     <tr>
    <td style="width:20%" align="right">Bed No. :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblbed" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" class="auto-style3">Gender :&nbsp; </td>
    <td style="width:20%" align="left">        
        <asp:Label ID="lblgender" runat="server" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Ward Name : </td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblward" runat="server" Text=""></asp:Label>
         </td>
    <td align="right" class="auto-style2">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
            <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
            </table>    
         
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
   
    <td style="width:60%" align="center">
        <div>

<asp:GridView ID="Grvcharge" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" AutoGenerateColumns="False" 
    BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CHARGE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblcharge" runat="server" Text='<%# Eval("Charge") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
         <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-Width="150">
            <ItemTemplate>
                 <asp:TextBox ID="txtdescription" runat="server" Text='<%# Eval("Charge") %>'></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRICE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:TextBox ID="txtprice" runat="server" Text='<%# Eval("Price") %>' pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
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
            
            
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">
        <div id="upcahrge" visible="false" >
<asp:GridView ID="Grvchargeupdate" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" AutoGenerateColumns="False"
     BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" checked="true"/>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CHARGE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblcharge" runat="server" Text='<%# Eval("CHARGETYPE") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
         <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-Width="150">
            <ItemTemplate>
                 <asp:TextBox ID="txtdescription" runat="server" Text='<%# Eval("DSR") %>'></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRICE" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:TextBox ID="txtprice" runat="server" Text='<%# Eval("PRICE") %>' pattern="[0-9]+([,\.][0-9]+)?"></asp:TextBox>
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
            
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Submit" />
         &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Btndelete_Click" Text="Delete" Visible="False" />
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
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"  AllowPaging="True" AllowSorting="True"
            OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"  CssClass="table table-bordered" >
            <Columns> 
                 <asp:BoundField DataField="BDate" HeaderText="Date" />
                <asp:BoundField DataField="PID" HeaderText="PID" />               
                <asp:BoundField DataField="NAME" HeaderText="Name" /> 
                <asp:BoundField DataField="BEDNO" HeaderText="Bed No" />
                <asp:BoundField DataField="WARD" HeaderText="Ward Name" />
                <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                
                <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />
            </Columns>
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>   
              </ContentTemplate>

 </asp:UpdatePanel>
</asp:Content>
