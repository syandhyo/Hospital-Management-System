<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="PHARMACYSTORE_USER_Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

	<title></title>
	
	
	
    <style type="text/css" media="screen">
    
    .slide-out-div {
       padding: 20px;
        width: 250px;
        background: #f2f2f2;
        border: #29216d 2px solid;
    }
    


	</style>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery.tabSlideOut.v1.3.js"></script>
         
         <script>
             $(function () {
                 $('.slide-out-div').tabSlideOut({
                     tabHandle: '.handle',                              //class of the element that will be your tab
                     pathToTabImage: 'img/contact_tab.gif',          //path to the image for the tab (optionaly can be set using css)
                     imageHeight: '122px',                               //height of tab image
                     imageWidth: '40px',                               //width of tab image    
                     tabLocation: 'left',                               //side of screen where tab lives, top, right, bottom, or left
                     speed: 300,                                        //speed of animation
                     action: 'click',                                   //options: 'click' or 'hover', action to trigger animation
                     topPos: '200px',                                   //position from the top
                     fixedPosition: false                               //options: true makes it stick(fixed position) on scroll
                 });
             });

         </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="slide-out-div">
        <a class="handle" href="http://link-for-non-js-users">Medicine List</a>
        <h3>Select Medicine</h3>
        <a href="mailto:xxx@xxx.com">xxx@xxx.com</a><br /><br />
        
    </div>
    </form>
</body>

</html>
