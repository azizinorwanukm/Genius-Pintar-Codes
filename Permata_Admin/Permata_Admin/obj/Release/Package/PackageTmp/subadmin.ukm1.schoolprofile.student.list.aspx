<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.schoolprofile.student.list.aspx.vb" Inherits="permatapintar.subadmin_ukm1_schoolprofile_student_list" %>

<%@ Register Src="commoncontrol/ukm1_school_studentprofile_list.ascx" TagName="ukm1_school_studentprofile_list"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <uc1:ukm1_school_studentprofile_list ID="ukm1_school_studentprofile_list1" runat="server" />
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnStudentSchoolUpdate" runat="server" Text="Pindah Sekolah" CssClass="fbbutton" />&nbsp;
                [Memindahkan semua pelajar di atas ke sekolah lain]
            </td>
        </tr>
    </table>
</asp:Content>
