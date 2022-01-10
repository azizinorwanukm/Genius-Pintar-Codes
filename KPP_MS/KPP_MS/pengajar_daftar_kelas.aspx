<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_daftar_kelas.aspx.vb" Inherits="KPP_MS.pengajar_daftar_kelas" %>

<%@ Register Src="~/commoncontrol/lecturer_AddClass.ascx" TagPrefix="uc1" TagName="lecturer_AddClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:lecturer_AddClass runat="server" id="lecturer_AddClass" />

</asp:Content>
