<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="pusatujian.student.select.aspx.vb" Inherits="permatapintar.pusatujian_student_select1" %>

<%@ Register Src="../commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/pusatujian_student_select.ascx" TagName="pusatujian_student_select" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Senarai Pusat Ujian>Penetapan Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <uc2:pusatujian_student_select ID="pusatujian_student_select1" runat="server" />
</asp:Content>