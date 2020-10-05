<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kemudahan.list.aspx.vb" Inherits="permatapintar.admin_kemudahan_list" %>

<%@ Register Src="../commoncontrol/kemudahan_list.ascx" TagName="kemudahan_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Lain-Lain>Tempahan>Senarai Kemudahan
            </td>
        </tr>
    </table>
    <uc1:kemudahan_list ID="kemudahan_list1" runat="server" />
    &nbsp;
</asp:Content>
