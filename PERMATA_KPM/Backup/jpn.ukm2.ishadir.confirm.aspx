<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master"
    CodeBehind="jpn.ukm2.ishadir.confirm.aspx.vb" Inherits="permatapintar.jpn_ukm2_ishadir_confirm" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td colspan="2">
                <asp:Button ID="btnHadir" runat="server" Text="Hadir" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnTidakHadir" runat="server" Text="Tidak Hadir" CssClass="fbbutton" />&nbsp;
                Tahun Ujian:<asp:Label ID="lblExamYear" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
</asp:Content>
