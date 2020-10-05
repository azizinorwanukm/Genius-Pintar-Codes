<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.view.aspx.vb" Inherits="permatapintar.admin_kelaskoko_view" %>

<%@ Register Src="../commoncontrol/kelaskoko_view.ascx" TagName="kelaskoko_view" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kumpulan Sukan & Permainan>Paparan
            </td>
        </tr>
    </table>
    <uc1:kelaskoko_view ID="kelaskoko_view1" runat="server" />
</asp:Content>
