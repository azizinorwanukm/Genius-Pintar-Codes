<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3.assign_staff.aspx.vb" Inherits="permatapintar.ukm3_assign_staff" %>

<%@ Register Src="~/commoncontrol/ppcs_users_assign.ascx" TagPrefix="uc1" TagName="ppcs_users_assign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Petugas>Carian Petugas"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_users_assign runat="server" ID="ppcs_users_assign" />
</asp:Content>
