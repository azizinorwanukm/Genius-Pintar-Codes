<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm1.schoolprofile.list.mas.aspx.vb" Inherits="permatapintar.ukm1_schoolprofile_list_mas1" %>

<%@ Register Src="../commoncontrol/ukm1_schoolprofile_list_mas.ascx" TagName="ukm1_schoolprofile_list_mas"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schoolprofile_list_mas ID="ukm1_schoolprofile_list_mas1" runat="server" />
</asp:Content>
