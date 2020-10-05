<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.ppmt.studentprofile.year.list.aspx.vb" Inherits="permatapintar.admin_ppmt_studentprofile_year_list" %>
<%@ Register src="../commoncontrol/ukm3_ppmt_year_list.ascx" tagname="ukm3_ppmt_year_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Program Pendidikan PERMATApintar>Senarai Pelajar - Tahun
            </td>
        </tr>
    </table>
    <uc1:ukm3_ppmt_year_list ID="ukm3_ppmt_year_list1" runat="server" />
</asp:Content>
