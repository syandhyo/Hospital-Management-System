<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/MasterPage1.master" AutoEventWireup="true" CodeFile="LabStockverif.aspx.cs" Inherits="LABORATORY_LabStockverif" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
        $(function () {
            SetDatePicker1();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker1();
                }
            });
        };
        function SetDatePicker1() {
            $("[id$=txtexpiry]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '0',

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
    <td style="width:20%" align="center"> <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
       
        <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
    
   
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;text-decoration:underline;" align="right" class="auto-style1"><strong>Lab Stock Verification&nbsp;</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
         <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
   <br />
  <div>
  <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">

      <div id="div3" runat="server" style="border: thin solid #000000;">

           <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Date : </td>
    <td style="width:25%" align="left">       
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
         </td>
    <td style="width:15%" align="right"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Item :</td>
    <td style="width:20%" align="left">
        <%--  <asp:TextBox ID="txtitem" runat="server" ></asp:TextBox>--%>
        <asp:DropDownList ID="dropitem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropitem_SelectedIndexChanged">

        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table> 
           <table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Batch No :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbatchno" runat="server" pattern="^[a-zA-Z0-9_]*" MaxLength="14" minlength="12" autocomplete="off"></asp:TextBox> 
      
         </td>
    <td style="width:20%" align="right">Expiry Date:</td>
    <td style="width:30%" align="left">
        <asp:TextBox ID="txtexpiry" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;" autocomplete="off"></asp:TextBox>
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table> 
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"><%--<asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>--%>Packing Type :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpacktype" runat="server" pattern="^[a-zA-Z0-9_]*" MaxLength="100" autocomplete="off"></asp:TextBox>
      
         </td>
    <td style="width:20%" align="right">No Of Packing :</td>
    <td style="width:30%" align="left">
         <asp:TextBox ID="txtnopacking" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" autocomplete="off" Title="Please!!Enter The Numeric Value.."></asp:TextBox>
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table> 
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"><%--<asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>--%>Closing Stock :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtclostock" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" Enabled="false"  Title="Please!!Enter The Numeric Value.."></asp:TextBox>
      
         </td>
    <td style="width:20%" align="right">Physical Stock :</td>
    <td style="width:30%" align="left">
         <asp:TextBox ID="txtphystck" runat="server" MaxLength="4" pattern="[0-9]+([,\.][0-9]+)?" autocomplete="off"  Title="Please!!Enter The Numeric Value.."></asp:TextBox>
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table> 
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"><%--<asp:Label ID="Label7" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>--%>Reason :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtreson" runat="server" TextMode="MultiLine"></asp:TextBox>
      
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:30%" align="left">
        <%-- <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox>--%>
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table> 


     <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
       
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
          
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    
           
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left"></td>
    <td style="width:40%; text-align: left;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm" Visible="False" OnClick="btnupdate_Click"/>
        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click"/>
         </td>
    <td style="width:10%" align="left"></td>
    <td style="width:10%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:10%" align="right">   
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
           <asp:TextBox ID="txtselid" runat="server" Visible="false"></asp:TextBox>
    </td>
    <td style="width:70%" align="center"> 
       <asp:GridView ID="grvlabst" runat="server" AutoGenerateColumns="False"  BackColor="White" DataKeyNames="ID"  BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" AllowPaging="True"  PageSize="10"  GridLines="Horizontal" OnSelectedIndexChanging="grvlabst_SelectedIndexChanging" OnRowDataBound="grvlabst_RowDataBound" OnRowDeleting="grvlabst_RowDeleting" OnPageIndexChanging="grvlabst_PageIndexChanging">
           <Columns>
           
                 <asp:TemplateField HeaderText="SL&nbsp;No" Visible="true" >
            <ItemTemplate>
              <%#Container.DisplayIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField HeaderText="DATE" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="ITEM&nbsp;NAME" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblcdnme" runat="server" Text='<%#Eval("ITEMNAME")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
                 <asp:TemplateField HeaderText="VALID&nbsp;DAY" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblbatch" runat="server" Text='<%#Eval("BATCHNO")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
         <asp:TemplateField HeaderText="PACKAGE&nbsp;TYPE" Visible="true" >
            <ItemTemplate>
                 <asp:Label ID="lblPacktp" runat="server" Text='<%#Eval("PACKTYPE")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
               
        <asp:CommandField  ShowEditButton="False" ShowSelectButton="true" ShowDeleteButton="true" SelectText="&nbsp;&nbsp;&nbsp;Edit" HeaderText="Action" />

    </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>      
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table>
          <%-- <div id="div2" runat="server" style="border: thin solid #000000;width:auto;">

               </div>--%>
      </div>
    </td>
    <td style="width:20%" align="right"></td>
</tr>
</table>
    </div>  
       <table width="100%">
     <tr>
    <td style="width:10%" align="right">
        <asp:HiddenField ID="hdntype" runat="server" />
    </td>
    <td style="width:70%" align="center"> 
       
    </td>

    <td style="width:20%" align="right"></td>
   
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%; text-align: left;" align="right">
&nbsp;&nbsp;</td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>

</asp:Content>

