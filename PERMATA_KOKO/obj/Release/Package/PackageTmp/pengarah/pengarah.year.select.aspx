<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah/pengarah.Master" CodeBehind="pengarah.year.select.aspx.vb" Inherits="permatapintar.pengarah_year_select" %>

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
                <asp:dropdownlist id="ddlTahun" runat="server" autopostback="false" width="200px">
                </asp:dropdownlist>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:button id="btnLoad" runat="server" text="Cari " cssclass="fbbutton" />
                &nbsp;
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:label id="lblMsg" runat="server" text="System message..."></asp:label>
    </div>
</asp:Content>