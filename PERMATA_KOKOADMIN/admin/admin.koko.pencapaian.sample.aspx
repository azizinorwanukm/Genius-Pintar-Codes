<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/print.Master" CodeBehind="admin.koko.pencapaian.sample.aspx.vb" Inherits="permatapintar.admin_koko_pencapaian_sample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Kemaskini Penyertaan dan Pencapaian Tahunan&nbsp;<asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Penyertaan dan Pencapaian:
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:TextBox ID="txtPencapaian" runat="server" Width="600px" TextMode="MultiLine" Rows="15"></asp:TextBox>&nbsp;*&nbsp;
            </td>
        </tr>
        <tr>
            <td class="fbform_sap_top">
                <asp:Label ID="lblMsg" runat="server" Text="Contoh bagaimana hendak mengisi Penyertaan dan Pencapaian Tahunan" CssClass="fblabel_msg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
