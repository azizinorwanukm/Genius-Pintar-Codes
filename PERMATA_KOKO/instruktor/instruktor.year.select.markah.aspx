<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.year.select.markah.aspx.vb" Inherits="permatapintar.instruktor_year_select_markah" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Pilih Tahun
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
            <td>Peperiksaan:
            </td>
            <td>
                <select name="selPeperiksaan" id="selPeperiksaan" style="width: 200px;" runat="server">
                    <option value="1">SEMESTER 1</option>
                    <option value="2">SEMESTER 2</option>
                    <%--   <option value="3">PEPERIKSAAN 3</option>
                    <option value="4">PEPERIKSAAN 4</option>--%>
                </select>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="4">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />
                &nbsp;
            </td>
        </tr>
    </table>
    <p>
        Perhatian: <a href="../download/PANDUAN_KEMASUKAN_MARKAH_KOKURIKULUM.pdf" target="_blank">PANDUAN KEMASUKAN MARKAH KOKURIKULUM KLIK DI SINI (Fail berformat PDF)</a>
    </p>
</asp:Content>
