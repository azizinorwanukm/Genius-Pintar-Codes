<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master"
    CodeBehind="jpn.studentschool.schoolprofile.confirm.aspx.vb" Inherits="permatapintar.jpn_studentschool_schoolprofile_confirm" %>
<%@ Register Src="commoncontrol/schoolprofile_view_old.ascx" TagName="schoolprofile_view_old"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/ukm1_school_studentprofile_list_old.ascx" TagName="ukm1_school_studentprofile_list_old"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/ukm1_school_studentprofile_list.ascx" TagName="ukm1_school_studentprofile_list"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_view_old ID="schoolprofile_view_old1" runat="server" />
    &nbsp; &nbsp;<uc3:ukm1_school_studentprofile_list_old ID="ukm1_school_studentprofile_list_old1"
        runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnStudentSchoolUpdate" runat="server" Text="Pindah Sekolah" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />&nbsp;
                [Memindahkan semua pelajar di atas ke sekolah di bawah]
            </td>
        </tr>
    </table>
    &nbsp;
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <uc4:ukm1_school_studentprofile_list ID="ukm1_school_studentprofile_list1" runat="server" />
</asp:Content>
