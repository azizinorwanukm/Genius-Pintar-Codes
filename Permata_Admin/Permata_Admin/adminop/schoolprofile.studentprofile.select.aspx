<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="schoolprofile.studentprofile.select.aspx.vb" Inherits="permatapintar.schoolprofile_studentprofile_select1" %>

<%@ Register Src="../commoncontrol/schoolprofile_studentprofile_select.ascx" TagName="schoolprofile_studentprofile_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Profil Sekolah>Carian Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_studentprofile_select ID="schoolprofile_studentprofile_select1"
        runat="server" />
</asp:Content>