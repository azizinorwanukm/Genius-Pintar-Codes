<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.studentprofile.search.aspx.vb" Inherits="permatapintar.admin_studentprofile_search" %>

<%@ Register Src="commoncontrol/studentprofile_search.ascx" TagName="studentprofile_search"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Maklumat Pelajar>Carian Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_search ID="studentprofile_search1" runat="server" />
</asp:Content>
