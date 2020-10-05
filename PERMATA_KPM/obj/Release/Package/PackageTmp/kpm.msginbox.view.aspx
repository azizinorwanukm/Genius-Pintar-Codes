<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.msginbox.view.aspx.vb" Inherits="permatapintar.kpm_msginbox_view" %>
<%@ Register src="commoncontrol/MsgInbox_view.ascx" tagname="MsgInbox_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:MsgInbox_view ID="MsgInbox_view1" runat="server" />
</asp:Content>
