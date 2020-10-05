<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.select.sukan.aspx.vb" Inherits="permatapintar.admin_koko_select_sukan" %>

<%@ Register src="../commoncontrol/koko_select_sukan.ascx" tagname="koko_select_sukan" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kumpulan Sukan & Permainan
            </td>
        </tr>
    </table>
    <uc1:koko_select_sukan ID="koko_select_sukan1" runat="server" />
</asp:Content>
