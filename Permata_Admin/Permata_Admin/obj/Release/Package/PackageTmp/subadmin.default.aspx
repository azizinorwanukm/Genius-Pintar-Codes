<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.default.aspx.vb" Inherits="permatapintar.subadmin_default" %>

<%@ Register Src="commoncontrol/user_post.ascx" TagName="user_post" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>
            Selamat Datang ke<br />
            Laman Utama Sistem Pengurusan dan Pemantauan Ujian UKM1 & UKM2.</h1>
    </center>
    <uc1:user_post ID="user_post1" runat="server" />
    &nbsp;
</asp:Content>
