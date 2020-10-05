<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.kelaskoko.list.aspx.vb" Inherits="permatapintar.admin_koko_kelaskoko_list" %>

<%@ Register Src="../commoncontrol/kelaskoko_list.ascx" TagName="kelaskoko_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Kumpulan Sukan & Permainan
            </td>
        </tr>
    </table>
    <uc1:kelaskoko_list ID="kelaskoko_list1" runat="server" />
</asp:Content>
