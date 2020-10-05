<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pelajar.pencapaian.update.aspx.vb" Inherits="permatapintar.admin_pelajar_pencapaian_update" %>

<%@ Register Src="../commoncontrol/pelajar_pencapaian_update.ascx" TagName="pelajar_pencapaian_update" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/studentprofile_view_header.ascx" TagName="studentprofile_view_header" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Sahkan Pencapaian
                
            </td>
        </tr>
    </table>
    <uc2:studentprofile_view_header ID="studentprofile_view_header1" runat="server" />
    &nbsp;
    <uc1:pelajar_pencapaian_update ID="pelajar_pencapaian_update1" runat="server" />
</asp:Content>
