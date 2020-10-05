<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="pusatujian.student.list.aspx.vb" Inherits="permatapintar.pusatujian_student_list1" %>

<%@ Register Src="../commoncontrol/pusatujian_student_list.ascx" TagName="pusatujian_student_list"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Senarai Pusat Ujian>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <uc1:pusatujian_student_list ID="pusatujian_student_list1" runat="server" />
</asp:Content>
