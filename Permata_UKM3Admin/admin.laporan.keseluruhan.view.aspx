<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.laporan.keseluruhan.view.aspx.vb" Inherits="permatapintar.admin_laporan_keseluruhan_view" %>

<%@ Register Src="~/commoncontrol/studentprofile_header.ascx" TagPrefix="uc1" TagName="studentprofile_header" %>
<%@ Register Src="~/commoncontrol/laporan_keseluruhan_view.ascx" TagPrefix="uc1" TagName="laporan_keseluruhan_view" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan Pentaksiran Akademik
            </td>
            <td style="text-align: right;">
                <asp:ImageButton ID="imgPrint" ImageUrl="../icons/print-icon.png" AlternateText="Nota"
                    runat="server" Visible="true" />
            </td>
        </tr>
    </table>
    <uc1:studentprofile_header runat="server" ID="studentprofile_header" />
    <uc1:laporan_keseluruhan_view runat="server" id="laporan_keseluruhan_view" />
</asp:Content>
