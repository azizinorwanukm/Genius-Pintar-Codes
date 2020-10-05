<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.admin_ukm1_schoolstate_summary" %>

<%@ Register src="commoncontrol/ukm1_schoolstate_summary.ascx" tagname="ukm1_schoolstate_summary" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Negeri"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:ukm1_schoolstate_summary ID="ukm1_schoolstate_summary1" runat="server" />
</asp:Content>
