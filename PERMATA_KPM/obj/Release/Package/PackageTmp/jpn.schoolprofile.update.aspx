<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.schoolprofile.update.aspx.vb" Inherits="permatapintar.jpn_schoolprofile_update" %>

<%@ Register Src="commoncontrol/schoolprofile_update.ascx" TagName="schoolprofile_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Senarai Sekolah Negeri>Kemaskini Maklumat Sekolah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_update ID="schoolprofile_update1" runat="server" />
</asp:Content>
