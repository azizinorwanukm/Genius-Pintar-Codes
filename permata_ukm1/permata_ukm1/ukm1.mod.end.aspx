<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="ukm1.mod.end.aspx.vb" Inherits="permatapintar.ukm1_mod_end" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblModEnd" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModEndMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton" />
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Sila baca arahan yang diberikan..."></asp:Label></div>
</asp:Content>
