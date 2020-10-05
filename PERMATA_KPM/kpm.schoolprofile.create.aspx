<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.schoolprofile.create.aspx.vb" Inherits="permatapintar.kpm_schoolprofile_create" %>

<%@ Register Src="commoncontrol/schoolprofile_create.ascx" TagName="schoolprofile_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Senarai Sekolah Negeri>Sekolah Baru" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_create ID="schoolprofile_create1" runat="server" />
</asp:Content>
