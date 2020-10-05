<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="ppcs.pelajar.register.aspx.vb" Inherits="permatapintar.ppcs_pelajar_register" %>

<%@ Register Src="../commoncontrol/studentprofile_ppcs_create.ascx" TagName="studentprofile_ppcs_create"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Pelajar>Daftar Pelajar Baru
            </td>
        </tr>
    </table>
    <uc1:studentprofile_ppcs_create ID="studentprofile_ppcs_create1" runat="server" />
    &nbsp;
</asp:Content>
