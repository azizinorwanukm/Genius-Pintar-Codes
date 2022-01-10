<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_konfigurasi.aspx.vb" Inherits="KPP_MS.admin_konfigurasi" %>

<%@ Register Src="commoncontrol/configuration_setting.ascx" TagName="configuration_setting" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:configuration_setting ID="configuration_setting1" runat="server" />

</asp:Content>
