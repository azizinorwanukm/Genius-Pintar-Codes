<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.kolejpermata.list.aspx.vb" Inherits="permatapintar.admin_koko_kolejpermata_list1" %>

<%@ Register Src="../commoncontrol/koko_kolejpermata_list.ascx" TagName="koko_kolejpermata_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>
                Selenggara>Kokurikulum
            </td>
        </tr>
    </table>
    <uc1:koko_kolejpermata_list ID="koko_kolejpermata_list1" runat="server" />
    &nbsp;
</asp:Content>
