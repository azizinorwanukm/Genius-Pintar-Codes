<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ukm1.schoolprofile.list.pindah.aspx.vb" Inherits="permatapintar.admin_ukm1_schoolprofile_list_pindah" %>

<%@ Register Src="commoncontrol/ukm1_schoolprofile_list_pindah.ascx" TagName="ukm1_schoolprofile_list_pindah"
    TagPrefix="uc1" %>
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
    <uc1:ukm1_schoolprofile_list_pindah ID="ukm1_schoolprofile_list_pindah1" runat="server" />
    &nbsp;
</asp:Content>
