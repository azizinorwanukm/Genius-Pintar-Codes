<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pensyarah.create.aspx.vb" Inherits="permatapintar.admin_pensyarah_create" %>

<%@ Register Src="../commoncontrol/pensyarah_create.ascx" TagName="pensyarah_create" TagPrefix="uc1" %>
<%@ Register src="../commoncontrol/pensyarah_list.ascx" tagname="pensyarah_list" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pensyarah>Tambah
            </td>
        </tr>
    </table>
    <uc1:pensyarah_create ID="pensyarah_create1" runat="server" />
    <uc2:pensyarah_list ID="pensyarah_list1" runat="server" />
</asp:Content>
