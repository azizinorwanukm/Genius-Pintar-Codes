<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm2.dobyear.list.aspx.vb" Inherits="permatapintar.admin_ukm2_dobyear_list" %>

<%@ Register Src="commoncontrol/ukm2_dobyear_list.ascx" TagName="ukm2_dobyear_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Umur>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_dobyear_list ID="ukm2_dobyear_list1" runat="server" />
</asp:Content>
