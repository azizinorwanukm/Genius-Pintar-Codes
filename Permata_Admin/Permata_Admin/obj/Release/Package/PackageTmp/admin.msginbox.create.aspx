<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.msginbox.create.aspx.vb" Inherits="permatapintar.admin_msginbox_create" %>

<%@ Register Src="commoncontrol/MsgInbox_create.ascx" TagName="MsgInbox_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:MsgInbox_create ID="MsgInbox_create1" runat="server" />
</asp:Content>
