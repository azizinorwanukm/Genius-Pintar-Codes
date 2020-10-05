<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.user.update.aspx.vb" Inherits="permatapintar.admin_user_update" %>

<%@ Register Src="../commoncontrol/koko_user_update.ascx" TagName="koko_user_update" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Lain-Lain>Kemaskini Profil
            </td>
        </tr>
    </table>
    <uc1:koko_user_update ID="koko_user_update1" runat="server" />
    &nbsp;
</asp:Content>
