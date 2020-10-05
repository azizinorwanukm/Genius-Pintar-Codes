<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="tempat.create.aspx.vb" Inherits="permatapintar.tempat_create" %>

<%@ Register Src="../commoncontrol/tempat_create.ascx" TagName="tempat_create" TagPrefix="uc1" %>
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
    <uc1:tempat_create ID="tempat_create1" runat="server" />
    &nbsp;
    <uc2:tempat_list ID="tempat_list1" runat="server" />
    &nbsp;
</asp:Content>
