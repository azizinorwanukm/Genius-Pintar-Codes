<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.instruktor.view.aspx.vb" Inherits="permatapintar.admin_instruktor_view" %>

<%@ Register Src="../commoncontrol/instruktor_view.ascx" TagName="instruktor_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Instruktor>Paparan
            </td>
        </tr>
    </table>
    <uc1:instruktor_view ID="instruktor_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
                &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapuskan " CssClass="fbbutton" />
                &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Instruktor</asp:LinkButton>
            </td>
        </tr>

    </table>
</asp:Content>
