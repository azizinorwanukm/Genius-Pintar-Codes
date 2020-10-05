<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.schoolprofile.studentprofile.list.aspx.vb" Inherits="permatapintar.admin_schoolprofile_studentprofile_list" %>

<%@ Register Src="commoncontrol/schoolprofile_studentprofile_list.ascx" TagName="schoolprofile_studentprofile_list" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Profil Sekolah>Carian Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;<uc1:schoolprofile_studentprofile_list ID="schoolprofile_studentprofile_list1" runat="server" />
    &nbsp;
    <asp:Button ID="btnStudentSchoolUpdate" runat="server" Text="Pindah Sekolah" CssClass="fbbutton" Visible="false" />
    <asp:Label ID="lblMsg" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
