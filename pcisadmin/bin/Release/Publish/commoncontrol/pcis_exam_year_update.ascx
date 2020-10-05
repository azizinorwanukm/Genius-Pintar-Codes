<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pcis_exam_year_update.ascx.vb" Inherits="araken.pcisadmin.pcis_exam_year_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini pcis_exam_year
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">pcis_exam_year
        </td>
        <td>:<asp:TextBox ID="txtdescription" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">start_date
        </td>
        <td>:<asp:TextBox ID="txtstart_date" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">end_date
        </td>
        <td>:<asp:TextBox ID="txtend_date" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    
     <tr>
        <td class="fbform_sap" colspan="2"></td>
    </tr>
    <tr>
        <td  colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Padam" CssClass="fbbutton" />&nbsp;&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkExamYear" runat="server">Set pcis_setting exam_year</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkView" runat="server">Senarai Tahun Ujian</asp:LinkButton>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>