﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ketuamodul/main.Master" CodeBehind="laporan.akhir.create.aspx.vb" Inherits="permatapintar.laporan_akhir_create2" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="ukm2" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/laporan.akhir.create.ascx" TagName="laporan"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan Akhir
            </td>
        </tr>
    </table>
    <uc1:ukm2 ID="ukm21" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:laporan ID="laporan1" runat="server" />
</asp:Content>
