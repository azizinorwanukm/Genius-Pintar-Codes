<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="result.page.aspx.vb" Inherits="permatapintar.result_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                System Message
            </td>
        </tr>
    </table>
    <br />
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
</asp:Content>
