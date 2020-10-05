<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm2.status.aspx.vb" Inherits="permatapintar.subadmin_ukm2_status" %>

<%@ Register src="commoncontrol/ukm2_status_list.ascx" tagname="ukm2_status_list" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM2>Status Ujian UKM2" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_status_list ID="ukm2_status_list1" runat="server" />
</asp:Content>
