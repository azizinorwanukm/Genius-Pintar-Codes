<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="ukm1.session.end.aspx.vb" Inherits="permatapintar.ukm1_session_end"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbbluebox" width="100%" border="0px">
        <tr>
            <td class="fbsection_header">
                <h2>
                    PERHATIAN!</h2>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <a href="default.aspx">[Sila klik disini untuk menyambung semula ke laman terakhir anda.]</a><br />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Anda dikehendaki memasukkan semula MYKAD/MYKID#
            </td>
        </tr>
    </table>
</asp:Content>
