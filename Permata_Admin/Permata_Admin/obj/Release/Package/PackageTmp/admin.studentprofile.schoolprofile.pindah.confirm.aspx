<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.schoolprofile.pindah.confirm.aspx.vb" Inherits="permatapintar.admin_studentprofile_schoolprofile_pindah_confirm" %>

<%@ Register src="commoncontrol/schoolprofile_view.ascx" tagname="schoolprofile_view" tagprefix="uc1" %>
<%@ Register Src="~/commoncontrol/studentpindah_list.ascx" TagPrefix="uc1" TagName="studentpindah_list" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="Label1" runat="server" text="Maklumat Pelajar>Pindah Sekolah>Kepastian"
                    cssclass="lblBreadcrum"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    <uc1:studentpindah_list runat="server" id="studentpindah_list" />
</asp:Content>
