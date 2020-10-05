<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.schooltype.summary.aspx.vb" Inherits="permatapintar.ppcs_schooltype_summary2" %>

<%@ Register Src="../commoncontrol/ppcs_schooltype_summary.ascx" TagName="ppcs_schooltype_summary"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan PPCS>Ringkasan Jenis Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_schooltype_summary ID="ppcs_schooltype_summary1" runat="server" />
</asp:Content>
