<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="schoolprofile.list.pindah.aspx.vb" Inherits="permatapintar.schoolprofile_list_pindah1" %>

<%@ Register Src="../commoncontrol/schoolprofile_list_pindah.ascx" TagName="schoolprofile_list_pindah" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Profil Sekolah>Pindah Sekolah>Pilih Sekolah Baru"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_list_pindah ID="schoolprofile_list_pindah1" runat="server" />
</asp:Content>
