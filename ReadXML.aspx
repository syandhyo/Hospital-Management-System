<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadXML.aspx.cs" Inherits="ReadXML" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
         <Columns>
        <asp:BoundField DataField="date" HeaderText="Date" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        <asp:BoundField DataField="PatientName" HeaderText="PatientName" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        <asp:BoundField DataField="RegnNo" HeaderText="RegnNo" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        <asp:BoundField DataField="CaseNo" HeaderText="CaseNo" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        <asp:BoundField DataField="Remark" HeaderText="Remark" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        <asp:BoundField DataField="BillInvoice" HeaderText="BillInvoice" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        <asp:BoundField DataField="BillAmnt" HeaderText="BillAmnt" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
              <asp:BoundField DataField="DiscAmnt" HeaderText="DiscAmnt" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
              <asp:BoundField DataField="ReceiptAmnt" HeaderText="ReceiptAmnt" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
              <asp:BoundField DataField="RefundAmnt" HeaderText="RefundAmnt" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
              <asp:BoundField DataField="Rcpt.No" HeaderText="Rcpt.No" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
              <asp:BoundField DataField="CollTypeB" HeaderText="CollTypeB" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
              <asp:BoundField DataField="Bank" HeaderText="Bank" ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
             </asp:BoundField>
        </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
