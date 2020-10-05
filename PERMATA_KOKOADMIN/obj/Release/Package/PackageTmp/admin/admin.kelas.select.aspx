<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.select.aspx.vb" Inherits="permatapintar.admin_kelas_select" %>

<%@ Register Src="../commoncontrol/kelas_select.ascx" TagName="kelas_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kelas><asp:Label ID="lblType" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:kelas_select ID="kelas_select1" runat="server" />
</asp:Content>
