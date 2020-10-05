<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ukm2.kelayakan.sekolah.aspx.vb" Inherits="permatapintar.admin_ukm2_kelayakan_sekolah" %>

<%@ Register Src="commoncontrol/ukm2_kelayakan_sekolah.ascx" TagName="ukm2_kelayakan_sekolah"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Kelayakan UKM2: SEKOLAH" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_kelayakan_sekolah ID="ukm2_kelayakan_sekolah1" runat="server" />
</asp:Content>
