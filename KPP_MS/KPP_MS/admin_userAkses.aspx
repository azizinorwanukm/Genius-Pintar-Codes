<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_userAkses.aspx.vb" Inherits="KPP_MS.admin_userAkses" %>

<%@ Register Src="~/commoncontrol/User_Access.ascx" TagPrefix="uc1" TagName="User_Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:User_Access runat="server" id="User_Access" />
</asp:Content>
