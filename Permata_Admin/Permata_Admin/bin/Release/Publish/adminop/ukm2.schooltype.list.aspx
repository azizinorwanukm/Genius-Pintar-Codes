<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.schooltype.list.aspx.vb" Inherits="permatapintar.ukm2_schooltype_list2" %>

<%@ Register Src="../commoncontrol/ukm2_schooltype_list.ascx" TagName="ukm2_schooltype_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Jenis Sekolah>Senarai Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_schooltype_list ID="ukm2_schooltype_list1" runat="server" />
</asp:Content>
