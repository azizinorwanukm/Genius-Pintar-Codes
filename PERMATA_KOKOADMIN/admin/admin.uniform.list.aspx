<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.uniform.list.aspx.vb" Inherits="permatapintar.admin_uniform_list" %>

<%@ Register Src="../commoncontrol/uniform_list.ascx" TagName="uniform_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>
                Selenggara>Badan Beruniform
            </td>
        </tr>
    </table>
    <uc1:uniform_list ID="uniform_list1" runat="server" />
    &nbsp;
</asp:Content>
