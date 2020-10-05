<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pensyarah.list.aspx.vb" Inherits="permatapintar.admin_pensyarah_list" %>

<%@ Register Src="../commoncontrol/pensyarah_list.ascx" TagName="pensyarah_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Carian>Carian Pensyarah
            </td>
        </tr>
    </table>
    <uc1:pensyarah_list ID="pensyarah_list1" runat="server" />
</asp:Content>
