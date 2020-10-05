<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.pensyarah.assign.aspx.vb" Inherits="permatapintar.admin_kelas_pensyarah_assign" %>

<%@ Register Src="../commoncontrol/kelas_view.ascx" TagName="kelas_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/pensyarah_select.ascx" TagName="pensyarah_select" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kelas>Penetapan PENSYARAH>Pilih PENSYARAH
            </td>
        </tr>
    </table>
    <uc1:kelas_view ID="kelas_view1" runat="server" />
    &nbsp;
    <uc2:pensyarah_select ID="pensyarah_select1" runat="server" />
</asp:Content>
