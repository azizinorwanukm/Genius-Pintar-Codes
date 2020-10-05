<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="default.aspx.vb" Inherits="permatapintar._default1" %>

<%@ Register Src="commoncontrol/user_login.ascx" TagName="user_login" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_login ID="user_login1" runat="server" />
    <p>
        Nota:<br />
        Site ini memerlukan anda "Allow Cookies". <a href="Allow_cookies.pdf" target="_blank">
            Klik di sini untuk manual bagaimana hendak "Allow Cookies"</a>
    </p>
</asp:Content>
