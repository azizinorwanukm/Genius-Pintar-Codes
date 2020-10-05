<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.ppmt.studentprofile.list.aspx.vb" Inherits="permatapintar.admin_ppmt_studentprofile_list" %>

<%@ Register Src="../commoncontrol/ukm3_ppmt_list.ascx" TagName="ukm3_ppmt_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Program Pendidikan PERMATApintar>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc1:ukm3_ppmt_list ID="ukm3_ppmt_list1" runat="server" />
</asp:Content>
