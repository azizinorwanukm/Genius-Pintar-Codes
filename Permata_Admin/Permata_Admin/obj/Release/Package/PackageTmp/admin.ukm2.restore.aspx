<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm2.restore.aspx.vb" Inherits="permatapintar.admin_ukm2_restore" %>

<%@ Register Src="commoncontrol/ukm2_insert.ascx" TagName="ukm2_insert" TagPrefix="uc2" %>
<%@ Register src="commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ujian UKM2>Restore"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc2:ukm2_insert ID="ukm2_insert1" runat="server" />
</asp:Content>
