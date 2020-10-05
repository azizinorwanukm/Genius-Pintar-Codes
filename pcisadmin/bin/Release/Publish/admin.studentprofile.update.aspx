<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.update.aspx.vb" Inherits="araken.pcisadmin.admin_studentprofile_update" %>

<%@ Register Src="commoncontrol/userprofile_update.ascx" TagName="userprofile_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Kemaskini Profil Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:userprofile_update ID="userprofile_update1" runat="server" />
    &nbsp;
</asp:Content>
