<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kelayakanTahap1.aspx.vb" Inherits="permatapintar.admin_kelayakanTahap1" %>

<%@ Register Src="~/commoncontrol/admin.kelayakan_Tahap1.ascx" TagPrefix="uc1" TagName="adminkelayakan_Tahap1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminkelayakan_Tahap1 runat="server" id="adminkelayakan_Tahap1" />
</asp:Content>
