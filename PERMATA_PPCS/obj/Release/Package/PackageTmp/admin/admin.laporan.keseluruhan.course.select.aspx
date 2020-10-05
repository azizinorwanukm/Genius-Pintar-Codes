<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.laporan.keseluruhan.course.select.aspx.vb" Inherits="permatapintar.admin_laporan_keseluruhan_course_select" %>

<%@ Register Src="../commoncontrol/ppcs_course_select.ascx" TagName="ppcs_course_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan Penaksiran Akademik
            </td>
        </tr>
    </table>
    <uc1:ppcs_course_select ID="ppcs_course_select1" runat="server" />
</asp:Content>
