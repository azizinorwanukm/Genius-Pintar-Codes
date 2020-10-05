<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengkomputeran.view.04.ascx.vb" Inherits="permatapintar.pengkomputeran_view_04" %>
<table class="fbform">
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            #
        </td>
        <td style="width: 50%;">
            <asp:Label ID="lblSoalan" runat="server" Text="Soalan"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:Label ID="lblJawapan" runat="server" Text="Pilihan Jawapan"></asp:Label>
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
            <asp:Label ID="lblNo01" runat="server" Text="49."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:CheckBoxList ID="cblQ001" runat="server">
            </asp:CheckBoxList>
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
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
        Lain-lain (Nyatakan)<br />
            <asp:TextBox ID="txtQ002" runat="server" TextMode="MultiLine" Width="350px" Rows="2"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    
</table>