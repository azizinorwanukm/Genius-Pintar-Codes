<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.schoolprofile.schoolcity.update.aspx.vb" Inherits="permatapintar.admin_schoolprofile_schoolcity_update" %>

<%@ Register Src="commoncontrol/schoolprofile_schoolcity_update.ascx" TagName="schoolprofile_schoolcity_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Profil Sekolah>Kemaskini Nama Bandar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>

    <uc1:schoolprofile_schoolcity_update ID="schoolprofile_schoolcity_update1"
        runat="server" />
</asp:Content>
