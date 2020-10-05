<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ketuapengurusakademik/main.Master"
    CodeBehind="kpa.laporan.keseluruhan.view.aspx.vb" Inherits="permatapintar.kpa_laporan_keseluruhan_view" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="student"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/laporan_keseluruhan_view.ascx" TagName="laporan_keseluruhan_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan Penaksiran Akademik
            </td>
        </tr>
    </table>
    <uc1:student ID="student1" runat="server" />
    <br />
    <table>
        <tr>
            <td colspan="10">
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:laporan_keseluruhan_view ID="laporan_keseluruhan_view1" runat="server" />
</asp:Content>
