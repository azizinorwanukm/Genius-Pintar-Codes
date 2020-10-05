<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="default.system.message.aspx.vb" Inherits="permatapintar.default_system_message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td colspan="2">PERHATIAN
            </td>
        </tr>
        <tr>
            <td>Sistem mendapati anda telah mengubah jenis pengguna anda! <b>Username:</b><asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>&nbsp;<b>User Type:</b><asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>.
            </td>
        </tr>
        <tr>
            <td>Perbuatan ini telah dirakam dan disimpan di dalam pengkalan data kami bagi tujuan siasatan lanjut.</td>
        </tr>

    </table>
    <div class="warning" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Sila pastikan browser anda membernarkan cookies."></asp:Label>
    </div>

    <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
</asp:Content>
