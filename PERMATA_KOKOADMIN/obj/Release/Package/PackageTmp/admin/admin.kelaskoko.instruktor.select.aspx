<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.instruktor.select.aspx.vb" Inherits="permatapintar.admin_kelaskoko_instruktor_select" %>

<%@ Register Src="../commoncontrol/kelaskoko_view_header.ascx" TagName="kelaskoko_view_header" TagPrefix="uc1" %>
<%@ Register src="../commoncontrol/kelaskoko_instruktor_select.ascx" tagname="kelaskoko_instruktor_select" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kumpulan Sukan & Permainan>Penetapan Instruktor
            </td>
        </tr>
    </table>
    <uc1:kelaskoko_view_header ID="kelaskoko_view_header1" runat="server" />
    <uc2:kelaskoko_instruktor_select ID="kelaskoko_instruktor_select1" runat="server" />
</asp:Content>
