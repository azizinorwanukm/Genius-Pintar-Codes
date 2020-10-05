<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.schoolprofile.schoollokasi.list.aspx.vb" Inherits="permatapintar.admin_schoolprofile_schoollokasi_list" %>

<%@ Register Src="commoncontrol/schoolprofile_schoollokasi_list.ascx" TagName="schoolprofile_schoollokasi_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Lokasi>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:schoolprofile_schoollokasi_list ID="schoolprofile_schoollokasi_list1" runat="server" />
    &nbsp;
</asp:Content>
