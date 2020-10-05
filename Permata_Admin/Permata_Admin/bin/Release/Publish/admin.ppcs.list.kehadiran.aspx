<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ppcs.list.kehadiran.aspx.vb" Inherits="permatapintar.admin_ppcs_list_kehadiran" %>

<%@ Register Src="commoncontrol/ppcs_list_kehadiran.ascx" TagName="ppcs_list_kehadiran"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>PPCS>Kehadiran Pelajar
            </td>
        </tr>
    </table>
    <uc1:ppcs_list_kehadiran ID="ppcs_list_kehadiran1" runat="server" />
</asp:Content>
