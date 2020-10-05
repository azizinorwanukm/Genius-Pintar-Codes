<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pelajar.course.assign.01.aspx.vb" Inherits="permatapintar.pelajar_course_assign_01" %>

<%@ Register Src="../commoncontrol/ppcs_course_list.ascx" TagName="ppcs_course_list"
    TagPrefix="uc1" %>
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
    <uc1:ppcs_course_list ID="ppcs_course_list1" runat="server" />
    &nbsp;
</asp:Content>
