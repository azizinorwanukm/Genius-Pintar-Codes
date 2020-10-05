<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="result.page.aspx.vb" Inherits="permatapintar.result_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">System Message
            </td>
        </tr>
    </table>
    &nbsp;
              <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

</asp:Content>
