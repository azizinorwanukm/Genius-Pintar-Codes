<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.persatuan.create.aspx.vb" Inherits="permatapintar.admin_persatuan_create" %>

<%@ Register Src="../commoncontrol/persatuan_create.ascx" TagName="persatuan_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelab & Persatuan>Tambah
            </td>
        </tr>
    </table>
    <uc1:persatuan_create ID="persatuan_create1" runat="server" />
</asp:Content>
