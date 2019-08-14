<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="ACCOUNTS_Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>  
<%--<a href="~/bin/AjaxControlToolkit.dll">~/bin/AjaxControlToolkit.dll</a>--%>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <title>jQuery Datepicker: Disable Future Dates</title>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>
 
    <script type="text/javascript">
        $(function () {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $('#txtdate').datepicker({
                maxDate: new Date(currentYear, currentMonth, currentDate)
             
        
            });
        });
      
    </script>
           
</head>
<body>
    <form id="form1" runat="server">
        
      
       
    <div>
        
      
   <%-- <p>Date: <input type="text" id="datepicker" /></p>--%>
       <%-- <asp:TextBox ID="txtdate" runat="server"  Enabled="true" ></asp:TextBox>
              <asp:CalendarExtenderID="Calendar1"runat="server" 
    Enabled="True" TargetControlID="TextBox1"Format="dd/MM/yyyy" >
                  </asp:CalendarExtender>--%>
   
<asp:TextBox ID="txtDate" runat="server" ReadOnly="true"></asp:TextBox>
<asp:ImageButton ID="imgPopup" ImageUrl="../calendar.png" ImageAlign="Bottom"
    runat="server" />
<ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDate"
    Format="dd/MM/yyyy">
</ajaxToolkit:CalendarExtender>
    </div>
    </form>
</body>
</html>
