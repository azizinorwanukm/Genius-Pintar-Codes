<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpm.ukm1.dobyear.list.aspx.vb" Inherits="permatapintar.kpm_ukm1_dobyear_list" %>

<%@ Register Src="commoncontrol/ukm1_dobyear_list.ascx" TagName="ukm1_dobyear_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Umur>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_dobyear_list ID="ukm1_dobyear_list1" runat="server" />
</asp:Content>
