<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ppcs.student.search.aspx.vb" Inherits="permatapintar.admin_ppcs_student_search" %>

<%@ Register Src="commoncontrol/ppcs_student_search.ascx" TagName="ppcs_student_search"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="PPCS>Carian Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_student_search ID="ppcs_student_search1" runat="server" />
</asp:Content>
