<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="PPCS.Eval.Daily.create.aspx.vb" Inherits="permatapintar.PPCS_Eval_Daily_create" %>

<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/PPCS_Eval_Daily_create.ascx" TagName="PPCS_Eval_Daily_create"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="PPCS>Laporan Harian"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;<uc2:PPCS_Eval_Daily_create ID="PPCS_Eval_Daily_create1" runat="server" />
</asp:Content>
