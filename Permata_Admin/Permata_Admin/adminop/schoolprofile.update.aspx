<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="schoolprofile.update.aspx.vb" Inherits="permatapintar.schoolprofile_update1" %>

<%@ Register Src="../commoncontrol/schoolprofile_update.ascx" TagName="schoolprofile_update"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Profil Sekolah>Carian Sekolah>Kemaskini"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_update ID="schoolprofile_update1" runat="server" />
</asp:Content>
