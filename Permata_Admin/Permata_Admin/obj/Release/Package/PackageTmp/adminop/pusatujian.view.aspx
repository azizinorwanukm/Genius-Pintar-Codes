<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="pusatujian.view.aspx.vb" Inherits="permatapintar.pusatujian_view1" %>

<%@ Register Src="../commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="~/css/material_button.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td class="fbtd_left">Fungsi Pusat Ujian</td>
            <td style="text-align: right;">
                <asp:DropDownList ID="ddlMenudesc" runat="server" Width="200px" Height="20px">
                </asp:DropDownList>&nbsp;<asp:Button ID="btnExecute" runat="server" Text="   Go   " CssClass="fbbuttonnext" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
</asp:Content>
