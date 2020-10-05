<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.default.aspx.vb" Inherits="permatapintar.kpm_default" %>

<%@ Register src="commoncontrol/MsgInbox_list.ascx" tagname="MsgInbox_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
<h1>Selamat Datang ke<br />
Laman Utama Sistem Pengurusan dan Pemantauan Ujian UKM1 & UKM2.</h1></center>
<uc1:MsgInbox_list ID="MsgInbox_list1" runat="server" />
&nbsp;

</asp:Content>
