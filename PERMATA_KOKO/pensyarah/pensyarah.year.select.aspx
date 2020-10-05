<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pensyarah/pensyarah.master" CodeBehind="pensyarah.year.select.aspx.vb" Inherits="permatapintar.pensyarah_year_select" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Main Menu>Masukkan Markah>Pilih Tahun
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="4">Pilih
            </td>
        </tr>
        <tr>
            <td class="fbtd_left">Tahun:
            </td>
            <td>
                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
    </div>

</asp:Content>
