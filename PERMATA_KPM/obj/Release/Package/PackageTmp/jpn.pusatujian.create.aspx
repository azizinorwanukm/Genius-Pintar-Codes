<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.pusatujian.create.aspx.vb" Inherits="permatapintar.jpn_pusatujian_create" %>
<%@ Register src="commoncontrol/pusatujian_create.ascx" tagname="pusatujian_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Pusat Ujian UKM2>Daftar Pusat Ujian Baru" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_create ID="pusatujian_create1" runat="server" />
</asp:Content>
