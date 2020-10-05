<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="tempat.update.aspx.vb" Inherits="permatapintar.tempat_update" %>

<%@ Register Src="../commoncontrol/tempat_update.ascx" TagName="tempat_update" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/tempat_list.ascx" TagName="tempat_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Pengurusan Am>Pengurusan Tempat
            </td>
        </tr>
    </table>
    <uc1:tempat_update ID="tempat_update1" runat="server" />
    &nbsp;
    <uc2:tempat_list ID="tempat_list1" runat="server" />
</asp:Content>
