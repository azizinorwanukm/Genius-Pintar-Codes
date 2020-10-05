<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.pindah.aspx.vb" Inherits="permatapintar.admin_studentprofile_pindah" %>

<%@ Register Src="commoncontrol/studentprofile_pindah.ascx" TagName="studentprofile_pindah" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Maklumat Pelajar>Pindah Sekolah>Pilih Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_pindah ID="studentprofile_pindah1" runat="server" />
</asp:Content>
