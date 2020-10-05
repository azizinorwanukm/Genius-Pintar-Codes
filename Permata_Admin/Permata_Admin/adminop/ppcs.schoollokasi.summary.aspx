<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.schoollokasi.summary.aspx.vb" Inherits="permatapintar.ppcs_schoollokasi_summary2" %>

<%@ Register Src="../commoncontrol/ppcs_schoollokasi_summary.ascx" TagName="ppcs_schoollokasi_summary"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan PPCS>Ringkasan Lokasi"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_schoollokasi_summary ID="ppcs_schoollokasi_summary1" runat="server" />
</asp:Content>
