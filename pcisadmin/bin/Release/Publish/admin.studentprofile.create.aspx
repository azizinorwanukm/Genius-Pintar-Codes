<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.create.aspx.vb" Inherits="araken.pcisadmin.admin_studentprofile_create" %>

<%@ Register Src="commoncontrol/userprofile_create.ascx" TagName="userprofile_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Profil Pelajar> Daftar Pelajar Baru" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:userprofile_create ID="userprofile_create1" runat="server" />
</asp:Content>
