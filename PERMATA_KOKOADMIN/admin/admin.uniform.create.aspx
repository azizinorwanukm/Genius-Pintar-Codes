<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.uniform.create.aspx.vb" Inherits="permatapintar.admin_uniform_create" %>
<%@ Register src="../commoncontrol/uniform_create.ascx" tagname="uniform_create" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>
                Selenggara>Badan Beruniform>Tambah
            </td>
        </tr>
    </table>
    <uc2:uniform_create ID="uniform_create1" runat="server" />
</asp:Content>
