<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master"
    CodeBehind="esurvey.page.done.aspx.vb" Inherits="PERMATA_EQTest.esurvey_page_done" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="right">
                <img src="img/permatapintar.jpg" width="160px" height="150px" alt="PERMATApintar" />
            </td>
        </tr>
    </table>
    <h2>
        <asp:Label ID="lblThankheader" runat="server" Text="Label"></asp:Label>
    </h2>

    <table id="mycustomtable">
        <tr>
            <th>
                <asp:Label ID="Label1" runat="server" Text="SelfAwareness"></asp:Label>
            </th>
            <th>
                <asp:Label ID="Label2" runat="server" Text="SelfRegulation"></asp:Label>
            </th>
            <th>
                <asp:Label ID="Label3" runat="server" Text="SelfMotivation"></asp:Label>
            </th>
            <th>
                <asp:Label ID="Label4" runat="server" Text="Empathy"></asp:Label>
            </th>
            <th>
                <asp:Label ID="Label5" runat="server" Text="SocialSkill"></asp:Label>
            </th>
            <th>
                <asp:Label ID="Label6" runat="server" Text="Spirituality"></asp:Label>
            </th>
            <th>
                <asp:Label ID="Label7" runat="server" Text="Maturity"></asp:Label>
            </th>
        </tr>
        <tr>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain01" runat="server" Text="0"></asp:Label>%</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain02" runat="server" Text="0"></asp:Label>%</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain03" runat="server" Text="0"></asp:Label>%</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain04" runat="server" Text="0"></asp:Label>%</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain05" runat="server" Text="0"></asp:Label>%</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain06" runat="server" Text="0"></asp:Label>%</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">
                <asp:Label ID="lblDomain07" runat="server" Text="0"></asp:Label>%</td>
        </tr>
        <tr>
            <td colspan="7">&nbsp;</td>

        </tr>
        <tr>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;">Your Index</td>
            <td style="font-size: 20px; font-weight: bolder; vertical-align: top;" colspan="6">
                <asp:Label ID="lblScorePercentage" runat="server" Text="Score"></asp:Label>%</td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:Button ID="btnEmail" runat="server" Text="Send Email" CssClass="fbbutton" />&nbsp;<asp:Label ID="lblEmailAdd" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;<asp:LinkButton ID="lnkChart" runat="server">Display Chart</asp:LinkButton></td>
        </tr>

    </table>

    <p>
        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Visible="true"></asp:Label><br />
    </p>
</asp:Content>
