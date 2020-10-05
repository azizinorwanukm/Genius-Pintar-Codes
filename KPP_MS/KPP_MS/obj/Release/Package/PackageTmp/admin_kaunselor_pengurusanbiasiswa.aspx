<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_pengurusanbiasiswa.aspx.vb" Inherits="KPP_MS.admin_kaunselor_pengurusanbiasiswa" %>

<%@ Register Src="~/commoncontrol/scholarship_create.ascx" TagPrefix="uc1" TagName="scholarship_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <uc1:scholarship_create runat="server" id="scholarship_create" />
 
</asp:Content>
