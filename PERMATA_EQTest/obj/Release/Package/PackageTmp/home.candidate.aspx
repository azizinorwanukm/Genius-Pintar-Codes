<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master"
    CodeBehind="home.candidate.aspx.vb" Inherits="PERMATA_EQTest.home_candidate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="right">
                <img src="img/permata-moto.png" width="400px" height="80px" alt="PERMATApintar" />
            </td>
        </tr>
    </table>
    <h2>
        <asp:Label ID="lblHomeOverview" runat="server" Text=""></asp:Label></h2>
    <p>
        <asp:Label ID="lblHomeCandidate" runat="server" Text=""></asp:Label></p>
    <h2>
        <asp:Label ID="lblHomeBenefit" runat="server" Text="" Visible="false"></asp:Label></h2>
    <p>
        <asp:Label ID="lblHomeBenefitContent" runat="server" Text="" Visible="false"></asp:Label></p>
    <p>
        <asp:LinkButton ID="lnkNext" runat="server">Continue</asp:LinkButton>
    </p>
</asp:Content>
