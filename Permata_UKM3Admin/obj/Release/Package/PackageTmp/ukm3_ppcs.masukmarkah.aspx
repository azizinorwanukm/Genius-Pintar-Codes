<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_ppcs.masukmarkah.aspx.vb" Inherits="permatapintar.ukm3_ppcs_masukmarkah" %>

<%@ Register Src="~/commoncontrol/studentprofile_header.ascx" TagPrefix="uc1" TagName="studentprofile_header" %>
<%@ Register Src="~/commoncontrol/Ppcs_masukmarkah.ascx" TagPrefix="uc1" TagName="Ppcs_masukmarkah" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header runat="server" ID="studentprofile_header" />
    &nbsp;
    <uc1:Ppcs_masukmarkah runat="server" id="Ppcs_masukmarkah" />
</asp:Content>
