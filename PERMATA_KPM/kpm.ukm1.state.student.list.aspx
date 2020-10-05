<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.ukm1.state.student.list.aspx.vb" Inherits="permatapintar.kpm_ukm1_state_student_list" %>

<%@ Register Src="commoncontrol/ukm1_state_student_list.ascx" TagName="ukm1_state_student_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Status Ujian Negeri" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_state_student_list ID="ukm1_state_student_list1" runat="server" />
</asp:Content>

