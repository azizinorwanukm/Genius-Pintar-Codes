<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppmt.studentprofile.alumniid.update.aspx.vb" Inherits="permatapintar.ppmt_studentprofile_alumniid_update" %>


<%@ Register Src="../commoncontrol/studentprofile_ukm3_alumniid_update.ascx" TagName="studentprofile_ukm3_alumniid_update" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Program Pendidikan PERMATApintar>Kemaskini AlumniID
            </td>
        </tr>
    </table>
    <uc1:studentprofile_ukm3_alumniid_update ID="studentprofile_ukm3_alumniid_update1" runat="server" />
    &nbsp;
</asp:Content>
