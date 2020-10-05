<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.schoolprofile.search.aspx.vb" Inherits="permatapintar.ukm_schoolprofile_search" %>

<%@ Register Src="commoncontrol/SchoolProfile_search.ascx" TagName="SchoolProfile_search" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Carian>Carian Sekolah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:SchoolProfile_search ID="SchoolProfile_search1" runat="server" />

</asp:Content>
