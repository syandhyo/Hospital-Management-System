<%@ Page Title="" Language="C#" MasterPageFile="~/RECEPTION/MasterPage.master" AutoEventWireup="true" CodeFile="nicu.aspx.cs" Inherits="RECEPTION_nicu" %>

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
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        Sys.Application.add_load(function () {


            $('.formdate').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",

            });
            $('.todate').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate: '-75Y',
                yearRange: "c-75:c+10",
            });
        });
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
    <td style="width:20%" align="center" class="auto-style1"><strong>NICU Admission</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"><asp:TextBox ID="txtid" runat="server" CssClass="" Visible="false"></asp:TextBox>
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>   
             
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">

        <div style="border: thin solid #000000">
        <table width="100%">
     <tr>
    <td style="width:20%" align="right">Date :</td>
    <td style="width:20%" align="left">        
        <asp:TextBox ID="txtdate" runat="server" CssClass="formdate" Enabled="False"></asp:TextBox>
    </td>
    <td style="width:20%" align="right"><asp:Label ID="Label3" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Regn No.</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtregn" runat="server" ></asp:TextBox>
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
        <table width="100%">
            <tr>
    <td style="width:20%" align="right">
        <asp:Label ID="Label9" runat="server" ForeColor="#CC0000" Text="*"></asp:Label>
        Patient Id :</td>
    <td style="width:20%" align="left">
        
         <asp:TextBox ID="txtpatientid" runat="server"></asp:TextBox>
        
                </td>
    <td align="right" class="auto-style3">IP No. :</td>
    <td style="width:20%" align="left">        
        
         <asp:TextBox ID="txtipno" runat="server" Enabled="False"></asp:TextBox>
                </td>
    <td style="width:20%" align="center"></td>
</tr>
     <tr>
    <td style="width:20%" align="right">Name :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtname" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td align="right" class="auto-style3">Mother Exit :</td>
    <td style="width:20%" align="left">        
        <asp:DropDownList ID="dropmotherexit" runat="server" style="width:131px">
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">   
    </td>
    <td style="width:20%" align="right">Combine Bill with Mother :</td>
    <td style="width:20%" align="left">
        
         <asp:DropDownList ID="dropcombinebill" runat="server" style="width:131px">
             <asp:ListItem>Yes</asp:ListItem>
             <asp:ListItem>No</asp:ListItem>
         </asp:DropDownList>
        
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">Minor Patient :</td>
    <td style="width:20%" align="left">        
        
        <asp:TextBox ID="txtminorpatient" runat="server"></asp:TextBox>
        
    </td>
    <td style="width:20%" align="right">Emergency :</td>
    <td style="width:20%" align="left">
        
         <asp:TextBox ID="txtemergency" runat="server"></asp:TextBox>
        
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
            <table width="100%">
     <tr>
    <td style="width:20%" align="right">Patient Condition :</td>
    <td style="width:20%" align="left">        
        
        <asp:TextBox ID="txtpatientcond" runat="server"></asp:TextBox>
        
    </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left">
        
         </td>
    <td style="width:20%" align="left">        
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>    
</tr>
</table>--%>
        <div>
        
            <div style="border: thin solid #000000">
            <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">--%>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:30%" align="center" class="auto-style1"><strong>Patient&#39;s Contact &amp; Other Information</strong></td>
    <td style="width:10%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label5" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Gender :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropgender" runat="server" style="width:133px">
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Date of Birth :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdateofbirth" runat="server" CssClass="formdate"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Blood Group :-</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropbloodgroup" runat="server" style="width:133px">
            <asp:ListItem>O+ve</asp:ListItem>
            <asp:ListItem>O-ve</asp:ListItem>
            <asp:ListItem>A+ve</asp:ListItem>
            <asp:ListItem>A-ve</asp:ListItem>
            <asp:ListItem>B+ve</asp:ListItem>
            <asp:ListItem>B-ve</asp:ListItem>
            <asp:ListItem>AB+ve</asp:ListItem>
            <asp:ListItem>AB-ve</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label6" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Address :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtaddress" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">District :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdistrict" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Pin :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtpin" runat="server" pattern="[0-9]+" MaxLength="6" Minlength="6"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">State :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtstate" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Telephone No :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttelno" runat="server" pattern="[0-9]{10,10}" MaxLength="10" Minlength="10"></asp:TextBox>
         </td>
   <td style="width:20%" align="right"><asp:Label ID="Label7" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Mobile No.:-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtmobileno" runat="server" pattern="[0-9]{10,10}" MaxLength="10" Minlength="10"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     <table width="100%">
     <tr>

    <td style="width:20%" align="right">Email Id :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtemailid" runat="server" type="email"  pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        &nbsp;</td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>    
    </tr>
