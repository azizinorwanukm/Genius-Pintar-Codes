﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="pusatujian.petugas.list.aspx.vb" Inherits="permatapintar.pusatujian_petugas_list1" %>

<%@ Register src="../commoncontrol/pusatujian_petugas_list.ascx" tagname="pusatujian_petugas_list" tagprefix="uc1" %>
<%@ Register src="../commoncontrol/pusatujian_view.ascx" tagname="pusatujian_view" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Senarai Pusat Ujian>Senarai Petugas" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:pusatujian_view ID="pusatujian_view1" runat="server" />&nbsp; 
    <uc1:pusatujian_petugas_list ID="pusatujian_petugas_list1" runat="server" />
</asp:Content>