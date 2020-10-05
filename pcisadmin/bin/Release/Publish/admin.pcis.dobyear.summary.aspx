<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.dobyear.summary.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_dobyear_summary" %>

<%@ Register Src="~/commoncontrol/pcis_summary_age.ascx" TagPrefix="uc1" TagName="pcis_summary_age" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian PCIS>Ringkasan Ujian Umur"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pcis_summary_age runat="server" id="pcis_summary_age" />
</asp:Content>
