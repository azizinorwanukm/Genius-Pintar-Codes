<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.rumahsukan.create.aspx.vb" Inherits="permatapintar.admin_rumahsukan_create" %>

<%@ Register src="../commoncontrol/rumahsukan_create.ascx" tagname="rumahsukan_create" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Rumah Sukan>Tambah
            </td>
        </tr>
    </table>
    <uc1:rumahsukan_create ID="rumahsukan_create1" runat="server" />&nbsp;
</asp:Content>
