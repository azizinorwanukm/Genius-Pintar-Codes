<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.msginbox.create.aspx.vb" Inherits="permatapintar.jpn_msginbox_create" %>
<%@ Register src="commoncontrol/MsgInbox_create.ascx" tagname="MsgInbox_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:MsgInbox_create ID="MsgInbox_create1" runat="server" />
</asp:Content>
