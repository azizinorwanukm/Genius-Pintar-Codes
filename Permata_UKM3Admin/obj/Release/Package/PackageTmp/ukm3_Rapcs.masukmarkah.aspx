<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_Rapcs.masukmarkah.aspx.vb" Inherits="permatapintar.ukm3_Rapcs_masukmarkah" %>

<%@ Register Src="~/commoncontrol/Rapcs_masukmarkah.ascx" TagPrefix="uc1" TagName="Rapcs_masukmarkah" %>
<%@ Register Src="~/commoncontrol/studentprofile_header.ascx" TagPrefix="uc1" TagName="studentprofile_header" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header runat="server" ID="studentprofile_header" />
    &nbsp;
    <uc1:Rapcs_masukmarkah runat="server" ID="Rapcs_masukmarkah" />

</asp:Content>
