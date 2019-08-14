<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/MasterPage1.master" AutoEventWireup="true" CodeFile="OPIndent.aspx.cs" Inherits="LABORATORY_OPIndent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type = "text/javascript">

        function SetTarget() {

            document.forms[0].target = "_blank";
        }
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",

            });
            $('.todate').datepicker({
                dateFormat: 'dd-mm-yy',
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
    <td style="width:20%" align="left">
         <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
      <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;font-size:22px;font-weight:bold;color:#009688;font-family:serif;" align="right" class="auto-style1"><strong>Test Result Entry</strong></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="TXTID" runat="server" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Type &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:20%;" align="left">
        <asp:DropDownList ID="droprtype" runat="server" CssClass="btn btn-default dropdown-toggle" style="padding: 5px 64px;">
            <%--<asp:ListItem>OUTPATIENT</asp:ListItem>--%>
            <asp:ListItem>INPATIENT</asp:ListItem>
           <%-- <asp:ListItem>ONCOUNTER</asp:ListItem>--%>
        </asp:DropDownList>
         </td>
    <td style="width:20%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>IPD No. &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox Style="padding: 5px 16px;" ID="txtIPNO" runat="server" CssClass="form-control input-sm m-bot15" Enabled="true" OnTextChanged="txtIPNO_TextChanged" AutoPostBack="true"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right">Name &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:30%" align="left">
        <asp:TextBox ID="txtname" runat="server" Width="226px" CssClass="form-control input-sm m-bot15"></asp:TextBox>
         </td>
    <td style="width:10%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right">Datetime &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:20%" align="left">
          <asp:TextBox ID="txtinvdate" runat="server" CssClass="formdate" Style="padding: 5px 16px;"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblindetype" runat="server" style="color: #CC0000; " Text=""></asp:Label>
         </td>
    <td style="width:20%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right">Insurance &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblinsurance" runat="server" style="color: #CC0000" Text=""></asp:Label>
        <asp:Label ID="UHID" runat="server" Text="0" Visible="false"></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right"><asp:Label ID="Label2" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Select Test Type &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:DropDownList style="padding: 5px 16px;" ID="droptesttype" runat="server" AutoPostBack="true" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="droptesttype_SelectedIndexChanged">
        </asp:DropDownList>
         </td>
    <td style="width:20%;font-size:14px;color:#0b2c4a;font-weight:bold;" align="right">Ref. By &nbsp;&nbsp;:&nbsp;&nbsp;</td>
    <td style="width:30%;" align="left;">
        <asp:TextBox ID="txtref" runat="server" CssClass="form-control input-sm m-bot15" Width="180px"></asp:TextBox>
         </td>
    <td style="width:10%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:10%" align="right"></td>
    <td style="width:80%" align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="ID" GridLines="Horizontal">
           <Columns>

               <asp:TemplateField HeaderText="TEST NAME" >
            <ItemTemplate>
                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
            </ItemTemplate>
       </asp:TemplateField>

         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
         <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="lblprice" runat="server" Text='<%#Eval("PRICE")%>' ></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>

               <asp:TemplateField HeaderText="RANGE" >
            <ItemTemplate>
                <asp:TextBox ID="txtref" runat="server" Text='<%#Eval("RANGE")%>'></asp:TextBox>
            </ItemTemplate>
       </asp:TemplateField>
                   <asp:TemplateField HeaderText="UNIT" >
            <ItemTemplate>
                <asp:TextBox ID="txtunit" runat="server" Text='<%#Eval("UNIT")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>

        <asp:TemplateField HeaderText="TESTVALUE" >
            <ItemTemplate>
                <asp:TextBox ID="txt_value" runat="server" Text="0" TextMode="MultiLine"></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
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
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="ID" GridLines="Horizontal">
           <Columns>
         <asp:TemplateField HeaderText="INVESTIGATION" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinv0" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
          <asp:TemplateField HeaderText="PRICE" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice0" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="lblprice0" runat="server" Text='<%#Eval("PRICE")%>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
               <asp:TemplateField HeaderText="RANGE" >
            <ItemTemplate>
                <asp:TextBox ID="txtref" runat="server" Text='<%#Eval("RANGE")%>'></asp:TextBox>
            </ItemTemplate>
       </asp:TemplateField>
                   <asp:TemplateField HeaderText="UNIT" >
            <ItemTemplate>
                <asp:TextBox ID="txtunit" runat="server" Text='<%#Eval("UNIT")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="TESTVALUE" >
            <ItemTemplate>
                <asp:TextBox ID="txt_value0" runat="server" Text='<%#Eval("VALUE")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
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

      <asp:GridView ID="Gvwidaltest" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" DataKeyNames="ID" CellSpacing="2" ForeColor="Black">
           <Columns>
         <asp:TemplateField HeaderText="WIDAL&nbsp;TEST&nbsp;INVESTIGATIONS" Visible="true" >
            <ItemTemplate>
                <asp:Label ID="lblinvw" runat="server" Text='<%#Eval("INV")%>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
          <asp:TemplateField HeaderText="1:140" Visible="true" >
            <ItemTemplate>
                <%--<asp:Label ID="lblprice0" runat="server" Text='<%#Eval("PRICE")%>'></asp:Label>--%>
                <asp:TextBox ID="txt140" runat="server" Text='<%#Eval("A140")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
        <asp:TemplateField HeaderText="1:160" >
            <ItemTemplate>
                <asp:TextBox ID="txt160" runat="server" Text='<%#Eval("A160")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
                   <asp:TemplateField HeaderText="1:180" >
            <ItemTemplate>
                <asp:TextBox ID="txt180" runat="server" Text='<%#Eval("A180")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
                <asp:TemplateField HeaderText="1:320" >
            <ItemTemplate>
                <asp:TextBox ID="txt320" runat="server" Text='<%#Eval("A320")%>'></asp:TextBox>
            </ItemTemplate>

        </asp:TemplateField>
    </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </td>

    <td style="width:10%" align="right"></td>
   
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="LBLCORPORATE" runat="server" Text="0"></asp:Label>
        <asp:TextBox ID="txtdisc" runat="server" Visible="true">0</asp:TextBox>
         </td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td align="center">

         <asp:Button ID="btncreate" runat="server" Text="Create" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm"  OnClick="Button2_Click" Visible="False" />
         &nbsp;<asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn btn-warning btn-sm" OnClientClick="return DeleteItem()"  OnClick="Btndelete_Click" Visible="False" />
         &nbsp;<asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" />

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
</table><table width="100%">
     <tr>
    <td style="background-color: #3399FF;" align="right">
        <asp:TextBox ID="TXTID0" runat="server" Visible="False"></asp:TextBox>
         </td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;font-size:26px;font-weight:bold;color:#0b2c4a;font-family:Calibri;" align="right" class="auto-style2"><strong>Test History</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr><br />
</table><table width="100%">
     <tr><br />
    <td style="width:20%" align="right"></td>
    <td align="center">

          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10"  AllowPaging="True" AllowSorting="True"  CssClass="table table-bordered" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
            <Columns>
                 <asp:BoundField DataField="ID" HeaderText="" />
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                  <asp:BoundField DataField="PID" HeaderText="&nbsp;NAME&nbsp;" />
                
             
                <asp:CommandField  ShowSelectButton="true" ShowDeleteButton="false" HeaderText="ACTION"/>
              <asp:TemplateField HeaderText="PRINT">
              <ItemTemplate>
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
              </ItemTemplate>
          </asp:TemplateField>
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
</table>
    </div>
 </ContentTemplate></asp:UpdatePanel>
</asp:Content>

