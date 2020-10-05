<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master"
    CodeBehind="kpm.pusatujian.view.aspx.vb" Inherits="permatapintar.kpm_pusatujian_view" %>

<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;<table class="fbform">
        <tr>
            <td>
                <asp:DropDownList ID="ddlMenudesc" runat="server" Width="200px">
                </asp:DropDownList>
                <asp:Button ID="btnExecute" runat="server" Text="Execute" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
</asp:Content>