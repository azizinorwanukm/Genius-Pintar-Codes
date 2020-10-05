<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PPCS_Eval_Weekly_create.ascx.vb"
    Inherits="permatapintar.PPCS_Eval_Weekly_create1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td class="fbsection_sap" colspan="3">LAPORAN MINGGUAN.
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo01" runat="server" Text="01."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
        <td>&nbsp
        </td>
        <td>Catatan:<br />
            <asp:TextBox ID="Q001Remarks" runat="server" TextMode="MultiLine" Rows="6" Width="600px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">&nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo02" runat="server" Text="02."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
        <td>&nbsp
        </td>
        <td>Catatan:<br />
            <asp:TextBox ID="Q002Remarks" runat="server" TextMode="MultiLine" Rows="6" Width="600px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">&nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo03" runat="server" Text="03."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
        <td>&nbsp
        </td>
        <td>Catatan:<br />
            <asp:TextBox ID="Q003Remarks" runat="server" TextMode="MultiLine" Rows="6" Width="600px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />&nbsp;
            <asp:LinkButton
                ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
        </td>
    </tr>
</table>
