<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppcs.list.status.aspx.vb" Inherits="permatapintar.ppcs_list_status" %>

<%@ Register Src="../commoncontrol/ppcs_list_status.ascx" TagName="ppcs_list_status" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Pendaftaran>Status Pelajar
                
            </td>
        </tr>
    </table>
    <uc1:ppcs_list_status ID="ppcs_list_status1" runat="server" />
</asp:Content>
