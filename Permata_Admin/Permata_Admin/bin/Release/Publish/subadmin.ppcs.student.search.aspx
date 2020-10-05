<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.ppcs.student.search.aspx.vb" Inherits="permatapintar.subadmin_ppcs_student_search" %>


<%@ Register Src="commoncontrol/ppcs_student_search_subadmin.ascx" TagName="ppcs_student_search_subadmin" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="PPCS>Carian Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>

    <uc1:ppcs_student_search_subadmin ID="ppcs_student_search_subadmin1" runat="server" />
</asp:Content>
