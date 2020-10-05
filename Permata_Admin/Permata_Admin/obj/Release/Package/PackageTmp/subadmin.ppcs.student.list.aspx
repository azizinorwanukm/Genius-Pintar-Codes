<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.ppcs.student.list.aspx.vb" Inherits="permatapintar.subadmin_ppcs_student_list" %>

<%@ Register Src="commoncontrol/ppcs_student_list.ascx" TagName="ppcs_student_list"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/ppcs_student_list_sub.ascx" TagName="ppcs_student_list_sub"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="PPCS>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:ppcs_student_list_sub ID="ppcs_student_list_sub1" runat="server" />
    &nbsp;
</asp:Content>
