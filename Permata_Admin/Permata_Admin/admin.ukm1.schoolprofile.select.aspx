<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm1.schoolprofile.select.aspx.vb" Inherits="permatapintar.admin_ukm1_schoolprofile_select" %>


<%@ Register Src="commoncontrol/ukm1_schoolprofile_select.ascx" TagName="ukm1_schoolprofile_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Pindah Sekolah>Pilih Sekolah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schoolprofile_select ID="ukm1_schoolprofile_select1" runat="server" />
</asp:Content>
