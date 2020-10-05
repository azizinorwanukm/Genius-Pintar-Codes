<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/print.Master" CodeBehind="admin.tempahan.print.aspx.vb" Inherits="permatapintar.admin_tempahan_print" %>

<%@ Register Src="../commoncontrol/tempahan_view.ascx" TagName="tempahan_view" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Lain-Lain>Tempahan>Paparan
            </td>
        </tr>
    </table>
    <uc1:tempahan_view ID="tempahan_view1" runat="server" />
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
