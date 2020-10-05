<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.config_session_edit.aspx.vb" Inherits="permatapintar.admin_config_session_edit" %>

<%@ Register Src="~/commoncontrol/config_session_edit.ascx" TagPrefix="uc1" TagName="config_session_edit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Config Session>Ubah Session>Edit"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:config_session_edit runat="server" ID="config_session_edit" />
</asp:Content>
