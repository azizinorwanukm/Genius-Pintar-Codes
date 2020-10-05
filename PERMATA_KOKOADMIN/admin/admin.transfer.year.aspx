<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.transfer.year.aspx.vb" Inherits="permatapintar.admin_transfer_year" %>
<%@ Register src="../commoncontrol/koko_pelajar_transfer.ascx" tagname="koko_pelajar_transfer" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_bread">
            <td>
                Lain-Lain>Pelajar Lama
            </td>
        </tr>
    </table>
     <uc1:koko_pelajar_transfer ID="koko_pelajar_transfer1" runat="server" />
</asp:Content>
