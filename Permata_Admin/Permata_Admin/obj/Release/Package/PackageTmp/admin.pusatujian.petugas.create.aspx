<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.pusatujian.petugas.create.aspx.vb" Inherits="permatapintar.admin_pusatujian_petugas_create" %>

<%@ Register Src="commoncontrol/pusatujian_petugas_create.ascx" TagName="pusatujian_petugas_create"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Daftar Petugas" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_petugas_create ID="pusatujian_petugas_create1" runat="server" />
</asp:Content>
