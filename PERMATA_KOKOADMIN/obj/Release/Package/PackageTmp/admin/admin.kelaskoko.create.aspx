<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.create.aspx.vb" Inherits="permatapintar.admin_kelaskoko_create" %>

<%@ Register Src="../commoncontrol/kelaskoko_create.ascx" TagName="kelaskoko_create" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kumpulan Sukan & Permainan>Tambah
            </td>
        </tr>
    </table>
    <uc1:kelaskoko_create ID="kelaskoko_create1" runat="server" />
</asp:Content>
