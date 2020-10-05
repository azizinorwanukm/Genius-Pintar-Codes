<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pcis_exam_year_create.ascx.vb" Inherits="araken.pcisadmin.pcis_exam_year_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tambah pcis_exam_year
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">pcis_exam_year
        </td>
        <td>:<asp:TextBox ID="txtdescription" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    
     <tr>
        <td class="fbform_sap" colspan="2"></td>
    </tr>
    <tr>
        <td  colspan="2">
            <asp:Button ID="btnCreate" runat="server" Text="Tambah" CssClass="fbbutton" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkView" runat="server">Senarai Tahun Ujian</asp:LinkButton>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>