<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm1.ppd.student.list.aspx.vb" Inherits="permatapintar.ukm1_ppd_student_list1" %>

<%@ Register Src="../commoncontrol/ukm1_ppd_student_list.ascx" TagName="ukm1_ppd_student_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian PPD>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_ppd_student_list ID="ukm1_ppd_student_list1" runat="server" />
    &nbsp;
</asp:Content>