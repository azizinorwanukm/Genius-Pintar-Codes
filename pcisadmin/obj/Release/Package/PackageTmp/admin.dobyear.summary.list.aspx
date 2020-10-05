<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.dobyear.summary.list.aspx.vb" Inherits="araken.pcisadmin.admin_dobyear_summary_list" %>

<%@ Register Src="~/commoncontrol/pcis_dobyear_list.ascx" TagPrefix="uc1" TagName="pcis_dobyear_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ringkasan Ujian PCIS>Ringkasan Ujian Umur>Senarai Pelajar"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pcis_dobyear_list runat="server" id="pcis_dobyear_list" />
</asp:Content>
