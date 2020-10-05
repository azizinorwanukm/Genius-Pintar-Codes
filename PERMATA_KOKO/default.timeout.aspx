<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="default.timeout.aspx.vb" Inherits="permatapintar.default_timeout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td colspan="2">ANDA TELAH LOG KELUAR.
            </td>
        </tr>
        <tr>
            <td>Sistem mendapati anda telah log keluar. Sila log masuk semula.
            </td>
        </tr>

    </table>
    <div class="warning" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Sila pastikan browser anda membernarkan cookies."></asp:Label>
    </div>
</asp:Content>
