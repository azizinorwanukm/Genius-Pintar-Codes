<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.pusatujian.student.list.aspx.vb" Inherits="permatapintar.ukm_pusatujian_student_list" %>

<%@ Register Src="commoncontrol/pusatujian_student_list.ascx" TagName="pusatujian_student_list"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <uc1:pusatujian_student_list ID="pusatujian_student_list1" runat="server" />
</asp:Content>
