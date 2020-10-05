<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentuni.update.aspx.vb" Inherits="permatapintar.admin_studentuni_update" %>

<%@ Register Src="commoncontrol/studentuni_update.ascx" TagName="studentuni_update" TagPrefix="uc2" %>
<%@ Register src="commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Kemaskini Pengajian Tinggi" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />&nbsp;
    <uc2:studentuni_update ID="studentuni_update1" runat="server" />
</asp:Content>
