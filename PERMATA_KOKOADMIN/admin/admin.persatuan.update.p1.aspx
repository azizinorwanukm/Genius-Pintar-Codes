﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.persatuan.update.p1.aspx.vb" Inherits="permatapintar.admin_persatuan_update_p1" %>

<%@ Register Src="../commoncontrol/instruktor_view_header.ascx" TagName="instruktor_view_header" TagPrefix="uc2" %>
<%@ Register src="../commoncontrol/koko_pelajar_mark_persatuan_p1.ascx" tagname="koko_pelajar_mark_persatuan_p1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Masukkan Markah>Kemaskini
            </td>
        </tr>
    </table>
    <uc2:instruktor_view_header ID="instruktor_view_header1" runat="server" />
    &nbsp;
    <uc1:koko_pelajar_mark_persatuan_p1 ID="koko_pelajar_mark_persatuan_p11" runat="server" />
</asp:Content>