<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            Enter Barcode Code:<br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Generate" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Show" />
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" />
        
        </ContentTemplate>
        </asp:UpdatePanel>
        
    
    </div>
    </form>
</body>
</html>
