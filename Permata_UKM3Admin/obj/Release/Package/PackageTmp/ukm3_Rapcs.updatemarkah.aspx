<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_Rapcs.updatemarkah.aspx.vb" Inherits="permatapintar.ukm3_Rapcs_updatemarkah" %>

<%@ Register Src="~/commoncontrol/Rapcs_updatemarkah.ascx" TagPrefix="uc1" TagName="Rapcs_updatemarkah" %>
<%@ Register Src="~/commoncontrol/studentprofile_header.ascx" TagPrefix="uc1" TagName="studentprofile_header" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header runat="server" ID="studentprofile_header" />
    &nbsp;
    <uc1:Rapcs_updatemarkah runat="server" id="Rapcs_updatemarkah" />
</asp:Content>
