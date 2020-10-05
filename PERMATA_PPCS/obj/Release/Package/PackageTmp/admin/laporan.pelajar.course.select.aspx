<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="laporan.pelajar.course.select.aspx.vb" Inherits="permatapintar.laporan_pelajar_course_select" %>

<%@ Register Src="../commoncontrol/ppcs_course_select.ascx" TagName="ppcs_course_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Pelajar>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc1:ppcs_course_select ID="ppcs_course_select1" runat="server" />
    &nbsp;
</asp:Content>
