<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ukm2.schoolprofile.select.aspx.vb" Inherits="permatapintar.admin_ukm2_schoolprofile_select" %>

<%@ Register Src="commoncontrol/ukm2_schoolprofile_select.ascx" TagName="ukm2_schoolprofile_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Maklumat Pelajar>Daftar Pelajar Baru>Pilih Sekolah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_schoolprofile_select ID="ukm2_schoolprofile_select1" runat="server" />
    &nbsp;
</asp:Content>
