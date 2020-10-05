<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.instruktor.create.aspx.vb" Inherits="permatapintar.admin_instruktor_create" %>

<%@ Register Src="../commoncontrol/instruktor_create.ascx" TagName="instruktor_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Instruktor>Tambah
            </td>
        </tr>
    </table>
    <uc1:instruktor_create ID="instruktor_create1" runat="server" />
</asp:Content>
