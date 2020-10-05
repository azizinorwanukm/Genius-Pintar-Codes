<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.dobyear.summary.aspx.vb" Inherits="permatapintar.ppcs_dobyear_summary1" %>

<%@ Register Src="../commoncontrol/ppcs_dobyear_summary.ascx" TagName="ppcs_dobyear_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan PPCS>Ringkasan Umur"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_dobyear_summary ID="ppcs_dobyear_summary1" runat="server" />
</asp:Content>