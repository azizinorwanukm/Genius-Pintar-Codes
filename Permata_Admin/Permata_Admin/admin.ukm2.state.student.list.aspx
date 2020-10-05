<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm2.state.student.list.aspx.vb" Inherits="permatapintar.admin_ukm2_state_student_list" %>

<%@ Register Src="commoncontrol/ukm2_state_student_list.ascx" TagName="ukm2_state_student_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Negeri>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_state_student_list ID="ukm2_state_student_list1" runat="server" />
</asp:Content>
