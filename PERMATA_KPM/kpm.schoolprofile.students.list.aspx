<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.schoolprofile.students.list.aspx.vb" Inherits="permatapintar.kpm_schoolprofile_students_list" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view" TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/schoolprofile_student_list.ascx" TagName="schoolprofile_student_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Senarai Pelajar Negeri>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;<uc1:schoolprofile_student_list ID="schoolprofile_student_list1" runat="server" />
</asp:Content>
