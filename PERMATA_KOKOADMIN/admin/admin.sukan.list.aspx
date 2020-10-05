<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.sukan.list.aspx.vb" Inherits="permatapintar.admin_sukan_list" %>

<%@ Register Src="../commoncontrol/sukan_list.ascx" TagName="sukan_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Sukan & Permainan
            </td>
        </tr>
    </table>
    <uc1:sukan_list ID="sukan_list1" runat="server" />
</asp:Content>
