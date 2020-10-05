<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/asasi.Master" CodeBehind="asasi.ukm2.ishadir.confirm.aspx.vb" Inherits="permatapintar.asasi_ukm2_ishadir_confirm" %>
<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />&nbsp;
    <table class="fbform">
        <tr>
            <td colspan="2">
                <asp:Button ID="btnHadir" runat="server" Text="Hadir" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnTidakHadir" runat="server" Text="Tidak Hadir" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="system message..."></asp:Label></div>
</asp:Content>
