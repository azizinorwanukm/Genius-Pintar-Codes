﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.year.select.aspx.vb" Inherits="permatapintar.pelajar_year_select" %>

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
            <td class="fbform_sap" colspan="4">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />

            </td>
        </tr>
    </table>

</asp:Content>
