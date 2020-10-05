<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pensyarah.view.aspx.vb" Inherits="permatapintar.admin_pensyarah_view" %>

<%@ Register Src="../commoncontrol/pensyarah_view.ascx" TagName="pensyarah_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Pensyarah>Paparan
            </td>
        </tr>
    </table>
    <uc1:pensyarah_view ID="pensyarah_view1" runat="server" />
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
                &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapuskan " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
