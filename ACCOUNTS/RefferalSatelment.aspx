<%@ Page Title="" Language="C#" MasterPageFile="~/ACCOUNTS/MasterPage.master" AutoEventWireup="true" CodeFile="RefferalSatelment.aspx.cs" Inherits="ACCOUNTS_RefferalSatelment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label" Font-Size="Smaller" ForeColor="#CCCCCC"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <div>
      <%--  <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
          <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
          <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
            <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>--%>


        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>--%>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
         <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
          <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
            <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>
    <script type="text/javascript">
        //Sys.Application.add_load(function () {
        //    $('.fromdate').datepicker({
        //        dateFormat: 'yy-mm-dd',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'yy-mm-dd',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",
        //    });
        //});

        $(function () {
            SetDatePicker();
        });

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
    </script>
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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
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
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Referral Settlement</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <br />
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server"  Visible="false"></asp:TextBox></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%;" align="right" ><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Referral Name :</td>
    <td style="width:20%;" align="left">
        <asp:DropDownList ID="droprefname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="droprefname_SelectedIndexChanged"></asp:DropDownList>
         </td></td>
    <td style="width:20%" align="right">
         Date :
    </td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdate" runat="server" CssClass="fromdate"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:40%" align="center">
       
    <td style="width:20%" align="right"></td>
    <td style="width:10%" align="left"></td>
    <td style="width:10%" align="center"></td>
</tr>
</table>--%>
           
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
        
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="center">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"    AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" >
            <Columns>  
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Pharmacy">
                    <ItemTemplate>
                        <asp:Label ID="lblpchg" runat="server" Text='<%#Eval("pchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Lab">
                    <ItemTemplate>
                        <asp:Label ID="lblichg" runat="server" Text='<%#Eval("lpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> <asp:TemplateField HeaderText="Bed">
                    <ItemTemplate>
                        <asp:Label ID="lblbchg" runat="server" Text='<%#Eval("bpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Radiology">
                    <ItemTemplate>
                        <asp:Label ID="lblrchg" runat="server" Text='<%#Eval("RpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Others">
                    <ItemTemplate>
                        <asp:Label ID="lblochg" runat="server" Text='<%#Eval("MpchgDsc")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="TOTAL&nbsp;AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="lbltotamt" runat="server" Text='<%#Eval("totamt")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>      
            
                <%--<asp:BoundField DataField="Adate" HeaderText="Date" DataFormatString="{0:MM/dd/yy}"/>          --%>  
               <%-- <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>--%>
            </Columns>
               <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <div id="divtotamt" runat="server" visible="false">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%;padding-left:60px;" align="left" >Total Amount : <asp:Label ID="lbltotamt" runat="server" Text=""></asp:Label></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
             <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%;padding-left:55px;" align="left" > Settlement Amt : <asp:TextBox ID="txtsatelment" runat="server" Width="60px"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>  
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btncreate_Click" Text="Create" /> 
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm"  Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btncancel_Click"/>       
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="center">
        <asp:GridView ID="grdReffsatel" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"   AllowPaging="True" AllowSorting="True" CssClass="table table-bordered" >
            <Columns>  
                <asp:TemplateField>
                    <ItemTemplate>
                        <%#Container.DisplayIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="REFFNAME">
                    <ItemTemplate>
                        <asp:Label ID="lblpchg" runat="server" Text='<%#Eval("REFFNAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                      <asp:TemplateField HeaderText="TOTAL&nbsp;AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="lblichg" runat="server" Text='<%#Eval("TOTAMT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="SETTLEMENT&nbsp;AMT">
                    <ItemTemplate>
                        <asp:Label ID="lblbchg" runat="server" Text='<%#Eval("SATELAMOUNT")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                  
            
                <%--<asp:BoundField DataField="Adate" HeaderText="Date" DataFormatString="{0:MM/dd/yy}"/>          --%>  
               <%-- <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>--%>
            </Columns>
               <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
         </div>
    </ContentTemplate></asp:UpdatePanel>
</asp:Content>

