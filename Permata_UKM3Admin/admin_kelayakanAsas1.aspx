<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kelayakanAsas1.aspx.vb" Inherits="permatapintar.admin_kelayakanAsas1" %>

<%@ Register Src="~/commoncontrol/admin.kelayakan_Asas1.ascx" TagPrefix="uc1" TagName="adminkelayakan_Asas1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminkelayakan_Asas1 runat="server" id="adminkelayakan_Asas1" />
</asp:Content>
