<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ukm2.kelayakan.aspx.vb" Inherits="permatapintar.admin_ukm2_kelayakan" %>

<%@ Register Src="commoncontrol/ukm2_kelayakan_umur.ascx" TagName="ukm2_kelayakan_umur"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Kelayakan UKM2: UMUR" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_kelayakan_umur ID="ukm2_kelayakan_umur1" runat="server" />
</asp:Content>
