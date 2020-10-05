<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.kolejpermata.update.aspx.vb" Inherits="permatapintar.admin_koko_kolejpermata_update" %>

<%@ Register src="../commoncontrol/koko_kolejpermata_update.ascx" tagname="koko_kolejpermata_update" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kokurikulum
            </td>
        </tr>
    </table>
    <uc1:koko_kolejpermata_update ID="koko_kolejpermata_update1" runat="server" />
</asp:Content>
