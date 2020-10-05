<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="ukm1.invalid.url.aspx.vb" Inherits="permatapintar.ukm1_invalid_url" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblPerhatian" runat="server" Text="WARNING!"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblInvalidURL" runat="server" Text="We had detected INVALID URL or and attempt to change the URL. Please DO NOT key in the URL manually."></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
