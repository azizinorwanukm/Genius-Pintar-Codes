<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.pusatujian.view.aspx.vb" Inherits="permatapintar.subadmin_pusatujian_view" %>

<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    <table class="fbform">
        <tr class="fbform_msg">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Tahun Ujian:
                <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList>
                &nbsp;
                 <asp:DropDownList ID="ddlMenudesc" runat="server" Width="250px">
                 </asp:DropDownList>&nbsp;
                <asp:Button ID="btnExecute" runat="server" Text="Execute" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
