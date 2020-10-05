<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.select.aspx.vb" Inherits="permatapintar.admin_koko_select" %>

<%@ Register Src="../commoncontrol/studentprofile_view_header.ascx" TagName="studentprofile_view_header" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/koko_select.ascx" TagName="koko_select" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pelajar>Penetapan Kokurikulum
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view_header ID="studentprofile_view_header1" runat="server" />
    &nbsp;
    <uc2:koko_select ID="koko_select1" runat="server" />
</asp:Content>
