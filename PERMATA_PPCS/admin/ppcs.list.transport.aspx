<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppcs.list.transport.aspx.vb" Inherits="permatapintar.ppcs_list_transport" %>

<%@ Register Src="../commoncontrol/ppcs_list_transport.ascx" TagName="ppcs_list_transport" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Pendaftaran>Pengangkutan Pelajar-PERGI
            </td>
        </tr>
    </table>
    <uc1:ppcs_list_transport ID="ppcs_list_transport1" runat="server" />

</asp:Content>
