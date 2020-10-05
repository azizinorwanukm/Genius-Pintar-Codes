<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.schoolprofile.list.aspx.vb" Inherits="permatapintar.admin_schoolprofile_list" %>

<%@ Register Src="commoncontrol/schoolprofile_list.ascx" TagName="schoolprofile_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Maklumat Pelajar>Daftar Pelajar Baru>Pilih Sekolah"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_list ID="schoolprofile_list1" runat="server" />
</asp:Content>
