<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="kpm.studentschool.schoolprofile.confirm.aspx.vb" Inherits="permatapintar.kpm_studentschool_schoolprofile_confirm" %>

<%@ Register Src="commoncontrol/schoolprofile_view_old.ascx" TagName="schoolprofile_view_old"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/school_studentprofile_list_old.ascx" TagName="school_studentprofile_list_old" TagPrefix="uc5" %>
<%@ Register src="commoncontrol/school_studentprofile_list.ascx" tagname="school_studentprofile_list" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Senarai Sekolah Negeri>Pindah Sekolah>Kepastian" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>

    <uc1:schoolprofile_view_old ID="schoolprofile_view_old1" runat="server" />
    &nbsp;<uc5:school_studentprofile_list_old ID="school_studentprofile_list_old1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnStudentSchoolUpdate" runat="server" Text="Pindah Sekolah" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />
                [Memindahkan semua pelajar di atas ke sekolah di bawah. Sekolah "XXX" akan ditanda IsDeleted=Y.]
            </td>
        </tr>
    </table>
    &nbsp;<asp:Label ID="lblMsg" runat="server" Text="Sekolah Baru" ForeColor="Red"></asp:Label>
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;<uc6:school_studentprofile_list ID="school_studentprofile_list1" runat="server" />
</asp:Content>
