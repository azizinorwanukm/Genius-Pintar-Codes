<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Ukm3.userDetail.aspx.vb" Inherits="permatapintar.Ukm3_userDetail" %>

<%@ Register Src="~/commoncontrol/admin_passwordUser.ascx" TagPrefix="uc1" TagName="admin_passwordUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_passwordUser runat="server" id="admin_passwordUser" />
</asp:Content>
