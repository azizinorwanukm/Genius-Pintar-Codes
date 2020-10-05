<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.schoolprofile.list.aspx.vb" Inherits="permatapintar.ukm2_schoolprofile_list2" %>

<%@ Register Src="../commoncontrol/ukm2_schoolprofile_list.ascx" TagName="ukm2_schoolprofile_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_schoolprofile_list ID="ukm2_schoolprofile_list1" runat="server" />
    &nbsp;
</asp:Content>
