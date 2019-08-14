<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="Item_Entry.aspx.cs" Inherits="ADMIN_PHARMACYSTORE_Item_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              
                <div style="background-color: #FFFFFF">
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
        //        maxDate: '0',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '0',
        //        yearRange: "c-75:c+10",
        //    });
        //});
        //----------------------
        $(function () {
            SetDatePicker();
        });
        $(function () {
            SetDatePicker1();
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
            $("[id$=txtexpirydate]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: 'img/calendar.png'

            });
        }
        function SetDatePicker1() {
            $("[id$=txtmfgdate0]").datepicker({
                dateFormat: 'dd-mm-yy',
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: '0',

                yearRange: "c-75:c+10",
                buttonImage: 'img/calendar.png'

            });
        }
   
    </script>

        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">Search Item :</td>
    <td style="width:20%; text-align: left;" align="right">
        <asp:TextBox ID="txtsearchname" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Za-z ]+$" ToolTip="Please Enter Character" MaxLength="30"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
        <asp:Button ID="btn_search" runat="server" BackColor="#66CCFF" OnClick="Btnsearch_click" Text="Search" />
         </td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"></asp:Label>
    </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left" class="auto-style1"><strong>Item Master</strong></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%; text-align: right;" align="left">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:TextBox ID="txtid" runat="server" Visible="False"></asp:TextBox>
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lbldate" runat="server" Text="Label" Visible="false"></asp:Label>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Company:</td>
    <td style="width:20%; text-align: left;" align="left">
          <asp:DropDownList ID="dropcompany" runat="server" CssClass="form-control input-sm m-bot15">
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: right;" align="right">
          Name :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtname" runat="server" CssClass="form-control input-sm m-bot15" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Hsn Code :</td>
    <td style="width:20%; text-align: left;" align="left">
          <asp:TextBox ID="txthsncode" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="15" MinLength="12" pattern="[A-Z0-9]+" ToolTip="Please Enter Number"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: right;" align="right">
          Tablet Per Strip(for tablets only) :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txttabletperstrip" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" Enabled="false" CssClass="form-control input-sm m-bot15" Text="0"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Batchno :</td>
    <td style="width:20%; text-align: left;" align="left">
          <asp:TextBox ID="txtbatchno" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="15" MinLength="12" pattern="[A-Z0-9]+" ToolTip="Please Enter Number"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: right;" align="right" class="auto-style1">
          Location :</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">Category :</td>
    <td style="width:20%; text-align: left;" align="left">
          <asp:DropDownList ID="dropcate" runat="server" CssClass="form-control input-sm m-bot15" AutoPostBack="True" OnSelectedIndexChanged="dropcate_SelectedIndexChanged">
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: right;" align="right">
          1.Shelf :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtself" runat="server" CssClass="form-control input-sm m-bot15" pattern="[A-Z0-9]+"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Purchase Unit :</td>
    <td style="width:20%; text-align: left;" align="left">
          <asp:DropDownList ID="droppurchaseunit" runat="server" CssClass="form-control input-sm m-bot15">
              <asp:ListItem>PCS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: right;" align="right">
          2.Rack :</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtrack" runat="server" CssClass="form-control input-sm m-bot15" pattern="[A-Z0-9]+"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Sale Unit :</td>
    <td style="width:20%; text-align: left;" align="left">
          <asp:DropDownList ID="dropsaleunit" runat="server" CssClass="form-control input-sm m-bot15">
              <asp:ListItem>PCS</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%; text-align: right;" align="right">
          Reorder Point:</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txtreorder" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" title="Please Enter Numeric Value" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Purchase Price :</td>
    <td style="width:20%; text-align: left;" align="left">
         <asp:TextBox ID="txtpprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" title="Please Enter Numeric Value" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
         Expiry Date :</td>
    
    <td style="width:40%" align="left">
        <asp:TextBox ID="txtexpirydate" runat="server"  onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:0%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">MRP(With out tax) :</td>
    <td style="width:20%; text-align: left;" align="left"> <asp:TextBox ID="txtsprice" runat="server" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" Text="0" title="Please Enter Numeric Value" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"> Manufacturing Date:</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmfgdate0" runat="server" CssClass=" formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">GST(%) :</td>
    <td style="width:20%; text-align: left;" align="left"> <asp:TextBox ID="TXTGST" runat="server" Text="0" pattern="[0-9]+([,\.][0-9]+)?" MaxLength="10" CssClass="form-control input-sm m-bot15" title="Please Enter Numeric Value" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"> Opening Qty :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtopening" runat="server" CssClass="form-control input-sm m-bot15" MaxLength="10" pattern="[0-9]+([,\.][0-9]+)?" Text="0" title="Please Enter Numeric Value" onclick="if(this.value=='0'){this.value=''}" onblur="if(this.value==''){this.value='0'}"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%; text-align: left;" align="left">
        &nbsp;</td>
    <td style="width:20%; text-align: left;" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: left;" align="right">
         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
&nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />
    </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="3" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ID" OnRowDeleting="gvDetails_RowDeleting" AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="Name" HeaderStyle-CssClass="text-center"/>
                 <asp:BoundField DataField="CATEGORY" HeaderText="Category" HeaderStyle-CssClass="text-center"/>
                  <asp:BoundField DataField="QTY" HeaderText="Quantity" HeaderStyle-CssClass="text-center"/>
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>
            </Columns>
               <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>
          </ContentTemplate></asp:UpdatePanel></div>
   
   
</asp:Content>

