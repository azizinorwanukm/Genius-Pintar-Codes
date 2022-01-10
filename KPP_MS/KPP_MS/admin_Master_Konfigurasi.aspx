<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_Master_Konfigurasi.aspx.vb" Inherits="KPP_MS.admin_Master_Konfigurasi" %>

<%@ Register Src="~/commoncontrol/admin_access.ascx" TagPrefix="uc1" TagName="admin_access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:admin_access runat="server" id="admin_access" />

</asp:Content>
