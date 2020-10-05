<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.ppd.student.list.aspx.vb" Inherits="permatapintar.ppcs_ppd_student_list1" %>

<%@ Register Src="../commoncontrol/ppcs_ppd_student_list.ascx" TagName="ppcs_ppd_student_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan PPCS>Ringkasan PPD>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>

            </td>
        </tr>
    </table>
    <uc1:ppcs_ppd_student_list ID="ppcs_ppd_student_list1" runat="server" />
</asp:Content>
