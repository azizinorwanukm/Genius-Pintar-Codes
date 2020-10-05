<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_alumni.aspx.vb" Inherits="KPP_MS.admin_alumni" %>

<%@ Register Src="~/commoncontrol/admin_alumni.ascx" TagPrefix="uc1" TagName="admin_alumni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_alumni runat="server" id="admin_alumni" />
</asp:Content>
