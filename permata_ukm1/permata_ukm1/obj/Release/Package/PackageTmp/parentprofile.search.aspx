<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="parentprofile.search.aspx.vb" Inherits="permatapintar.parentprofile_search" %>

<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Carian Ibubapa/Penjaga 
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                MYKAD Ibu:
            </td>
            <td>
                <asp:TextBox ID="txtMotherMYKADNo" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;*&nbsp;[Contoh:020820086011.
                Tanpa "-"]
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="info">
        • Carian menggunakan MYKAD Ibu.</div>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
</asp:Content>
