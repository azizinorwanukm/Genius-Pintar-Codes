﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.msginbox.reply.aspx.vb" Inherits="permatapintar.jpn_msginbox_reply" %>
<%@ Register src="commoncontrol/MsgInbox_reply.ascx" tagname="MsgInbox_reply" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:MsgInbox_reply ID="MsgInbox_reply1" runat="server" />
</asp:Content>
