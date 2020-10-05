<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="system.error.aspx.vb" Inherits="araken.pcisadmin.system_error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>System Error!
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:label id="lblMsg" runat="server" text="" forecolor="Red"></asp:label>
            </td>
        </tr>
    </table>
</asp:Content>
