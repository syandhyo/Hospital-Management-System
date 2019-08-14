
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<title>HMS | Login</title>
<link href="css/style.css" rel="stylesheet" type="text/css" media="all"/>
<!-- Custom Theme files -->
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
<meta name="keywords" content="" />
<!--web-fonts-->
<link href='//fonts.googleapis.com/css?family=PT+Sans+Narrow:400,700' rel='stylesheet' type='text/css'>
<!--web-fonts-->
    <script type = "text/javascript" >
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
</script>
    
<body>
    <form id="form1" runat="server">
   <div class="main">
				<div class="login">
					<div class="login-top">
						<img src="images/Capture.png">
                      
					</div><br/>
					<h1>Login</h1>
					<div class="login-bottom">
					<form>
						<%--<input type="text" placeholder="Username" required=" ">--%>	
                        <asp:TextBox ID="txtusername" runat="server" type="text" placeholder="Username" />	
                        <br/>	
                      		 <br/>	
						<%--<input type="password" class="password" placeholder="Password" required=" ">--%>	
                         <asp:TextBox ID="txtpassword" runat="server"  TextMode="Password"  placeholder="Password"/>	
                         <br/>
                         <br/>				
						  <asp:Button ID="Button2" runat="server" Text="Login" OnClick="Button2_Click"/>
					</form>
					<a href="#"><p>Forgot your password? Click Here</p></a>
					</div>
				</div>
			</div>
		<div class="footer">
			<p>&copy 2018 HMS . All rights reserved | Design by <a href="http://zemusitech.com">Zemusitech Solution.</a></p>
		</div>

    </form>
</body>
</html>
