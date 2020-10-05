<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.list.aspx.vb" Inherits="permatapintar.admin_kelaskoko_list" %>
<%@ Register src="../commoncontrol/kelaskoko_list_all.ascx" tagname="kelaskoko_list_all" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_bread">
            <td>Kumpulan Sukan & Permainan><asp:Label ID="lblTitle" runat="server" Text=''></asp:Label>
            </td>
        </tr>
    </table>
     <uc1:kelaskoko_list_all ID="kelaskoko_list_all1" runat="server" />
</asp:Content>
