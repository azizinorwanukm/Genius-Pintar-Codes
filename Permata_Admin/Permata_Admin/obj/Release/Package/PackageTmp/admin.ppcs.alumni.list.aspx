<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ppcs.alumni.list.aspx.vb" Inherits="permatapintar.admin_ppcs_alumni_list" %>

<%@ Register Src="commoncontrol/ppcs_alumni_master_list.ascx" TagName="ppcs_alumni_master_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Senarai Alumni" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_alumni_master_list ID="ppcs_alumni_master_list1" runat="server" />
    &nbsp;
</asp:Content>
