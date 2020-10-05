<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupengajar/main.Master" CodeBehind="laporan.harian.update.aspx.vb" Inherits="permatapintar.laporan_harian_update" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="ukm2" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/laporan.harian.update.ascx" TagName="laporan"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td>
                Laporan Harian
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