<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pengarah.create.aspx.vb" Inherits="permatapintar.admin_pengarah_create" %>

<%@ Register Src="../commoncontrol/pengarah_create.ascx" TagName="pengarah_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pengarah>Tambah
            </td>
        </tr>
    </table>
    <uc1:pengarah_create ID="pengarah_create1" runat="server" />
</asp:Content>