<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="semak.view.04.ascx.vb"
    Inherits="permatapintar.semak_view_04" %>
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
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            &nbsp;
        </td>
        <td style="width: 50%;">
            &nbsp;
        </td>
        <td style="width: 50%;">
            [1]. Tidak ada dipamerkan oleh pelajar&nbsp;<br />
            [2]. Kadang-kadang dipamerkan oleh pelajar&nbsp;<br />
            [3]. Kerap dipamerkan oleh pelajar &nbsp;<br />
            [4]. Sentiasa dipamerkan oleh pelajar
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
            <asp:Label ID="lblNo02" runat="server" Text="02."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q2" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo03" runat="server" Text="03."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q3" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo04" runat="server" Text="04."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ004" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q4" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo05" runat="server" Text="05."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ005" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q5" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo06" runat="server" Text="06."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ006" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q6" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo07" runat="server" Text="07."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ007" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q7" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo08" runat="server" Text="08."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ008" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q8" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo09" runat="server" Text="09."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ009" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q9" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo10" runat="server" Text="10."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ010" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q10" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo11" runat="server" Text="11."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ011" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q11" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo12" runat="server" Text="12."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ012" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q12" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo13" runat="server" Text="13."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ013" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q13" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo14" runat="server" Text="14."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ014" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q14" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo15" runat="server" Text="15."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ015" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q15" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
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
            <asp:Label ID="lblNo16" runat="server" Text="16."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ016" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q16" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
