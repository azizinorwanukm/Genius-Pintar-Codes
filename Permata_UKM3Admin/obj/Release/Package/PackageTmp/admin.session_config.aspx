<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.session_config.aspx.vb" Inherits="permatapintar.WebForm3" %>

<%@ Register Src="~/commoncontrol/config_session_list.ascx" TagPrefix="uc1" TagName="config_session_list" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Config Session>Ubah Session"></asp:label>
            </td>
        </tr>
    </table>

    <uc1:config_session_list runat="server" id="config_session_list" />
</asp:Content>
