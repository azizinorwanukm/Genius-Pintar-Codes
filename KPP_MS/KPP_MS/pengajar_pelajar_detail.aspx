<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_pelajar_detail.aspx.vb" Inherits="KPP_MS.pengajar_pelajar_detail" %>

<%@ Register Src="~/commoncontrol/student_DetailLecturer.ascx" TagPrefix="uc1" TagName="student_DetailLecturer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:student_DetailLecturer runat="server" id="student_DetailLecturer" />
    <asp:HiddenField ID="Hidden_Data" runat="server" />
</asp:Content>
