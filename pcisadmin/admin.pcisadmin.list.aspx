<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcisadmin.list.aspx.vb" Inherits="araken.pcisadmin.admin_pcisadmin_list" %>

<%@ Register Src="~/commoncontrol/admin_list.ascx" TagPrefix="uc1" TagName="admin_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Lain-lain>Senarai Pengguna Sistem" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:admin_list runat="server" id="admin_list" />
</asp:Content>