</table> --%>              
                </div>
        <div style="border: thin solid #000000">
            <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="center">--%>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>&nbsp;Referal Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
       <table width="100%">
     <tr>
    <td style="width:20%" align="right">From . :-</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtfrom" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
       <td style="width:20%" align="right">Referal Name :-&nbsp;</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtrefname" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>
    </tr>
</table>--%>
                 </div>
        <div style="border: thin solid #000000">
            <%--<table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:60%" align="left">--%>
        <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"><strong>&nbsp;Booking Details</strong></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center"></td>
</tr>




</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Present Complaint :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtcomplaint" runat="server" TextMode="MultiLine"></asp:TextBox>
         </td>
    <td style="width:20%" align="right">Consulting Department :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdept" runat="server" style="width:131px" OnSelectedIndexChanged="dropdept_SelectedIndexChanged">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Doctor Name :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="dropdoctor" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Operation/Treatment :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txttreatment" runat="server" pattern="^[A-Za-z -]+$"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right"><asp:Label ID="Label1" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Room Type :</td>
    <td style="width:20%" align="left">
        <asp:DropDownList ID="droproomtype" runat="server" style="width:134px">
            <asp:ListItem>A/C</asp:ListItem>
            <asp:ListItem>Non A/C</asp:ListItem>
        </asp:DropDownList>
         </td>
    <td style="width:20%" align="right">Operation Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtoperationdate" runat="server" CssClass="formdate"></asp:TextBox>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table><table width="100%">
     <tr>
    <td style="width:20%" align="right">Days For Stay :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtdays" runat="server"></asp:TextBox>
         </td>
    <td style="width:20%" align="right"><asp:Label ID="Label8" runat="server" Text="*" ForeColor="#CC0000"></asp:Label>Booking For Date :</td>
    <td style="width:20%" align="left">
        <asp:TextBox ID="txtbookingdate" runat="server" CssClass="formdate" ></asp:TextBox>
        `</td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
    <%--</td>
    <td style="width:20%" align="right"></td>
</tr>
</table>--%>

        </div>
         <table width="100%">
     <tr>
    <td style="width:20%" align="right">&nbsp;</td>
    <td style="width:20%" align="left">
        &nbsp;</td>
    <td style="width:20%" align="center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Submit" />
        &nbsp;<asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="Button2_Click" Text="Update" Visible="False" />       
         &nbsp;<asp:Button ID="btncancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="Button3_Click" Text="Cancel" />
         </td>
    <td style="width:20%" align="left">
        &nbsp;</td>
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
    <td style="text-align: center;" align="center">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="10" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging" >
            <Columns> 
                <asp:BoundField DataField="REGN_NO" HeaderText="REGN_NO No" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="IPNO" HeaderText="IPNO" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="NAME" HeaderText="Name" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="MOBILE" HeaderText="MOBILE" HeaderStyle-CssClass="text-center"/>
                <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:MM/dd/yy}" HeaderStyle-CssClass="text-center"/>                                  
                <asp:CommandField ShowDeleteButton="true" ShowEditButton="False" ShowSelectButton="true" SelectText="Edit" HeaderText="Action" HeaderStyle-CssClass="text-center"/>
            </Columns>
              <SelectedRowStyle BackColor="LightPink" Font-Italic="true" ForeColor="Crimson" />
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </td>
    <td style="width:20%" align="left"></td>
</tr>
</table>


            </td>
    <td style="width:20%" align="right"></td>    
</tr>
</table>
        
        
              </ContentTemplate></asp:UpdatePanel> 
</asp:Content>

