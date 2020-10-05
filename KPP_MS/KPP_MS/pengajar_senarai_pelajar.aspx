<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_senarai_pelajar.aspx.vb" Inherits="KPP_MS.pengajar_senarai_kelas" %>

<%@ Register Src="~/commoncontrol/lecturer_list_student.ascx" TagPrefix="uc1" TagName="lecturer_list_student" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_list_student runat="server" id="lecturer_list_student" />
</asp:Content>
