<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="ukm1.select.page00.aspx.vb" Inherits="permatapintar.ukm1_select_page00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbbluebox" width="100%" border="0px">
        <tr>
            <td>
                <asp:Label ID="lblSelect" runat="server" Text="*Sila pilih jawapan yang mempunyai maksud terdekat."
                    CssClass="fbinstruction"></asp:Label>
            </td>
        </tr>  
        <tr>
            <td>
                <asp:Label ID="lblQ01" runat="server" Text="" CssClass="lblQuestion"></asp:Label>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rbQ01"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rbQ01" runat="server" CssClass="lblSelect" RepeatLayout="Table"
                    RepeatDirection="Vertical">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
