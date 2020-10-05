<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_admin_kpp_markUpdate.aspx.vb" Inherits="permatapintar.ukm3_admin_kpp_markUpdate" %>

<%@ Register Src="~/commoncontrol/ukm3_adminKPP_studentlist_mark.ascx" TagPrefix="uc1" TagName="ukm3_adminKPP_studentlist_mark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Pentaksiran > Instruktor KPP"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:ukm3_adminKPP_studentlist_mark runat="server" id="ukm3_adminKPP_studentlist_mark" />
</asp:Content>
