<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentuni.create.aspx.vb" Inherits="permatapintar.admin_studentuni_create" %>

<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentuni_create.ascx" TagName="studentuni_create" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Pengajian Tinggi Baru" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc2:studentuni_create ID="studentuni_create1" runat="server" />
    &nbsp;

</asp:Content>
