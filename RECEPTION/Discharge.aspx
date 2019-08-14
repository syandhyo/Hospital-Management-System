<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="Discharge.aspx.cs" Inherits="RECEPTION_Discharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            color: #000000;
            text-decoration: underline;
        }
        .auto-style2 {
            color: #000000;
        }
        .auto-style3 {
            color: #000000;
            font-weight: bold;
        }
    </style>
      <script type = "text/javascript">

          function SetTarget() {

              document.forms[0].target = "_blank";

          }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
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
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
         <asp:Label ID="lblins" runat="server" Text="" Visible="false"> </asp:Label>
       
    </td>
</tr>
</table>
 
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="TXTID" runat="server" CssClass="form-control input-sm m-bot15" Enabled="False" Visible="False"></asp:TextBox>
         </td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"><strong>Patient Discharge</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align: center;"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; align: center;"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    
    <table width="100%">
     <tr>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left" class="auto-style2">
        &nbsp;</td>
    <td style="width:20%" align="right">
        &nbsp;</td>
    <td style="width:20%" align="left">&nbsp;</td>
    <td style="width:20%" align="center">
         <asp:Label ID="LBBALANCEAMT" runat="server" Text="0" Visible="false"></asp:Label>
         <asp:Label ID="LBPAIDAMT" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
</tr>

    
   


    <table width="80%">
<tr>
    <td style="width:20%" align="right" class="auto-style2">
       
        <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Select Bed No :</td>
    <td style="width:20%" align="left">
        
        <asp:DropDownList ID="dropbedno" runat="server" AutoPostBack="true" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged">
        </asp:DropDownList>
        
        </td>
    <td style="width:20%" align="right" class="auto-style2">
      <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" Text="*"></asp:Label> 
        Date :</td>
    <td style="width:40%" align="left">
       
         <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" onkeydown="return false;" onpaste ="return false;"></asp:TextBox>
       
         </td>
    
</tr>




     <tr>
    <td style="width:20%" align="right" class="auto-style2">
        <asp:Label ID="Label5" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        IPD NO. :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblpid" runat="server" CssClass="auto-style3" Text=""></asp:Label>
        </td>
    <td style="width:20%" align="right" class="auto-style2">
        <asp:Label ID="Label7" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Name :</td>
    <td style="width:40%" align="left">
        <asp:Label ID="lblpname" runat="server" CssClass="auto-style3" Text=""></asp:Label>
         </td>
    
</tr>
     <tr>
    <td style="width:20%" align="right" class="auto-style2">
        <asp:Label ID="Label6" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Ward :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblward" runat="server" CssClass="auto-style3" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="right" class="auto-style2">
        <asp:Label ID="Label8" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Bed No :</td>
    <td style="width:20%" align="left">
        <asp:Label ID="lblbedno" runat="server" CssClass="auto-style3" Text=""></asp:Label>
         </td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Condition on Discharge :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropstatus" runat="server" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem>Alive</asp:ListItem>
            <asp:ListItem>Dead</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right" class="auto-style2"><asp:Label ID="Label4" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Patient Discharge :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdischarge" runat="server" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="dropbedno_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem>Discharge</asp:ListItem>
            <asp:ListItem>Abscond</asp:ListItem>
             <asp:ListItem>UnBlock</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right">
        <%--<asp:Label ID="Label10" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>--%>
        <span class="auto-style2">Consultant :</span></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="TXTdoctor" runat="server" CssClass="form-control input-sm m-bot15"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">
        <%--<asp:Label ID="Label11" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>--%>
        <span class="auto-style2">Diagnosis :</span></td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="TXTdiagnosis" runat="server" CssClass="form-control input-sm m-bot15" TextMode="MultiLine" Width="300px"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2">Operative Procedure:</td>
    <td style="width:40%" align="left">
        <asp:TextBox ID="TXTclinicalsummary" runat="server" CssClass="form-control input-sm m-bot15" Height="63px" TextMode="MultiLine" Width="304px"></asp:TextBox>
        
         </td>    
    <td style="width:20%" align="right" class="auto-style2">Investigation :</td>
    <td style="width:20%" align="center"> <asp:TextBox ID="TXTinvestigation" runat="server" CssClass="form-control input-sm m-bot15" Height="63px" Width="304px"></asp:TextBox>
        
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2"></td>
    <td style="width:40%" align="left">
       
         </td>
    
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"> </td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2">Treatment :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="TXTtraetment" runat="server" CssClass="form-control input-sm m-bot15" Height="63px" TextMode="MultiLine" Width="304px"></asp:TextBox>
         </td>
    <td style="width:20%" align="right" class="auto-style2">Operative Findings :</td>
    <td style="width:20%" align="left"><asp:TextBox ID="TXTdiscussion" runat="server" CssClass="form-control input-sm m-bot15" Height="63px" TextMode="MultiLine" Width="304px"></asp:TextBox></td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2">Advice :</td>
    <td style="width:40%" align="left"><asp:TextBox ID="TXTadvice" runat="server" CssClass="form-control input-sm m-bot15" Height="63px" TextMode="MultiLine" Width="304px"></asp:TextBox>
        
        
         </td>
    
    <td style="width:20%" align="right" class="auto-style2">Past History :</td>
    <td style="width:20%" align="center"><asp:TextBox ID="TXTpast" runat="server" CssClass="form-control input-sm m-bot15" TextMode="MultiLine" Height="63px" Width="304px"></asp:TextBox>
      
         </td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2">Chief Complains :</td>
    <td style="width:40%" align="left"><asp:TextBox ID="TXTcomplains" runat="server" CssClass="form-control input-sm m-bot15" TextMode="MultiLine" Height="63px" Width="304px"></asp:TextBox>
        
         </td>
    
    <td style="width:20%" align="right" class="auto-style2">Follow Up :</td>
    <td style="width:20%" align="center">
         <asp:TextBox ID="TXTRemarks" runat="server" CssClass="form-control input-sm m-bot15" Height="63px" TextMode="MultiLine" Width="304px"></asp:TextBox></td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2">&nbsp;</td>
    <td style="width:40%" align="left">
        &nbsp;</td>
    
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        
         </td>
</tr>

     <tr>
    <td style="width:20%" align="right" class="auto-style2">&nbsp;</td>
    <td style="width:40%" align="left">
        &nbsp;</td>
    
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr><table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>

<table width="100%">
     
       
     <tr>
    <td style="width:40%" align="right"></td>
    <td align="center">
        <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Discharge" Width="80px" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Delete" Visible="False" />
        &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td  align="right"></td>
</tr>

     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%; text-align: center;" align="right" class="auto-style1"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>

     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
          
         </td>
    <td style="width:20%" align="center">&nbsp;</td>
</tr>

     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>

</table>
        &nbsp;<table class="nav-justified">
            <tr><td text-align: center;" align="center" class="auto-style1"> <strong>Discharge History</strong></td></tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" PageSize="10">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="" />
                             <asp:BoundField DataField="DISCHARGE" HeaderText="DISCHARGE" />
                            <asp:BoundField DataField="DATE" HeaderText="DATE" />
                            <asp:BoundField DataField="PID" HeaderText="PID" />
                            <asp:BoundField DataField="NAME" HeaderText="NAME" />
                            <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                            <asp:CommandField HeaderText="Action" SelectText="&nbsp;&nbsp;&nbsp; Edit" ShowDeleteButton="False" ShowEditButton="False" ShowSelectButton="true" />
                            <asp:TemplateField HeaderText="PRINT">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="SetTarget();">Print</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
</div>
   </ContentTemplate></asp:UpdatePanel>
    </asp:Content>

