<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.pusatujian.list.aspx.vb" Inherits="permatapintar.jpn_pusatujian_list1" %>

<%@ Register Src="commoncontrol/pusatujian_list.ascx" TagName="pusatujian_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Pusat Ujian UKM2>Senarai Pusat Ujian" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_list ID="pusatujian_list1" runat="server" />
</asp:Content>
