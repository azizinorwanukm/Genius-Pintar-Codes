<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.schoollokasi.list.aspx.vb" Inherits="permatapintar.ukm2_schoollokasi_list2" %>

<%@ Register Src="../commoncontrol/ukm2_schoollokasi_list.ascx" TagName="ukm2_schoollokasi_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Lokasi"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_schoollokasi_list ID="ukm2_schoollokasi_list1" runat="server" />
</asp:Content>