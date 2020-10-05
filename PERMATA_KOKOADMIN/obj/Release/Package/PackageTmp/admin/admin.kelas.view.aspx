<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.view.aspx.vb" Inherits="permatapintar.admin_kelas_view" %>

<%@ Register Src="../commoncontrol/kelas_view.ascx" TagName="kelas_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelas>Paparan
            </td>
        </tr>
    </table>
    <uc1:kelas_view ID="kelas_view1" runat="server" />

</asp:Content>
