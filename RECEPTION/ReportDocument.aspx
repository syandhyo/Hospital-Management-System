<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="ReportDocument.aspx.cs" Inherits="RECEPTION_ReportDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
    <script type="text/javascript">
        
        function Validate() {
          
            if (document.getElementById("<%=txt_Fdate.ClientID%>").value == "") {
                alert("*Please!!Enter The From Date..");
                document.getElementById("<%=txt_Fdate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txt_Todate.ClientID%>").value == "") {
                alert("*Please!!Enter The To Date..");
                document.getElementById("<%=txt_Todate.ClientID%>").focus();
                return false;
            }
        }
    </script>
   
  

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>--%>
    <div>
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
            $("[id$=txt_Fdate]").datepicker({
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
            $("[id$=txt_Todate]").datepicker({
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
    <td style="width:20%" align="center">
         <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
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
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center" class="auto-style1"><strong>Download Report</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server" CssClass="form-control input-sm m-bot15" Visible="false"></asp:TextBox>
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <br />
        <div id="div1" runat="server">
       
            
    </div>
        <div id="div2" runat="server" >
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
    <td style="width:20%" align="right">From Date :</td>
    <td style="width:20%" align="left"> <asp:TextBox ID="txt_Fdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox></td>
    <td style="width:20%" align="left">
       

    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">To Date :</td>
    <td style="width:20%" align="left">
         <asp:TextBox ID="txt_Todate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;" ></asp:TextBox>

    </td>
    <td style="width:20%" align="left">
     
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
         
    </td>
         
    <td style="width:20%;" align="right">        </td>
    <td style="width:20%;" align="center"><asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary btn-sm"  Text="Show" OnClick="btnShow_Click" OnClientClick="return Validate();" />
       
         </td>
    <td style="width:20%">        
        &nbsp;</td>
    <td style="width:20%;padding-right:350px;"> </td>
</tr>
</table>
        
             <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">        
        
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
      <%--  <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm m-bot15 formdate" BackColor="#CCFFFF"></asp:TextBox>--%>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        
          
         <table width="100%">
             <%--button--%>
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right" >
       
         &nbsp;<%--<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btndelete" runat="server" CssClass="btn btn-primary btn-sm"  Text="Delete" Visible="False" />--%>&nbsp;
        <%--<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm"  Text="Cancel" />--%>
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <br />
            </div>
        <table width="100%">
           <%-- gridview--%>
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True" AllowSorting="True"  pagesize="10" CssClass="table table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns> 
                 <%--<asp:BoundField DataField="ID" HeaderText="Sl No" />--%>
                 <asp:BoundField DataField="PATIENTID" HeaderText="PATIENT ID" />
                <asp:BoundField DataField="NAME" HeaderText="NAME" />               
                <asp:BoundField DataField="DATE" HeaderText="DATE" /> 
                 <asp:BoundField DataField="SYSDATE" HeaderText="CURRENT DATE" />
                <asp:TemplateField HeaderText="ACTION">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbn_download" runat="server" OnClick="lbn_download_Click" href='<%#Eval("FILEUPLOAD") %>' >Show</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />--%>
            </Columns>
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        </div>   
             <%-- </ContentTemplate></asp:UpdatePanel>--%>

</asp:Content>

