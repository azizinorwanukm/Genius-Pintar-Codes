<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kemudahan.create.aspx.vb" Inherits="permatapintar.admin_kemudahan_create" %>


<%@ Register Src="../commoncontrol/kemudahan_create.ascx" TagName="kemudahan_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelab & Persatuan>Tambah
            </td>
        </tr>
    </table>
    <uc1:kemudahan_create ID="kemudahan_create1" runat="server" />
</asp:Content>
