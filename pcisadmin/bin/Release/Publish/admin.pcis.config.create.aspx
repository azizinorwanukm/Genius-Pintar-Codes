<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.config.create.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_config_create" %>

<%@ Register Src="commoncontrol/pcis_config_create.ascx" TagName="pcis_config_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pcis_config_create ID="pcis_config_create1" runat="server" />
    &nbsp;
</asp:Content>
