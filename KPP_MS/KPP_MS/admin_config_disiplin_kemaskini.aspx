<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_config_disiplin_kemaskini.aspx.vb" Inherits="KPP_MS.admin_config_disiplin_kemaskini" %>

<%@ Register Src="~/commoncontrol/Disiplin_config_edit.ascx" TagPrefix="uc1" TagName="Disiplin_config_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Disiplin_config_edit runat="server" id="Disiplin_config_edit" />
</asp:Content>
