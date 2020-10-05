<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.config.update.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_config_update" %>

<%@ Register Src="commoncontrol/pcis_config_update.ascx" TagName="pcis_config_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pcis_config_update ID="pcis_config_update1" runat="server" />
    &nbsp;
</asp:Content>
