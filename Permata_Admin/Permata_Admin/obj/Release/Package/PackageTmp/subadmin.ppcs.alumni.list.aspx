<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ppcs.alumni.list.aspx.vb" Inherits="permatapintar.subadmin_ppcs_alumni_list" %>

<%@ Register Src="commoncontrol/ppcs_alumni_master_list.ascx" TagName="ppcs_alumni_master_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Alumni PPCS>Senarai Alumni" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_alumni_master_list ID="ppcs_alumni_master_list1" runat="server" />
    &nbsp;
</asp:Content>
