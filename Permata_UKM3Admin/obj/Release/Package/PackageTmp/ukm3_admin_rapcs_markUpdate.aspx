<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_admin_rapcs_markUpdate.aspx.vb" Inherits="permatapintar.ukm3_admin_rapcs_markUpdate" %>

<%@ Register Src="~/commoncontrol/ukm3_adminRAPCS_studentlist_mark.ascx" TagPrefix="uc1" TagName="ukm3_adminRAPCS_studentlist_mark" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Pentaksiran > RAPCS"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:ukm3_adminRAPCS_studentlist_mark runat="server" id="ukm3_adminRAPCS_studentlist_markascx" />
</asp:Content>
