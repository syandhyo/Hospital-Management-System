<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="PHARMACYSTORE_USER_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     
</asp:Content>

