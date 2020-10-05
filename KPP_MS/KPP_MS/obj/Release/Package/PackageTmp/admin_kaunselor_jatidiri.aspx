<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_jatidiri.aspx.vb" Inherits="KPP_MS.admin_kaunselor_jatidiri" %>

<%@ Register Src="~/commoncontrol/counselor_Personality_Development.ascx" TagPrefix="uc1" TagName="counselor_Personality_Development" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Personality_Development runat="server" id="counselor_Personality_Development" />
</asp:Content>
