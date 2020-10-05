<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.schooltype.summary.aspx.vb" Inherits="permatapintar.subadmin_ukm1_schooltype_summary" %>

<%@ Register src="commoncontrol/ukm1_schooltype_summary.ascx" tagname="ukm1_schooltype_summary" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Jenis Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schooltype_summary ID="ukm1_schooltype_summary1" runat="server" />
</asp:Content>
