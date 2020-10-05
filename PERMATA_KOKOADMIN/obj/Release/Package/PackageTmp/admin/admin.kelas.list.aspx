<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.list.aspx.vb" Inherits="permatapintar.admin_kelas_list" %>

<%@ Register Src="../commoncontrol/kelas_list.ascx" TagName="kelas_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kelas
            </td>
        </tr>
    </table>
    <uc1:kelas_list ID="kelas_list1" runat="server" />
</asp:Content>
