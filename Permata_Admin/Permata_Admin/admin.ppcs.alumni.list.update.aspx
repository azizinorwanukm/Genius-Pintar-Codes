<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ppcs.alumni.list.update.aspx.vb" Inherits="permatapintar.admin_ppcs_alumni_list_update" %>
<%@ Register src="commoncontrol/ppcs_alumni_select.ascx" tagname="ppcs_alumni_select" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="ALUMNI PPCS>Kelayakan PPCS" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_alumni_select ID="ppcs_alumni_select1" runat="server" />
</asp:Content>
