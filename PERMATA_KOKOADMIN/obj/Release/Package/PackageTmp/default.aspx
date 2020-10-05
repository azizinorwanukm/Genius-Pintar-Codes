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
            <td>
                <p>Laman ini bagi kegunaan Pihak Pengurusan Kokurikulum PERMATApintar sahaja!</p>
            </td>
        </tr>

    </table>
    &nbsp;<uc1:pengumuman_view_top ID="pengumuman_view_top1" runat="server" />
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Arahan tetap atau arahan yang wajib dipatuhi oleh semua pengguna."></asp:Label>
    </div>

</asp:Content>
