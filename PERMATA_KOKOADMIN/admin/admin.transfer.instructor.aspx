<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.transfer.instructor.aspx.vb" Inherits="permatapintar.admin_transfer_instructor" %>

<%@ Register src="../commoncontrol/koko_instruktor_transfer.ascx" tagname="koko_instruktor_transfer" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_bread">
            <td>
                Lain-Lain>Instruktor Lama
            </td>
        </tr>
    </table>
     <uc1:koko_instruktor_transfer ID="koko_instruktor_transfer1" runat="server" />
</asp:Content>
