<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.config_session_add.aspx.vb" Inherits="permatapintar.admin_config_session_add" %>

<%@ Register Src="~/commoncontrol/config_sesssion_add.ascx" TagPrefix="uc1" TagName="config_sesssion_add" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_sesssion_add runat="server" id="config_sesssion_add" />
</asp:Content>
