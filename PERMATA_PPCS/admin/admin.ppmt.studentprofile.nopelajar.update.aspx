<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.ppmt.studentprofile.nopelajar.update.aspx.vb" Inherits="permatapintar.admin_ppmt_studentprofile_nopelajar_update" %>

<%@ Register Src="../commoncontrol/studentprofile_ukm3_nopelajar_update.ascx" TagName="studentprofile_ukm3_nopelajar_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Program Pendidikan PERMATApintar>Kemaskini No Pelajar
            </td>
        </tr>
    </table>
    <uc1:studentprofile_ukm3_nopelajar_update ID="studentprofile_ukm3_nopelajar_update1"
        runat="server" />
</asp:Content>
