<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="schoolprofile.select.pindah.aspx.vb" Inherits="permatapintar.schoolprofile_select_pindah1" %>

<%@ Register Src="../commoncontrol/schoolprofile_select_pindah.ascx" TagName="schoolprofile_select_pindah" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Profil Sekolah>Pindah Sekolah>Pilih Sekolah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_select_pindah ID="schoolprofile_select_pindah1" runat="server" />
</asp:Content>
