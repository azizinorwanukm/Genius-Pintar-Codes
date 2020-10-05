<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="parentprofile.confirm.aspx.vb" Inherits="permatapintar.parentprofile_confirm" %>

<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:parentprofile_view ID="parentprofile_view1" runat="server" />
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Mengesahkan Maklumat Ibubapa/Penjaga
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnBack" runat="server" Text="Back " CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm " CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="warning">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
</asp:Content>