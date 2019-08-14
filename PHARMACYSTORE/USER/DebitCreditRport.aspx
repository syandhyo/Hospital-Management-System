<%@ Page Title="" Language="C#" MasterPageFile="~/PHARMACYSTORE/USER/MasterPage.master" AutoEventWireup="true" CodeFile="DebitCreditRport.aspx.cs" Inherits="PHARMACYSTORE_USER_DebitCreditRport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>( <asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)

     <script type="text/javascript">

         function Validate() {

             if (document.getElementById("<%=txt_Fdate.ClientID%>").value == "") {
                 alert("Admission From Date Is Required !");
                 document.getElementById("<%=txt_Fdate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txt_Todate.ClientID%>").value == "") {
                 alert("Admission To Date is Required !");
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
        //Sys.Application.add_load(function () {
        //    $('.formdate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",

        //    });
        //    $('.todate').datepicker({
        //        dateFormat: 'dd-mm-yy',
        //        changeMonth: true,
        //        changeYear: true,
        //        minDate: '-75Y',
        //        yearRange: "c-75:c+10",
        //    });
        //});
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
            $("[id$=txt_Fdate]").datepicker({
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
                buttonImage: 'img/calendar.png'

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
    <td style="width:20%" align="center" class="auto-style1"><strong>Debit/Credit Report</strong></td>
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
    <td style="width:20%" align="right">From Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txt_Fdate" runat="server" CssClass="formdate" BackColor="#CCFFFF" onkeydown="return false;" onpaste ="return false;"></asp:TextBox></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="right">To Date :</td>
    <td style="width:20%" align="left">
       <asp:TextBox ID="txt_Todate" runat="server" CssClass="formdate" BackColor="#CCFFFF" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
         </td>
    <td style="width:20%" align="left">
       </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">
       
    </td>
    <td style="width:20%" align="left">
         
    </td>
    <td style="width:20%" align="right">
         <asp:Button ID="btnDebitShow" runat="server" CssClass="btn btn-primary btn-sm"  Text="Debit Bill" OnClick="btnDebitShow_Click" OnClientClick="return Validate();" />
    </td>
    <td style="width:20%" align="left">
        <asp:Button ID="btnCredit" runat="server" CssClass="btn btn-primary btn-sm"  Text="Credit Bill" OnClick="btnCredit_Click"  OnClientClick="return Validate();"/>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">        
        <asp:Button ID="btn_Download" runat="server" CssClass="btn btn-primary btn-sm"  Text="Download" Visible="false" OnClick="btn_Download_Click"/></td>
    <td style="width:20%" align="left"></td>
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
        <div runat="server" id="debit_Grid" >
        
        <table width="100%">
           
           <%-- gridview--%>
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
       <asp:GridView ID="grdd_show" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True" AllowSorting="True"   CssClass="table table-bordered"  >
            <Columns> 
                 <asp:BoundField DataField="ID" HeaderText="Sl No" />
                 <asp:BoundField DataField="RTYPE" HeaderText="Sale Type " />
                <asp:BoundField DataField="PNAME" HeaderText="PATIENT NAME" />               
                <asp:BoundField DataField="INVDATE" HeaderText="INVOICE DATE" /> 
                 <asp:BoundField DataField="TOTALPRICE" HeaderText="TOTAL PRICE" />
                 <asp:BoundField DataField="TOTALDISCAMT" HeaderText="TOTAL DISCAMT" /> 
                 <asp:BoundField DataField="TOTALAMT" HeaderText="TOTAL AMOUNT" />
                 <asp:BoundField DataField="GSTAMT" HeaderText="GST AMOUNT" /> 
                 <asp:BoundField DataField="GT" HeaderText="GRAND TOTAL" />

                  <asp:BoundField DataField="IPNO" HeaderText="IP NO" /> 
                 <asp:BoundField DataField="PAMT" HeaderText="PAID AMT" />
                  <asp:BoundField DataField="BAMT" HeaderText="BALANCE AMOUNT" /> 
                 
              <%--  <asp:TemplateField HeaderText="DOWNLOAD">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbn_download" runat="server" OnClick="lbn_download_Click" href='<%#Eval("FILEUPLOAD") %>' >Download</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
               <%-- <asp:CommandField ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" SelectText="&nbsp;&nbsp;&nbsp; Edit" HeaderText="Action" />--%>
            </Columns>
        </asp:GridView>
    </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            </div>

          

        </div>   
             <%-- </ContentTemplate></asp:UpdatePanel>--%>

</asp:Content>

