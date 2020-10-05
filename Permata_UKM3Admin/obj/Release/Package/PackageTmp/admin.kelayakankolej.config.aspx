<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.kelayakankolej.config.aspx.vb" Inherits="permatapintar.admin_kelayakankolej_config" %>

<%@ Register Src="~/commoncontrol/admin.kelayakanKolej.config.ascx" TagPrefix="uc1" TagName="adminkelayakanKolejconfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminkelayakanKolejconfig runat="server" id="adminkelayakanKolejconfig" />
</asp:Content>
