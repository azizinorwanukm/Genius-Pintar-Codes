<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_kemasukan_markah.aspx.vb" Inherits="KPP_MS.pengajar_kemasukan_markah" %>

<%@ Register Src="~/commoncontrol/lecturer_update_result.ascx" TagPrefix="uc1" TagName="lecturer_update_result" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_update_result runat="server" ID="lecturer_update_result" />
</asp:Content>
