<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/asasi.Master" CodeBehind="asasi.pusatujian.kehadiran.pelajar.aspx.vb" Inherits="permatapintar.asasi_pusatujian_kehadiran_pelajar" %>

<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/pusatujian_student_kehadiran_list.ascx" TagName="pusatujian_student_kehadiran_list"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <uc2:pusatujian_student_kehadiran_list ID="pusatujian_student_kehadiran_list1" runat="server" />
</asp:Content>
