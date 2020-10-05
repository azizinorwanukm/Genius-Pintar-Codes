<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.update.aspx.vb" Inherits="permatapintar.admin_kelaskoko_update" %>

<%@ Register src="../commoncontrol/kelaskoko_update.ascx" tagname="kelaskoko_update" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kumpulan Sukan & Permainan>Kemaskini
            </td>
        </tr>
    </table>
    <uc1:kelaskoko_update ID="kelaskoko_update1" runat="server" />
&nbsp;
</asp:Content>
