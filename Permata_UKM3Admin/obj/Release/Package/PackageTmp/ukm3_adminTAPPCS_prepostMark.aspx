<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_adminTAPPCS_prepostMark.aspx.vb" Inherits="permatapintar.ukm3_adminTAPPCS_prepostMark" %>

<%@ Register Src="~/commoncontrol/tappcs_prepostMark.ascx" TagPrefix="uc1" TagName="tappcs_prepostMark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Pentaksiran > TAPCS"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:tappcs_prepostMark runat="server" id="tappcs_prepostMark" />
</asp:Content>
