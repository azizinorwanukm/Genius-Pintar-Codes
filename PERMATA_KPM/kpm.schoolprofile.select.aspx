<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master"
    CodeBehind="kpm.schoolprofile.select.aspx.vb" Inherits="permatapintar.kpm_schoolprofile_select" %>

<%@ Register Src="commoncontrol/schoolprofile_select.ascx" TagName="schoolprofile_select" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Senarai Pelajar Negeri" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_select ID="schoolprofile_select1" runat="server" />
    &nbsp;
</asp:Content>
