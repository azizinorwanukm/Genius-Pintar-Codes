<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="kpm.studentschool.schoolprofile.select.aspx.vb" Inherits="permatapintar.kpm_studentschool_schoolprofile_select" %>

<%@ Register Src="commoncontrol/schoolprofile_view_old.ascx" TagName="schoolprofile_view_old"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/studentschool_schoolprofile_select.ascx" TagName="studentschool_schoolprofile_select"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_studentprofile_select_new.ascx" TagName="schoolprofile_studentprofile_select_new" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Senarai Sekolah Negeri>Pindah Sekolah>Pilih Target" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:schoolprofile_view_old ID="schoolprofile_view_old1" runat="server" />
    &nbsp;<asp:Label ID="lblMsg" runat="server" Text="Carian Sekolah Baru" ForeColor="Red"></asp:Label>
    <uc3:schoolprofile_studentprofile_select_new ID="schoolprofile_studentprofile_select_new1" runat="server" />
    &nbsp;
</asp:Content>
