<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="public.pengumuman.view.aspx.vb" Inherits="permatapintar.public_pengumuman_view" %>

<%@ Register Src="commoncontrol/pengumuman_view_pub.ascx" TagName="pengumuman_view_pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pengumuman_view_pub ID="pengumuman_view_pub1" runat="server" />
    &nbsp;
</asp:Content>
