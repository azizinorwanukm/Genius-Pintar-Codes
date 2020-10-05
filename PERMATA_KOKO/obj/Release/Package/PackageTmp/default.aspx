<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="default.aspx.vb" Inherits="permatapintar._default1" %>

<%@ Register Src="commoncontrol/pengumuman_view_top.ascx" TagName="pengumuman_view_top" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">ARAHAN
            </td>
        </tr>
        <tr>
            <td>Sila perhatikan PENGUMUMAN pada menu di sebelah kanan skrin. Klik untuk melihat maklumat lanjut.
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td>Pelajar: Sila masukkan MYKAD yang telah didaftarkan di dalam sistem PERMATApintar.</td>
        </tr>
        <tr>
            <td>Instruktor: Sila dapatkan Login ID dan Kata Laluan dari pihak pentadbiran.</td>
        </tr>
        <tr>
            <td>Pengarah: Sila dapatkan Login ID dan Kata Laluan dari pihak pentadbiran.</td>
        </tr>

    </table>
    &nbsp;<uc1:pengumuman_view_top ID="pengumuman_view_top1" runat="server" />
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Arahan tetap atau arahan yang wajib dipatuhi oleh semua pengguna."></asp:Label>
    </div>

</asp:Content>
