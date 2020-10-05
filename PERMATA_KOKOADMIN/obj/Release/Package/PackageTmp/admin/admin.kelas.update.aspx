<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.update.aspx.vb" Inherits="permatapintar.admin_kelas_update" %>

<%@ Register Src="../commoncontrol/kelas_update.ascx" TagName="kelas_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelas>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:kelas_update ID="kelas_update1" runat="server" />
</asp:Content>
