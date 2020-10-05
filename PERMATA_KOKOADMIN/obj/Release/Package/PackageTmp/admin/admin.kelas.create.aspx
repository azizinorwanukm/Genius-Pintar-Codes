<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.create.aspx.vb" Inherits="permatapintar.admin_kelas_create" %>

<%@ Register Src="../commoncontrol/kelas_create.ascx" TagName="kelas_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelas>Tambah
            </td>
        </tr>
    </table>
    <uc1:kelas_create ID="kelas_create1" runat="server" />
    &nbsp;
</asp:Content>
