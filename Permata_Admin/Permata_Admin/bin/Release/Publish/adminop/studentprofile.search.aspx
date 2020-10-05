<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="studentprofile.search.aspx.vb" Inherits="permatapintar.studentprofile_search1" %>


<%@ Register src="../commoncontrol/studentprofile_search.ascx" tagname="studentprofile_search" tagprefix="uc1" %>
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