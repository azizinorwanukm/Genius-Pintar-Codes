<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.formatPemarkahanKeseluruhan.aspx.vb" Inherits="permatapintar.admin_formatPemarkahanKeseluruhan" %>

<%@ Register Src="~/commoncontrol/admin.configPemarkahan.ascx" TagPrefix="uc1" TagName="adminconfigPemarkahan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminconfigPemarkahan runat="server" id="adminconfigPemarkahan" />
</asp:Content>
