<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pengarah.list.aspx.vb" Inherits="permatapintar.admin_pengarah_list" %>

<%@ Register src="../commoncontrol/pengarah_list.ascx" tagname="pengarah_list" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Carian>Carian Pengarah
            </td>
        </tr>
    </table>
    <uc1:pengarah_list ID="pengarah_list1" runat="server" />
</asp:Content>
