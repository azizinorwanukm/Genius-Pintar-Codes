<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pelajar.class.assign.01.aspx.vb" Inherits="permatapintar.pelajar_class_assign_01" %>

<%@ Register Src="../commoncontrol/ppcs_course_select.ascx" TagName="ppcs_course_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Pelajar>Menentukan Kelas
            </td>
        </tr>
    </table>
    <uc1:ppcs_course_select ID="ppcs_course_select1" runat="server" />
</asp:Content>
