<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.sukan.create.aspx.vb" Inherits="permatapintar.admin_sukan_create" %>

<%@ Register src="../commoncontrol/sukan_create.ascx" tagname="sukan_create" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Sukan & Permainan>Tambah
            </td>
        </tr>
    </table>
    <uc1:sukan_create ID="sukan_create1" runat="server" />&nbsp;
</asp:Content>
