<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.kolejpermata.create.aspx.vb" Inherits="permatapintar.admin_koko_kolejpermata_create" %>

<%@ Register Src="../commoncontrol/koko_kolejpermata_create.ascx" TagName="koko_kolejpermata_create" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kokurikulum
            </td>
        </tr>
    </table>
    <uc1:koko_kolejpermata_create ID="koko_kolejpermata_create1" runat="server" />
</asp:Content>
