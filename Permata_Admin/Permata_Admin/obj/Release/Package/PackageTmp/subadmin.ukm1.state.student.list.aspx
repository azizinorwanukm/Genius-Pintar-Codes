<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.state.student.list.aspx.vb" Inherits="permatapintar.subadmin_ukm1_state_student_list" %>

<%@ Register src="commoncontrol/ukm1_state_student_list.ascx" tagname="ukm1_state_student_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Status Ujian Negeri"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_state_student_list ID="ukm1_state_student_list1" runat="server" />
</asp:Content>

