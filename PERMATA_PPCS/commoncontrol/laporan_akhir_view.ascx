<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="laporan_akhir_view.ascx.vb"
    Inherits="permatapintar.laporan_akhir_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsgTop" runat="server" Text="System message..." ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            #
        </td>
        <td style="width: 30%;">
            <asp:Label ID="lblSoalan" runat="server" Text="Soalan"></asp:Label>
        </td>
        <td style="width: 70%;">
            <asp:Label ID="lblJawapan" runat="server" Text="Pilihan Jawapan"></asp:Label>
        </td>
    </tr>
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            &nbsp;
        </td>
        <td style="width: 30%;">
            &nbsp;
        </td>
        <td style="width: 70%;">
            <img src="../img/keamatan01.png" alt="1.Amat Rendah ----------------------------------------->10.Amat Tinggi" />
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo01" runat="server" Text="01."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q1" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Ringkasan:<br />
            <asp:TextBox ID="Q001Remarks" runat="server" TextMode="MultiLine" Rows="25" Width="98%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnCreate" runat="server" Text="Jana Laporan Akhir PDF" CssClass="fbbutton"
                Visible="true" />&nbsp;<asp:HyperLink ID="hyPDF" runat="server" Target="_blank"
                    Visible="false">Klik disini.</asp:HyperLink>
        </td>
    </tr>
</table>
