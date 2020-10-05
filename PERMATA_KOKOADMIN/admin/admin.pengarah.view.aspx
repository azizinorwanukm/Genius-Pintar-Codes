<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pengarah.view.aspx.vb" Inherits="permatapintar.admin_pengarah_view" %>

<%@ Register Src="../commoncontrol/pengarah_view.ascx" TagName="pengarah_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pengarah>Paparan
            </td>
        </tr>
    </table>
    <uc1:pengarah_view ID="pengarah_view1" runat="server" />
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
                &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapuskan " CssClass="fbbutton" />
                &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Pengarah</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
