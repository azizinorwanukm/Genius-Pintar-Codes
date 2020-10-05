<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ppcs.update.aspx.vb" Inherits="permatapintar.ppcs_update2" %>

<%@ Register Src="commoncontrol/ppcs_update.ascx" TagName="ppcs_update" TagPrefix="uc1" %>
<%@ Register src="commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Kemaskini PPCS" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc3:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;<uc1:ppcs_update ID="ppcs_update1" runat="server" />
    &nbsp;
</asp:Content>
