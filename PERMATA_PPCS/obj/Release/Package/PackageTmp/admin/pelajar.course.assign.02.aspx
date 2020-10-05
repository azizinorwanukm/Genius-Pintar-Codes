<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pelajar.course.assign.02.aspx.vb" Inherits="permatapintar.pelajar_course_assign_02" %>

<%@ Register Src="../commoncontrol/ppcs_course_view.ascx" TagName="ppcs_course_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_course_assign.ascx" TagName="ppcs_course_assign"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Pelajar>Menentukan Kursus
            </td>
        </tr>
    </table>
    <uc1:ppcs_course_view ID="ppcs_course_view1" runat="server" />
    &nbsp;
    <uc2:ppcs_course_assign ID="ppcs_course_assign1" runat="server" />
</asp:Content>
