<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="schoolprofile.update.aspx.vb" Inherits="permatapintar.schoolprofile_update1" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;<uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr>
            <td>
                <asp:Button ID="btnChange" runat="server" Text="Tukar Sekolah " CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
