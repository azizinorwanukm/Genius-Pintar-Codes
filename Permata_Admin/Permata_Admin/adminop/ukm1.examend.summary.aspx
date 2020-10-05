<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm1.examend.summary.aspx.vb" Inherits="permatapintar.ukm1_examend_summary1" %>

<%@ Register Src="../commoncontrol/ukm1_examstart_datelist.ascx" TagName="ukm1_examstart_datelist"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Tarikh"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_examstart_datelist ID="ukm1_examstart_datelist1" runat="server" />
</asp:Content>
