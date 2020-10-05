<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.class.search.aspx.vb" Inherits="permatapintar.admin_class_search" %>
<%@ Register src="../commoncontrol/class_search.ascx" tagname="class_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Am>Senarai Kelas
            </td>
        </tr>
    </table>
    <uc1:class_search ID="class_search1" runat="server" />
&nbsp;
</asp:Content>
