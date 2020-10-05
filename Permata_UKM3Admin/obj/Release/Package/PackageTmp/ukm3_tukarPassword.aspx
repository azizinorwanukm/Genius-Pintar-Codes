<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_tukarPassword.aspx.vb" Inherits="permatapintar.ukm3_tukarPassword" %>

<%@ Register Src="~/commoncontrol/admin_ubahPassword.ascx" TagPrefix="uc1" TagName="admin_ubahPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_ubahPassword runat="server" id="admin_ubahPassword" />
</asp:Content>
