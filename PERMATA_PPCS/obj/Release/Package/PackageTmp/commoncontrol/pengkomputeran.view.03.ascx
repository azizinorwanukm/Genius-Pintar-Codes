<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pengkomputeran.view.03.ascx.vb" Inherits="permatapintar.pengkomputeran_view_03" %>
<table class="fbform">
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            #
        </td>
        <td style="width: 50%;">
            <asp:Label ID="lblSoalan" runat="server" Text="Soalan"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:Label ID="lblJawapan" runat="server" Text="Pilihan Penilaian"></asp:Label>
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
    <tr class="fbsection_bold">
        <td style="width: 2%;">
        </td>
        <td style="width: 50%;">
        </td>
        <td style="width: 50%;">
            1. Tidak Pernah<br />
            2. Kurang daripada 5 kali seminggu<br />
            3. 6 – 10 kali seminggu<br />
            4. Lebih daripada 10 kali seminggu
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
            <asp:Label ID="lblNo01" runat="server" Text="34."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q1" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo02" runat="server" Text="35."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q2" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo03" runat="server" Text="36."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q3" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo04" runat="server" Text="37."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ004" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q4" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo05" runat="server" Text="38."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ005" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q5" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo06" runat="server" Text="39."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ006" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q6" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo07" runat="server" Text="40."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ007" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q7" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo08" runat="server" Text="41."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ008" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q8" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo09" runat="server" Text="42."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ009" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q9" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo10" runat="server" Text="43."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ010" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q10" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo11" runat="server" Text="44."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ011" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q11" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo12" runat="server" Text="45."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ012" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q12" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo13" runat="server" Text="46."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ013" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q13" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo14" runat="server" Text="47."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ014" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q14" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
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
            <asp:Label ID="lblNo15" runat="server" Text="48."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ015" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q15" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
