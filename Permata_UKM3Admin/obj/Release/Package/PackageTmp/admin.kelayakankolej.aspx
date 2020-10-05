<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.kelayakankolej.aspx.vb" Inherits="permatapintar.admin_kelayakankolej" %>

<%@ Register Src="~/commoncontrol/admin.kelayakanKolej.ascx" TagPrefix="uc1" TagName="adminkelayakanKolej" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminkelayakanKolej runat="server" id="adminkelayakanKolej" />
</asp:Content>
