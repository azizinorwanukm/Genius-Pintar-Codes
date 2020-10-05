<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm1.schoolprofile.student.list.confirm.aspx.vb" Inherits="permatapintar.admin_ukm1_schoolprofile_student_list_confirm" %>

<%@ Register src="commoncontrol/schoolprofile_view.ascx" tagname="schoolprofile_view" tagprefix="uc1" %>
<%@ Register src="commoncontrol/schoolpindah_list.ascx" tagname="schoolpindah_list" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="Label1" runat="server" text="Profil Sekolah>Pindah Sekolah>Kepastian"
                    cssclass="lblBreadcrum"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    <uc2:schoolpindah_list ID="schoolpindah_list1" runat="server" />
</asp:Content>
