<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_master_config.aspx.vb" Inherits="permatapintar.ukm3_master_config" %>

<%@ Register Src="~/commoncontrol/ukm3_configmasterset.ascx" TagPrefix="uc1" TagName="ukm3_configmasterset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm3_configmasterset runat="server" ID="ukm3_configmasterset" />
</asp:Content>
