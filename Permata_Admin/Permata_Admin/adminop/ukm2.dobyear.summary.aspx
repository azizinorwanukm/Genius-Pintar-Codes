<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.dobyear.summary.aspx.vb" Inherits="permatapintar.ukm2_dobyear_summary1" %>

<%@ Register Src="../commoncontrol/ukm2_dobyear_summary.ascx" TagName="ukm2_dobyear_summary"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Umur"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_dobyear_summary ID="ukm2_dobyear_summary1" runat="server" />
</asp:Content>
