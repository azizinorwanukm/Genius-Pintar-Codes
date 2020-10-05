<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspejabat/main.Master"
    CodeBehind="ppcs.list.kehadiran.aspx.vb" Inherits="permatapintar.ppcs_list_kehadiran2" %>

<%@ Register Src="../commoncontrol/ppcs_list_kehadiran.ascx" TagName="ppcs_list_kehadiran"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pendaftaran>Kehadiran Pelajar
            </td>
        </tr>
    </table>
    <uc1:ppcs_list_kehadiran ID="ppcs_list_kehadiran1" runat="server" />
</asp:Content>
