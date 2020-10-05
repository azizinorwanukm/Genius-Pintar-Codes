<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.progress.aspx.vb" Inherits="permatapintar.subadmin_ukm1_progress" %>

<%@ Register Src="commoncontrol/ukm1_progress.ascx" TagName="ukm1_progress" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM1>Status Ujian Terkini"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_progress ID="ukm1_progress1" runat="server" />

</asp:Content>
