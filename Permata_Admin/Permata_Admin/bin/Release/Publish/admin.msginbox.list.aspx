﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.msginbox.list.aspx.vb" Inherits="permatapintar.admin_msginbox_list" %>
<%@ Register src="commoncontrol/MsgInbox_list.ascx" tagname="MsgInbox_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:MsgInbox_list ID="MsgInbox_list1" runat="server" />
</asp:Content>
