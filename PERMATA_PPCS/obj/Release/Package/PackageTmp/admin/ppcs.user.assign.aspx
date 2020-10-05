<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="ppcs.user.assign.aspx.vb" Inherits="permatapintar.ppcs_user_assign" %>

<%@ Register Src="../commoncontrol/ppcs_users_assign.ascx" TagName="ppcs_users_assign"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Pengguna>Pilih Petugas
            </td>
        </tr>
    </table>
    <uc1:ppcs_users_assign ID="ppcs_users_assign1" runat="server" />
</asp:Content>
