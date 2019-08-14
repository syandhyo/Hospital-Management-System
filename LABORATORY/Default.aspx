<%@ Page Title="" Language="C#" MasterPageFile="~/LABORATORY/MasterPage1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="LABORATORY_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>(<asp:Label ID="lblfyear" runat="server" Text="Label"></asp:Label>)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="right"></td>
    <td style="width:20%" align="left"></td>
    <td style="width:20%" align="center">
        <asp:Label ID="lblorgid" runat="server" Text="Label" Visible="false"> </asp:Label>
            <asp:Label ID="lbluid" runat="server" Text="Label" Visible="false"> </asp:Label>
       
    </td>
         <td> <img src="../gimg/Laboratory.jpg"  height="630px" width="1180px" style="margin-top:-50px;margin-left:-25px;"></td>
</tr>
</table>
    <table width="100%">
     <tr>
    <td style="width:20%" align="right"></td>
    <td style="text-align: center;" align="left">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:Timer ID="Timer1" runat="server" Interval="600" OnTick="Timer1_Tick"></asp:Timer>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered" DataKeyNames="ID" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanging="GridView2_SelectedIndexChanging">
            <Columns>
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                 <asp:BoundField DataField="WARD" HeaderText="WARD" />
                 <asp:BoundField DataField="TESTTYPE" HeaderText="TEST&nbsp;TYPE" />
                <asp:BoundField DataField="PID" HeaderText="PID" />
                <asp:BoundField DataField="NAME" HeaderText="&nbsp;NAME&nbsp;" />
                <asp:BoundField DataField="BEDNO" HeaderText="BEDNO" />
                <asp:BoundField DataField="STATUS" />
                <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Select" Enabled="false" Height="35px" ImageUrl='<%# Eval("STATUS") %>' Width="110px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="false" ShowSelectButton="true" />
            </Columns>
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
                 </ContentTemplate>
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
          </Triggers>
 
      </asp:UpdatePanel>
         </td>
    <td style="width:20%" align="center"></td>
</tr>
</table>
     
</asp:Content>


