<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.jsc.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_jsc" %>

<%@ Register Src="~/commoncontrol/pcis_jsc_create.ascx" TagPrefix="uc1" TagName="pcis_jsc_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian PCIS>Kelayakan Ke JSC" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pcis_jsc_create runat="server" id="pcis_jsc_create" />
</asp:Content>
