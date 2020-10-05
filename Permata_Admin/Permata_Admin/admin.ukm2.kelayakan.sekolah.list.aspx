<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ukm2.kelayakan.sekolah.list.aspx.vb" Inherits="permatapintar.admin_ukm2_kelayakan_sekolah_list" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/ukm2_kelayakan_sekolah_list.ascx" TagName="ukm2_kelayakan_sekolah_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Kelayakan UKM2: SEKOLAH>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;<uc2:ukm2_kelayakan_sekolah_list ID="ukm2_kelayakan_sekolah_list1" runat="server" />
    &nbsp;
</asp:Content>
