<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="ukm1.system.message.aspx.vb" Inherits="permatapintar.ukm1_system_message" %>


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
                <asp:Label ID="lblMsg01" runat="server" Text="This test is available between 7AM until 12AM only. Please come again."
                    Font-Bold="true" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
