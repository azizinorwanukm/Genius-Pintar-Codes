<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.schoolstate.student.list.aspx.vb" Inherits="permatapintar.subadmin_ukm1_schoolstate_student_list" %>

<%@ Register Src="commoncontrol/ukm1_schoolstate_student_list.ascx" TagName="ukm1_schoolstate_student_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Senarai Negeri>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schoolstate_student_list ID="ukm1_schoolstate_student_list1" runat="server" />
    &nbsp;
</asp:Content>
